using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using QuizServices.ViewModels;

namespace QuizServices.Models
{
    public partial class QuizContext : DbContext
    {
        //public QuizContext()
        //{
        //}

        public QuizContext(DbContextOptions<QuizContext> options)
            : base(options)
        {
        }

        public virtual DbSet<QuizAccounts> QuizAccounts { get; set; }
        public virtual DbSet<QuizClasses> QuizClasses { get; set; }
        //public virtual DbSet<QuizMaster> QuizMaster { get; set; }
        public virtual DbSet<QuizQuestions> QuizQuestions { get; set; }
        public virtual DbSet<QuizQuestionsOptions> QuizQuestionsOptions { get; set; }
        public virtual DbSet<QuizQuestionsTypes> QuizQuestionsTypes { get; set; }
        public virtual DbSet<QuizResultDetails> QuizResultDetails { get; set; }
        public virtual DbSet<QuizResultMaster> QuizResultMaster { get; set; }
        public virtual DbSet<QuizSubjects> QuizSubjects { get; set; }
        public virtual DbSet<QuizUsers> QuizUsers { get; set; }

        public virtual DbSet<AvailaleClassAndSubjects> QuestionAvailaleClassAndSubjects { get; set; }
        public virtual DbSet<ClassSubject> ClassSubject { get; set; }

        public virtual DbSet<Question> Question { get; set; }

        // Unable to generate entity type for table 'dbo.Quiz_Classes_Subject'. Please see the warning messages.

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //        optionsBuilder.UseSqlServer("Server=TSLC0750\\SQLEXPRESS;Database=Quiz;Integrated Security=True;user id=sa;password=maa@1234;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuizAccounts>(entity =>
            {
                entity.ToTable("Quiz_Accounts");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AccountName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OfficeName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Website)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QuizClasses>(entity =>
            {
                entity.ToTable("Quiz_Classes");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            //modelBuilder.Entity<QuizMaster>(entity =>
            //{
            //    entity.ToTable("Quiz_Master");

            //    entity.Property(e => e.Id).HasColumnName("ID");

            //    entity.Property(e => e.Description).IsUnicode(false);

            //    entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            //    entity.Property(e => e.Name)
            //        .HasMaxLength(100)
            //        .IsUnicode(false);
            //});

            modelBuilder.Entity<QuizQuestions>(entity =>
            {
                entity.ToTable("Quiz_Questions");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClassSubjectId).HasColumnName("ClassSubjectID");

                entity.Property(e => e.CorrectAnswerOptionId).HasColumnName("CorrectAnswerOptionID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.QuestionTypeId)
                    .HasColumnName("QuestionTypeID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AccountId).HasColumnName("AccountId");
            });

            modelBuilder.Entity<QuizQuestionsOptions>(entity =>
            {
                entity.ToTable("Quiz_Questions_Options");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Label)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            });

            modelBuilder.Entity<QuizQuestionsTypes>(entity =>
            {
                entity.ToTable("Quiz_Questions_Types");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<QuizResultDetails>(entity =>
            {
                entity.ToTable("Quiz_Result_Details");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.OptionId).HasColumnName("OptionID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.QuizResultMasterId).HasColumnName("QuizResultMasterID");
            });

            modelBuilder.Entity<QuizResultMaster>(entity =>
            {
                entity.ToTable("Quiz_Result_Master");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClassSubjectId).HasColumnName("ClassSubjectID");

                entity.Property(e => e.QuizAppearDate).HasColumnType("datetime");

                entity.Property(e => e.QuizMasterId).HasColumnName("QuizMasterID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<QuizSubjects>(entity =>
            {
                entity.ToTable("Quiz_Subjects");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<QuizUsers>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Quiz_Users");

                entity.HasIndex(e => e.UserName)
                    .HasName("IX_QuizUser")
                    .IsUnique();

                entity.Property(e => e.AccessLevel).HasDefaultValueSql("((100))");

                entity.Property(e => e.AllowLogin)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.Salt)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword).HasMaxLength(255);
            });
        }
    }
}
