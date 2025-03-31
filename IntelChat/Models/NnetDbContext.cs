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

    public virtual DbSet<AuditCountTimeSlot> AuditCountTimeSlots { get; set; }

    public virtual DbSet<Brain> Brains { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<CertTaskNova> CertTaskNovas { get; set; }

    public virtual DbSet<CfgGuideInterview> CfgGuideInterviews { get; set; }

    public virtual DbSet<CfgIviewAudit> CfgIviewAudits { get; set; }

    public virtual DbSet<CfgNovaSubject> CfgNovaSubjects { get; set; }

    public virtual DbSet<CfgPod2> CfgPod2s { get; set; }

    public virtual DbSet<CfgProgramGuide> CfgProgramGuides { get; set; }

    public virtual DbSet<CfgVerifyPype> CfgVerifyPypes { get; set; }

    public virtual DbSet<CommandObso> CommandObsos { get; set; }

    public virtual DbSet<Composition> Compositions { get; set; }

    public virtual DbSet<CountNounType> CountNounTypes { get; set; }

    public virtual DbSet<CountTimeSlot> CountTimeSlots { get; set; }

    public virtual DbSet<Element> Elements { get; set; }

    public virtual DbSet<Execute> Executes { get; set; }

    public virtual DbSet<FilterPype> FilterPypes { get; set; }

    public virtual DbSet<FixPype> FixPypes { get; set; }

    public virtual DbSet<Guide> Guides { get; set; }

    public virtual DbSet<Ingrediance> Ingrediances { get; set; }

    public virtual DbSet<Interview> Interviews { get; set; }

    public virtual DbSet<LascauxFromNova> LascauxFromNovas { get; set; }

    public virtual DbSet<LascauxFromTask> LascauxFromTasks { get; set; }

    public virtual DbSet<LascauxFromUrl> LascauxFromUrls { get; set; }

    public virtual DbSet<LascauxNovaJunk> LascauxNovaJunks { get; set; }

    public virtual DbSet<LascauxNovasTemp> LascauxNovasTemps { get; set; }

    public virtual DbSet<LascauxViewTest> LascauxViewTests { get; set; }

    public virtual DbSet<LoadGuideQ> LoadGuideQs { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Logue> Logues { get; set; }

    public virtual DbSet<MapPerson> MapPeople { get; set; }

    public virtual DbSet<MapPod> MapPods { get; set; }

    public virtual DbSet<Memo> Memos { get; set; }

    public virtual DbSet<MyAnswer> MyAnswers { get; set; }

    public virtual DbSet<MyGoal> MyGoals { get; set; }

    public virtual DbSet<MyGuide> MyGuides { get; set; }

    public virtual DbSet<MyGuideList> MyGuideLists { get; set; }

    public virtual DbSet<MyWeekHistory> MyWeekHistories { get; set; }

    public virtual DbSet<Noun> Nouns { get; set; }

    public virtual DbSet<NounPypeType> NounPypeTypes { get; set; }

    public virtual DbSet<Nova> Novas { get; set; }

    public virtual DbSet<NovaSvoUrl> NovaSvoUrls { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PersonTemp> PersonTemps { get; set; }

    public virtual DbSet<Phase> Phases { get; set; }

    public virtual DbSet<Pod> Pods { get; set; }

    public virtual DbSet<PodPype> PodPypes { get; set; }

    public virtual DbSet<PodTask> PodTasks { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<ProgramGuide> ProgramGuides { get; set; }

    public virtual DbSet<ProgramSpaQ> ProgramSpaQs { get; set; }

    public virtual DbSet<Pype> Pypes { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<RegistrationCfg> RegistrationCfgs { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TempLascaux> TempLascauxes { get; set; }

    public virtual DbSet<TimeSlot> TimeSlots { get; set; }

    public virtual DbSet<Tuner> Tuners { get; set; }

    public virtual DbSet<Url> Urls { get; set; }

    public virtual DbSet<Verb> Verbs { get; set; }

    public virtual DbSet<VerbArchive> VerbArchives { get; set; }

    public virtual DbSet<VerbTest> VerbTests { get; set; }

    public virtual DbSet<View1> View1s { get; set; }

    public virtual DbSet<Work> Works { get; set; }

    public virtual DbSet<XnovaDictionaryTask> XnovaDictionaryTasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:lascaux.database.windows.net,1433;Initial Catalog=LascauxDB;Persist Security Info=False;User ID=LascauxLogin;Password=UMDlascaux2024;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

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
                .HasDefaultValue("name of artist")
                .HasColumnName("Artist");
            entity.Property(e => e.ArtistCommon)
                .HasMaxLength(24)
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

        modelBuilder.Entity<AuditCountTimeSlot>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Audit_Count_TimeSlot");

            entity.Property(e => e.Count)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("count");
            entity.Property(e => e.DOfW).HasColumnName("D of W");
            entity.Property(e => e.Minutes).HasColumnName("minutes");
            entity.Property(e => e.MultBy).HasColumnName("mult by");
            entity.Property(e => e.Person)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("person");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.Primitive)
                .HasMaxLength(16)
                .IsFixedLength();
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
            entity.Property(e => e.LanguageIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Language_ID_FK");
            entity.Property(e => e.LocationIdFk)
                .HasDefaultValue(2)
                .HasColumnName("Location_ID_FK");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK_Brand_new");

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
            entity.Property(e => e.BrandGuide).HasColumnName("Brand_Guide");
            entity.Property(e => e.BrandImage)
                .HasMaxLength(50)
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
                .HasDefaultValueSql("(user_name())")
                .IsFixedLength()
                .HasColumnName("Brand_Role");
            entity.Property(e => e.BrandStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .HasColumnName("Brand_status");
            entity.Property(e => e.ChannelAlpha)
                .HasMaxLength(50)
                .HasColumnName("Channel_Alpha");
            entity.Property(e => e.ChannelBeta)
                .HasMaxLength(50)
                .HasColumnName("Channel_Beta");
            entity.Property(e => e.ChannelGamma)
                .HasMaxLength(50)
                .HasColumnName("Channel_Gamma");
            entity.Property(e => e.GuideIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Guide_ID_FK");
            entity.Property(e => e.LocationIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Location_ID_FK");
            entity.Property(e => e.NovaIdFk)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.PersonIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Person_ID_FK");
            entity.Property(e => e.PodIdFk)
                .HasDefaultValue(1)
                .HasColumnName("POD_ID_FK");
            entity.Property(e => e.ProgramIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Program_ID_FK");
            entity.Property(e => e.UrlIdFk)
                .HasDefaultValue(1)
                .HasColumnName("URL_ID_FK");
        });

        modelBuilder.Entity<CertTaskNova>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Cert_Task_NOVA");

            entity.Property(e => e.NovaDescription)
                .HasMaxLength(1024)
                .HasColumnName("NOVA_description");
            entity.Property(e => e.NovaId).HasColumnName("NOVA_ID");
            entity.Property(e => e.TaskDescription)
                .HasMaxLength(255)
                .HasColumnName("Task_description");
            entity.Property(e => e.TaskId).HasColumnName("Task_ID");
            entity.Property(e => e.TaskLabel32)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("Task_label32");
            entity.Property(e => e.TaskParent).HasColumnName("Task_parent");
            entity.Property(e => e.TaskPrevious).HasColumnName("Task_previous");
            entity.Property(e => e.TaskStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("Task_status");
            entity.Property(e => e.TaskType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Task_type");
        });

        modelBuilder.Entity<CfgGuideInterview>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CFG_Guide_Interview");

            entity.Property(e => e.GuideId).HasColumnName("Guide_ID");
            entity.Property(e => e.GuideLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Guide_label");
            entity.Property(e => e.InterviewSeq).HasColumnName("Interview_seq");
            entity.Property(e => e.InterviewStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("Interview_status");
            entity.Property(e => e.QuestionId).HasColumnName("Question_ID");
            entity.Property(e => e.QuestionText)
                .HasMaxLength(255)
                .HasColumnName("Question_text");
        });

        modelBuilder.Entity<CfgIviewAudit>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CFG_Iview_Audit");

            entity.Property(e => e.GuideIdFk).HasColumnName("Guide_ID_FK");
            entity.Property(e => e.GuidePurpose)
                .HasMaxLength(255)
                .HasColumnName("Guide_purpose");
            entity.Property(e => e.MyGuideDtFinish).HasColumnName("MY_Guide_DT_finish");
            entity.Property(e => e.MyGuideDtInitial).HasColumnName("MY_Guide_DT_initial");
            entity.Property(e => e.PersonId).HasColumnName("Person_ID");
        });

        modelBuilder.Entity<CfgNovaSubject>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CFG_NOVA_subject");

            entity.Property(e => e.NounDescription)
                .HasMaxLength(255)
                .HasColumnName("Noun_description");
            entity.Property(e => e.NounLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Noun_label");
            entity.Property(e => e.NounType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Noun_type");
            entity.Property(e => e.NovaId).HasColumnName("NOVA_ID");
            entity.Property(e => e.NovaSubject).HasColumnName("NOVA_subject");
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.PypeType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_type");
            entity.Property(e => e.UrlCloud)
                .HasMaxLength(128)
                .HasColumnName("URL_cloud");
            entity.Property(e => e.UrlIdPk).HasColumnName("URL_ID_PK");
            entity.Property(e => e.UrlLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("URL_label");
            entity.Property(e => e.Verify).HasColumnName("verify");
        });

        modelBuilder.Entity<CfgPod2>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CFG_POD_2");

            entity.Property(e => e.NounDescription)
                .HasMaxLength(255)
                .HasColumnName("Noun_description");
            entity.Property(e => e.NounId).HasColumnName("Noun_ID");
            entity.Property(e => e.NounLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Noun_label");
            entity.Property(e => e.NounStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("Noun_status");
            entity.Property(e => e.NounType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Noun_type");
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.PodPypeNoun)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("POD_pype_noun");
            entity.Property(e => e.PypeId)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_ID");
        });

        modelBuilder.Entity<CfgProgramGuide>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CFG_Program_Guide");

            entity.Property(e => e.GuideIview).HasColumnName("Guide Iview");
            entity.Property(e => e.GuideLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Guide_label");
            entity.Property(e => e.ProgramIdFk).HasColumnName("Program_ID_FK");
            entity.Property(e => e.ProgramLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Program_label");
        });

        modelBuilder.Entity<CfgVerifyPype>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CFG_Verify_Pype");

            entity.Property(e => e.DropDown)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Drop_Down");
            entity.Property(e => e.PodId).HasColumnName("POD_ID");
            entity.Property(e => e.PodLabel)
                .HasMaxLength(16)
                .HasColumnName("POD_label");
            entity.Property(e => e.PodLockNoun)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("POD Lock_NOUN");
            entity.Property(e => e.PodLockVerb)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("POD Lock_VERB");
            entity.Property(e => e.PypeDesc)
                .HasMaxLength(64)
                .HasColumnName("Pype_desc");
            entity.Property(e => e.PypeId)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_ID");
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

        modelBuilder.Entity<CommandObso>(entity =>
        {
            entity.HasKey(e => new { e.CommandInitiator, e.CommandDtime }).HasName("PK_Command");

            entity.ToTable("Command_obso");

            entity.Property(e => e.CommandInitiator)
                .HasDefaultValue(1)
                .HasColumnName("Command_Initiator");
            entity.Property(e => e.CommandDtime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Command_DTime");
            entity.Property(e => e.CommandConnect)
                .HasMaxLength(128)
                .HasDefaultValue("Connection 128  not assigned")
                .HasComment("Physical Site")
                .HasColumnName("Command_Connect");
            entity.Property(e => e.CommandLabel16)
                .HasMaxLength(16)
                .HasDefaultValue("label16")
                .IsFixedLength()
                .HasColumnName("Command_label16");
            entity.Property(e => e.CommandResponse)
                .HasDefaultValue((byte)1)
                .HasColumnName("Command_Response");
            entity.Property(e => e.CommandSeverity)
                .HasDefaultValue((byte)4)
                .HasColumnName("Command_Severity");
            entity.Property(e => e.CommandStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Command_status");
            entity.Property(e => e.CommandString)
                .HasMaxLength(512)
                .HasDefaultValue("Command 512 No String")
                .HasColumnName("Command_String");
            entity.Property(e => e.CommandType)
                .HasMaxLength(4)
                .HasDefaultValue("loca")
                .IsFixedLength()
                .HasColumnName("Command_type");
            entity.Property(e => e.NeuronIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Neuron_ID_FK");
            entity.Property(e => e.NovaIdFk)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.PersonIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Person_ID_FK");
        });

        modelBuilder.Entity<Composition>(entity =>
        {
            entity.ToTable("Composition");

            entity.Property(e => e.CompositionId).HasColumnName("Composition_ID");
            entity.Property(e => e.Composition1)
                .HasMaxLength(64)
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

        modelBuilder.Entity<CountNounType>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Count_Noun_type");

            entity.Property(e => e.Pod).HasColumnName("POD");
            entity.Property(e => e.Type)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("type");
            entity.Property(e => e.TypeCnt).HasColumnName("Type_cnt");
        });

        modelBuilder.Entity<CountTimeSlot>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Count_Time_Slot");

            entity.Property(e => e.IdPersonFk).HasColumnName("ID_Person_FK");
            entity.Property(e => e.NounLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Noun_label");
            entity.Property(e => e.TimeCalendarWeekId).HasColumnName("Time_Calendar_Week_ID");
        });

        modelBuilder.Entity<Element>(entity =>
        {
            entity.ToTable("Element");

            entity.Property(e => e.ElementId).HasColumnName("Element_ID");
            entity.Property(e => e.ElementLabel16)
                .HasMaxLength(16)
                .HasDefaultValue("label16")
                .IsFixedLength()
                .HasColumnName("Element_label16");
            entity.Property(e => e.ElementStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Element_status");
            entity.Property(e => e.ElementType)
                .HasMaxLength(4)
                .HasDefaultValue("loca")
                .IsFixedLength()
                .HasColumnName("Element_type");
        });

        modelBuilder.Entity<Execute>(entity =>
        {
            entity.HasKey(e => e.ExecId).HasName("PK_Execute_new");

            entity.ToTable("Execute");

            entity.Property(e => e.ExecId).HasColumnName("Exec_ID");
            entity.Property(e => e.ExecuteR)
                .HasDefaultValue((byte)5)
                .HasColumnName("Execute_R");
            entity.Property(e => e.ExecuteS)
                .HasDefaultValue((byte)4)
                .HasColumnName("Execute_S");
            entity.Property(e => e.ExecuteText)
                .HasMaxLength(255)
                .HasDefaultValue("default")
                .HasColumnName("Execute_text");
            entity.Property(e => e.GuideIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Guide_ID_FK");
            entity.Property(e => e.Question).HasDefaultValue(1);
            entity.Property(e => e.Role)
                .HasMaxLength(4)
                .HasDefaultValue("xprt")
                .IsFixedLength();
            entity.Property(e => e.WorkTypeStatusE)
                .HasMaxLength(4)
                .HasDefaultValue("Locl")
                .IsFixedLength()
                .HasColumnName("Work_type_status_E");
        });

        modelBuilder.Entity<FilterPype>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Filter_Pype");

            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.PodLabel)
                .HasMaxLength(16)
                .HasColumnName("POD_label");
            entity.Property(e => e.PypeDesc)
                .HasMaxLength(64)
                .HasColumnName("Pype_desc");
            entity.Property(e => e.PypeLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Pype_label");
            entity.Property(e => e.PypeStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("Pype_status");
            entity.Property(e => e.PypeType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_type");
        });

        modelBuilder.Entity<FixPype>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("FIX_Pype");

            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.PypeDesc)
                .HasMaxLength(64)
                .HasColumnName("Pype_desc");
            entity.Property(e => e.PypeDropTax).HasColumnName("Pype_drop_tax");
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
            entity.Property(e => e.PypeSeq).HasColumnName("Pype_seq");
            entity.Property(e => e.PypeStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("Pype_status");
            entity.Property(e => e.PypeType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_type");
        });

        modelBuilder.Entity<Guide>(entity =>
        {
            entity.HasKey(e => e.GuideId).HasName("PK_Guide_UMD");

            entity.ToTable("Guide");

            entity.Property(e => e.GuideId).HasColumnName("Guide_ID");
            entity.Property(e => e.BrainIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Brain_ID_FK");
            entity.Property(e => e.GuideDtOrgin)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Guide_dt_orgin");
            entity.Property(e => e.GuideDtRevision)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Guide_dt_revision");
            entity.Property(e => e.GuideEligible)
                .HasDefaultValue((byte)50)
                .HasColumnName("Guide_eligible");
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
            entity.Property(e => e.GuideTags)
                .HasMaxLength(255)
                .HasDefaultValue("tag")
                .HasColumnName("Guide_tags");
            entity.Property(e => e.GuideType)
                .HasMaxLength(4)
                .HasDefaultValue("nnet")
                .IsFixedLength()
                .HasColumnName("Guide_type");
            entity.Property(e => e.NovaIdFk)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.PodIdFk)
                .HasDefaultValue(1)
                .HasColumnName("POD_ID_FK");
            entity.Property(e => e.ProgramIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Program_ID_FK");
            entity.Property(e => e.UrlIdFk)
                .HasDefaultValue(1)
                .HasColumnName("URL_ID_FK");
        });

        modelBuilder.Entity<Ingrediance>(entity =>
        {
            entity.HasKey(e => e.NounIdFk);

            entity.ToTable("Ingrediance");

            entity.Property(e => e.NounIdFk).HasColumnName("Noun_ID_FK");
            entity.Property(e => e.ElementIdFk)
                .HasDefaultValueSql("(N'label16')")
                .HasColumnName("Element_ID_FK");
            entity.Property(e => e.IngredHowmuch)
                .HasDefaultValueSql("(N'A')")
                .HasColumnName("Ingred_howmuch");
            entity.Property(e => e.IngredMeasure)
                .HasMaxLength(4)
                .HasDefaultValue("loca")
                .IsFixedLength()
                .HasColumnName("Ingred_measure");
        });

        modelBuilder.Entity<Interview>(entity =>
        {
            entity.HasKey(e => new { e.GuideId, e.QuestionId });

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

        modelBuilder.Entity<LascauxFromNova>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Lascaux_from_NOVA");

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
            entity.Property(e => e.N).HasColumnName("N#");
            entity.Property(e => e.NovaDescription).HasColumnName("novaDescription");
            entity.Property(e => e.NovaIn)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("NOVA_in");
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

        modelBuilder.Entity<LascauxFromTask>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Lascaux_from_TASK");

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
            entity.Property(e => e.N).HasColumnName("N#");
            entity.Property(e => e.NovaDescription).HasColumnName("novaDescription");
            entity.Property(e => e.NovaIn)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("NOVA_in");
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

        modelBuilder.Entity<LascauxFromUrl>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Lascaux_from_URLS");

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
            entity.Property(e => e.N).HasColumnName("N#");
            entity.Property(e => e.NovaDescription).HasColumnName("novaDescription");
            entity.Property(e => e.NovaIn)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("NOVA_in");
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
        });

        modelBuilder.Entity<LascauxNovaJunk>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Lascaux_NOVA_junk");

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

        modelBuilder.Entity<LascauxNovasTemp>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Lascaux_NOVAs_temp");

            entity.Property(e => e.From)
                .HasMaxLength(4)
                .IsUnicode(false);
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

        modelBuilder.Entity<LascauxViewTest>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Lascaux_view_TEST");

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

        modelBuilder.Entity<LoadGuideQ>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("LOAD_Guide_Qs");

            entity.Property(e => e.GProgram).HasColumnName("G_program");
            entity.Property(e => e.GuideId).HasColumnName("Guide_ID");
            entity.Property(e => e.GuideIdFk).HasColumnName("Guide_ID_FK");
            entity.Property(e => e.GuideLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Guide_label");
            entity.Property(e => e.IvewProg).HasColumnName("Ivew_prog");
            entity.Property(e => e.QId).HasColumnName("Q_ID");
            entity.Property(e => e.QuestionText)
                .HasMaxLength(255)
                .HasColumnName("Question_text");
            entity.Property(e => e.Seq).HasColumnName("seq");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Location");

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
            entity.Property(e => e.LocationId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Location_ID");
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
            entity.Property(e => e.LocationTag)
                .HasMaxLength(1024)
                .HasDefaultValue("tag")
                .HasColumnName("Location_tag");
            entity.Property(e => e.LocationTimeZone)
                .HasDefaultValue(-5)
                .HasColumnName("Location_time_zone");
            entity.Property(e => e.LocationType)
                .HasMaxLength(4)
                .HasDefaultValue("loca")
                .IsFixedLength()
                .HasColumnName("Location_type");
            entity.Property(e => e.Longitude).HasDefaultValue(-83.23397f);
            entity.Property(e => e.NovaIdFk)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_ID_FK");
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
            entity.Property(e => e.ProgramIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Program_ID_FK");
        });

        modelBuilder.Entity<Logue>(entity =>
        {
            entity.HasKey(e => new { e.Pod, e.Pid, e.DateTime });

            entity.ToTable("Logue");

            entity.Property(e => e.Pod).HasColumnName("pod");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.DateTime).HasColumnName("date_time");
            entity.Property(e => e.Command)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("command");
        });

        modelBuilder.Entity<MapPerson>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("map_Person");

            entity.Property(e => e.PersonFirst)
                .HasMaxLength(32)
                .HasColumnName("Person_first");
            entity.Property(e => e.PersonId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Person_ID");
            entity.Property(e => e.PersonLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Person_label");
            entity.Property(e => e.PersonLast)
                .HasMaxLength(32)
                .HasColumnName("Person_last");
            entity.Property(e => e.PersonPypeDdMyme)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Person_pypeDD_myme");
        });

        modelBuilder.Entity<MapPod>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("map_POD");

            entity.Property(e => e.PersonFirst)
                .HasMaxLength(32)
                .HasColumnName("Person_first");
            entity.Property(e => e.PersonId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Person_ID");
            entity.Property(e => e.PersonLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Person_label");
            entity.Property(e => e.PersonPypeDdMyme)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Person_pypeDD_myme");
            entity.Property(e => e.PersonPypeDdRole)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Person_pypeDD_role");
            entity.Property(e => e.PersonStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("Person_status");
        });

        modelBuilder.Entity<Memo>(entity =>
        {
            entity.HasKey(e => new { e.MemoPersonTo, e.MemoPersonFrom, e.MemoDateTime }).HasName("PK_Memo_1");

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
            entity.Property(e => e.GuideIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Guide_ID_FK");
            entity.Property(e => e.MemoChannel)
                .HasDefaultValue(1)
                .HasColumnName("Memo_Channel");
            entity.Property(e => e.MemoDtOriginal)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Memo_DT_original");
            entity.Property(e => e.MemoMessage)
                .HasMaxLength(1024)
                .HasDefaultValue("message")
                .HasColumnName("Memo_message");
            entity.Property(e => e.MemoNova).HasColumnName("Memo_Nova");
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
            entity.Property(e => e.PodIdFk)
                .HasDefaultValue(1)
                .HasColumnName("POD_ID_FK");
            entity.Property(e => e.QuestionIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Question_ID_FK");
        });

        modelBuilder.Entity<MyAnswer>(entity =>
        {
            entity.HasKey(e => new { e.PersonIdFk, e.QuestionIdFk }).HasName("PK_MY_Answers_1");

            entity.ToTable("MY_Answers");

            entity.Property(e => e.PersonIdFk).HasColumnName("Person_ID_FK");
            entity.Property(e => e.QuestionIdFk).HasColumnName("Question_ID_FK");
            entity.Property(e => e.DateIview)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Date_Iview");
            entity.Property(e => e.MyResponse).HasColumnName("MY_response");
            entity.Property(e => e.MySeverity).HasColumnName("MY_severity");
        });

        modelBuilder.Entity<MyGoal>(entity =>
        {
            entity.HasKey(e => new { e.PersonIdFk, e.TaskIdFk });

            entity.ToTable("MY_Goal");

            entity.Property(e => e.PersonIdFk).HasColumnName("Person_ID_FK");
            entity.Property(e => e.TaskIdFk).HasColumnName("Task_ID_FK");
            entity.Property(e => e.MyGoalDtFinish)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("MY_Goal_DT_finish");
            entity.Property(e => e.MyGoalDtStart)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("MY_Goal_DT_start");
            entity.Property(e => e.MyGoalType)
                .HasMaxLength(4)
                .HasDefaultValue("myGO")
                .IsFixedLength()
                .HasColumnName("MY_Goal_type");
        });

        modelBuilder.Entity<MyGuide>(entity =>
        {
            entity.HasKey(e => new { e.PersonIdFk, e.GuideIdFk }).HasName("PK_MY_Guide_NEW");

            entity.ToTable("MY_Guide");

            entity.Property(e => e.PersonIdFk).HasColumnName("Person_ID_FK");
            entity.Property(e => e.GuideIdFk).HasColumnName("Guide_ID_FK");
            entity.Property(e => e.MyGuide1)
                .HasMaxLength(1)
                .HasDefaultValue("B")
                .IsFixedLength()
                .HasColumnName("MY_Guide");
            entity.Property(e => e.MyGuideDtEvaluated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("MY_Guide_DT_evaluated");
            entity.Property(e => e.MyGuideDtFinish)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("MY_Guide_DT_finish");
            entity.Property(e => e.MyGuideDtInitial)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("MY_Guide_DT_initial");
            entity.Property(e => e.MyGuideStars)
                .HasDefaultValue((byte)3)
                .HasColumnName("MY_Guide_stars");
        });

        modelBuilder.Entity<MyGuideList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MY_Guide_list");

            entity.Property(e => e.BrainIdFk).HasColumnName("Brain_ID_FK");
            entity.Property(e => e.GuideDtOrgin)
                .HasColumnType("datetime")
                .HasColumnName("Guide_dt_orgin");
            entity.Property(e => e.GuideDtRevision)
                .HasColumnType("datetime")
                .HasColumnName("Guide_dt_revision");
            entity.Property(e => e.GuideEligible).HasColumnName("Guide_eligible");
            entity.Property(e => e.GuideId).HasColumnName("Guide_ID");
            entity.Property(e => e.GuideImage).HasColumnName("Guide_image");
            entity.Property(e => e.GuideLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Guide_label");
            entity.Property(e => e.GuidePurpose)
                .HasMaxLength(255)
                .HasColumnName("Guide_purpose");
            entity.Property(e => e.GuideStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("Guide_status");
            entity.Property(e => e.GuideTags)
                .HasMaxLength(255)
                .HasColumnName("Guide_tags");
            entity.Property(e => e.GuideType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Guide_type");
            entity.Property(e => e.NovaIdFk).HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.ProgramIdFk).HasColumnName("Program_ID_FK");
            entity.Property(e => e.UrlIdFk).HasColumnName("URL_ID_FK");
        });

        modelBuilder.Entity<MyWeekHistory>(entity =>
        {
            entity.HasKey(e => new { e.IdPersonFk, e.TimeCalendarWeekId, e.NovaIdFk });

            entity.ToTable("MY_Week_history");

            entity.Property(e => e.IdPersonFk).HasColumnName("ID_Person_FK");
            entity.Property(e => e.TimeCalendarWeekId).HasColumnName("Time_Calendar_Week_ID");
            entity.Property(e => e.NovaIdFk)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.TimeObjectSum)
                .HasDefaultValueSql("((1.0))")
                .HasColumnName("Time_Object_Sum");
        });

        modelBuilder.Entity<Noun>(entity =>
        {
            entity.ToTable("Noun");

            entity.Property(e => e.NounId).HasColumnName("Noun_ID");
            entity.Property(e => e.NounDescription)
                .HasMaxLength(255)
                .HasDefaultValue("description of Noun")
                .HasColumnName("Noun_description");
            entity.Property(e => e.NounIdFkCommon)
                .HasDefaultValue(1)
                .HasColumnName("Noun_ID_FK_common");
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
            entity.Property(e => e.NounTag)
                .HasMaxLength(1024)
                .HasDefaultValue("tag")
                .HasColumnName("Noun_tag");
            entity.Property(e => e.NounType)
                .HasMaxLength(4)
                .HasDefaultValue("base")
                .IsFixedLength()
                .HasColumnName("Noun_type");
            entity.Property(e => e.PodIdFk)
                .HasDefaultValue(2)
                .HasColumnName("POD_ID_FK");
            entity.Property(e => e.UrlIdPk)
                .HasDefaultValue(1)
                .HasColumnName("URL_ID_PK");
        });

        modelBuilder.Entity<NounPypeType>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Noun_pype_type");

            entity.Property(e => e.NounDescription)
                .HasMaxLength(255)
                .HasColumnName("Noun_description");
            entity.Property(e => e.NounId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Noun_ID");
            entity.Property(e => e.NounLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Noun_label");
            entity.Property(e => e.NounStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("Noun_status");
            entity.Property(e => e.NounType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Noun_type");
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.UrlIdPk).HasColumnName("URL_ID_PK");
        });

        modelBuilder.Entity<Nova>(entity =>
        {
            entity.ToTable("NOVA");

            entity.Property(e => e.NovaId).HasColumnName("NOVA_ID");
            entity.Property(e => e.LocationIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Location_ID_FK");
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
            entity.Property(e => e.NovaDescription)
                .HasMaxLength(1024)
                .HasColumnName("NOVA_description");
            entity.Property(e => e.NovaLabel)
                .HasMaxLength(16)
                .HasDefaultValue("label16")
                .IsFixedLength()
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
            entity.Property(e => e.NovaTag)
                .HasMaxLength(1000)
                .HasDefaultValue("tag")
                .HasColumnName("NOVA_tag");
            entity.Property(e => e.NovaType)
                .HasMaxLength(4)
                .HasDefaultValue("time")
                .IsFixedLength()
                .HasColumnName("NOVA_type");
            entity.Property(e => e.PersonIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Person_ID_FK");
            entity.Property(e => e.PodIdFk)
                .HasDefaultValue(4)
                .HasColumnName("POD_ID_FK");
        });

        modelBuilder.Entity<NovaSvoUrl>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("NOVA_SVO_URL");

            entity.Property(e => e.Expr1)
                .HasMaxLength(16)
                .IsFixedLength();
            entity.Property(e => e.Expr2)
                .HasMaxLength(4)
                .IsFixedLength();
            entity.Property(e => e.NounLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Noun_label");
            entity.Property(e => e.NounType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Noun_type");
            entity.Property(e => e.NovaDescription).HasColumnName("NOVA_description");
            entity.Property(e => e.NovaId).HasColumnName("NOVA_ID");
            entity.Property(e => e.NovaType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("NOVA_type");
            entity.Property(e => e.PodIdFk).HasColumnName("POD_ID_FK");
            entity.Property(e => e.VerbLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Verb_label");
            entity.Property(e => e.VerbType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Verb_type");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK_Person_2");

            entity.ToTable("Person");

            entity.Property(e => e.PersonId).HasColumnName("Person_ID");
            entity.Property(e => e.BrainIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Brain_ID_FK");
            entity.Property(e => e.GuideIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Guide_ID_FK");
            entity.Property(e => e.LocationIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Location_ID_FK");
            entity.Property(e => e.NovaIdFk)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.PersonDatetime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Person_datetime");
            entity.Property(e => e.PersonEligible)
                .HasDefaultValue((byte)100)
                .HasColumnName("Person_eligible");
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
            entity.Property(e => e.PersonMyCloud)
                .HasMaxLength(128)
                .HasDefaultValue("MY_cloud")
                .HasColumnName("Person_MY_cloud");
            entity.Property(e => e.PersonPypeDdMyme)
                .HasMaxLength(4)
                .HasDefaultValue("hman")
                .IsFixedLength()
                .HasColumnName("Person_pypeDD_myme");
            entity.Property(e => e.PersonPypeDdRole)
                .HasMaxLength(4)
                .HasDefaultValue("role")
                .IsFixedLength()
                .HasColumnName("Person_pypeDD_role");
            entity.Property(e => e.PersonStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Person_status");
            entity.Property(e => e.PersonTag)
                .HasMaxLength(255)
                .HasDefaultValue("tag")
                .HasColumnName("Person_tag");
            entity.Property(e => e.PodIdFk)
                .HasDefaultValue(1)
                .HasColumnName("POD_ID_FK");
            entity.Property(e => e.ProgramIdFk)
                .HasDefaultValue(3)
                .HasColumnName("Program_ID_FK");
            entity.Property(e => e.UrlIdFk)
                .HasDefaultValue(1)
                .HasColumnName("URL_ID_FK");
        });

        modelBuilder.Entity<PersonTemp>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK_Person");

            entity.ToTable("Person_temp");

            entity.Property(e => e.PersonId).HasColumnName("Person_ID");
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
        });

        modelBuilder.Entity<Phase>(entity =>
        {
            entity.ToTable("Phase");

            entity.Property(e => e.PhaseId).HasColumnName("Phase_ID");
            entity.Property(e => e.Finder)
                .HasMaxLength(128)
                .HasDefaultValue("URL to information about Art Period");
            entity.Property(e => e.Location).HasDefaultValue(1);
            entity.Property(e => e.Narative)
                .HasMaxLength(255)
                .HasDefaultValue("about art period");
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
            entity.Property(e => e.GuideIdFk)
                .HasDefaultValue(2)
                .HasColumnName("Guide_ID_FK");
            entity.Property(e => e.LocationIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Location_ID_FK");
            entity.Property(e => e.NovaIdFk)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.PersonIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Person_ID_FK");
            entity.Property(e => e.PodDescription)
                .HasMaxLength(255)
                .HasDefaultValue("Parsed Object Domain - description")
                .HasColumnName("POD_description");
            entity.Property(e => e.PodImage)
                .HasMaxLength(128)
                .HasDefaultValue("URL pointer to storage")
                .HasColumnName("POD_image");
            entity.Property(e => e.PodLabel)
                .HasMaxLength(16)
                .HasDefaultValue("POD label")
                .HasColumnName("POD_label");
            entity.Property(e => e.PodPypeDdChan)
                .HasMaxLength(4)
                .HasDefaultValue("chan")
                .IsFixedLength()
                .HasColumnName("POD_pypeDD_chan");
            entity.Property(e => e.PodPypeDdTime)
                .HasMaxLength(4)
                .HasDefaultValue("hour")
                .IsFixedLength()
                .HasColumnName("POD_pypeDD_time");
            entity.Property(e => e.PodPypeNoun)
                .HasMaxLength(4)
                .HasDefaultValue("NOUN")
                .IsFixedLength()
                .HasColumnName("POD_pype_noun");
            entity.Property(e => e.PodPypeVerb)
                .HasMaxLength(4)
                .HasDefaultValue("VERB")
                .IsFixedLength()
                .HasColumnName("POD_pype_verb");
            entity.Property(e => e.PodRgb)
                .HasMaxLength(7)
                .HasDefaultValue("#223344")
                .IsFixedLength()
                .HasColumnName("POD_RGB");
            entity.Property(e => e.PodStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("POD_status");
            entity.Property(e => e.PodTag)
                .HasMaxLength(255)
                .HasDefaultValue("tag")
                .HasColumnName("POD_tag");
            entity.Property(e => e.PodType)
                .HasMaxLength(4)
                .HasDefaultValue("pods")
                .IsFixedLength()
                .HasColumnName("POD_type");
            entity.Property(e => e.ProgramIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Program_ID_FK");
            entity.Property(e => e.UrlIdFk)
                .HasDefaultValue(1)
                .HasColumnName("URL_ID_FK");
        });

        modelBuilder.Entity<PodPype>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("POD_Pype");

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
            entity.Property(e => e.PypeSeq).HasColumnName("Pype_seq");
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

            entity.Property(e => e.NovaIdFk).HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.PersonFirst)
                .HasMaxLength(32)
                .HasColumnName("Person_first");
            entity.Property(e => e.PersonId).HasColumnName("Person_ID");
            entity.Property(e => e.PersonLast)
                .HasMaxLength(32)
                .HasColumnName("Person_last");
            entity.Property(e => e.TaskId).HasColumnName("Task_ID");
            entity.Property(e => e.TaskLabel32)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("Task_label32");
        });

        modelBuilder.Entity<Program>(entity =>
        {
            entity.ToTable("Program");

            entity.Property(e => e.ProgramId).HasColumnName("Program_ID");
            entity.Property(e => e.PersonIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Person_ID_FK");
            entity.Property(e => e.ProgramDesc)
                .HasMaxLength(128)
                .HasDefaultValue("Not assigned")
                .HasComment("Physical Site")
                .HasColumnName("Program_desc");
            entity.Property(e => e.ProgramLabel)
                .HasMaxLength(16)
                .HasDefaultValue("label16")
                .IsFixedLength()
                .HasColumnName("Program_label");
            entity.Property(e => e.ProgramStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Program_status");
            entity.Property(e => e.ProgramTag)
                .HasMaxLength(255)
                .HasDefaultValue("tag")
                .HasColumnName("Program_tag");
            entity.Property(e => e.ProgramType)
                .HasMaxLength(4)
                .HasDefaultValue("loca")
                .IsFixedLength()
                .HasColumnName("Program_type");
            entity.Property(e => e.UrlIdFk)
                .HasDefaultValue(1)
                .HasColumnName("URL_ID_FK");
        });

        modelBuilder.Entity<ProgramGuide>(entity =>
        {
            entity.HasKey(e => new { e.ProgramIdFk, e.GuideIdFk });

            entity.ToTable("Program_Guide");

            entity.Property(e => e.ProgramIdFk).HasColumnName("Program_ID_FK");
            entity.Property(e => e.GuideIdFk).HasColumnName("Guide_ID_FK");
        });

        modelBuilder.Entity<ProgramSpaQ>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Program_SPA_Qs");

            entity.Property(e => e.GuideId).HasColumnName("Guide_ID");
            entity.Property(e => e.GuideLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Guide_label");
            entity.Property(e => e.InterviewSeq).HasColumnName("Interview_seq");
            entity.Property(e => e.ProgramDesc)
                .HasMaxLength(128)
                .HasColumnName("Program_desc");
            entity.Property(e => e.ProgramId).HasColumnName("Program_ID");
            entity.Property(e => e.ProgramLabel)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("Program_label");
            entity.Property(e => e.QuestionId).HasColumnName("Question_ID");
            entity.Property(e => e.QuestionText)
                .HasMaxLength(255)
                .HasColumnName("Question_text");
        });

        modelBuilder.Entity<Pype>(entity =>
        {
            entity.HasKey(e => new { e.PypeId, e.PypeType });

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
                .HasDefaultValue(4)
                .HasColumnName("POD_ID_FK");
            entity.Property(e => e.PypeDesc)
                .HasMaxLength(64)
                .HasDefaultValue("Domain Expert Definition")
                .HasColumnName("Pype_desc");
            entity.Property(e => e.PypeDropTax)
                .HasDefaultValue((short)4)
                .HasColumnName("Pype_drop_tax");
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
            entity.Property(e => e.PypePointer)
                .HasDefaultValue(1)
                .HasColumnName("Pype_Pointer");
            entity.Property(e => e.PypeSeq)
                .HasDefaultValue((byte)1)
                .HasColumnName("Pype_seq");
            entity.Property(e => e.PypeStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Pype_status");
            entity.Property(e => e.PypeTag)
                .HasMaxLength(1024)
                .HasDefaultValue("tag")
                .HasColumnName("Pype_Tag");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.ToTable("Question");

            entity.Property(e => e.QuestionId).HasColumnName("Question_ID");
            entity.Property(e => e.NovaIdFk)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.QuestionEligible)
                .HasDefaultValue((byte)100)
                .HasColumnName("Question_eligible");
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

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK_Registration_1");

            entity.ToTable("Registration");

            entity.Property(e => e.RegistrationId).HasColumnName("Registration_ID");
            entity.Property(e => e.PersonIdFk).HasColumnName("Person_ID_FK");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Registration_date");
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

        modelBuilder.Entity<RegistrationCfg>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK_Registration_new");

            entity.ToTable("Registration_CFG");

            entity.Property(e => e.RegistrationId).HasColumnName("Registration_ID");
            entity.Property(e => e.PersonIdFk).HasColumnName("Person_ID_FK");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Registration_date");
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
            entity.Property(e => e.TaskParent)
                .HasDefaultValue(1)
                .HasColumnName("Task_parent");
            entity.Property(e => e.TaskPlatue)
                .HasDefaultValue((short)1)
                .HasColumnName("Task_platue");
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
            entity.Property(e => e.TaskTag)
                .HasMaxLength(1024)
                .HasDefaultValue("tag")
                .HasColumnName("Task_tag");
            entity.Property(e => e.TaskType)
                .HasMaxLength(4)
                .HasDefaultValue("time")
                .IsFixedLength()
                .HasColumnName("Task_type");
            entity.Property(e => e.TaskUrl).HasColumnName("Task_URL");
        });

        modelBuilder.Entity<TempLascaux>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("temp_Lascaux");

            entity.Property(e => e.A).HasColumnName("A#");
            entity.Property(e => e.Action)
                .HasMaxLength(16)
                .HasColumnName("action");
            entity.Property(e => e.ActionDesc)
                .HasMaxLength(128)
                .HasColumnName("actionDesc");
            entity.Property(e => e.ActionUrl)
                .HasMaxLength(128)
                .HasColumnName("actionURL");
            entity.Property(e => e.N).HasColumnName("N#");
            entity.Property(e => e.NovaDesc)
                .HasMaxLength(128)
                .HasColumnName("novaDesc");
            entity.Property(e => e.NovaIn)
                .HasMaxLength(4)
                .HasColumnName("NOVA_in");
            entity.Property(e => e.O).HasColumnName("O#");
            entity.Property(e => e.Object)
                .HasMaxLength(16)
                .HasColumnName("object");
            entity.Property(e => e.ObjectDesc)
                .HasMaxLength(128)
                .HasColumnName("objectDesc");
            entity.Property(e => e.ObjectUrl)
                .HasMaxLength(128)
                .HasColumnName("objectURL");
            entity.Property(e => e.P).HasColumnName("P#");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.S).HasColumnName("S#");
            entity.Property(e => e.Subject)
                .HasMaxLength(16)
                .HasColumnName("subject");
            entity.Property(e => e.SubjectDesc)
                .HasMaxLength(128)
                .HasColumnName("subjectDesc");
            entity.Property(e => e.SujectUrl)
                .HasMaxLength(128)
                .HasColumnName("sujectURL");
        });

        modelBuilder.Entity<TimeSlot>(entity =>
        {
            entity.HasKey(e => new { e.IdPersonFk, e.TimeCalendarWeekId, e.TimeDay, e.TimeTimeslot, e.TimeOffset }).HasName("PK_Time_Slot_1");

            entity.ToTable("Time_Slot");

            entity.Property(e => e.IdPersonFk).HasColumnName("ID_Person_FK");
            entity.Property(e => e.TimeCalendarWeekId).HasColumnName("Time_Calendar_Week_ID");
            entity.Property(e => e.TimeDay).HasColumnName("Time_Day");
            entity.Property(e => e.TimeTimeslot).HasColumnName("Time_Timeslot");
            entity.Property(e => e.TimeOffset).HasColumnName("Time_Offset");
            entity.Property(e => e.MemoIdFk)
                .HasDefaultValue(1)
                .HasColumnName("Memo_ID_FK");
            entity.Property(e => e.NovaIdFk)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.PodIdFk)
                .HasDefaultValue(1)
                .HasColumnName("POD_ID_FK");
            entity.Property(e => e.TimeAlarmOffset).HasColumnName("Time_Alarm_offset");
            entity.Property(e => e.TimeObject).HasColumnName("Time_Object");
            entity.Property(e => e.TimeObjectFactor)
                .HasDefaultValue(1.0)
                .HasColumnName("Time_Object_Factor");
            entity.Property(e => e.TimePriority).HasColumnName("Time_Priority");
            entity.Property(e => e.TimeStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Time_Status");
        });

        modelBuilder.Entity<Tuner>(entity =>
        {
            entity.HasKey(e => new { e.TunerChannel, e.TuberPushbtn });

            entity.ToTable("Tuner");

            entity.Property(e => e.TunerChannel)
                .HasMaxLength(4)
                .HasDefaultValue("ABCD")
                .IsFixedLength()
                .HasColumnName("Tuner_channel");
            entity.Property(e => e.TuberPushbtn)
                .HasDefaultValue((byte)1)
                .HasColumnName("Tuber_pushbtn");
            entity.Property(e => e.TaskIdFk)
                .HasDefaultValue(1)
                .HasColumnName("TASK_ID_FK");
            entity.Property(e => e.TunerLabel)
                .HasMaxLength(16)
                .HasDefaultValue("label 16")
                .IsFixedLength()
                .HasColumnName("Tuner_label");
            entity.Property(e => e.TunerType)
                .HasMaxLength(4)
                .HasDefaultValue("tune")
                .IsFixedLength()
                .HasColumnName("Tuner_type");
            entity.Property(e => e.UrlIdFk)
                .HasDefaultValue(1)
                .HasColumnName("URL_ID_FK");
        });

        modelBuilder.Entity<Url>(entity =>
        {
            entity.HasKey(e => e.UrlId).HasName("PK_URLid");

            entity.ToTable("URL");

            entity.Property(e => e.UrlId).HasColumnName("URL_ID");
            entity.Property(e => e.NovaIdFk)
                .HasDefaultValue(1)
                .HasColumnName("NOVA_ID_FK");
            entity.Property(e => e.UrlAdvanceLevel)
                .HasDefaultValue((byte)3)
                .HasColumnName("URL_advance_level");
            entity.Property(e => e.UrlChain)
                .HasDefaultValue(1)
                .HasColumnName("URL_chain");
            entity.Property(e => e.UrlCloud)
                .HasMaxLength(128)
                .HasDefaultValue("https://agineuralnet.blob.core.windows.net/cards/ImageName.png")
                .HasColumnName("URL_cloud");
            entity.Property(e => e.UrlDescription)
                .HasMaxLength(128)
                .HasDefaultValue("URL description")
                .HasColumnName("URL_Description");
            entity.Property(e => e.UrlLabel)
                .HasMaxLength(16)
                .HasDefaultValue("label16")
                .IsFixedLength()
                .HasColumnName("URL_label");
            entity.Property(e => e.UrlStars)
                .HasDefaultValue((byte)3)
                .HasColumnName("URL_stars");
            entity.Property(e => e.UrlStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("URL_status");
            entity.Property(e => e.UrlTag)
                .HasMaxLength(1024)
                .HasDefaultValue("tag")
                .HasColumnName("URL_tag");
            entity.Property(e => e.UrlType)
                .HasMaxLength(4)
                .HasDefaultValue("html")
                .IsFixedLength()
                .HasColumnName("URL_type");
        });

        modelBuilder.Entity<Verb>(entity =>
        {
            entity.HasKey(e => e.VerbId).HasName("PK_Verb_1");

            entity.ToTable("Verb");

            entity.Property(e => e.VerbId).HasColumnName("Verb_ID");
            entity.Property(e => e.PodIdFk)
                .HasDefaultValue(3)
                .HasColumnName("POD_ID_FK");
            entity.Property(e => e.UrlIdPk)
                .HasDefaultValue(1)
                .HasColumnName("URL_ID_PK");
            entity.Property(e => e.VerbDescription)
                .HasMaxLength(255)
                .HasDefaultValue("Description of Verb")
                .HasColumnName("Verb_description");
            entity.Property(e => e.VerbIdFkCommon)
                .HasDefaultValue(1)
                .HasColumnName("Verb_ID_FK_common");
            entity.Property(e => e.VerbLabel)
                .HasMaxLength(16)
                .HasDefaultValue("label 16")
                .IsFixedLength()
                .HasColumnName("Verb_label");
            entity.Property(e => e.VerbStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Verb_status");
            entity.Property(e => e.VerbTag)
                .HasMaxLength(1024)
                .HasDefaultValue("tag")
                .HasColumnName("Verb_tag");
            entity.Property(e => e.VerbType)
                .HasMaxLength(4)
                .HasDefaultValue("base")
                .IsFixedLength()
                .HasColumnName("Verb_type");
        });

        modelBuilder.Entity<VerbArchive>(entity =>
        {
            entity.HasKey(e => e.VerbId).HasName("PK_Verb");

            entity.ToTable("Verb_archive");

            entity.Property(e => e.VerbId).HasColumnName("Verb_ID");
            entity.Property(e => e.PodIdFk)
                .HasDefaultValue(3)
                .HasColumnName("POD_ID_FK");
            entity.Property(e => e.UrlIdPk)
                .HasDefaultValue(1)
                .HasColumnName("URL_ID_PK");
            entity.Property(e => e.VerbCategory)
                .HasDefaultValue(1)
                .HasColumnName("Verb_category");
            entity.Property(e => e.VerbDescription)
                .HasMaxLength(255)
                .HasDefaultValue("Description of Verb")
                .HasColumnName("Verb_description");
            entity.Property(e => e.VerbLabel)
                .HasMaxLength(16)
                .HasDefaultValue("label 16")
                .IsFixedLength()
                .HasColumnName("Verb_label");
            entity.Property(e => e.VerbStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Verb_status");
            entity.Property(e => e.VerbTag)
                .HasMaxLength(1024)
                .HasDefaultValue("tag")
                .HasColumnName("Verb_tag");
            entity.Property(e => e.VerbType)
                .HasMaxLength(4)
                .HasDefaultValue("base")
                .IsFixedLength()
                .HasColumnName("Verb_type");
        });

        modelBuilder.Entity<VerbTest>(entity =>
        {
            entity.HasKey(e => e.VerbId).HasName("PK_Verb_tag");

            entity.ToTable("Verb_TEST");

            entity.Property(e => e.VerbId).HasColumnName("Verb_ID");
            entity.Property(e => e.PodIdFk)
                .HasDefaultValue(4)
                .HasColumnName("POD_ID_FK");
            entity.Property(e => e.UrlIdPk)
                .HasDefaultValue(1)
                .HasColumnName("URL_ID_PK");
            entity.Property(e => e.VerbDescription)
                .HasMaxLength(255)
                .HasDefaultValue("Description of Verb")
                .HasColumnName("Verb_description");
            entity.Property(e => e.VerbLabel)
                .HasMaxLength(16)
                .HasDefaultValue("label 16")
                .IsFixedLength()
                .HasColumnName("Verb_label");
            entity.Property(e => e.VerbStatus)
                .HasMaxLength(1)
                .HasDefaultValue("A")
                .IsFixedLength()
                .HasColumnName("Verb_status");
            entity.Property(e => e.VerbTag)
                .HasMaxLength(1024)
                .HasDefaultValue("tag")
                .HasColumnName("Verb_tag");
            entity.Property(e => e.VerbType)
                .HasMaxLength(4)
                .HasDefaultValue("base")
                .IsFixedLength()
                .HasColumnName("Verb_type");
        });

        modelBuilder.Entity<View1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_1");

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
            entity.Property(e => e.PypeSeq).HasColumnName("Pype_seq");
            entity.Property(e => e.PypeStatus)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("Pype_status");
            entity.Property(e => e.PypeType)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("Pype_type");
        });

       modelBuilder.Entity<Work>(entity =>
        {
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
            entity.Property(e => e.WorkTag)
                .HasMaxLength(1024)
                .HasDefaultValue("tag")
                .HasColumnName("Work_tag");
            entity.Property(e => e.WorkType)
                .HasMaxLength(4)
                .HasDefaultValue("task")
                .IsFixedLength()
                .HasColumnName("Work_type");
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
            entity.Property(e => e.NovaDescription).HasColumnName("novaDescription");
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
