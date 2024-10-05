using Microsoft.AspNetCore.Components.Forms;
using System.Data.SqlClient;
using System.Data;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Components;
using IntelChat.Services;
namespace IntelChat.Pages
{
	public partial class ImageUpload
  {
		[Parameter]public IConfiguration Configuration { get; set; }
		private IBrowserFile? imageFile;
    public int DDCounter = 0;
    List<DropDownArtist> artistlist = new List<DropDownArtist>();
    List<DropDownComposition> compositionlist = new List<DropDownComposition>();
    List<DropDownPhase> phaselist = new List<DropDownPhase>();
    List<DropDownLocation> locationlist = new List<DropDownLocation>();
        [Inject]
        public NotificationService? NotificationService { get; set; }
        public string? ImgUrl { get; set; }

        ArtData objartdata = new ArtData();

    private void HandleSelected(InputFileChangeEventArgs e)
    {
      imageFile = e.File;
    }
		/// <summary>Submits the image sent by the user into blob storage</summary>
		private async Task OnSubmit()
    {
      if (imageFile == null)
        return;

      var resizedFile = await imageFile.RequestImageFileAsync("image/png", 700, 700);
      var file = resizedFile;
	  var azureBlobConnectionString = Configuration.GetConnectionString("AzureBlobConnection");
	  var container = new BlobContainerClient(azureBlobConnectionString, "exprtimgs");
      var createResponse = await container.CreateIfNotExistsAsync();
      if (createResponse != null && createResponse.GetRawResponse().Status == 201)
        await container.SetAccessPolicyAsync(PublicAccessType.Blob);

      var blob = container.GetBlobClient(file.Name);
      await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

      using (var fileStream = file.OpenReadStream(file.Size))
      {
        await blob.UploadAsync(fileStream);
      }

      ImgUrl = blob.Uri.ToString();

      objartdata.ImgUrl = ImgUrl;

            if (objartdata.ImgUrl != null)
            {
                UploadToDb();
                NotificationService.Notify("Image uploaded successfully!", NotificationType.Success);
            }
    }

		/// <summary>Creates new entry for Artifact into the database using a stored procedure</summary>
		private void UploadToDb()
    {

      string spName = "dbo.[AddArtifact]";
			var sqlServerConnectionString = Configuration.GetConnectionString("DefaultConnection");
			var connection = new SqlConnection(sqlServerConnectionString);

			//define the SqlCommand object
			SqlCommand cmd = new SqlCommand(spName, connection);

      //Set SqlParameter - the employee id parameter value will be set from the command line
      //SqlParameter param1 = new SqlParameter();
      //param1.ParameterName = "@Artifact_ID";
      //param1.SqlDbType = SqlDbType.Int;
      //param1.Value = 1;

      SqlParameter param2 = new SqlParameter();
      param2.ParameterName = "@Artifact";
      param2.Value = objartdata.ArtTitle;

      SqlParameter param3 = new SqlParameter();
      param3.ParameterName = "@Artist";
      param3.Value = objartdata.Artist_id;

      SqlParameter param4 = new SqlParameter();
      param4.ParameterName = "@Artifact_Image";
      param4.Value = objartdata.ImgUrl;

      SqlParameter param5 = new SqlParameter();
      param5.ParameterName = "@Phase";
      param5.Value = objartdata.Phase_id;

      SqlParameter param6 = new SqlParameter();
      param6.ParameterName = "@Location";
      param6.Value = objartdata.Location_id;

      SqlParameter param7 = new SqlParameter();
      param7.ParameterName = "@Composition";
      param7.Value = objartdata.Compostion_id;

      SqlParameter param8 = new SqlParameter();
      param8.ParameterName = "@Narative";
      param8.Value = objartdata.Narative;

      //add the parameter to the SqlCommand object
      //cmd.Parameters.Add(param1);
      cmd.Parameters.Add(param2);
      cmd.Parameters.Add(param3);
      cmd.Parameters.Add(param4);
      cmd.Parameters.Add(param5);
      cmd.Parameters.Add(param6);
      cmd.Parameters.Add(param7);
      cmd.Parameters.Add(param8);

      connection.Open();
      cmd.CommandType = CommandType.StoredProcedure;

      //execute the stored procedure
      cmd.ExecuteNonQuery();
    }
		/// <summary>Searches in the Artist table in the database using a stored procedure and displays each entry in a dropdown menu</summary>
		private void DropDownArtist()
    {
      string spName = "dbo.[dropDownArtist]";
			var sqlServerConnectionString = Configuration.GetConnectionString("DefaultConnection");
			var connection = new SqlConnection(sqlServerConnectionString);
			//define the SqlCommand object
			SqlCommand cmd = new SqlCommand(spName, connection);

      connection.Open();
      SqlDataReader reader = cmd.ExecuteReader();

       while (reader.Read())
      {
        artistlist.Add(new DropDownArtist
        {
            ArtistID = reader.GetInt32(0),
            Artist = reader.GetString(1)
        });
      }

      var artistlist_old = artistlist;
      artistlist = artistlist_old.GroupBy(x => x.ArtistID).Select(x => x.First()).ToList();
    }



