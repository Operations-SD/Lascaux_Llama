using IntelChat.Services;
using System.Data;
using System.Data.SqlClient;

namespace IntelChat.Pages
{
    public partial class AddArtToList
  {


    private readonly string sqlServerconnectionString = "DefaultConnection";


    bool showRemoveButton = true;
        //user UserData = user;
    ThreadData threadData = new ThreadData();
    ArtData threadedArtData = new ArtData();
    List<DropDownThread> threadlist = new List<DropDownThread>();
    List<DropDownArtTitle> titlelist = new List<DropDownArtTitle>();
        public NotificationService NotificationService { get; set; }
        string? artTitle = null;
    int dropDownCounter = 0;
    bool displayMessage = false;

		/// <summary>Reads entities from the database using a stored procedure</summary>
	private void getArtifact()
    {
      string spName = "dbo.[AddSelect]";

      SqlConnection connection = new SqlConnection(sqlServerconnectionString);

      //define the SqlCommand object
      SqlCommand cmd = new SqlCommand(spName, connection);

      SqlParameter param1 = new SqlParameter();
      param1.ParameterName = "@Artifact";
      param1.Value = artTitle;

      cmd.Parameters.Add(param1);

      connection.Open();
      cmd.CommandType = CommandType.StoredProcedure;

      SqlDataReader reader = cmd.ExecuteReader();

      while (reader.Read())
      {
        threadedArtData.Id = reader.GetInt32(0);
        threadedArtData.ArtTitle = reader.GetString(1);
        threadedArtData.Artist_id = reader.GetInt32(2);
        threadedArtData.Artist = reader.GetString(3);
        threadedArtData.Phase_id = reader.GetInt32(6);
        threadedArtData.Phase = reader.GetString(7);
        threadedArtData.Location_id = reader.GetInt32(4);
        threadedArtData.Location = reader.GetString(5);
        threadedArtData.Compostion_id = reader.GetInt32(8);
        threadedArtData.Compostion = reader.GetString(9);
        threadedArtData.ImgUrl = reader.GetString(10);
      }
    }
	private void insertArtifact()
    {
      string spName = "dbo.[AddArtifactToList]";

      //define the SqlCommand object
      SqlConnection connection = new SqlConnection(sqlServerconnectionString);

      SqlCommand cmd = new SqlCommand(spName, connection);

      SqlParameter param1 = new SqlParameter();
      param1.ParameterName = "@Artifact_ID";
      param1.Value = threadedArtData.Id;

      SqlParameter param2 = new SqlParameter();
      param2.ParameterName = "@Thread_ID";
      param2.Value = threadData.ThreadID;
    
      //add the parameter to the SqlCommand object
      cmd.Parameters.Add(param1);
      cmd.Parameters.Add(param2);

      connection.Open();

      cmd.CommandType = CommandType.StoredProcedure;
      //execute the stored procedure
      cmd.ExecuteNonQuery();
      getArtifact();
    }

    private void GetThreadListSize()
    {
      string spName = "dbo.[SelectArtifact]";

      SqlConnection connection = new SqlConnection(sqlServerconnectionString);

      //define the SqlCommand object
      SqlCommand cmd = new SqlCommand(spName, connection);

      connection.Open();

      SqlDataReader reader = cmd.ExecuteReader();

      while (reader.Read())
      {
        threadedArtData.Id = reader.GetInt32(0);
      }
    }

    private void DropDownThread()
    {
      string spName = "dbo.[dropDownThread]";

      SqlConnection connection = new SqlConnection(sqlServerconnectionString);

      //define the SqlCommand object
      SqlCommand cmd = new SqlCommand(spName, connection);

      connection.Open();
      SqlDataReader reader = cmd.ExecuteReader();

      while (reader.Read())
      {
        threadlist.Add(new DropDownThread
        {
            ThreadID = reader.GetInt32(0),
            Thread = reader.GetString(1)
        });
      }
      var threadlist_old = threadlist;
      threadlist = threadlist_old.GroupBy(x => x.ThreadID).Select(x => x.First()).ToList();
    }

    private void DropDownTitle()
        {
            string spName = "dbo.[dropDownTitle]";

            SqlConnection connection = new SqlConnection(sqlServerconnectionString);

            //define the SqlCommand object
            SqlCommand cmd = new SqlCommand(spName, connection);

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                titlelist.Add(new DropDownArtTitle
                {
                    Title = reader.GetString(0)
                });
            }
            var titlelist_old = titlelist;
            titlelist = titlelist_old.GroupBy(x => x.Title).Select(x => x.First()).ToList();
        }

    private void DisplayMessageYes()
    {
        displayMessage = true;
    }
    private void DisplayMessageNo()
    {
            displayMessage = false;
    }
  }

    internal class DropDownThread
    {
        public int ThreadID { get; set; }
        public string? Thread { get; set; }
    }

    internal class DropDownArtTitle
    {
        public string? Title { get; set; }
    }
    public class ThreadData
    {
        public int ThreadID { get; set; }
        public string? Thread { get; set; }
    }

}
public class ArtData
{
    public int Id { get; set; }
    public string? ImgUrl { get; set; }
    public string? ArtTitle { get; set; }
    public int Artist_id { get; set; }
    public string? Artist { get; set; }
    public string? Phase { get; set; }
    public int Phase_id { get; set; }
    public string? Location { get; set; }
    public int Location_id { get; set; }
    public string? Compostion { get; set; }
    public int Compostion_id { get; set; }
    public string? Narative { get; set; }
}
