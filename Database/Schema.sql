USE [FaaToolDB]
GO
/****** Object:  Table [dbo].[Donation_Ledger]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Donation_Ledger](
	[TransactionID] [int] IDENTITY(1,1) NOT NULL,
	[TransactionDate] [smalldatetime] NULL,
	[Credit] [float] NULL,
	[Debit] [float] NULL,
	[Memo] [varchar](50) NULL,
 CONSTRAINT [PK_Faat_Donation_Ledger] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAAT_APP_Comments]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAAT_APP_Comments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[[User_ID]]] [int] NULL,
	[[Application_ID]]] [int] NULL,
	[Comments] [varchar](max) NULL,
	[Amount] [int] NULL,
	[Class_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAAT_APP_Guardian_Other_Income_Resource_Files]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAAT_APP_Guardian_Other_Income_Resource_Files](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[File_Data] [varbinary](max) NULL,
	[File_Type] [varchar](max) NULL,
	[File_Name] [varchar](max) NULL,
	[Resource_Type] [varchar](100) NULL,
	[FAAT_APP_Parent_Detail_id] [int] NULL,
	[Income] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAAT_APP_Mother_Other_Income_Resource_Files]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAAT_APP_Mother_Other_Income_Resource_Files](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[File_Data] [varbinary](max) NULL,
	[File_Type] [varchar](max) NULL,
	[File_Name] [varchar](max) NULL,
	[Resource_Type] [varchar](100) NULL,
	[Income] [int] NULL,
	[FAAT_APP_Parent_Detail_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAAT_APP_Parent_Detail]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAAT_APP_Parent_Detail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Application_ID] [int] NOT NULL,
	[G_Name] [varchar](128) NULL,
	[G_Company] [varchar](128) NULL,
	[G_Occupation] [varchar](128) NULL,
	[G_Designation] [varchar](128) NULL,
	[G_Monthly_Income] [float] NULL,
	[G_Office_Tel_No] [varchar](25) NULL,
	[G_Office_Address] [varchar](256) NULL,
	[G_Email_Address] [varchar](50) NULL,
	[M_Name] [varchar](128) NULL,
	[M_Company] [varchar](128) NULL,
	[M_Occupation] [varchar](128) NULL,
	[M_Designation] [varchar](128) NULL,
	[M_Monthly_Income] [float] NULL,
	[M_Office_Tel_No] [varchar](25) NULL,
	[M_Office_Address] [varchar](256) NULL,
	[M_Email_Address] [varchar](50) NULL,
	[Financing_Person] [varchar](128) NULL,
	[Mother_CNIC_DeathCertificate_File_Data] [varbinary](max) NULL,
	[Mother_CNIC_DeathCertificate_File_Name] [varchar](max) NULL,
	[Mother_CNIC_DeathCertificate_File_Type] [varchar](max) NULL,
	[Father_CNIC_DeathCertificate_File_Data] [varbinary](max) NULL,
	[Father_CNIC_DeathCertificate_File_Type] [varchar](max) NULL,
	[Father_CNIC_DeathCertificate_File_Name] [varchar](max) NULL,
	[Father_Is_Alive] [bit] NOT NULL,
	[Mother_Is_Alive] [bit] NOT NULL,
 CONSTRAINT [PK_FAAT_APP_Parent_Detail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAAT_APP_Residence_Info]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAAT_APP_Residence_Info](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Application_ID] [int] NOT NULL,
	[Address] [varchar](256) NULL,
	[Phone_No] [varchar](25) NULL,
	[Mode] [varchar](15) NULL,
	[Residence_Info_File_Data] [varbinary](max) NULL,
	[Residence_Info_File_Type] [varchar](max) NULL,
	[Residence_Info_File_Name] [varchar](max) NULL,
 CONSTRAINT [PK_FAAT_APP_Residence_Info] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAAT_APP_Sib_Job_Holder]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAAT_APP_Sib_Job_Holder](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Sibling_ID] [int] NOT NULL,
	[Name] [varchar](128) NULL,
	[Company] [varchar](128) NULL,
	[Designation] [varchar](128) NULL,
	[Monthly_Income] [float] NULL,
	[Contract_File_Data] [varbinary](max) NULL,
	[Contract_File_Type] [varchar](max) NULL,
	[Contract_File_Name] [varchar](max) NULL,
 CONSTRAINT [PK_FAAT_APP_Sib_Job_Holder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAAT_APP_Sib_Student]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAAT_APP_Sib_Student](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Sibling_ID] [int] NOT NULL,
	[Name] [varchar](128) NULL,
	[Class_Institute_Name] [varchar](256) NULL,
	[Stdent_Card_File_Data] [varbinary](max) NULL,
	[Stdent_Card_File_Type] [varchar](max) NULL,
	[Stdent_Card_File_Name] [varchar](max) NULL,
 CONSTRAINT [PK_FAAT_APP_Sib_Student] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAAT_APP_Sibling_Info]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAAT_APP_Sibling_Info](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Application_ID] [int] NOT NULL,
	[No_Of_Sibling] [int] NULL,
 CONSTRAINT [PK_FAAT_APP_Sibling_Info] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAAT_APP_Student_Detail]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAAT_APP_Student_Detail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Application_ID] [int] NOT NULL,
	[Arid_No] [varchar](50) NULL,
	[Name] [varchar](128) NULL,
	[Class] [varchar](100) NULL,
	[Section] [varchar](50) NULL,
	[CPGA] [float] NULL,
	[Email_Address] [varchar](50) NULL,
	[Residence] [varchar](25) NULL,
	[Mobile_No] [varchar](25) NULL,
	[Reason_To_Apply] [varchar](256) NULL,
	[Semester] [varchar](25) NULL,
 CONSTRAINT [PK_FAAT_APP_Student_Detail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAAT_Application]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAAT_Application](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Insertion_Timestamp] [datetime] NOT NULL,
	[Update_Timestamp] [datetime] NOT NULL,
	[Application_Data] [varbinary](max) NULL,
	[Class_id] [int] NULL,
	[AridNo] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[CGPA] [float] NULL,
	[status] [varchar](max) NULL,
	[User_Image] [varbinary](max) NULL,
	[User_Image_FileType] [varchar](max) NULL,
	[User_Image_FileName] [varchar](max) NULL,
 CONSTRAINT [PK_FAAT_Application] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Faat_Class_Definition]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faat_Class_Definition](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Discipline] [varchar](30) NULL,
	[Semester] [varchar](20) NULL,
	[SemesterCount] [varchar](10) NULL,
	[Section] [varchar](10) NULL,
	[ClassStrength] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAAT_Files]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAAT_Files](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[File_Data] [varbinary](max) NULL,
	[File_Type] [varchar](max) NULL,
	[Application_ID] [int] NULL,
	[File_Name] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Faat_Intake_Season]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faat_Intake_Season](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Intake_Season] [varchar](100) NULL,
	[Year] [int] NULL,
	[Insertion_Timestamp] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAAT_Policy]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAAT_Policy](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[isSelected] [int] NULL,
	[MeritMinCGPA] [float] NULL,
	[NeedMinCGPA] [float] NULL,
 CONSTRAINT [PK_FAAT_Policy] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAAT_Rule]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAAT_Rule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Policy_ID] [int] NULL,
	[Strength] [int] NULL,
	[Top] [int] NULL,
 CONSTRAINT [PK_FAAT_Rule] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAAT_Rule_Description]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAAT_Rule_Description](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Rule_ID] [int] NULL,
	[Student_No] [int] NULL,
	[Amount] [float] NULL,
 CONSTRAINT [PK_FAAT_Rule_Description] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAAT_Scholar_Ledger]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAAT_Scholar_Ledger](
	[TransactionID] [int] IDENTITY(1,1) NOT NULL,
	[TransactionDate] [smalldatetime] NULL,
	[Credit] [float] NULL,
	[Debit] [float] NULL,
	[Memo] [varchar](50) NULL,
 CONSTRAINT [PK_Faat_Student_Ledger] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAAT_ScholarLog]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAAT_ScholarLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tid] [int] NULL,
	[AridNo] [varchar](50) NULL,
	[name] [varchar](100) NULL,
	[Status] [varchar](50) NULL,
	[Type] [varchar](50) NULL,
	[Application_ID] [int] NULL,
	[Insertion_Timestamp] [datetime2](7) NULL,
	[Update_Timestamp] [datetime2](7) NULL,
	[CGPA] [float] NULL,
	[Class_ID] [int] NULL,
	[Allocation_Amount] [float] NULL,
	[Default_Amount] [float] NULL,
	[Is_Manual] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Faat_Scholarship_Status]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faat_Scholarship_Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Intake_Season_ID] [int] NULL,
	[Type] [varchar](100) NULL,
	[Status] [varchar](100) NULL,
	[policy] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/18/2021 12:13:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[AridNo] [varchar](100) NULL,
	[Password] [varchar](100) NULL,
	[Role] [varchar](100) NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NULL,
 CONSTRAINT [PK_FAAT_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[FAAT_APP_Parent_Detail] ADD  DEFAULT ((0)) FOR [Father_Is_Alive]
GO
ALTER TABLE [dbo].[FAAT_APP_Parent_Detail] ADD  DEFAULT ((0)) FOR [Mother_Is_Alive]
GO
ALTER TABLE [dbo].[FAAT_Policy] ADD  CONSTRAINT [df_IsSelected]  DEFAULT ((0)) FOR [isSelected]
GO
ALTER TABLE [dbo].[FAAT_ScholarLog] ADD  DEFAULT ('FALSE') FOR [Is_Manual]
GO
ALTER TABLE [dbo].[FAAT_APP_Comments]  WITH CHECK ADD FOREIGN KEY([[Application_ID]]])
REFERENCES [dbo].[FAAT_Application] ([ID])
GO
ALTER TABLE [dbo].[FAAT_APP_Comments]  WITH CHECK ADD FOREIGN KEY([[User_ID]]])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[FAAT_APP_Comments]  WITH CHECK ADD  CONSTRAINT [FK_ClassId] FOREIGN KEY([Class_ID])
REFERENCES [dbo].[Faat_Class_Definition] ([Id])
GO
ALTER TABLE [dbo].[FAAT_APP_Comments] CHECK CONSTRAINT [FK_ClassId]
GO
ALTER TABLE [dbo].[FAAT_APP_Guardian_Other_Income_Resource_Files]  WITH CHECK ADD FOREIGN KEY([FAAT_APP_Parent_Detail_id])
REFERENCES [dbo].[FAAT_APP_Parent_Detail] ([ID])
GO
ALTER TABLE [dbo].[FAAT_APP_Mother_Other_Income_Resource_Files]  WITH CHECK ADD FOREIGN KEY([FAAT_APP_Parent_Detail_id])
REFERENCES [dbo].[FAAT_APP_Parent_Detail] ([ID])
GO
ALTER TABLE [dbo].[FAAT_APP_Parent_Detail]  WITH CHECK ADD  CONSTRAINT [FK_FAAT_APP_Parent_Detail] FOREIGN KEY([Application_ID])
REFERENCES [dbo].[FAAT_Application] ([ID])
GO
ALTER TABLE [dbo].[FAAT_APP_Parent_Detail] CHECK CONSTRAINT [FK_FAAT_APP_Parent_Detail]
GO
ALTER TABLE [dbo].[FAAT_APP_Residence_Info]  WITH CHECK ADD  CONSTRAINT [FK_FAAT_APP_Residence_Info] FOREIGN KEY([Application_ID])
REFERENCES [dbo].[FAAT_Application] ([ID])
GO
ALTER TABLE [dbo].[FAAT_APP_Residence_Info] CHECK CONSTRAINT [FK_FAAT_APP_Residence_Info]
GO
ALTER TABLE [dbo].[FAAT_APP_Sib_Job_Holder]  WITH CHECK ADD  CONSTRAINT [FK_FAAT_APP_Sib_Job_Holder] FOREIGN KEY([Sibling_ID])
REFERENCES [dbo].[FAAT_APP_Sibling_Info] ([ID])
GO
ALTER TABLE [dbo].[FAAT_APP_Sib_Job_Holder] CHECK CONSTRAINT [FK_FAAT_APP_Sib_Job_Holder]
GO
ALTER TABLE [dbo].[FAAT_APP_Sib_Student]  WITH CHECK ADD  CONSTRAINT [FK_FAAT_APP_Sib_Student] FOREIGN KEY([Sibling_ID])
REFERENCES [dbo].[FAAT_APP_Sibling_Info] ([ID])
GO
ALTER TABLE [dbo].[FAAT_APP_Sib_Student] CHECK CONSTRAINT [FK_FAAT_APP_Sib_Student]
GO
ALTER TABLE [dbo].[FAAT_APP_Sibling_Info]  WITH CHECK ADD  CONSTRAINT [FK_FAAT_APP_Sibling_Info] FOREIGN KEY([Application_ID])
REFERENCES [dbo].[FAAT_Application] ([ID])
GO
ALTER TABLE [dbo].[FAAT_APP_Sibling_Info] CHECK CONSTRAINT [FK_FAAT_APP_Sibling_Info]
GO
ALTER TABLE [dbo].[FAAT_APP_Student_Detail]  WITH CHECK ADD  CONSTRAINT [FK_FAAT_APP_Student_Detail] FOREIGN KEY([Application_ID])
REFERENCES [dbo].[FAAT_Application] ([ID])
GO
ALTER TABLE [dbo].[FAAT_APP_Student_Detail] CHECK CONSTRAINT [FK_FAAT_APP_Student_Detail]
GO
ALTER TABLE [dbo].[FAAT_Application]  WITH CHECK ADD FOREIGN KEY([Class_id])
REFERENCES [dbo].[Faat_Class_Definition] ([Id])
GO
ALTER TABLE [dbo].[FAAT_Files]  WITH CHECK ADD FOREIGN KEY([Application_ID])
REFERENCES [dbo].[FAAT_Application] ([ID])
GO
ALTER TABLE [dbo].[FAAT_Rule]  WITH CHECK ADD FOREIGN KEY([Policy_ID])
REFERENCES [dbo].[FAAT_Policy] ([ID])
GO
ALTER TABLE [dbo].[FAAT_Rule_Description]  WITH CHECK ADD FOREIGN KEY([Rule_ID])
REFERENCES [dbo].[FAAT_Rule] ([ID])
GO
ALTER TABLE [dbo].[FAAT_ScholarLog]  WITH CHECK ADD FOREIGN KEY([Tid])
REFERENCES [dbo].[FAAT_Scholar_Ledger] ([TransactionID])
GO
ALTER TABLE [dbo].[FAAT_ScholarLog]  WITH CHECK ADD  CONSTRAINT [FK_Application] FOREIGN KEY([Application_ID])
REFERENCES [dbo].[FAAT_Application] ([ID])
GO
ALTER TABLE [dbo].[FAAT_ScholarLog] CHECK CONSTRAINT [FK_Application]
GO
ALTER TABLE [dbo].[FAAT_ScholarLog]  WITH CHECK ADD  CONSTRAINT [FK_Class_Definition] FOREIGN KEY([Class_ID])
REFERENCES [dbo].[Faat_Class_Definition] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FAAT_ScholarLog] CHECK CONSTRAINT [FK_Class_Definition]
GO
ALTER TABLE [dbo].[Faat_Scholarship_Status]  WITH CHECK ADD FOREIGN KEY([Intake_Season_ID])
REFERENCES [dbo].[Faat_Intake_Season] ([Id])
GO
ALTER TABLE [dbo].[Faat_Scholarship_Status]  WITH CHECK ADD FOREIGN KEY([Intake_Season_ID])
REFERENCES [dbo].[Faat_Intake_Season] ([Id])
GO