		/// <summary>Searches in the Composition table in the database using a stored procedure and displays each entry in a dropdown menu</summary>
		private void DropDownComposition()
    {
      string spName = "dbo.[dropDownComposition]";
			var sqlServerConnectionString = Configuration.GetConnectionString("DefaultConnection");
			var connection = new SqlConnection(sqlServerConnectionString);
			//define the SqlCommand object
			SqlCommand cmd = new SqlCommand(spName, connection);

      connection.Open();
      SqlDataReader reader = cmd.ExecuteReader();

      while (reader.Read())
      {
        compositionlist.Add(new DropDownComposition
        {
            CompositionID = reader.GetInt32(0),
            Composition = reader.GetString(1)
        });
      }
      var compositionlist_old = compositionlist;
      compositionlist = compositionlist_old.GroupBy(x => x.CompositionID).Select(x => x.First()).ToList();
    }



		/// <summary>Searches in the Phase table in the database using a stored procedure and displays each entry in a dropdown menu</summary>
		private void DropDownPhase()
    {
      string spName = "dbo.[dropDownPhase]";
			var sqlServerConnectionString = Configuration.GetConnectionString("DefaultConnection");
			var connection = new SqlConnection(sqlServerConnectionString);
			//define the SqlCommand object
			SqlCommand cmd = new SqlCommand(spName, connection);

      connection.Open();
      SqlDataReader reader = cmd.ExecuteReader();

      while (reader.Read())
      {
        phaselist.Add(new DropDownPhase
        {
            PhaseID = reader.GetInt32(0),
            Phase = reader.GetString(1)
        });
      }
      var phaselist_old = phaselist;
      phaselist = phaselist_old.GroupBy(x => x.PhaseID).Select(x => x.First()).ToList();
    }



		/// <summary>Searches in the Location table in the database using a stored procedure and displays each entry in a dropdown menu</summary>
		private void DropDownLocation()
    {
      string spName = "dbo.[dropDownLocation]";
			var sqlServerConnectionString = Configuration.GetConnectionString("DefaultConnection");
			var connection = new SqlConnection(sqlServerConnectionString);
      //define the SqlCommand object
      SqlCommand cmd = new SqlCommand(spName, connection);

      connection.Open();
      SqlDataReader reader = cmd.ExecuteReader();

      while (reader.Read())
      {
          locationlist.Add(new DropDownLocation
          {
              LocationID = reader.GetInt32(0),
              Location = reader.GetString(1)
          });
      }
      var locationlist_old = locationlist;
      locationlist = locationlist_old.GroupBy(x => x.LocationID).Select(x => x.First()).ToList();
    }
  }

    internal class DropDownLocation
    {
        public int LocationID { get; set; }
        public string? Location { get; set; }
    }

    internal class DropDownPhase
    {
        public int PhaseID { get; set; }
        public string? Phase { get; set; }
    }

    internal class DropDownComposition
    {
        public int CompositionID { get; set; }
        public string? Composition { get; set; }
    }

    internal class DropDownArtist
    {
        public int ArtistID { get; set; }
        public string? Artist { get; set; }
    }
}