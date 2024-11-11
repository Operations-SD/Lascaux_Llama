using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IntelChat.Models;

public partial class NnetDbContext : DbContext
{
    public NnetDbContext()
    {
    }

    public NnetDbContext(DbContextOptions<NnetDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Artifact> Artifacts { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Brain> Brains { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<CfgGanttTask> CfgGanttTasks { get; set; }

    public virtual DbSet<CfgGanttWork> CfgGanttWorks { get; set; }

    public virtual DbSet<CfgLocationUrl> CfgLocationUrls { get; set; }

    public virtual DbSet<CfgPypePype> CfgPypePypes { get; set; }

    public virtual DbSet<CfgPypeStem> CfgPypeStems { get; set; }

    public virtual DbSet<CfgTaskL4Noun> CfgTaskL4Nouns { get; set; }

    public virtual DbSet<CfgTaskParmPt> CfgTaskParmPts { get; set; }

    public virtual DbSet<CfgTaskUrl> CfgTaskUrls { get; set; }

    public virtual DbSet<CfgWorkTask> CfgWorkTasks { get; set; }

    public virtual DbSet<Channel> Channels { get; set; }

    public virtual DbSet<Composition> Compositions { get; set; }

    public virtual DbSet<DeltaPod> DeltaPods { get; set; }

    public virtual DbSet<DeltaPype> DeltaPypes { get; set; }

    public virtual DbSet<DeltaTask> DeltaTasks { get; set; }

    public virtual DbSet<Element> Elements { get; set; }

    public virtual DbSet<Guide> Guides { get; set; }

    public virtual DbSet<Hercule> Hercules { get; set; }

    public virtual DbSet<HerculesGantt> HerculesGantts { get; set; }

    public virtual DbSet<Interview> Interviews { get; set; }

    public virtual DbSet<IviewGuideQuestion> IviewGuideQuestions { get; set; }

    public virtual DbSet<IviewPersonQAnswered> IviewPersonQAnswereds { get; set; }

    public virtual DbSet<ListBrainLocPerson> ListBrainLocPeople { get; set; }

    public virtual DbSet<ListPodNoun> ListPodNouns { get; set; }

    public virtual DbSet<ListPodNounUrl> ListPodNounUrls { get; set; }

    public virtual DbSet<ListPodVerbUrl> ListPodVerbUrls { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Memo> Memos { get; set; }

    public virtual DbSet<MemoLink> MemoLinks { get; set; }

    public virtual DbSet<Noun> Nouns { get; set; }

    public virtual DbSet<Nova> Novas { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Phase> Phases { get; set; }

    public virtual DbSet<Pod> Pods { get; set; }

    public virtual DbSet<PodBrand> PodBrands { get; set; }

    public virtual DbSet<PodNovaLascaux> PodNovaLascauxes { get; set; }

    public virtual DbSet<PodPersonLocation> PodPersonLocations { get; set; }

    public virtual DbSet<PodPypeAudit> PodPypeAudits { get; set; }

    public virtual DbSet<PodPypeLink> PodPypeLinks { get; set; }

    public virtual DbSet<PodTask> PodTasks { get; set; }

    public virtual DbSet<PodTaskWork> PodTaskWorks { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<ProgramGuide> ProgramGuides { get; set; }

    public virtual DbSet<Pype> Pypes { get; set; }

    public virtual DbSet<PypeNoun> PypeNouns { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<RecipeArtifact> RecipeArtifacts { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<RegistrationRole> RegistrationRoles { get; set; }

    public virtual DbSet<ReportArtifact> ReportArtifacts { get; set; }

    public virtual DbSet<ReportNovaSvo> ReportNovaSvos { get; set; }

    public virtual DbSet<ReportPersonLocation> ReportPersonLocations { get; set; }

    public virtual DbSet<ReportQuestionAnswer> ReportQuestionAnswers { get; set; }

    public virtual DbSet<ReportQuestionGuide> ReportQuestionGuides { get; set; }

    public virtual DbSet<ReportTaskWork> ReportTaskWorks { get; set; }

    public virtual DbSet<RptPersonLocation> RptPersonLocations { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

	public virtual DbSet<LascauxJunk> LascauxJunks { get; set; }

	public virtual DbSet<LascauxNova> LascauxNovas { get; set; }

	public virtual DbSet<LascauxTemp> LascauxTemps { get; set; }

	public virtual DbSet<TaskWorkGantt> TaskWorkGantts { get; set; }

    public virtual DbSet<Url> Urls { get; set; }

    public virtual DbSet<Verb> Verbs { get; set; }

    public virtual DbSet<View1> View1s { get; set; }

    public virtual DbSet<WeeklyActivity> WeeklyActivities { get; set; }

    public virtual DbSet<WeeklyAnalytic> WeeklyAnalytics { get; set; }

    public virtual DbSet<WeeklyDetail> WeeklyDetails { get; set; }

    public virtual DbSet<WeeklyPypeDetail> WeeklyPypeDetails { get; set; }

    public virtual DbSet<Work> Works { get; set; }

    public virtual DbSet<XnovaDictionaryInterview> XnovaDictionaryInterviews { get; set; }

    public virtual DbSet<XnovaDictionaryNounObject> XnovaDictionaryNounObjects { get; set; }

    public virtual DbSet<XnovaDictionaryNounSubject> XnovaDictionaryNounSubjects { get; set; }

    public virtual DbSet<XnovaDictionaryNova> XnovaDictionaryNovas { get; set; }

    public virtual DbSet<XnovaDictionaryPod> XnovaDictionaryPods { get; set; }

    public virtual DbSet<XnovaDictionaryQuestion> XnovaDictionaryQuestions { get; set; }

    public virtual DbSet<XnovaDictionaryTask> XnovaDictionaryTasks { get; set; }

    public virtual DbSet<XnovaDictionaryVerb> XnovaDictionaryVerbs { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => new { e.PersonId, e.QuestionId });

            entity.ToTable("Answer");

            entity.Property(e => e.PersonId)
                .HasDefaultValue(1)
                .HasColumnName("Person_ID");
            entity.Property(e => e.QuestionId)
                .HasDefaultValue(1)
                .HasColumnName("Question_ID");
            entity.Property(e => e.AnswerDtOrgin)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Answer_dt_orgin");
            entity.Property(e => e.AnswerDtRevision)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Answer_dt_revision");
            entity.Property(e => e.AnswerResponse)
                .HasDefaultValue((short)1)
                .HasColumnName("Answer_response");
            entity.Property(e => e.AnswerSeverity)
                .HasDefaultValue((short)4)
                .HasColumnName("Answer_severity");
        });

        modelBuilder.Entity<Artifact>(entity =>
        {
            entity.ToTable("Artifact");

            entity.Property(e => e.ArtifactId).HasColumnName("Artifact_ID");
            entity.Property(e => e.Artifact1)
                .HasMaxLength(64)
                .HasDefaultValue("Title of Artifact")
                .HasColumnName("Artifact");
            entity.Property(e => e.ArtifactImage)
                .HasMaxLength(300)
                .HasDefaultValue("https://agineuralnet.blob.core.windows.net/artstorage/")
                .HasColumnName("Artifact_Image ");
            entity.Property(e => e.Artist)
                .HasDefaultValue(1)
                .HasColumnName("Artist#");
            entity.Property(e => e.Composition)
                .HasDefaultValue(1)
                .HasColumnName("Composition#");
            entity.Property(e => e.Finder)
                .HasMaxLength(128)
                .HasDefaultValue("https://agineuralnet.blob.core.windows.net/artstorage/Finder.png")
                .IsFixedLength();
            entity.Property(e => e.HistoricalDate)
                .HasDefaultValue(new DateTime(2048, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnName("Historical_Date");
            entity.Property(e => e.Location)
                .HasDefaultValue(1)
                .HasColumnName("Location#");
            entity.Property(e => e.Narative).HasDefaultValue("max");
            entity.Property(e => e.Phase)
                .HasDefaultValue(1)
                .HasColumnName("Phase#");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("A");
            entity.Property(e => e.Symbolic).HasDefaultValue(1);
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.ToTable("Artist");

            entity.Property(e => e.ArtistId).HasColumnName("Artist_ID");
            entity.Property(e => e.Artist1)
                .HasMaxLength(64)
                .HasDefaultValue("creater of object")
                .HasColumnName("Artist");
            entity.Property(e => e.ArtistCommon)
                .HasMaxLength(24)
                .HasDefaultValue("call name")
                .HasColumnName("Artist_common");
            entity.Property(e => e.Finder)
                .HasMaxLength(128)
                .HasDefaultValue("link to Artist information");
            entity.Property(e => e.Life)
                .HasMaxLength(16)
                .HasDefaultValue("lifetime");
            entity.Property(e => e.Location)
                .HasDefaultValue(1)
                .HasColumnName("Location#");
            entity.Property(e => e.Narative)
                .HasMaxLength(255)
                .HasDefaultValue("about this artist");
            entity.Property(e => e.Phase)
                .HasDefaultValue(1)
                .HasColumnName("Phase#");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("A");
            entity.Property(e => e.Type)
                .HasMaxLength(4)
                .HasDefaultValue("arts")
                .HasColumnName("type");
        });

        modelBuilder.Entity<Brain>(entity =>
        {
            entity.ToTable("Brain");

            entity.Property(e => e.BrainId).HasColumnName("Brain_ID");
            entity.Property(e => e.BrainConnectionString)
                .HasMaxLength(255)
                .HasDefaultValue("server connection string")
                .HasColumnName("Brain_connection_string");
            entity.Property(e => e.BrainDescription)
                .HasMaxLength(255)
                .HasDefaultValue("description of BRAIN")
                .HasColumnName("Brain_description");
            entity.Property(e => e.BrainFkLocation)
                .HasDefaultValue(1)
                .HasColumnName("Brain_FK_Location");
            entity.Property(e => e.BrainLabel)
                .HasMaxLength(16)
                .HasDefaultValue("BRAIN label")
                .IsFixedLength()
                .HasColumnName("Brain_label");
            entity.Property(e => e.BrainLogin)
                .HasMaxLength(16)
                .HasDefaultValue("ReadOnly")
                .IsFixedLength()
                .HasColumnName("Brain_login");
            entity.Property(e => e.BrainStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Brain_status");
            entity.Property(e => e.BrainStorage)
                .HasMaxLength(255)
                .HasDefaultValue("Cloud storage account")
                .HasColumnName("Brain_storage");
            entity.Property(e => e.BrainType)
                .HasMaxLength(4)
                .HasDefaultValue("Nnet")
                .IsFixedLength()
                .HasColumnName("Brain_type");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK_BrandJD2");

            entity.ToTable("Brand");

            entity.Property(e => e.BrandId).HasColumnName("Brand_ID");
            entity.Property(e => e.BrandCntMax)
                .HasDefaultValue((short)64)
                .HasColumnName("Brand_Cnt_Max");
            entity.Property(e => e.BrandCntReg).HasColumnName("Brand_Cnt_Reg");
            entity.Property(e => e.BrandCode)
                .HasMaxLength(42)
                .HasDefaultValue("Link Users")
                .HasColumnName("Brand_code");
            entity.Property(e => e.BrandCost)
                .HasDefaultValue(1m)
                .HasColumnType("money")
                .HasColumnName("Brand_Cost");
            entity.Property(e => e.BrandEligibility)
                .HasDefaultValue((short)50)
                .HasColumnName("Brand_Eligibility");
            entity.Property(e => e.BrandGuide)
                .HasDefaultValue(7)
                .HasColumnName("Brand_Guide");
            entity.Property(e => e.BrandImage)
                .HasMaxLength(128)
                .HasDefaultValue("Blob pointer")
                .HasColumnName("Brand_Image");
            entity.Property(e => e.BrandLabel)
                .HasMaxLength(42)
                .HasDefaultValue("Registration Title")
                .HasColumnName("Brand_label");
            entity.Property(e => e.BrandRegDateClosed)
                .HasDefaultValue(new DateTime(2048, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("Brand_Reg_Date_Closed");
            entity.Property(e => e.BrandRole)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Brand_Role");
            entity.Property(e => e.BrandStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .HasColumnName("Brand_status");
            entity.Property(e => e.ChannelAlpha)
                .HasMaxLength(4)
                .HasColumnName("Channel_alpha");
            entity.Property(e => e.ChannelBeta)
                .HasMaxLength(4)
                .HasColumnName("Channel_beta");
            entity.Property(e => e.ChannelGamma)
                .HasMaxLength(4)
                .HasColumnName("Channel_gamma");
            entity.Property(e => e.LocationIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Location_ID_FK");
            entity.Property(e => e.NovaIdFk)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.ProgramIdFk)
                .HasDefaultValue(7)
                .HasColumnName("Program_ID_FK");
        });

        modelBuilder.Entity<CfgGanttTask>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CFG_Gantt_Task");

            entity.Property(e => e.Duration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IsExpand)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Notes)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Predecessor)
                .HasMaxLength(32)
                .IsFixedLength();
            entity.Property(e => e.Progress)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("progress");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(32)
                .IsFixedLength();
            entity.Property(e => e.String)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("string");
            entity.Property(e => e.TaskType)
                .HasMaxLength(4)
                .IsFixedLength();
        });

        modelBuilder.Entity<CfgGanttWork>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CFG_Gantt_Work");

            entity.Property(e => e.Duration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IsExpand)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Notes)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Predecessor)
                .HasMaxLength(32)
                .IsFixedLength();
            entity.Property(e => e.Progress)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("progress");
            entity.Property(e => e.ProjectName).HasMaxLength(16);
            entity.Property(e => e.String)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("string");
            entity.Property(e => e.TaskType)
                .HasMaxLength(4)
                .IsFixedLength();
        });

        modelBuilder.Entity<CfgLocationUrl>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CFG_Location_URL");

            entity.Property(e => e.Cloud)
                .HasMaxLength(144)
                .HasColumnName("cloud");
            entity.Property(e => e.LocationLabel16)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Location_label16");
            entity.Property(e => e.NounLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Noun_label");
            entity.Property(e => e.PodLabel)
                .HasMaxLength(16)
                .HasColumnName("POD_label");
            entity.Property(e => e.PodUrlBase)
                .HasMaxLength(128)
                .HasColumnName("POD_URL_base");
            entity.Property(e => e.UrlCloud)
                .HasMaxLength(128)
                .HasColumnName("URL_cloud");
            entity.Property(e => e.UrlDescription)
                .HasMaxLength(128)
                .HasColumnName("URL_Description");
            entity.Property(e => e.UrlLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("URL_label");
            entity.Property(e => e.UrlType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("URL_type");
            entity.Property(e => e.Urlconcat)
                .HasMaxLength(144)
                .HasColumnName("URLconcat");
        });

        modelBuilder.Entity<CfgPypePype>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CFG_Pype_Pype");

            entity.Property(e => e.Expr1)
                .HasMaxLength(4)
                .IsFixedLength();
            entity.Property(e => e.Expr2)
                .HasMaxLength(4)
                .IsFixedLength();
            entity.Property(e => e.Expr3)
                .HasMaxLength(16)
                .IsFixedLength();
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.PypeId)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_ID");
            entity.Property(e => e.PypeLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Pype_label");
            entity.Property(e => e.PypeLink)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_link");
            entity.Property(e => e.PypeStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("Pype_status");
            entity.Property(e => e.PypeType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_type");
        });

        modelBuilder.Entity<CfgPypeStem>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CFG_Pype_STEM");

            entity.Property(e => e.L1Id)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("L1_ID");
            entity.Property(e => e.L1Type)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("L1_type");
            entity.Property(e => e.ParentPype)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Parent Pype");
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.Pype)
                .HasMaxLength(4)
                .IsFixedLength();
            entity.Property(e => e.PypeDesc)
                .HasMaxLength(64)
                .HasColumnName("Pype_desc");
            entity.Property(e => e.PypeLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Pype_label");
            entity.Property(e => e.PypeType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_type");
        });

        modelBuilder.Entity<CfgTaskL4Noun>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CFG_Task_L4_Noun");

            entity.Property(e => e.Label)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("label");
            entity.Property(e => e.NounId).HasColumnName("Noun_ID");
            entity.Property(e => e.Nova).HasColumnName("NOVA");
            entity.Property(e => e.Object)
                .HasMaxLength(16)
                .IsFixedLength();
            entity.Property(e => e.ParFinish).HasColumnName("Par_finish");
            entity.Property(e => e.ParId).HasColumnName("Par_ID");
            entity.Property(e => e.ParStart).HasColumnName("Par_start");
            entity.Property(e => e.Pod).HasColumnName("POD");
            entity.Property(e => e.TaskDescription)
                .HasMaxLength(255)
                .HasColumnName("Task_description");
            entity.Property(e => e.TaskFinishDate).HasColumnName("Task_finish_date");
            entity.Property(e => e.TaskId).HasColumnName("Task_ID");
            entity.Property(e => e.TaskStartDate).HasColumnName("Task_start_date");
            entity.Property(e => e.Type)
                .HasMaxLength(4)
                .IsFixedLength();
        });

        modelBuilder.Entity<CfgTaskParmPt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CFG_Task_parmPT");

            entity.Property(e => e.NovaDescription).HasColumnName("NOVA_description");
            entity.Property(e => e.NovaIdFk).HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.Object)
                .HasMaxLength(16)
                .IsFixedLength();
            entity.Property(e => e.ParFinish).HasColumnName("Par_finish");
            entity.Property(e => e.ParStart).HasColumnName("Par_start");
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.TaskDescription)
                .HasMaxLength(255)
                .HasColumnName("Task_description");
            entity.Property(e => e.TaskFinishDate).HasColumnName("Task_finish_date");
            entity.Property(e => e.TaskId).HasColumnName("Task_ID");
            entity.Property(e => e.TaskLabel32)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("Task_label32");
            entity.Property(e => e.TaskLevel).HasColumnName("Task_level");
            entity.Property(e => e.TaskStartDate).HasColumnName("Task_start_date");
            entity.Property(e => e.TaskType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Task_type");
        });

