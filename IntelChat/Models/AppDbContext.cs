using System;
using System.Collections.Generic;
using IntelChat.Models;
using Microsoft.EntityFrameworkCore;

namespace IntelChat.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

   //HS public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<CatMajorSet> CatMajorSets { get; set; }

    public virtual DbSet<LpElement> LpElements { get; set; }

    public virtual DbSet<LpMajor> LpMajors { get; set; }

    public virtual DbSet<LpMinor> LpMinors { get; set; }

    public virtual DbSet<MajorElem> MajorElems { get; set; }
    
    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<ViewSlot> ViewSlots { get; set; }

    public virtual DbSet<ViewTimeslot> ViewTimeslots { get; set; }

    public virtual DbSet<WeekCalendar> WeekCalendars { get; set; }

    public virtual DbSet<WeekMyHistory> WeekMyHistories { get; set; }

    public virtual DbSet<WeekMyTimeslot> WeekMyTimeslots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DefaultConnection");

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   /*
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.ToTable("Brand");

            entity.Property(e => e.ActiveGuideId)
                .HasDefaultValueSql("((7))")
                .HasColumnName("Active_Guide_ID");
            entity.Property(e => e.BrandImage)
                .HasMaxLength(128)
                .HasDefaultValueSql("(N'Blob pointer')")
                .HasColumnName("Brand_Image");
            entity.Property(e => e.BrandLink)
                .HasMaxLength(42)
                .HasDefaultValueSql("(N'Link Users')");
            entity.Property(e => e.BrandName)
                .HasMaxLength(42)
                .HasDefaultValueSql("(N'Registration Title')");
            entity.Property(e => e.ChannelAlpha)
                .HasMaxLength(9)
                .HasDefaultValueSql("(N'alph')")
                .HasComment("type collecter Alpha")
                .HasColumnName("Channel_Alpha");
            entity.Property(e => e.ChannelBeta)
                .HasMaxLength(4)
                .HasDefaultValueSql("(N'beta')")
                .HasColumnName("Channel_Beta");
            entity.Property(e => e.ChannelGamma)
                .HasMaxLength(4)
                .HasDefaultValueSql("(N'gama')")
                .HasColumnName("Channel_Gamma");
            entity.Property(e => e.CntMax)
                .HasDefaultValueSql("((64))")
                .HasColumnName("Cnt_Max");
            entity.Property(e => e.CntReg)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Cnt_Reg");
            entity.Property(e => e.Cost)
                .HasDefaultValueSql("((1))")
                .HasColumnType("money");
            entity.Property(e => e.Eligibility).HasDefaultValueSql("((50))");
            entity.Property(e => e.LocationId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Location_ID");
            entity.Property(e => e.MenuLock)
                .HasMaxLength(4)
                .HasDefaultValueSql("(N'xxxx')")
                .IsFixedLength()
                .HasColumnName("Menu_Lock");
            entity.Property(e => e.PersonId).HasColumnName("Person_ID");
            entity.Property(e => e.ProgramId)
                .HasDefaultValueSql("((7))")
                .HasColumnName("Program_ID");
            entity.Property(e => e.RegDateClosed)
                .HasDefaultValueSql("('2048-4-2')")
                .HasColumnType("datetime")
                .HasColumnName("Reg_Date_Closed");
            entity.Property(e => e.ScopeLock)
                .HasDefaultValueSql("((1))")
                .HasComment("scoping global variable")
                .HasColumnName("Scope_Lock");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValueSql("(N'A')");

            entity.HasOne(d => d.Person).WithMany(p => p.Brands)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK_Brand_Person");
        });
    */
        modelBuilder.Entity<CatMajorSet>(entity =>
        {
            entity.HasKey(e => new { e.CatMajorSet1, e.CatMajor }).HasName("PK_Cat_Major_SetXXX");

            entity.ToTable("Cat_Major_Set");

            entity.Property(e => e.CatMajorSet1).HasColumnName("Cat_Major_Set");
            entity.Property(e => e.CatMajor)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Cat_Major#");
            entity.Property(e => e.CatMajorSetLabel)
                .HasMaxLength(16)
                .HasDefaultValueSql("(N'Collector')")
                .IsFixedLength()
                .HasColumnName("Cat_Major_Set_Label");
            entity.Property(e => e.CatMajorSetSeq)
                .HasDefaultValueSql("((8))")
                .HasColumnName("Cat_Major_Set_seq");
            entity.Property(e => e.CatMajorSetStatus)
                .HasMaxLength(1)
                .HasDefaultValueSql("(N'A')")
                .IsFixedLength()
                .HasColumnName("Cat_Major_Set_status");
            entity.Property(e => e.CatMajorSetType)
                .HasMaxLength(4)
                .HasDefaultValueSql("(N'base')")
                .IsFixedLength()
                .HasColumnName("Cat_Major_Set_type");
        });

        modelBuilder.Entity<LpElement>(entity =>
        {
            entity.HasKey(e => e.LpElemId);

            entity.ToTable("LP_Element");

            entity.Property(e => e.LpElemId).HasColumnName("LP_Elem_ID");
            entity.Property(e => e.LpElemDesc)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("('Description of Minor Element')")
                .HasColumnName("LP_Elem_desc");
            entity.Property(e => e.LpElemDtime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("LP_Elem_Dtime");
            entity.Property(e => e.LpElemLabel)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasDefaultValueSql("('Element Minor')")
                .IsFixedLength()
                .HasColumnName("LP_Elem_label");
            entity.Property(e => e.LpElemStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('A')")
                .IsFixedLength()
                .HasColumnName("LP_Elem_status");
            entity.Property(e => e.LpElemType)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasDefaultValueSql("('elem')")
                .IsFixedLength()
                .HasColumnName("LP_Elem_type");
            entity.Property(e => e.LpMajorId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("LP_Major_ID");

            entity.HasOne(d => d.LpMajor).WithMany(p => p.LpElements)
                .HasForeignKey(d => d.LpMajorId)
                .HasConstraintName("LP_Element_FK");
        });

        modelBuilder.Entity<LpMajor>(entity =>
        {
            entity.HasKey(e => e.LpMajorId).HasName("LP_Major_PK");

            entity.ToTable("LP_Major");

            entity.Property(e => e.LpMajorId).HasColumnName("LP_Major_ID");
            entity.Property(e => e.LpMajorDesc)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("('Major Description')")
                .HasColumnName("LP_Major_desc");
            entity.Property(e => e.LpMajorDtime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("LP_Major_Dtime");
            entity.Property(e => e.LpMajorLabel)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasDefaultValueSql("('Major_Cat_Pend')")
                .IsFixedLength()
                .HasColumnName("LP_Major_label");
            entity.Property(e => e.LpMajorRgb)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasDefaultValueSql("('#FFFF00')")
                .IsFixedLength()
                .HasColumnName("LP_Major_RGB");
            entity.Property(e => e.LpMajorSeq)
                .HasDefaultValueSql("((1))")
                .HasColumnName("LP_Major_seq");
            entity.Property(e => e.LpMajorStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('A')")
                .IsFixedLength()
                .HasColumnName("LP_Major_status");
            entity.Property(e => e.LpMajorType)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasDefaultValueSql("('catm')")
                .IsFixedLength()
                .HasColumnName("LP_Major_type");
            entity.Property(e => e.PersonId).HasColumnName("Person_ID");

            entity.HasOne(d => d.Person).WithMany(p => p.LpMajors)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("LP_Major_FK");
        });

        modelBuilder.Entity<LpMinor>(entity =>
        {
            entity.HasKey(e => e.LpMinorId).HasName("LP_Minor_PK");

            entity.ToTable("LP_Minor");

            entity.Property(e => e.LpMinorId).HasColumnName("LP_Minor_ID");
            entity.Property(e => e.Location).HasDefaultValueSql("((1))");
            entity.Property(e => e.LpMajorId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("LP_Major_ID");
            entity.Property(e => e.LpMinorAvoid)
                .HasDefaultValueSql("((0))")
                .HasColumnName("LP_Minor_avoid");
            entity.Property(e => e.LpMinorDesc)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("('Minor Description')")
                .HasColumnName("LP_Minor_desc");
            entity.Property(e => e.LpMinorDtime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("LP_Minor_Dtime");
            entity.Property(e => e.LpMinorLabel)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasDefaultValueSql("('category minor')")
                .IsFixedLength()
                .HasColumnName("LP_Minor_label");
            entity.Property(e => e.LpMinorPersue)
                .HasDefaultValueSql("((0))")
                .HasColumnName("LP_Minor_persue");
            entity.Property(e => e.LpMinorStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('A')")
                .IsFixedLength()
                .HasColumnName("LP_Minor_status");
            entity.Property(e => e.LpMinorType)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasDefaultValueSql("('base')")
                .IsFixedLength()
                .HasColumnName("LP_Minor_type");

            entity.HasOne(d => d.LpMajor).WithMany(p => p.LpMinors)
                .HasForeignKey(d => d.LpMajorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LP_Minor_FK");
        });

        modelBuilder.Entity<MajorElem>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Major_Elem");

            entity.Property(e => e.LpElemLabel)
                .HasMaxLength(16)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("LP_Elem_label");
            entity.Property(e => e.LpMajorId).HasColumnName("LP_Major_ID");
            entity.Property(e => e.LpMajorLabel)
                .HasMaxLength(16)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("LP_Major_LABEL");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("Person");

            entity.Property(e => e.PersonId).HasColumnName("Person_ID");
            entity.Property(e => e.AccountId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Account_ID");
            entity.Property(e => e.DateEntry)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Date_entry");
            entity.Property(e => e.Host)
                .HasDefaultValueSql("((0))")
                .HasColumnName("host");
            entity.Property(e => e.LocationId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Location_ID");
            entity.Property(e => e.NameFirst)
                .HasMaxLength(32)
                .HasDefaultValueSql("(N'first')")
                .HasColumnName("name_first");
            entity.Property(e => e.NameLast)
                .HasMaxLength(32)
                .HasDefaultValueSql("(N'last')")
                .HasColumnName("name_last");
            entity.Property(e => e.OnLine)
                .HasDefaultValueSql("((0))")
                .HasColumnName("On_Line");
            entity.Property(e => e.Person1)
                .HasMaxLength(450)
                .HasColumnName("Person");
            entity.Property(e => e.PersonPw)
                .HasMaxLength(12)
                .HasDefaultValueSql("('password')")
                .HasColumnName("Person_PW");
            entity.Property(e => e.SsmaTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("SSMA_TimeStamp");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValueSql("('A')")
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(9)
                .HasDefaultValueSql("('user')")
                .HasComment("\"branded role\" to determine home menu person is locked into")
                .HasColumnName("type");
        });

        modelBuilder.Entity<ViewSlot>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_Slot");

            entity.Property(e => e.LpMajorDesc)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LP_Major_desc");
            entity.Property(e => e.LpMajorLabel)
                .HasMaxLength(16)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("LP_Major_label");
            entity.Property(e => e.LpMajorRgb)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("LP_Major_RGB");
            entity.Property(e => e.LpMinorDesc)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LP_Minor_desc");
            entity.Property(e => e.LpMinorLabel)
                .HasMaxLength(16)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("LP_Minor_label");
            entity.Property(e => e.PersonId).HasColumnName("Person_ID");
            entity.Property(e => e.WeekActualFactor).HasColumnName("Week_Actual_Factor");
            entity.Property(e => e.WeekAlarmOffset).HasColumnName("Week_Alarm_offset");
            entity.Property(e => e.WeekCalendarWeekId).HasColumnName("Week_Calendar_Week_ID");
            entity.Property(e => e.WeekCatMinorId).HasColumnName("Week_Cat_Minor_ID");
            entity.Property(e => e.WeekDay).HasColumnName("Week_Day");
            entity.Property(e => e.WeekDuration).HasColumnName("Week_Duration");
            entity.Property(e => e.WeekOffset).HasColumnName("Week_Offset");
            entity.Property(e => e.WeekPriority).HasColumnName("Week_Priority");
            entity.Property(e => e.WeekStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Week_Status");
            entity.Property(e => e.WeekTimeslot).HasColumnName("Week_Timeslot");
        });

        modelBuilder.Entity<ViewTimeslot>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_Timeslot");

            entity.Property(e => e.Expr1)
                .HasMaxLength(16)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LpElemLabel)
                .HasMaxLength(16)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("LP_Elem_label");
            entity.Property(e => e.LpMajorId).HasColumnName("LP_Major_ID");
            entity.Property(e => e.LpMajorLabel)
                .HasMaxLength(16)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("LP_Major_LABEL");
            entity.Property(e => e.LpMinorId).HasColumnName("LP_Minor_ID");
            entity.Property(e => e.LpMinorLabel)
                .HasMaxLength(16)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("LP_Minor_label");
        });

        modelBuilder.Entity<WeekCalendar>(entity =>
        {
            entity.HasKey(e => e.WeekCalendarWeekId).HasName("Week_Calendar_PK");

            entity.ToTable("Week_Calendar");

            entity.Property(e => e.WeekCalendarWeekId).HasColumnName("Week_Calendar_Week_ID");
            entity.Property(e => e.WeekCalendarDate)
                .HasColumnType("date")
                .HasColumnName("Week_Calendar_Date");
        });

        modelBuilder.Entity<WeekMyHistory>(entity =>
        {
            entity.HasKey(e => new { e.WeekCalendarWeekId, e.PersonId, e.WeekCatMinorId });

            entity.ToTable("Week_My_History");

            entity.Property(e => e.WeekCalendarWeekId).HasColumnName("Week_Calendar_Week_ID");
            entity.Property(e => e.PersonId)
                .HasDefaultValueSql("((2))")
                .HasColumnName("Person_ID");
            entity.Property(e => e.WeekCatMinorId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Week_Cat_Minor_ID");
            entity.Property(e => e.WeekWatchElem1)
                .HasDefaultValueSql("((4))")
                .HasColumnName("Week_Watch_Elem_1");
            entity.Property(e => e.WeekWatchElem2)
                .HasDefaultValueSql("((4))")
                .HasColumnName("Week_Watch_Elem_2");
            entity.Property(e => e.WeekWatchElem3)
                .HasDefaultValueSql("((4))")
                .HasColumnName("Week_Watch_Elem_3");
            entity.Property(e => e.WeekWatchElem4)
                .HasDefaultValueSql("((4))")
                .HasColumnName("Week_Watch_Elem_4");
            entity.Property(e => e.WeekWatchElem5)
                .HasDefaultValueSql("((4))")
                .HasColumnName("Week_Watch_Elem_5");

            entity.HasOne(d => d.Person).WithMany(p => p.WeekMyHistories)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Week_My_History_FK");

            entity.HasOne(d => d.WeekCalendarWeek).WithMany(p => p.WeekMyHistories)
                .HasForeignKey(d => d.WeekCalendarWeekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Week_My_History_FK2");

            entity.HasOne(d => d.WeekCatMinor).WithMany(p => p.WeekMyHistories)
                .HasForeignKey(d => d.WeekCatMinorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Week_My_History_LP_Minor");
        });

        modelBuilder.Entity<WeekMyTimeslot>(entity =>
        {
            entity.HasKey(e => new { e.PersonId, e.WeekCalendarWeekId, e.WeekDay, e.WeekTimeslot, e.WeekOffset, e.WeekCatMinorId });

            entity.ToTable("Week_My_Timeslots");

            entity.Property(e => e.PersonId)
                .HasDefaultValueSql("((2))")
                .HasColumnName("Person_ID");
            entity.Property(e => e.WeekCalendarWeekId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Week_Calendar_Week_ID");
            entity.Property(e => e.WeekDay)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Week_Day");
            entity.Property(e => e.WeekTimeslot)
                .HasDefaultValueSql("((25))")
                .HasColumnName("Week_Timeslot");
            entity.Property(e => e.WeekOffset).HasColumnName("Week_Offset");
            entity.Property(e => e.WeekCatMinorId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("Week_Cat_Minor_ID");
            entity.Property(e => e.WeekActualFactor)
                .HasDefaultValueSql("((1.0))")
                .HasColumnName("Week_Actual_Factor");
            entity.Property(e => e.WeekAlarmOffset)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Week_Alarm_offset");
            entity.Property(e => e.WeekDuration)
                .HasDefaultValueSql("((30))")
                .HasColumnName("Week_Duration");
            entity.Property(e => e.WeekPriority)
                .HasDefaultValueSql("((4))")
                .HasColumnName("Week_Priority");
            entity.Property(e => e.WeekStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('A')")
                .IsFixedLength()
                .HasColumnName("Week_Status");

            entity.HasOne(d => d.Person).WithMany(p => p.WeekMyTimeslots)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Week_My_Timeslots_Week_My_Timeslots");

            entity.HasOne(d => d.WeekCalendarWeek).WithMany(p => p.WeekMyTimeslots)
                .HasForeignKey(d => d.WeekCalendarWeekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Week_My_Timeslots_FK");

            entity.HasOne(d => d.WeekCatMinor).WithMany(p => p.WeekMyTimeslots)
                .HasForeignKey(d => d.WeekCatMinorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Week_My_Timeslots_Week_My_Timeslots1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
