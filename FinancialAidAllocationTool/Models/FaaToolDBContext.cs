using Microsoft.EntityFrameworkCore;
using FinancialAidAllocationTool.Models.Application;
using FinancialAidAllocationTool.Models.Policy;
using FinancialAidAllocationTool.Models.Ledger;
using FinancialAidAllocationTool.Models.IntakeSeason;
using Z.EntityFramework.Plus;


namespace FinancialAidAllocationTool.Models
{
    public partial class FaaToolDBContext : DbContext
    {
        public FaaToolDBContext()
        {
        }

        public FaaToolDBContext(DbContextOptions<FaaToolDBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<FaatFiles> FaatFiles { get; set; }

        public virtual DbSet<FaatAppParentDetail> FaatAppParentDetail { get; set; }
        public virtual DbSet<FaatAppResidenceInfo> FaatAppResidenceInfo { get; set; }
        public virtual DbSet<FaatAppSibJobHolder> FaatAppSibJobHolder { get; set; }
        public virtual DbSet<FaatAppSibStudent> FaatAppSibStudent { get; set; }
        public virtual DbSet<FaatAppSiblingInfo> FaatAppSiblingInfo { get; set; }
        public virtual DbSet<FaatAppStudentDetail> FaatAppStudentDetail { get; set; }
        public virtual DbSet<FaatApplication> FaatApplication { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<FaatPolicy> FaatPolicy { get; set; }
        public virtual DbSet<FaatRule> FaatRule { get; set; }
        public virtual DbSet<FaatRuleDescription> FaatRuleDescription { get; set; }

        public virtual DbSet<DonationLedger> DonationLedger { get; set; }
        public virtual DbSet<FaatClassDefinition> FaatClassDefinition { get; set; }
        public virtual DbSet<FaatScholarLog> FaatScholarLog { get; set; }
        public virtual DbSet<FaatScholarLedger> FaatScholarLedger { get; set; }
        public virtual DbSet<FaatIntakeSeason> FaatIntakeSeason{get;set;}

        public virtual DbSet<FaatScholarshipStatus> FaatScholarshipStatus{get;set;}
      
        public virtual DbSet<FaatAppComments> FaatAppComments { get; set; }
        public virtual DbSet<FaatAppGuardianOtherIncomeResourceFiles> FaatAppGuardianOtherIncomeResourceFiles  { get; set; }
        public virtual DbSet<FaatAppMotherOtherIncomeResourceFiles> FaatAppMotherOtherIncomeResourceFiles  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  modelBuilder.Entity<DonationLedger>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK_Faat_Donation_Ledger");

                entity.ToTable("Donation_Ledger");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.Memo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDate).HasColumnType("smalldatetime");
            });