        modelBuilder.Entity<CfgTaskUrl>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CFG_Task_URL");

            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.TaskId).HasColumnName("Task_ID");
            entity.Property(e => e.TaskLabel32)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("Task_label32");
            entity.Property(e => e.TaskLevel).HasColumnName("Task_level");
            entity.Property(e => e.TaskType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Task_type");
            entity.Property(e => e.UrlCloud)
                .HasMaxLength(128)
                .HasColumnName("URL_cloud");
            entity.Property(e => e.UrlId).HasColumnName("URL_ID");
            entity.Property(e => e.UrlLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("URL_label");
            entity.Property(e => e.UrlType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("URL_type");
        });

        modelBuilder.Entity<CfgWorkTask>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CFG_Work_Task");

            entity.Property(e => e.NounId).HasColumnName("Noun_ID");
            entity.Property(e => e.NounLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Noun_label");
            entity.Property(e => e.NovaIdFk).HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.TaskIdFk).HasColumnName("Task_ID_FK");
            entity.Property(e => e.TaskLabel32)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("Task_label32");
            entity.Property(e => e.WorkDescription)
                .HasMaxLength(255)
                .HasColumnName("Work_description");
            entity.Property(e => e.WorkId).HasColumnName("Work_ID");
            entity.Property(e => e.WorkLabel32)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("Work_label32");
            entity.Property(e => e.WorkLevel).HasColumnName("Work_level");
            entity.Property(e => e.WorkStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("Work_status");
        });

        modelBuilder.Entity<Channel>(entity =>
        {
            entity.ToTable("Channel");

            entity.Property(e => e.ChannelId).HasColumnName("Channel_ID");
            entity.Property(e => e.ChannelLabel)
                .HasMaxLength(16)
                .HasDefaultValue("Comm Channel")
                .IsFixedLength()
                .HasColumnName("Channel_label");
            entity.Property(e => e.ChannelRole)
                .HasMaxLength(4)
                .HasDefaultValue("engr")
                .IsFixedLength()
                .HasColumnName("Channel_Role");
            entity.Property(e => e.ChannelType)
                .HasMaxLength(4)
                .HasDefaultValue("ABCD")
                .IsFixedLength()
                .HasColumnName("Channel_type");
            entity.Property(e => e.LocationIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Location_ID_FK");
            entity.Property(e => e.PersonIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Person_ID_FK");
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
        });

        modelBuilder.Entity<Composition>(entity =>
        {
            entity.HasKey(e => e.CompositionId).HasName("PK_Composition_1");

            entity.ToTable("Composition");

            entity.Property(e => e.CompositionId).HasColumnName("Composition_ID");
            entity.Property(e => e.Composition1)
                .HasMaxLength(64)
                .HasDefaultValue("Composition of Entry")
                .HasColumnName("Composition");
            entity.Property(e => e.Finder)
                .HasMaxLength(128)
                .HasDefaultValue("URL");
            entity.Property(e => e.Narative)
                .HasMaxLength(128)
                .HasDefaultValue("About");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("A");
        });

        modelBuilder.Entity<DeltaPod>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Delta_POD");

            entity.Property(e => e.LocationIdFk).HasColumnName("Location_ID_FK");
            entity.Property(e => e.NovaIdFk).HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.PersonIdFk).HasColumnName("Person_ID_FK");
            entity.Property(e => e.PodChannel)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("POD_channel");
            entity.Property(e => e.PodDescription)
                .HasMaxLength(255)
                .HasColumnName("POD_description");
            entity.Property(e => e.PodId).HasColumnName("POD_ID");
            entity.Property(e => e.PodLabel)
                .HasMaxLength(16)
                .HasColumnName("POD_label");
            entity.Property(e => e.PodStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("POD_status");
            entity.Property(e => e.PodType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("POD_type");
            entity.Property(e => e.PodUrlBase)
                .HasMaxLength(128)
                .HasColumnName("POD_URL_base");
        });

        modelBuilder.Entity<DeltaPype>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Delta_Pype");

            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.PypeDesc)
                .HasMaxLength(64)
                .HasColumnName("Pype_desc");
            entity.Property(e => e.PypeId)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_ID");
            entity.Property(e => e.PypeLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Pype_label");
            entity.Property(e => e.PypeLink)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_link");
            entity.Property(e => e.PypeStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("Pype_status");
            entity.Property(e => e.PypeType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_type");
        });

        modelBuilder.Entity<DeltaTask>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Delta_Task");

            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.TaskDescription)
                .HasMaxLength(255)
                .HasColumnName("Task_description");
            entity.Property(e => e.TaskId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Task_ID");
            entity.Property(e => e.TaskLabel32)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("Task_label32");
            entity.Property(e => e.TaskLevel).HasColumnName("Task_level");
            entity.Property(e => e.TaskType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Task_type");
        });

        modelBuilder.Entity<Element>(entity =>
        {
            entity.ToTable("Element");

            entity.Property(e => e.ElementId).HasColumnName("Element_ID");
            entity.Property(e => e.ElementByte).HasColumnName("Element_byte");
            entity.Property(e => e.ElementInt)
                .HasDefaultValue(1)
                .HasColumnName("Element_int");
            entity.Property(e => e.ElementLabel)
                .HasMaxLength(16)
                .HasDefaultValue("element 16")
                .IsFixedLength()
                .HasColumnName("Element_label");
            entity.Property(e => e.ElementReal).HasColumnName("Element_real");
            entity.Property(e => e.ElementSeq)
                .HasDefaultValue((short)1)
                .HasColumnName("Element_seq");
            entity.Property(e => e.ElementType)
                .HasMaxLength(4)
                .HasDefaultValue("desc")
                .IsFixedLength()
                .HasColumnName("Element_type");
            entity.Property(e => e.NounIdFk)
                .HasDefaultValue(1)
                .HasColumnName("NOUN_ID_FK");
        });

        modelBuilder.Entity<Guide>(entity =>
        {
            entity.ToTable("Guide");

            entity.Property(e => e.GuideId).HasColumnName("Guide_ID");
            entity.Property(e => e.GuideDtOrgin)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Guide_dt_orgin");
            entity.Property(e => e.GuideDtRevision)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Guide_dt_revision");
            entity.Property(e => e.GuideImage)
                .HasDefaultValue(1)
                .HasColumnName("Guide_image");
            entity.Property(e => e.GuideLabel)
                .HasMaxLength(16)
                .HasDefaultValue("label 16")
                .IsFixedLength()
                .HasColumnName("Guide_label");
            entity.Property(e => e.GuidePurpose)
                .HasMaxLength(255)
                .HasDefaultValue("guide purpose")
                .HasColumnName("Guide_purpose");
            entity.Property(e => e.GuideStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Guide_status");
            entity.Property(e => e.GuideType)
                .HasMaxLength(4)
                .HasDefaultValue("nnet")
                .IsFixedLength()
                .HasColumnName("Guide_type");
            entity.Property(e => e.NovaFk)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_FK");
            entity.Property(e => e.ProgramFk)
                .HasDefaultValue(1)
                .HasColumnName("Program_FK");
        });

        modelBuilder.Entity<Hercule>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("HERCULES");

            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.TaskDescription)
                .HasMaxLength(255)
                .HasColumnName("Task_description");
            entity.Property(e => e.TaskDuration).HasColumnName("Task_duration");
            entity.Property(e => e.TaskId).HasColumnName("Task_ID");
            entity.Property(e => e.TaskLabel32)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("Task_label32");
            entity.Property(e => e.TaskLevel).HasColumnName("Task_level");
            entity.Property(e => e.TaskParent).HasColumnName("Task_parent");
            entity.Property(e => e.TaskPrevious).HasColumnName("Task_previous");
            entity.Property(e => e.TaskType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Task_type");
        });

        modelBuilder.Entity<HerculesGantt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("HERCULES Gantt");

            entity.Property(e => e.Duration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IsExpand)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Notes)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Predecessor)
                .HasMaxLength(32)
                .IsFixedLength();
            entity.Property(e => e.Progress)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("progress");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(32)
                .IsFixedLength();
            entity.Property(e => e.String)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("string");
            entity.Property(e => e.TaskType)
                .HasMaxLength(4)
                .IsFixedLength();
        });

        modelBuilder.Entity<Interview>(entity =>
        {
            entity.HasKey(e => new { e.GuideId, e.QuestionId }).HasName("PK_InterviewNew");

            entity.ToTable("Interview");

            entity.Property(e => e.GuideId)
                .HasDefaultValue(1)
                .HasColumnName("Guide_ID");
            entity.Property(e => e.QuestionId)
                .HasDefaultValue(1)
                .HasColumnName("Question_ID");
            entity.Property(e => e.InterviewSeq)
                .HasDefaultValue((short)1)
                .HasColumnName("Interview_seq");
            entity.Property(e => e.InterviewStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Interview_status");
        });

        modelBuilder.Entity<IviewGuideQuestion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Iview_Guide_Questions");

            entity.Property(e => e.GuideLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Guide_label");
            entity.Property(e => e.InterviewSeq).HasColumnName("Interview_seq");
            entity.Property(e => e.QuestionText)
                .HasMaxLength(255)
                .HasColumnName("Question_text");
        });

        modelBuilder.Entity<IviewPersonQAnswered>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Iview_Person_Q_answered");

            entity.Property(e => e.AnswerResponse).HasColumnName("Answer_response");
            entity.Property(e => e.AnswerSeverity).HasColumnName("Answer_severity");
            entity.Property(e => e.GuideId).HasColumnName("Guide_ID");
            entity.Property(e => e.GuideLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Guide_label");
            entity.Property(e => e.GuidePurpose)
                .HasMaxLength(255)
                .HasColumnName("Guide_purpose");
            entity.Property(e => e.GuideType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Guide_type");
            entity.Property(e => e.InterviewSeq).HasColumnName("Interview_seq");
            entity.Property(e => e.NovaFk).HasColumnName("NOVA_FK");
            entity.Property(e => e.PersonFirst)
                .HasMaxLength(32)
                .HasColumnName("Person_first");
            entity.Property(e => e.PersonId).HasColumnName("Person_ID");
            entity.Property(e => e.PersonLast)
                .HasMaxLength(32)
                .HasColumnName("Person_last");
            entity.Property(e => e.QuestionText)
                .HasMaxLength(255)
                .HasColumnName("Question_text");
        });

        modelBuilder.Entity<ListBrainLocPerson>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("List_Brain_Loc_Person");

            entity.Property(e => e.BrainConnectionString)
                .HasMaxLength(255)
                .HasColumnName("Brain_connection_string");
            entity.Property(e => e.BrainDescription)
                .HasMaxLength(255)
                .HasColumnName("Brain_description");
            entity.Property(e => e.BrainLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Brain_label");
            entity.Property(e => e.BrainStorage)
                .HasMaxLength(255)
                .HasColumnName("Brain_storage");
            entity.Property(e => e.BrainType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Brain_type");
            entity.Property(e => e.LocationDescription)
                .HasMaxLength(128)
                .HasColumnName("Location_description");
            entity.Property(e => e.LocationLabel)
                .HasMaxLength(16)
                .HasColumnName("Location_label");
            entity.Property(e => e.PersonFirst)
                .HasMaxLength(32)
                .HasColumnName("Person_first");
            entity.Property(e => e.PersonLast)
                .HasMaxLength(32)
                .HasColumnName("Person_last");
        });

        modelBuilder.Entity<ListPodNoun>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("List_POD_Nouns");

            entity.Property(e => e.NounDescription)
                .HasMaxLength(255)
                .HasColumnName("Noun_description");
            entity.Property(e => e.NounId).HasColumnName("Noun_ID");
            entity.Property(e => e.NounLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Noun_label");
            entity.Property(e => e.NounType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Noun_type");
            entity.Property(e => e.PodDescription)
                .HasMaxLength(255)
                .HasColumnName("POD_description");
            entity.Property(e => e.PodId).HasColumnName("POD_ID");
            entity.Property(e => e.PodLabel)
                .HasMaxLength(16)
                .HasColumnName("POD_label");
            entity.Property(e => e.UrlIdPk).HasColumnName("URL_ID_PK");
        });

        modelBuilder.Entity<ListPodNounUrl>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("List_POD_Noun_URL");

            entity.Property(e => e.NounId).HasColumnName("Noun_ID");
            entity.Property(e => e.NounLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Noun_label");
            entity.Property(e => e.NounType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Noun_type");
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.PodLabel)
                .HasMaxLength(16)
                .HasColumnName("POD_label");
            entity.Property(e => e.UrlCloud)
                .HasMaxLength(128)
                .HasColumnName("URL_cloud");
        });

        modelBuilder.Entity<ListPodVerbUrl>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("List_POD_Verb_URL");

            entity.Property(e => e.PodId).HasColumnName("POD_ID");
            entity.Property(e => e.PodLabel)
                .HasMaxLength(16)
                .HasColumnName("POD_label");
            entity.Property(e => e.UrlCloud)
                .HasMaxLength(128)
                .HasColumnName("URL_cloud");
            entity.Property(e => e.UrlLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("URL_label");
            entity.Property(e => e.VerbId).HasColumnName("Verb_ID");
            entity.Property(e => e.VerbLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Verb_label");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK_Locat");

            entity.ToTable("Location");

            entity.Property(e => e.LocationId).HasColumnName("Location_ID");
            entity.Property(e => e.BrainFk)
                .HasDefaultValue(1)
                .HasColumnName("BRAIN_FK");
            entity.Property(e => e.Latitude).HasDefaultValue(42.31952f);
            entity.Property(e => e.LicenseFk)
                .HasDefaultValue(1)
                .HasColumnName("License_FK");
            entity.Property(e => e.LocationDesc)
                .HasMaxLength(64)
                .HasDefaultValue("Not assigned")
                .HasComment("Physical Site")
                .HasColumnName("Location_desc");
            entity.Property(e => e.LocationFinder)
                .HasMaxLength(128)
                .HasDefaultValue("Location_web_page")
                .HasColumnName("Location_Finder");
            entity.Property(e => e.LocationLabel16)
                .HasMaxLength(16)
                .HasDefaultValue("label16")
                .IsFixedLength()
                .HasColumnName("Location_label16");
            entity.Property(e => e.LocationPng)
                .HasMaxLength(50)
                .HasDefaultValue("Background_png")
                .HasColumnName("Location_png");
            entity.Property(e => e.LocationStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Location_status");
            entity.Property(e => e.LocationStorage)
                .HasMaxLength(128)
                .HasDefaultValue("Cloud-Storage")
                .HasColumnName("Location_Storage");
            entity.Property(e => e.LocationTimeZone)
                .HasDefaultValue(-5)
                .HasColumnName("Location_time_zone");
            entity.Property(e => e.LocationType)
                .HasMaxLength(4)
                .HasDefaultValue("loca")
                .IsFixedLength()
                .HasColumnName("Location_type");
            entity.Property(e => e.Longitude).HasDefaultValue(-83.23397f);
            entity.Property(e => e.PersonFkAdmn)
                .HasDefaultValue(1)
                .HasColumnName("Person_FK_admn");
            entity.Property(e => e.PersonFkEngr)
                .HasDefaultValue(1)
                .HasColumnName("Person_FK_engr");
            entity.Property(e => e.PersonFkXprt)
                .HasDefaultValue(1)
                .HasColumnName("Person_FK_xprt");
            entity.Property(e => e.PodFk)
                .HasDefaultValue(1)
                .HasColumnName("POD_FK");
            entity.Property(e => e.ProgramFk)
                .HasDefaultValue(1)
                .HasColumnName("Program_FK");
        });

        modelBuilder.Entity<Memo>(entity =>
        {
            entity.HasKey(e => new { e.MemoPersonTo, e.MemoPersonFrom, e.MemoDateTime }).HasName("PK_MemoNew");

            entity.ToTable("Memo");

            entity.Property(e => e.MemoPersonTo)
                .HasDefaultValue(1)
                .HasColumnName("Memo_Person_to");
            entity.Property(e => e.MemoPersonFrom)
                .HasDefaultValue(1)
                .HasColumnName("Memo_Person_from");
            entity.Property(e => e.MemoDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Memo_date_time");
            entity.Property(e => e.MemoChannel)
                .HasDefaultValue(1)
                .HasColumnName("Memo_Channel");
            entity.Property(e => e.MemoMessage).HasColumnName("Memo_message");
            entity.Property(e => e.MemoNova)
                .HasDefaultValue(1)
                .HasColumnName("Memo_NOVA");
            entity.Property(e => e.MemoPod)
                .HasDefaultValue(1)
                .HasColumnName("Memo_POD");
            entity.Property(e => e.MemoPriority)
                .HasDefaultValue((byte)3)
                .HasColumnName("Memo_priority");
            entity.Property(e => e.MemoStatus)
                .HasMaxLength(1)
                .HasDefaultValue("O")
                .IsFixedLength()
                .HasColumnName("Memo_status");
            entity.Property(e => e.MemoType)
                .HasMaxLength(4)
                .HasDefaultValue("memo")
                .IsFixedLength()
                .HasColumnName("Memo_type");
        });

        modelBuilder.Entity<MemoLink>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Memo_Link");

            entity.Property(e => e.About)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("about");
            entity.Property(e => e.From).HasColumnName("from");
            entity.Property(e => e.LocationTo)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Location_to");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.Open)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("open");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.Receive)
                .HasMaxLength(32)
                .HasColumnName("receive");
            entity.Property(e => e.Sender)
                .HasMaxLength(32)
                .HasColumnName("sender");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.To).HasColumnName("to");
        });

        modelBuilder.Entity<Noun>(entity =>
        {
            entity.HasKey(e => e.NounId).HasName("PK_Noun_1");

            entity.ToTable("Noun");

            entity.Property(e => e.NounId).HasColumnName("Noun_ID");
            entity.Property(e => e.NounDescription)
                .HasMaxLength(255)
                .HasDefaultValue("description of Noun")
                .HasColumnName("Noun_description");
            entity.Property(e => e.NounLabel)
                .HasMaxLength(16)
                .HasDefaultValue("label 16")
                .IsFixedLength()
                .HasColumnName("Noun_label");
            entity.Property(e => e.NounStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Noun_status");
            entity.Property(e => e.NounType)
                .HasMaxLength(4)
                .HasDefaultValue("base")
                .IsFixedLength()
                .HasColumnName("Noun_type");
            entity.Property(e => e.PodIdFk)
                .HasDefaultValue(1)
                .HasColumnName("POD_ID_FK");
            entity.Property(e => e.UrlIdPk)
                .HasDefaultValue(1)
                .HasColumnName("URL_ID_PK");
        });

        modelBuilder.Entity<Nova>(entity =>
        {
            entity.ToTable("NOVA");

            entity.Property(e => e.NovaId).HasColumnName("NOVA_ID");
            entity.Property(e => e.NovaAction)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_action");
            entity.Property(e => e.NovaChannel)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_channel");
            entity.Property(e => e.NovaDatetime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NOVA_datetime");
            entity.Property(e => e.NovaDescription).HasColumnName("NOVA_description");
            entity.Property(e => e.NovaLabel)
                .HasMaxLength(16)
                .HasColumnName("NOVA_label");
            entity.Property(e => e.NovaObject)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_object");
            entity.Property(e => e.NovaPrioriy)
                .HasDefaultValue((short)4)
                .HasColumnName("NOVA_prioriy");
            entity.Property(e => e.NovaStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("NOVA_status");
            entity.Property(e => e.NovaSubject)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_subject");
            entity.Property(e => e.NovaType)
                .HasMaxLength(4)
                .HasDefaultValue("nova")
                .IsFixedLength()
                .HasColumnName("NOVA_type");
            entity.Property(e => e.PersonIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Person_ID_FK");
            entity.Property(e => e.PodIdFk)
                .HasDefaultValue(1)
                .HasColumnName("POD_ID_FK");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK_Person_1");

            entity.ToTable("Person");

            entity.Property(e => e.PersonId).HasColumnName("Person_ID");
            entity.Property(e => e.LocationIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Location_ID_FK");
            entity.Property(e => e.PersonDatetime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Person_datetime");
            entity.Property(e => e.PersonFirst)
                .HasMaxLength(32)
                .HasDefaultValue("first")
                .HasColumnName("Person_first");
            entity.Property(e => e.PersonLabel)
                .HasMaxLength(16)
                .HasDefaultValue("label 16")
                .IsFixedLength()
                .HasColumnName("Person_label");
            entity.Property(e => e.PersonLast)
                .HasMaxLength(32)
                .HasDefaultValue("last")
                .HasColumnName("Person_last");
            entity.Property(e => e.PersonRole)
                .HasMaxLength(4)
                .HasDefaultValue("role")
                .IsFixedLength()
                .HasColumnName("Person_role");
            entity.Property(e => e.PersonStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Person_status");
            entity.Property(e => e.PersonType)
                .HasMaxLength(4)
                .HasDefaultValue("hman")
                .IsFixedLength()
                .HasColumnName("Person_type");
            entity.Property(e => e.PodIdFk)
                .HasDefaultValue(1)
                .HasColumnName("POD_ID_FK");
        });

        modelBuilder.Entity<Phase>(entity =>
        {
            entity.ToTable("Phase");

            entity.Property(e => e.PhaseId).HasColumnName("Phase_ID");
            entity.Property(e => e.Finder)
                .HasMaxLength(128)
                .HasDefaultValue("URL");
            entity.Property(e => e.Location).HasDefaultValue(1);
            entity.Property(e => e.Narative)
                .HasMaxLength(255)
                .HasDefaultValue("About");
            entity.Property(e => e.Period)
                .HasMaxLength(64)
                .HasDefaultValue("Art Period");
            entity.Property(e => e.Phase1)
                .HasMaxLength(64)
                .HasDefaultValue("Chronological Time Period")
                .HasColumnName("Phase");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValue("A");
        });

        modelBuilder.Entity<Pod>(entity =>
        {
            entity.ToTable("POD");

            entity.Property(e => e.PodId).HasColumnName("POD_ID");
            entity.Property(e => e.LocationIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Location_ID_FK");
            entity.Property(e => e.NovaIdFk)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.PersonIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Person_ID_FK");
            entity.Property(e => e.PodChannel)
                .HasMaxLength(4)
                .HasDefaultValue("chan")
                .IsFixedLength()
                .HasColumnName("POD_channel");
            entity.Property(e => e.PodDescription)
                .HasMaxLength(255)
                .HasDefaultValue("Parsed Object Domain - description")
                .HasColumnName("POD_description");
            entity.Property(e => e.PodLabel)
                .HasMaxLength(16)
                .HasDefaultValue("POD label")
                .HasColumnName("POD_label");
            entity.Property(e => e.PodStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("POD_status");
            entity.Property(e => e.PodType)
                .HasMaxLength(4)
                .HasDefaultValue("pods")
                .IsFixedLength()
                .HasColumnName("POD_type");
            entity.Property(e => e.PodUrlBase)
                .HasMaxLength(128)
                .HasDefaultValue("URL pointer to storage")
                .HasColumnName("POD_URL_base");
        });

        modelBuilder.Entity<PodBrand>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("POD-Brand");

            entity.Property(e => e.BrandCode)
                .HasMaxLength(42)
                .HasColumnName("Brand_code");
            entity.Property(e => e.BrandLabel)
                .HasMaxLength(42)
                .HasColumnName("Brand_label");
            entity.Property(e => e.BrandRole)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Brand_Role");
            entity.Property(e => e.PodId).HasColumnName("POD_ID");
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.PodLabel)
                .HasMaxLength(16)
                .HasColumnName("POD_label");
        });

        modelBuilder.Entity<PodNovaLascaux>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("POD_NOVA_Lascaux");

            entity.Property(e => e.NovaDescription).HasColumnName("NOVA_description");
            entity.Property(e => e.NovaId).HasColumnName("NOVA_ID");
            entity.Property(e => e.NovaType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("NOVA_type");
            entity.Property(e => e.OGlyph).HasColumnName("O_glyph");
            entity.Property(e => e.OId).HasColumnName("O_ID");
            entity.Property(e => e.OType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("O_type");
            entity.Property(e => e.Object)
                .HasMaxLength(16)
                .IsFixedLength();
            entity.Property(e => e.PodId).HasColumnName("POD_ID");
            entity.Property(e => e.SGlyph).HasColumnName("S_glyph");
            entity.Property(e => e.SId).HasColumnName("S_ID");
            entity.Property(e => e.SType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("S_type");
            entity.Property(e => e.Subject)
                .HasMaxLength(16)
                .IsFixedLength();
            entity.Property(e => e.VGlyph).HasColumnName("V_glyph");
            entity.Property(e => e.VId).HasColumnName("V_ID");
            entity.Property(e => e.VType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("V_type");
            entity.Property(e => e.Verb)
                .HasMaxLength(16)
                .IsFixedLength();
        });

        modelBuilder.Entity<PodPersonLocation>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("POD_Person_location");

            entity.Property(e => e.LocationDescription)
                .HasMaxLength(128)
                .HasColumnName("Location_description");
            entity.Property(e => e.PersonLast)
                .HasMaxLength(32)
                .HasColumnName("Person_last");
            entity.Property(e => e.PodDescription)
                .HasMaxLength(255)
                .HasColumnName("POD_description");
            entity.Property(e => e.PodId).HasColumnName("POD_ID");
            entity.Property(e => e.PodLabel)
                .HasMaxLength(16)
                .HasColumnName("POD_label");
            entity.Property(e => e.PodType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("POD_type");
        });

        modelBuilder.Entity<PodPypeAudit>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("POD_Pype_Audit");

            entity.Property(e => e.Drop)
                .HasMaxLength(4)
                .IsFixedLength();
            entity.Property(e => e.Drop16)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Drop 16");
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.PypeId)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_ID");
            entity.Property(e => e.PypeLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Pype_label");
            entity.Property(e => e.PypeLink)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_link");
            entity.Property(e => e.PypeStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("Pype_status");
            entity.Property(e => e.PypeType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_type");
        });

        modelBuilder.Entity<PodPypeLink>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("POD_PYPE_link");

            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.PypeDesc)
                .HasMaxLength(64)
                .HasColumnName("Pype_desc");
            entity.Property(e => e.PypeId)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_ID");
            entity.Property(e => e.PypeLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Pype_label");
            entity.Property(e => e.PypeLink)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_link");
            entity.Property(e => e.PypeStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("Pype_status");
            entity.Property(e => e.PypeType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_type");
        });

        modelBuilder.Entity<PodTask>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("POD_Task");

            entity.Property(e => e.ParentTaskStart).HasColumnName("Parent Task Start");
            entity.Property(e => e.PodId).HasColumnName("POD_ID");
            entity.Property(e => e.TaskDescription)
                .HasMaxLength(255)
                .HasColumnName("Task_description");
            entity.Property(e => e.TaskFinishDate).HasColumnName("Task_finish_date");
            entity.Property(e => e.TaskId).HasColumnName("Task_ID");
            entity.Property(e => e.TaskLabel32)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("Task_label32");
            entity.Property(e => e.TaskLevel).HasColumnName("Task_level");
            entity.Property(e => e.TaskStartDate).HasColumnName("Task_start_date");
            entity.Property(e => e.TaskType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Task_type");
        });

        modelBuilder.Entity<PodTaskWork>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("POD_Task_Work");

            entity.Property(e => e.NovaDescription).HasColumnName("NOVA_description");
            entity.Property(e => e.NovaType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("NOVA_type");
            entity.Property(e => e.Object)
                .HasMaxLength(16)
                .IsFixedLength();
            entity.Property(e => e.PodId).HasColumnName("POD_ID");
            entity.Property(e => e.Subject)
                .HasMaxLength(16)
                .IsFixedLength();
            entity.Property(e => e.TaskId).HasColumnName("Task_ID");
            entity.Property(e => e.TaskLabel32)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("Task_label32");
            entity.Property(e => e.TaskType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Task_type");
            entity.Property(e => e.Verb)
                .HasMaxLength(16)
                .IsFixedLength();
            entity.Property(e => e.WorkDescription)
                .HasMaxLength(255)
                .HasColumnName("Work_description");
            entity.Property(e => e.WorkId).HasColumnName("Work_ID");
            entity.Property(e => e.WorkType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Work_type");
        });

		modelBuilder.Entity<LascauxJunk>(entity =>
		{
			entity
				.HasNoKey()
				.ToTable("Lascaux_junk");

			entity.Property(e => e.A).HasColumnName("A#");
			entity.Property(e => e.About)
				.HasMaxLength(4)
				.IsFixedLength()
				.HasColumnName("about");
			entity.Property(e => e.Action)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("action");
			entity.Property(e => e.ActionDescription)
				.HasMaxLength(64)
				.IsFixedLength()
				.HasColumnName("actionDescription");
			entity.Property(e => e.ActionUrl).HasColumnName("actionURL");
			entity.Property(e => e.N).HasColumnName("N#");
			entity.Property(e => e.NovaDescription)
				.HasMaxLength(64)
				.IsFixedLength()
				.HasColumnName("novaDescription");
			entity.Property(e => e.O).HasColumnName("O#");
			entity.Property(e => e.Object)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("object");
			entity.Property(e => e.ObjectDescription)
				.HasMaxLength(64)
				.IsFixedLength()
				.HasColumnName("objectDescription");
			entity.Property(e => e.ObjectUrl).HasColumnName("objectURL");
			entity.Property(e => e.P).HasColumnName("P#");
			entity.Property(e => e.Pid).HasColumnName("pid");
			entity.Property(e => e.S).HasColumnName("S#");
			entity.Property(e => e.Subject)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("subject");
			entity.Property(e => e.SubjectDescription)
				.HasMaxLength(64)
				.IsFixedLength()
				.HasColumnName("subjectDescription");
			entity.Property(e => e.SubjectUrl).HasColumnName("subjectURL");
		});

		modelBuilder.Entity<LascauxNova>(entity =>
		{
			entity
				.HasNoKey()
				.ToView("Lascaux_NOVA");

			entity.Property(e => e.Expr3)
				.HasMaxLength(16)
				.IsFixedLength();
			entity.Property(e => e.Expr4)
				.HasMaxLength(16)
				.IsFixedLength();
			entity.Property(e => e.LascauxObject)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("Lascaux_object");
			entity.Property(e => e.LascauxObjectId).HasColumnName("Lascaux_object_ID");
			entity.Property(e => e.NounId).HasColumnName("Noun_ID");
			entity.Property(e => e.NounLabel)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("Noun_label");
			entity.Property(e => e.NovaDescription).HasColumnName("NOVA_description");
			entity.Property(e => e.NovaId).HasColumnName("NOVA_ID");
			entity.Property(e => e.NovaStatus)
				.HasMaxLength(1)
				.IsFixedLength()
				.HasColumnName("NOVA_status");
			entity.Property(e => e.NovaType)
				.HasMaxLength(4)
				.IsFixedLength()
				.HasColumnName("NOVA_type");
			entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
			entity.Property(e => e.PodLabel)
				.HasMaxLength(16)
				.HasColumnName("POD_label");
			entity.Property(e => e.UrlLabel)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("URL_label");
			entity.Property(e => e.VerbId).HasColumnName("Verb_ID");
			entity.Property(e => e.VerbLabel)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("Verb_label");
		});

		modelBuilder.Entity<LascauxTemp>(entity =>
		{
			entity
				.HasNoKey()
				.ToTable("Lascaux_temp");

			entity.Property(e => e.A).HasColumnName("A#");
			entity.Property(e => e.About)
				.HasMaxLength(4)
				.IsFixedLength()
				.HasColumnName("about");
			entity.Property(e => e.Action)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("action");
			entity.Property(e => e.ActionDescription)
				.HasMaxLength(64)
				.IsFixedLength()
				.HasColumnName("actionDescription");
			entity.Property(e => e.ActionUrl)
				.HasMaxLength(128)
				.HasColumnName("actionURL");
			entity.Property(e => e.N).HasColumnName("N#");
			entity.Property(e => e.NovaDescription)
				.HasMaxLength(64)
				.IsFixedLength()
				.HasColumnName("novaDescription");
			entity.Property(e => e.O).HasColumnName("O#");
			entity.Property(e => e.Object)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("object");
			entity.Property(e => e.ObjectDescription)
				.HasMaxLength(64)
				.IsFixedLength()
				.HasColumnName("objectDescription");
			entity.Property(e => e.ObjectUrl)
				.HasMaxLength(128)
				.HasColumnName("objectURL");
			entity.Property(e => e.P).HasColumnName("P#");
			entity.Property(e => e.Pid).HasColumnName("pid");
			entity.Property(e => e.S).HasColumnName("S#");
			entity.Property(e => e.Subject)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("subject");
			entity.Property(e => e.SubjectDescription)
				.HasMaxLength(64)
				.IsFixedLength()
				.HasColumnName("subjectDescription");
			entity.Property(e => e.SubjectUrl)
				.HasMaxLength(128)
				.HasColumnName("subjectURL");
		});

		modelBuilder.Entity<Program>(entity =>
        {
            entity.HasKey(e => e.ProgramId).HasName("PK_Program_1");

            entity.ToTable("Program");

            entity.Property(e => e.ProgramId)
                .ValueGeneratedNever()
                .HasColumnName("Program_Id");
            entity.Property(e => e.ProgramDesc)
                .HasMaxLength(128)
                .HasDefaultValue("Not assigned")
                .HasComment("Physical Site")
                .HasColumnName("Program_desc");
            entity.Property(e => e.ProgramLabel16)
                .HasMaxLength(16)
                .HasDefaultValue("label16")
                .IsFixedLength()
                .HasColumnName("Program_label16");
            entity.Property(e => e.ProgramStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Program_status");
            entity.Property(e => e.ProgramType)
                .HasMaxLength(4)
                .HasDefaultValue("loca")
                .IsFixedLength()
                .HasColumnName("Program_type");
        });

        modelBuilder.Entity<ProgramGuide>(entity =>
        {
            entity.HasKey(e => new { e.ProgramFk, e.GuideFk }).HasName("PK_Program");

            entity.ToTable("Program_Guides");

            entity.Property(e => e.ProgramFk).HasColumnName("Program_FK");
            entity.Property(e => e.GuideFk).HasColumnName("Guide_FK");
            entity.Property(e => e.SortSeq)
                .HasDefaultValue((short)1)
                .HasColumnName("sort_seq");
        });

        modelBuilder.Entity<Pype>(entity =>
        {
            entity.HasKey(e => new { e.PypeId, e.PypeType }).HasName("PK_Pype2");

            entity.ToTable("Pype");

            entity.Property(e => e.PypeId)
                .HasMaxLength(4)
                .HasDefaultValue("NOVA")
                .IsFixedLength()
                .HasColumnName("Pype_ID");
            entity.Property(e => e.PypeType)
                .HasMaxLength(4)
                .HasDefaultValue("type")
                .IsFixedLength()
                .HasColumnName("Pype_type");
            entity.Property(e => e.PodIdFk)
                .HasDefaultValue(1)
                .HasColumnName("POD_ID_FK");
            entity.Property(e => e.PypeDesc)
                .HasMaxLength(64)
                .HasDefaultValue("Domain Expert Definition")
                .HasColumnName("Pype_desc");
            entity.Property(e => e.PypeLabel)
                .HasMaxLength(16)
                .HasDefaultValue("label 16")
                .IsFixedLength()
                .HasColumnName("Pype_label");
            entity.Property(e => e.PypeLink)
                .HasMaxLength(4)
                .HasDefaultValue("stop")
                .IsFixedLength()
                .HasColumnName("Pype_link");
            entity.Property(e => e.PypeStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Pype_status");
        });

        modelBuilder.Entity<PypeNoun>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Pype_NOUN");

            entity.Property(e => e.NounDescription)
                .HasMaxLength(255)
                .HasColumnName("Noun_description");
            entity.Property(e => e.NounId).HasColumnName("Noun_ID");
            entity.Property(e => e.NounLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Noun_label");
            entity.Property(e => e.NounType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Noun_type");
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.PypeId)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_ID");
            entity.Property(e => e.PypeLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Pype_label");
            entity.Property(e => e.PypeType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_type");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK_QuestionNew");

            entity.ToTable("Question");

            entity.Property(e => e.QuestionId).HasColumnName("Question_ID");
            entity.Property(e => e.NovaIdFk)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.QuestionStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Question_status");
            entity.Property(e => e.QuestionText)
                .HasMaxLength(255)
                .HasDefaultValue("question")
                .HasColumnName("Question_text");
            entity.Property(e => e.QuestionType)
                .HasMaxLength(4)
                .HasDefaultValue("quiz")
                .IsFixedLength()
                .HasColumnName("Question_type");
        });

        modelBuilder.Entity<RecipeArtifact>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Recipe_Artifact");

            entity.Property(e => e.Category).HasMaxLength(64);
            entity.Property(e => e.Chef).HasMaxLength(64);
            entity.Property(e => e.Classification).HasMaxLength(64);
            entity.Property(e => e.Item).HasColumnName("item");
            entity.Property(e => e.Location).HasMaxLength(128);
            entity.Property(e => e.Period).HasColumnName("period");
            entity.Property(e => e.Recipe).HasMaxLength(64);
            entity.Property(e => e.What).HasColumnName("what");
            entity.Property(e => e.Where).HasColumnName("where");
            entity.Property(e => e.Who).HasColumnName("who");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.ToTable("Registration");

            entity.HasIndex(e => e.RegistrationEmail, "IX_Registration_email").IsUnique();

            entity.HasIndex(e => e.RegistrationUsername, "IX_Registration_username").IsUnique();

            entity.Property(e => e.RegistrationId).HasColumnName("Registration_ID");
            entity.Property(e => e.PersonIdFk).HasColumnName("Person_ID_FK");
            entity.Property(e => e.RegistrationEmail)
                .HasMaxLength(320)
                .IsUnicode(false)
                .HasDefaultValue("email 320")
                .HasColumnName("Registration_email");
            entity.Property(e => e.RegistrationPassword)
                .HasMaxLength(85)
                .HasDefaultValue("password 16")
                .IsFixedLength()
                .HasColumnName("Registration_password");
            entity.Property(e => e.RegistrationStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Registration_status");
            entity.Property(e => e.RegistrationUsername)
                .HasMaxLength(16)
                .HasDefaultValue("username 16")
                .HasColumnName("Registration_username");
        });

        modelBuilder.Entity<RegistrationRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Registration_Role");

            entity.Property(e => e.PersonRole)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Person_role");
            entity.Property(e => e.PersonType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Person_type");
            entity.Property(e => e.RegistrationId).HasColumnName("Registration_ID");
            entity.Property(e => e.RegistrationUsername)
                .HasMaxLength(16)
                .HasColumnName("Registration_username");
        });

        modelBuilder.Entity<ReportArtifact>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("report_Artifact");

            entity.Property(e => e.Artifact).HasMaxLength(64);
            entity.Property(e => e.ArtifactId).HasColumnName("Artifact_ID");
            entity.Property(e => e.Artist).HasMaxLength(64);
            entity.Property(e => e.ArtistId).HasColumnName("Artist_ID");
            entity.Property(e => e.Composition).HasMaxLength(64);
            entity.Property(e => e.CompositionId).HasColumnName("Composition_ID");
            entity.Property(e => e.LocationId).HasColumnName("Location_ID");
            entity.Property(e => e.LocationLabel16)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Location_label16");
            entity.Property(e => e.Phase).HasMaxLength(64);
            entity.Property(e => e.PhaseId).HasColumnName("Phase_ID");
        });

        modelBuilder.Entity<ReportNovaSvo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("report_NOVA_SVO");

            entity.Property(e => e.NovaAction).HasColumnName("NOVA_action");
            entity.Property(e => e.NovaChannel).HasColumnName("NOVA_channel");
            entity.Property(e => e.NovaDatetime)
                .HasColumnType("datetime")
                .HasColumnName("NOVA_datetime");
            entity.Property(e => e.NovaDescription).HasColumnName("NOVA_description");
            entity.Property(e => e.NovaId)
                .ValueGeneratedOnAdd()
                .HasColumnName("NOVA_ID");
            entity.Property(e => e.NovaLabel)
                .HasMaxLength(16)
                .HasColumnName("NOVA_label");
            entity.Property(e => e.NovaObject).HasColumnName("NOVA_object");
            entity.Property(e => e.NovaPrioriy).HasColumnName("NOVA_prioriy");
            entity.Property(e => e.NovaStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("NOVA_status");
            entity.Property(e => e.NovaSubject).HasColumnName("NOVA_subject");
            entity.Property(e => e.NovaType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("NOVA_type");
            entity.Property(e => e.PersonIdFk).HasColumnName("Person_ID_FK");
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
        });

        modelBuilder.Entity<ReportPersonLocation>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("report_Person_Location");

            entity.Property(e => e.LocationDesc)
                .HasMaxLength(64)
                .HasColumnName("Location_desc");
            entity.Property(e => e.LocationId).HasColumnName("Location_ID");
            entity.Property(e => e.LocationLabel16)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Location_label16");
            entity.Property(e => e.LocationType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Location_type");
            entity.Property(e => e.PersonFirst)
                .HasMaxLength(32)
                .HasColumnName("Person_first");
            entity.Property(e => e.PersonId).HasColumnName("Person_ID");
            entity.Property(e => e.PersonLast)
                .HasMaxLength(32)
                .HasColumnName("Person_last");
            entity.Property(e => e.PersonRole)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Person_role");
            entity.Property(e => e.PersonStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("Person_status");
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
        });

        modelBuilder.Entity<ReportQuestionAnswer>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("report_Question_Answer");

            entity.Property(e => e.AnswerResponse).HasColumnName("Answer_response");
            entity.Property(e => e.AnswerSeverity).HasColumnName("Answer_severity");
            entity.Property(e => e.PersonFirst)
                .HasMaxLength(32)
                .HasColumnName("Person_first");
            entity.Property(e => e.PersonId).HasColumnName("Person_ID");
            entity.Property(e => e.PersonLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Person_label");
            entity.Property(e => e.PersonLast)
                .HasMaxLength(32)
                .HasColumnName("Person_last");
            entity.Property(e => e.QuestionText)
                .HasMaxLength(255)
                .HasColumnName("Question_text");
        });

        modelBuilder.Entity<ReportQuestionGuide>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("report_Question_Guide");

            entity.Property(e => e.GuideId).HasColumnName("Guide_ID");
            entity.Property(e => e.GuideLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Guide_label");
            entity.Property(e => e.GuidePurpose)
                .HasMaxLength(255)
                .HasColumnName("Guide_purpose");
            entity.Property(e => e.GuideType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Guide_type");
            entity.Property(e => e.QuestionId).HasColumnName("Question_ID");
            entity.Property(e => e.QuestionText)
                .HasMaxLength(255)
                .HasColumnName("Question_text");
            entity.Property(e => e.QuestionType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Question_type");
        });

        modelBuilder.Entity<ReportTaskWork>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("report_Task_Work");

            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.TaskId).HasColumnName("Task_ID");
            entity.Property(e => e.TaskLabel32)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("Task_label32");
            entity.Property(e => e.TaskType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Task_type");
            entity.Property(e => e.WorkDescription)
                .HasMaxLength(255)
                .HasColumnName("Work_description");
            entity.Property(e => e.WorkId).HasColumnName("Work_ID");
            entity.Property(e => e.WorkLabel32)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("Work_label32");
            entity.Property(e => e.WorkType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Work_type");
        });

        modelBuilder.Entity<RptPersonLocation>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("rpt_Person_Location");

            entity.Property(e => e.LocationId).HasColumnName("Location_ID");
            entity.Property(e => e.LocationLabel16)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Location_label16");
            entity.Property(e => e.PersonFirst)
                .HasMaxLength(32)
                .HasColumnName("Person_first");
            entity.Property(e => e.PersonId).HasColumnName("Person_ID");
            entity.Property(e => e.PersonLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Person_label");
            entity.Property(e => e.PersonLast)
                .HasMaxLength(32)
                .HasColumnName("Person_last");
            entity.Property(e => e.PersonRole)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Person_role");
            entity.Property(e => e.PersonStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("Person_status");
            entity.Property(e => e.PersonType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Person_type");
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.ToTable("Task");

            entity.Property(e => e.TaskId).HasColumnName("Task_ID");
            entity.Property(e => e.NounIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Noun_ID_FK");
            entity.Property(e => e.NovaIdFk)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.PersonIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Person_ID_FK");
            entity.Property(e => e.PodIdFk)
                .HasDefaultValue(1)
                .HasColumnName("POD_ID_FK");
            entity.Property(e => e.TaskDescription)
                .HasMaxLength(255)
                .HasDefaultValue("description of Task")
                .HasColumnName("Task_description");
            entity.Property(e => e.TaskDuration)
                .HasDefaultValue((short)7)
                .HasColumnName("Task_duration");
            entity.Property(e => e.TaskEntryDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Task_entry_date");
            entity.Property(e => e.TaskFinishDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Task_finish_date");
            entity.Property(e => e.TaskLabel32)
                .HasMaxLength(32)
                .HasDefaultValue("Task label extended32")
                .IsFixedLength()
                .HasColumnName("Task_label32");
            entity.Property(e => e.TaskLevel)
                .HasDefaultValue((short)1)
                .HasColumnName("Task_level");
            entity.Property(e => e.TaskParent)
                .HasDefaultValue(1)
                .HasColumnName("Task_parent");
            entity.Property(e => e.TaskPrevious)
                .HasDefaultValue(1)
                .HasColumnName("Task_previous");
            entity.Property(e => e.TaskSeq).HasColumnName("Task_seq");
            entity.Property(e => e.TaskStartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Task_start_date");
            entity.Property(e => e.TaskStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Task_status");
            entity.Property(e => e.TaskType)
                .HasMaxLength(4)
                .HasDefaultValue("task")
                .IsFixedLength()
                .HasColumnName("Task_type");
            entity.Property(e => e.TaskUrl).HasColumnName("Task_URL");
        });

        modelBuilder.Entity<TaskWorkGantt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Task_Work_Gantt");

            entity.Property(e => e.TaskDescription)
                .HasMaxLength(255)
                .HasColumnName("Task_description");
            entity.Property(e => e.TaskId).HasColumnName("Task_ID");
            entity.Property(e => e.TaskLevel).HasColumnName("Task_level");
            entity.Property(e => e.TaskParent).HasColumnName("Task_parent");
            entity.Property(e => e.TaskStartDate).HasColumnName("Task_start_date");
            entity.Property(e => e.TaskType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Task_type");
            entity.Property(e => e.WorkDescription)
                .HasMaxLength(255)
                .HasColumnName("Work_description");
            entity.Property(e => e.WorkStart).HasColumnName("Work_start");
        });

        modelBuilder.Entity<Url>(entity =>
        {
            entity.ToTable("URL");

            entity.Property(e => e.UrlId).HasColumnName("URL_ID");
            entity.Property(e => e.UrlCloud)
                .HasMaxLength(128)
                .HasDefaultValue("https://intelchatstorage.blob.core.windows.net/glyphcontainer/Art.png")
                .HasColumnName("URL_cloud");
            entity.Property(e => e.UrlDescription)
                .HasMaxLength(128)
                .HasDefaultValue("description of image")
                .HasColumnName("URL_Description");
            entity.Property(e => e.UrlLabel)
                .HasMaxLength(16)
                .HasDefaultValue("label16")
                .IsFixedLength()
                .HasColumnName("URL_label");
            entity.Property(e => e.UrlStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("URL_status");
            entity.Property(e => e.UrlType)
                .HasMaxLength(4)
                .HasDefaultValue("urls")
                .IsFixedLength()
                .HasColumnName("URL_type");
        });

      

        modelBuilder.Entity<View1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_1");

            entity.Property(e => e.Expr2)
                .HasMaxLength(4)
                .IsFixedLength();
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.PypeDesc)
                .HasMaxLength(64)
                .HasColumnName("Pype_desc");
            entity.Property(e => e.PypeId)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_ID");
            entity.Property(e => e.PypeLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Pype_label");
            entity.Property(e => e.PypeLink)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_link");
            entity.Property(e => e.PypeStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("Pype_status");
            entity.Property(e => e.PypeType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_type");
        });

        modelBuilder.Entity<WeeklyActivity>(entity =>
        {
            entity.HasKey(e => new { e.WeekId, e.PersonIdFk, e.NounIdFk });

            entity.ToTable("Weekly_activity");

            entity.Property(e => e.WeekId).HasColumnName("Week_ID");
            entity.Property(e => e.PersonIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Person_ID_FK");
            entity.Property(e => e.NounIdFk)
                .HasDefaultValue(66)
                .HasColumnName("Noun_ID_FK");
            entity.Property(e => e.Quantity).HasDefaultValue((short)1);
        });

        modelBuilder.Entity<WeeklyAnalytic>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Weekly_analytics");

            entity.Property(e => e.ElementId).HasColumnName("Element_ID");
            entity.Property(e => e.ElementInt).HasColumnName("Element_int");
            entity.Property(e => e.ElementLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Element_label");
            entity.Property(e => e.ElementType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Element_type");
            entity.Property(e => e.Group).HasColumnName("group");
            entity.Property(e => e.NounIdFk).HasColumnName("NOUN_ID_FK");
            entity.Property(e => e.NounLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Noun_label");
            entity.Property(e => e.NounType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Noun_type");
            entity.Property(e => e.PersonId).HasColumnName("Person_ID");
            entity.Property(e => e.PersonLast)
                .HasMaxLength(32)
                .HasColumnName("Person_last");
            entity.Property(e => e.WeekId).HasColumnName("Week_ID");
        });

        modelBuilder.Entity<WeeklyDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Weekly_detail");

            entity.Property(e => e.ElementId).HasColumnName("Element_ID");
            entity.Property(e => e.ElementLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Element_label");
            entity.Property(e => e.NounIdFk).HasColumnName("Noun_ID_FK");
            entity.Property(e => e.PersonIdFk).HasColumnName("Person_ID_FK");
            entity.Property(e => e.WeekId).HasColumnName("Week_ID");
        });

        modelBuilder.Entity<WeeklyPypeDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Weekly_Pype_detail");

            entity.Property(e => e.ElementId).HasColumnName("Element_ID");
            entity.Property(e => e.ElementInt).HasColumnName("Element_int");
            entity.Property(e => e.ElementLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Element_label");
            entity.Property(e => e.ElementType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Element_type");
            entity.Property(e => e.Grp).HasColumnName("grp");
            entity.Property(e => e.NounLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Noun_label");
            entity.Property(e => e.NounType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Noun_type");
            entity.Property(e => e.PersonId).HasColumnName("Person_ID");
            entity.Property(e => e.PersonLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Person_label");
            entity.Property(e => e.Scaling).HasColumnName("scaling");
            entity.Property(e => e.WeekId).HasColumnName("Week_ID");
            entity.Property(e => e._8wkIntAve).HasColumnName("8WK int ave");
        });

        modelBuilder.Entity<Work>(entity =>
        {
            entity.HasKey(e => e.WorkId).HasName("PK_Work2");

            entity.ToTable("Work");

            entity.Property(e => e.WorkId).HasColumnName("Work_ID");
            entity.Property(e => e.NovaIdFk)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.PersonIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Person_ID_FK");
            entity.Property(e => e.PodCounterWork).HasColumnName("POD_Counter_Work");
            entity.Property(e => e.TaskIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Task_ID_FK");
            entity.Property(e => e.WorkDescription)
                .HasMaxLength(255)
                .HasDefaultValue("description of Task")
                .HasColumnName("Work_description");
            entity.Property(e => e.WorkDuration)
                .HasDefaultValue((short)7)
                .HasColumnName("Work_duration");
            entity.Property(e => e.WorkEntryData)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Work_entry_data");
            entity.Property(e => e.WorkFinish)
                .HasDefaultValue((short)1)
                .HasColumnName("Work_finish");
            entity.Property(e => e.WorkFinishDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Work_finish_date");
            entity.Property(e => e.WorkLabel32)
                .HasMaxLength(32)
                .HasDefaultValue("Task label extended32")
                .IsFixedLength()
                .HasColumnName("Work_label32");
            entity.Property(e => e.WorkLevel)
                .HasDefaultValue((short)1)
                .HasColumnName("Work_level");
            entity.Property(e => e.WorkStart).HasColumnName("Work_start");
            entity.Property(e => e.WorkStartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Work_start_date");
            entity.Property(e => e.WorkStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Work_status");
            entity.Property(e => e.WorkType)
                .HasMaxLength(4)
                .HasDefaultValue("task")
                .IsFixedLength()
                .HasColumnName("Work_type");
        });

        modelBuilder.Entity<XnovaDictionaryInterview>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Xnova_Dictionary_Interview");

            entity.Property(e => e.About)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("about");
            entity.Property(e => e.G).HasColumnName("G#");
            entity.Property(e => e.N).HasColumnName("N#");
            entity.Property(e => e.NovaDescription).HasColumnName("NOVA_description");
            entity.Property(e => e.O).HasColumnName("O#");
            entity.Property(e => e.Object)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("object");
            entity.Property(e => e.ObjectDescription)
                .HasMaxLength(255)
                .HasColumnName("objectDescription");
            entity.Property(e => e.ObjectUrl)
                .HasMaxLength(128)
                .HasColumnName("objectURL");
            entity.Property(e => e.P).HasColumnName("P#");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.Q).HasColumnName("Q#");
            entity.Property(e => e.QuestionText)
                .HasMaxLength(255)
                .HasColumnName("Question_text");
            entity.Property(e => e.S).HasColumnName("S#");
            entity.Property(e => e.Subject)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("subject");
            entity.Property(e => e.SubjectDescription)
                .HasMaxLength(255)
                .HasColumnName("subjectDescription");
            entity.Property(e => e.SubjectUrl)
                .HasMaxLength(128)
                .HasColumnName("subjectURL");
            entity.Property(e => e.V).HasColumnName("V#");
            entity.Property(e => e.Verb)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("verb");
            entity.Property(e => e.VerbDescription)
                .HasMaxLength(255)
                .HasColumnName("verbDescription");
            entity.Property(e => e.VerbUrl)
                .HasMaxLength(128)
                .HasColumnName("verbURL");
        });

        modelBuilder.Entity<XnovaDictionaryNounObject>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Xnova_Dictionary_Noun_Object");

            entity.Property(e => e.A).HasColumnName("A#");
            entity.Property(e => e.About)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("about");
            entity.Property(e => e.Action)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("action");
            entity.Property(e => e.ActionDescription)
                .HasMaxLength(255)
                .HasColumnName("actionDescription");
            entity.Property(e => e.ActionUrl)
                .HasMaxLength(128)
                .HasColumnName("actionURL");
            entity.Property(e => e.N).HasColumnName("N#");
            entity.Property(e => e.NovaDescription).HasColumnName("novaDescription");
            entity.Property(e => e.O).HasColumnName("O#");
            entity.Property(e => e.Object)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("object");
            entity.Property(e => e.ObjectDescription)
                .HasMaxLength(255)
                .HasColumnName("objectDescription");
            entity.Property(e => e.ObjectUrl)
                .HasMaxLength(128)
                .HasColumnName("objectURL");
            entity.Property(e => e.P).HasColumnName("P#");
            entity.Property(e => e.S).HasColumnName("S#");
            entity.Property(e => e.Subject)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("subject");
            entity.Property(e => e.SubjectDescription)
                .HasMaxLength(255)
                .HasColumnName("subjectDescription");
            entity.Property(e => e.SubjectUrl)
                .HasMaxLength(128)
                .HasColumnName("subjectURL");
        });

        modelBuilder.Entity<XnovaDictionaryNounSubject>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Xnova_Dictionary_Noun_Subject");

            entity.Property(e => e.A).HasColumnName("A#");
            entity.Property(e => e.About)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("about");
            entity.Property(e => e.Action)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("action");
            entity.Property(e => e.ActionDescription)
                .HasMaxLength(255)
                .HasColumnName("actionDescription");
            entity.Property(e => e.ActionUrl)
                .HasMaxLength(128)
                .HasColumnName("actionURL");
            entity.Property(e => e.N).HasColumnName("N#");
            entity.Property(e => e.NovaDescription).HasColumnName("novaDescription");
            entity.Property(e => e.O).HasColumnName("O#");
            entity.Property(e => e.Object)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("object");
            entity.Property(e => e.ObjectDescription)
                .HasMaxLength(255)
                .HasColumnName("objectDescription");
            entity.Property(e => e.ObjectUrl)
                .HasMaxLength(128)
                .HasColumnName("objectURL");
            entity.Property(e => e.P).HasColumnName("P#");
            entity.Property(e => e.S).HasColumnName("S#");
            entity.Property(e => e.Subject)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("subject");
            entity.Property(e => e.SubjectDescription)
                .HasMaxLength(255)
                .HasColumnName("subjectDescription");
            entity.Property(e => e.SubjectUrl)
                .HasMaxLength(128)
                .HasColumnName("subjectURL");
        });

        modelBuilder.Entity<XnovaDictionaryNova>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Xnova_Dictionary_NOVA");

            entity.Property(e => e.A).HasColumnName("A#");
            entity.Property(e => e.About)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("about");
            entity.Property(e => e.Action)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("action");
            entity.Property(e => e.ActionDescription)
                .HasMaxLength(255)
                .HasColumnName("actionDescription");
            entity.Property(e => e.ActionUrl)
                .HasMaxLength(128)
                .HasColumnName("actionURL");
            entity.Property(e => e.N).HasColumnName("N#");
            entity.Property(e => e.NovaDescription).HasColumnName("novaDescription");
            entity.Property(e => e.O).HasColumnName("O#");
            entity.Property(e => e.Object)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("object");
            entity.Property(e => e.ObjectDescription)
                .HasMaxLength(255)
                .HasColumnName("objectDescription");
            entity.Property(e => e.ObjectUrl)
                .HasMaxLength(128)
                .HasColumnName("objectURL");
            entity.Property(e => e.P).HasColumnName("P#");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.S).HasColumnName("S#");
            entity.Property(e => e.Subject)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("subject");
            entity.Property(e => e.SubjectDescription)
                .HasMaxLength(255)
                .HasColumnName("subjectDescription");
            entity.Property(e => e.SubjectUrl)
                .HasMaxLength(128)
                .HasColumnName("subjectURL");
        });

        modelBuilder.Entity<XnovaDictionaryPod>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Xnova_Dictionary_POD");

			entity.Property(e => e.A).HasColumnName("A#");
			entity.Property(e => e.About)
				.HasMaxLength(4)
				.IsFixedLength()
				.HasColumnName("about");
			entity.Property(e => e.Action)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("action");
			entity.Property(e => e.ActionDescription)
				.HasMaxLength(255)
				.HasColumnName("actionDescription");
			entity.Property(e => e.ActionUrl)
				.HasMaxLength(128)
				.HasColumnName("actionURL");
			entity.Property(e => e.N).HasColumnName("N#");
			entity.Property(e => e.NovaDescription).HasColumnName("novaDescription");
			entity.Property(e => e.O).HasColumnName("O#");
			entity.Property(e => e.Object)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("object");
			entity.Property(e => e.ObjectDescription)
				.HasMaxLength(255)
				.HasColumnName("objectDescription");
			entity.Property(e => e.ObjectUrl)
				.HasMaxLength(128)
				.HasColumnName("objectURL");
			entity.Property(e => e.P).HasColumnName("P#");
			entity.Property(e => e.Pid).HasColumnName("pid");
			entity.Property(e => e.S).HasColumnName("S#");
			entity.Property(e => e.Subject)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("subject");
			entity.Property(e => e.SubjectDescription)
				.HasMaxLength(255)
				.HasColumnName("subjectDescription");
			entity.Property(e => e.SubjectUrl)
				.HasMaxLength(128)
				.HasColumnName("subjectURL");
		});

		modelBuilder.Entity<XnovaDictionaryQuestion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Xnova_Dictionary_Question");

			entity.Property(e => e.A).HasColumnName("A#");
			entity.Property(e => e.About)
				.HasMaxLength(4)
				.IsFixedLength()
				.HasColumnName("about");
			entity.Property(e => e.Action)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("action");
			entity.Property(e => e.ActionDescription)
				.HasMaxLength(255)
				.HasColumnName("actionDescription");
			entity.Property(e => e.ActionUrl)
				.HasMaxLength(128)
				.HasColumnName("actionURL");
			entity.Property(e => e.N).HasColumnName("N#");
			entity.Property(e => e.NovaDescription).HasColumnName("novaDescription");
			entity.Property(e => e.O).HasColumnName("O#");
			entity.Property(e => e.Object)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("object");
			entity.Property(e => e.ObjectDescription)
				.HasMaxLength(255)
				.HasColumnName("objectDescription");
			entity.Property(e => e.ObjectUrl)
				.HasMaxLength(128)
				.HasColumnName("objectURL");
			entity.Property(e => e.P).HasColumnName("P#");
			entity.Property(e => e.Pid).HasColumnName("pid");
			entity.Property(e => e.S).HasColumnName("S#");
			entity.Property(e => e.Subject)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("subject");
			entity.Property(e => e.SubjectDescription)
				.HasMaxLength(255)
				.HasColumnName("subjectDescription");
			entity.Property(e => e.SubjectUrl)
				.HasMaxLength(128)
				.HasColumnName("subjectURL");
		});

		modelBuilder.Entity<XnovaDictionaryTask>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Xnova_Dictionary_Task");

            entity.Property(e => e.From)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("from");
            entity.Property(e => e.N).HasColumnName("N#");
            entity.Property(e => e.Ntype)
                .HasMaxLength(4)
                .IsFixedLength();
            entity.Property(e => e.O).HasColumnName("O#");
            entity.Property(e => e.Object)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("object");
            entity.Property(e => e.Odesc).HasMaxLength(255);
            entity.Property(e => e.P).HasColumnName("P#");
            entity.Property(e => e.Pdesc).HasMaxLength(255);
            entity.Property(e => e.S).HasColumnName("S#");
            entity.Property(e => e.Sdesc).HasMaxLength(255);
            entity.Property(e => e.Subject)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("subject");
            entity.Property(e => e.V).HasColumnName("V#");
            entity.Property(e => e.Vdesc).HasMaxLength(255);
            entity.Property(e => e.Verb)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("verb");
            entity.Property(e => e.X).HasColumnName("X#");
            entity.Property(e => e.Xdesc)
                .HasMaxLength(32)
                .IsFixedLength();
            entity.Property(e => e.Xtype)
                .HasMaxLength(4)
                .IsFixedLength();
        });

        modelBuilder.Entity<XnovaDictionaryVerb>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Xnova_Dictionary_Verb");

            entity.Property(e => e.A).HasColumnName("A#");
            entity.Property(e => e.Action)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("action");
            entity.Property(e => e.ActionDescription)
                .HasMaxLength(255)
                .HasColumnName("actionDescription");
            entity.Property(e => e.ActionUrl)
                .HasMaxLength(128)
                .HasColumnName("actionURL");
            entity.Property(e => e.Nova).HasColumnName("NOVA");
            entity.Property(e => e.NovaDescription).HasColumnName("NOVA_description");
            entity.Property(e => e.O).HasColumnName("O#");
            entity.Property(e => e.Object)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("object");
            entity.Property(e => e.ObjectDescription)
                .HasMaxLength(255)
                .HasColumnName("objectDescription");
            entity.Property(e => e.ObjectUrl)
                .HasMaxLength(128)
                .HasColumnName("objectURL");
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.S).HasColumnName("S#");
            entity.Property(e => e.Subject)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("subject");
            entity.Property(e => e.SubjectDescription)
                .HasMaxLength(255)
                .HasColumnName("subjectDescription");
            entity.Property(e => e.SubjectUrl)
                .HasMaxLength(128)
                .HasColumnName("subjectURL");
        });


		modelBuilder.Entity<XnovaDictionaryWork>(entity =>
		{
			entity
				.HasNoKey()
				.ToView("Xnova_Dictionary_Work");

			entity.Property(e => e.A).HasColumnName("A#");
			entity.Property(e => e.About)
				.HasMaxLength(4)
				.IsFixedLength()
				.HasColumnName("about");
			entity.Property(e => e.Action)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("action");
			entity.Property(e => e.ActionDescription)
				.HasMaxLength(255)
				.HasColumnName("actionDescription");
			entity.Property(e => e.ActionUrl)
				.HasMaxLength(128)
				.HasColumnName("actionURL");
			entity.Property(e => e.N).HasColumnName("N#");
			entity.Property(e => e.NovaDescription).HasColumnName("novaDescription");
			entity.Property(e => e.O).HasColumnName("O#");
			entity.Property(e => e.Object)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("object");
			entity.Property(e => e.ObjectDescription)
				.HasMaxLength(255)
				.HasColumnName("objectDescription");
			entity.Property(e => e.ObjectUrl)
				.HasMaxLength(128)
				.HasColumnName("objectURL");
			entity.Property(e => e.P).HasColumnName("P#");
			entity.Property(e => e.Pid).HasColumnName("pid");
			entity.Property(e => e.S).HasColumnName("S#");
			entity.Property(e => e.Subject)
				.HasMaxLength(16)
				.IsFixedLength()
				.HasColumnName("subject");
			entity.Property(e => e.SubjectDescription)
				.HasMaxLength(255)
				.HasColumnName("subjectDescription");
			entity.Property(e => e.SubjectUrl)
				.HasMaxLength(128)
				.HasColumnName("subjectURL");
		});








		OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