            modelBuilder.Entity<FaatClassDefinition>(entity =>
            {
                entity.ToTable("Faat_Class_Definition");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Discipline)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Section)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Semester)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SemesterCount)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<FaatScholarLog>(entity =>
            {
                entity.ToTable("FAAT_ScholarLog");

                entity.Property(e => e.Id)
                    .HasColumnName("ID");

                entity.Property(e => e.AridNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                
                entity.Property(e => e.Cgpa).HasColumnName("CGPA");

                entity.Property(e => e.IsManual).HasColumnName("Is_Manual");
                
                entity.Property(e => e.AllocationAmount).HasColumnName("Allocation_Amount");
                entity.Property(e => e.DefaultAmount).HasColumnName("Default_Amount");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsertionTimestamp)
                    .HasColumnName("Insertion_Timestamp")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateTimestamp)
                    .HasColumnName("Update_Timestamp")
                    .HasColumnType("datetime");
                entity.Property(e => e.Tid).HasColumnName("Tid");

                entity.HasOne(d => d.T)
                    .WithMany(p => p.FaatScholarLog)
                    .HasForeignKey(d => d.Tid)
                    .HasConstraintName("FK__FAAT_Studen__Tid__72910220");
                
                entity.Property(e => e.ApplicationId).HasColumnName("Application_ID");

                entity.HasOne(d => d.A)
                    .WithMany(p => p.FaatScholarLog)
                    .HasForeignKey(d => d.ApplicationId)
                    .HasConstraintName("FK_Application");

                entity.Property(e => e.ClassId).HasColumnName("Class_ID");

                entity.HasOne(d => d.CD)
                    .WithMany(p => p.FaatScholarLog)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Class_Definition");
                
                //entity.HasAlternateKey(c => new [] {c.AridNo, c.Type, c.ClassId}).IsUnicode()
                //.HasName("UC_Scholar");

            });

            modelBuilder.Entity<FaatScholarLedger>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK_Faat_Student_Ledger");

                entity.ToTable("Faat_Scholar_Ledger");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.Memo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDate).HasColumnType("smalldatetime");
            });

            
            modelBuilder.Entity<FaatAppParentDetail>(entity =>
            {
                entity.ToTable("FAAT_APP_Parent_Detail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApplicationId).HasColumnName("Application_ID");

                entity.Property(e => e.FatherIsAlive).HasColumnName("Father_Is_Alive");
                 entity.Property(e => e.FatherCNICDeathCertificateFileName).HasColumnName("Father_CNIC_DeathCertificate_File_Name");
                entity.Property(e => e.FatherCNICDeathCertificateFileType).HasColumnName("Father_CNIC_DeathCertificate_File_Type");
                entity.Property(e => e.FatherCNICDeathCertificateFileData).HasColumnName("Father_CNIC_DeathCertificate_File_Data");
                entity.Property(e => e.MotherCNICDeathCertificateFileName).HasColumnName("Mother_CNIC_DeathCertificate_File_Name");
                entity.Property(e => e.MotherCNICDeathCertificateFileType).HasColumnName("Mother_CNIC_DeathCertificate_File_Type");
                entity.Property(e => e.MotherCNICDeathCertificateFileData).HasColumnName("Mother_CNIC_DeathCertificate_File_Data");
                
                entity.Property(e => e.FinancingPerson)
                    .HasColumnName("Financing_Person")
                    .HasMaxLength(128)
                    .IsUnicode(false);
                
                entity.Property(e => e.GCompany)
                    .HasColumnName("G_Company")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.GDesignation)
                    .HasColumnName("G_Designation")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.GEmailAddress)
                    .HasColumnName("G_Email_Address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GMonthlyIncome).HasColumnName("G_Monthly_Income");

                entity.Property(e => e.GName)
                    .HasColumnName("G_Name")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.GOccupation)
                    .HasColumnName("G_Occupation")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.GOfficeAddress)
                    .HasColumnName("G_Office_Address")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.GOfficeTelNo)
                    .HasColumnName("G_Office_Tel_No")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.MCompany)
                    .HasColumnName("M_Company")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MDesignation)
                    .HasColumnName("M_Designation")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MEmailAddress)
                    .HasColumnName("M_Email_Address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MMonthlyIncome).HasColumnName("M_Monthly_Income");

                entity.Property(e => e.MName)
                    .HasColumnName("M_Name")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MOccupation)
                    .HasColumnName("M_Occupation")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MOfficeAddress)
                    .HasColumnName("M_Office_Address")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.MOfficeTelNo)
                    .HasColumnName("M_Office_Tel_No")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.MotherIsAlive).HasColumnName("Mother_Is_Alive");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.FaatAppParentDetail)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FAAT_APP_Parent_Detail");
            });

            modelBuilder.Entity<FaatAppResidenceInfo>(entity =>
            {
                entity.ToTable("FAAT_APP_Residence_Info");

                entity.Property(e => e.Id).HasColumnName("ID");

                 entity.Property(e => e.ResidenceInfoFileName).HasColumnName("Residence_Info_File_Name");
                entity.Property(e => e.ResidenceInfoFileType).HasColumnName("Residence_Info_File_Type");
                entity.Property(e => e.ResidenceInfoFileData).HasColumnName("Residence_Info_File_Data");
                entity.Property(e => e.Address)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicationId).HasColumnName("Application_ID");

                entity.Property(e => e.Mode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasColumnName("Phone_No")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.FaatAppResidenceInfo)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FAAT_APP_Residence_Info");
            });

            modelBuilder.Entity<FaatAppSibJobHolder>(entity =>
            {
                entity.ToTable("FAAT_APP_Sib_Job_Holder");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.ContractFileName).HasColumnName("Contract_File_Name");
                entity.Property(e => e.ContractFileType).HasColumnName("Contract_File_Type");
                entity.Property(e => e.ContractFileData).HasColumnName("Contract_File_Data");
                entity.Property(e => e.Company)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Designation)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.MonthlyIncome).HasColumnName("Monthly_Income");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.SiblingId).HasColumnName("Sibling_ID");

                entity.HasOne(d => d.Sibling)
                    .WithMany(p => p.FaatAppSibJobHolder)
                    .HasForeignKey(d => d.SiblingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FAAT_APP_Sib_Job_Holder");
            });

            modelBuilder.Entity<FaatAppSibStudent>(entity =>
            {
                entity.ToTable("FAAT_APP_Sib_Student");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.StdentCardFileName).HasColumnName("Stdent_Card_File_Name");
                entity.Property(e => e.StdentCardFileType).HasColumnName("Stdent_Card_File_Type");
                entity.Property(e => e.StdentCardFileData).HasColumnName("Stdent_Card_File_Data");
               
                entity.Property(e => e.ClassInstituteName)
                    .HasColumnName("Class_Institute_Name")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.SiblingId).HasColumnName("Sibling_ID");

                entity.HasOne(d => d.Sibling)
                    .WithMany(p => p.FaatAppSibStudent)
                    .HasForeignKey(d => d.SiblingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FAAT_APP_Sib_Student");
            });

            modelBuilder.Entity<FaatAppSiblingInfo>(entity =>
            {
                entity.ToTable("FAAT_APP_Sibling_Info");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApplicationId).HasColumnName("Application_ID");

                entity.Property(e => e.NoOfSibling).HasColumnName("No_Of_Sibling");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.FaatAppSiblingInfo)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FAAT_APP_Sibling_Info");
            });

            modelBuilder.Entity<FaatAppStudentDetail>(entity =>
            {
                entity.ToTable("FAAT_APP_Student_Detail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApplicationId).HasColumnName("Application_ID");

                entity.Property(e => e.AridNo)
                    .HasColumnName("Arid_No")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Class)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cpga).HasColumnName("CPGA");

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("Email_Address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNo)
                    .HasColumnName("Mobile_No")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ReasonToApply)
                    .HasColumnName("Reason_To_Apply")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Residence)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Section)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                    
                    entity.Property(e=>e.Semester)
                    .HasMaxLength(25)
                    .IsUnicode(false);

               entity.HasOne(d => d.Application)
                    .WithMany(p => p.FaatAppStudentDetail)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FAAT_APP_Student_Detail");
            });
             modelBuilder.Entity<FaatFiles>(entity =>
            {
                entity.ToTable("FAAT_Files");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ApplicationId).HasColumnName("Application_ID");
                entity.Property(e => e.FileName).HasColumnName("File_Name");
                entity.Property(e => e.FileType).HasColumnName("File_Type");
                entity.Property(e => e.FileData).HasColumnName("File_Data");

                    entity.HasOne(d => d.Application)
                    .WithMany(p => p.FaatFiles)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FAAT_File__Appli__0E04126B");
      
                   
            }
            );
            modelBuilder.Entity<FaatAppComments>(entity =>
            {
                entity.ToTable("FAAT_APP_Comments");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApplicationId).HasColumnName("[Application_ID]");
                entity.Property(e => e.ClassID).HasColumnName("Class_ID");

                entity.Property(e => e.Comments).IsUnicode(false);
                entity.Property(e=>e.Amount).HasColumnName("Amount");

                entity.Property(e => e.UserId).HasColumnName("[User_ID]");
                entity.HasOne(d=>d.Application)
                .WithMany(p=>p.FaatAppComments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FAAT_APP___Appli__220B0B18");

                 entity.HasOne(p=>p.Users)
                .WithMany(p=>p.FaatAppComments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FAAT_APP___Userl__2116E6DF");

                entity.HasOne(e=>e.ClassDefinition)
                .WithMany(e=>e.FaatAppComments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassID");
                 
                 
                
                 
                 
            }
            );
            modelBuilder.Entity<FaatApplication>(entity =>
            {
                entity.ToTable("FAAT_Application");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.ClassId).HasColumnName("Class_id");
                entity.Property(e=>e.status).HasColumnName("status");
                
                entity.Property(e => e.ApplicationData).HasColumnName("Application_Data");
                entity.Property(e => e.UserImageFileName).HasColumnName("User_Image_FileName");
                entity.Property(e => e.UserImageFileType).HasColumnName("User_Image_FileType");
                entity.Property(e => e.UserImage).HasColumnName("User_Image");
                
                 entity.Property(e => e.AridNo)
                     .HasColumnName("AridNo")
                     .HasMaxLength(50)
                     .IsUnicode(false);

                 entity.Property(e => e.CGPA).HasColumnName("CGPA");
                //     .HasMaxLength(100)
                //     .IsUnicode(false);

                entity.Property(e => e.InsertionTimestamp)
                    .HasColumnName("Insertion_Timestamp")
                    .HasColumnType("datetime");

                 entity.Property(e => e.Name)
                    .HasColumnName("Name")
                     .HasMaxLength(50)
                     .IsUnicode(false);

                 //entity.Property(e => e.Section)
                 //    .HasMaxLength(50)
                //     .IsUnicode(false);

                // entity.Property(e => e.Status)
                //     .IsRequired()
                //     .HasMaxLength(15)
                //     .IsUnicode(false);

                entity.Property(e => e.UpdateTimestamp)
                    .HasColumnName("Update_Timestamp")
                    .HasColumnType("datetime");

                //entity.Property(e => e.UserId).HasColumnName("User_ID");

                // entity.HasOne(d => d.User)
                //     .WithMany(p => p.FaatApplication)
                //     .HasForeignKey(d => d.UserId)
                //     .HasConstraintName("FK_Users");

                entity.HasOne(d => d.FaatClassDefinition)
                    .WithMany(p => p.FaatApplications)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__FAAT_Appl__Class__5B438874");
            });

          modelBuilder.Entity<FaatAppGuardianOtherIncomeResourceFiles>(entity =>
            {
                entity.ToTable("FAAT_APP_Guardian_Other_Income_Resource_Files");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.FaatAppParentDetailId).HasColumnName("FAAT_APP_Parent_Detail_id");
                entity.Property(e => e.FileName).HasColumnName("File_Name");
                entity.Property(e => e.FileType).HasColumnName("File_Type");
                entity.Property(e => e.FileData).HasColumnName("File_Data");
                entity.Property(e => e.ResourceType).HasColumnName("Resource_Type");
                entity.Property(e => e.Income).HasColumnName("Income");

                    entity.HasOne(d => d.FaatAppParentDetail)
                    .WithMany(p => p.FaatAppGuardianOtherIncomeResourceFiles)
                    .HasForeignKey(d => d.FaatAppParentDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FAAT_APP___FAAT___473C8FC7");
      
                   
            }
            );
            modelBuilder.Entity<FaatAppMotherOtherIncomeResourceFiles>(entity =>
            {
                entity.ToTable("FAAT_APP_Mother_Other_Income_Resource_Files");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.FaatAppParentDetailId).HasColumnName("FAAT_APP_Parent_Detail_id");
                entity.Property(e => e.FileName).HasColumnName("File_Name");
                entity.Property(e => e.FileType).HasColumnName("File_Type");
                entity.Property(e => e.FileData).HasColumnName("File_Data");
                entity.Property(e => e.ResourceType).HasColumnName("Resource_Type");
                entity.Property(e => e.Income).HasColumnName("Income");

                    entity.HasOne(d => d.FaatAppParentDetail)
                    .WithMany(p => p.FaatAppMotherOtherIncomeResourceFiles)
                    .HasForeignKey(d => d.FaatAppParentDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FAAT_APP___FAAT___4A18FC72");
      
                   
            }
            );

            
            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AridNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e=>e.Name).HasColumnName("Name");
            });

           

           modelBuilder.Entity<FaatPolicy>(entity =>
            {
                entity.ToTable("FAAT_Policy");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IsSelected).HasColumnName("isSelected");
                entity.Property(e=>e.MeritMinCGPA).HasColumnName("MeritMinCGPA");
                entity.Property(e=>e.NeedMinCGPA).HasColumnName("NeedMinCGPA");


                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FaatRule>(entity =>
            {
                entity.ToTable("FAAT_Rule");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PolicyId).HasColumnName("Policy_ID");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.FaatRule)
                    .HasForeignKey(d => d.PolicyId)
                    .HasConstraintName("FK__FAAT_Rule__Polic__37A5467C");
            });

            modelBuilder.Entity<FaatRuleDescription>(entity =>
            {
                entity.ToTable("FAAT_Rule_Description");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RuleId).HasColumnName("Rule_ID");

                entity.Property(e => e.StudentNo).HasColumnName("Student_No");

                entity.HasOne(d => d.Rule)
                    .WithMany(p => p.FaatRuleDescription)
                    .HasForeignKey(d => d.RuleId)
                    .HasConstraintName("FK__FAAT_Rule__Rule___3D5E1FD2");
            });
            modelBuilder.Entity<FaatIntakeSeason>(entity =>
            {
                entity.ToTable("Faat_Intake_Season");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IntakeSeason)
                    .HasColumnName("Intake_Season")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Year).HasColumnName("Year");

                 entity.Property(e => e.InsertionTimestamp)
                    .HasColumnName("Insertion_Timestamp")
                    .HasColumnType("datetime");
                
            });

            modelBuilder.Entity<FaatScholarshipStatus>(entity =>
            {
                entity.ToTable("Faat_Scholarship_Status");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IntakeSeasonId).HasColumnName("Intake_Season_ID");

                entity.Property(e => e.Policy).HasColumnName("policy");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IntakeSeason)
                    .WithMany(p => p.FaatScholarshipStatus)
                    .HasForeignKey(d => d.IntakeSeasonId)
                    .HasConstraintName("FK__Faat_Scho__Intak__1B9317B3");
            });




            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
