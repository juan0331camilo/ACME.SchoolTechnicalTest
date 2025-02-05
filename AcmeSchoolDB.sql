/****** Object:  Database [AcmeSchoolDB]    Script Date: 30/01/2025 09:47:55 p. m. ******/
CREATE DATABASE [AcmeSchoolDB]  (EDITION = 'Basic', SERVICE_OBJECTIVE = 'Basic', MAXSIZE = 2 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS, LEDGER = OFF;
GO
ALTER DATABASE [AcmeSchoolDB] SET COMPATIBILITY_LEVEL = 160
GO
ALTER DATABASE [AcmeSchoolDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AcmeSchoolDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AcmeSchoolDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AcmeSchoolDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AcmeSchoolDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [AcmeSchoolDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AcmeSchoolDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AcmeSchoolDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AcmeSchoolDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AcmeSchoolDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AcmeSchoolDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AcmeSchoolDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AcmeSchoolDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AcmeSchoolDB] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [AcmeSchoolDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AcmeSchoolDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [AcmeSchoolDB] SET  MULTI_USER 
GO
ALTER DATABASE [AcmeSchoolDB] SET ENCRYPTION ON
GO
ALTER DATABASE [AcmeSchoolDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [AcmeSchoolDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 7), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 10, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  Table [dbo].[Course]    Script Date: 30/01/2025 09:47:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[CourseID] [int] NOT NULL,
	[Title] [varchar](50) NULL,
	[Credits] [int] NULL,
	[RegistrationFee] [decimal](18, 0) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseInstructor]    Script Date: 30/01/2025 09:47:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseInstructor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CourseID] [int] NOT NULL,
	[InstructorID] [int] NOT NULL,
 CONSTRAINT [PK_CourseInstructor_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 30/01/2025 09:47:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Budget] [decimal](18, 2) NULL,
	[StartDate] [datetime] NULL,
	[InstructorID] [int] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Enrollment]    Script Date: 30/01/2025 09:47:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enrollment](
	[EnrollmentID] [int] IDENTITY(1,1) NOT NULL,
	[CourseID] [int] NULL,
	[StudentID] [int] NULL,
	[Grade] [int] NULL,
 CONSTRAINT [PK_Enrollment] PRIMARY KEY CLUSTERED 
(
	[EnrollmentID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Instructor]    Script Date: 30/01/2025 09:47:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instructor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [varchar](50) NULL,
	[FirstMidName] [varchar](50) NULL,
	[HireDate] [datetime] NULL,
 CONSTRAINT [PK_Instructor] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OfficeAssignment]    Script Date: 30/01/2025 09:47:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OfficeAssignment](
	[InstructorID] [int] NOT NULL,
	[Location] [varchar](50) NULL,
 CONSTRAINT [PK_OfficeAssignment] PRIMARY KEY CLUSTERED 
(
	[InstructorID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 30/01/2025 09:47:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [varchar](50) NULL,
	[FirstMidName] [varchar](50) NULL,
	[EnrollmentDate] [datetime] NULL,
	[BirthDate] [datetime] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Course] ([CourseID], [Title], [Credits], [RegistrationFee], [StartDate], [EndDate]) VALUES (1045, N'Calculus', 3, CAST(8481 AS Decimal(18, 0)), CAST(N'2025-01-14T02:32:59.353' AS DateTime), CAST(N'2025-02-14T02:32:59.353' AS DateTime))
INSERT [dbo].[Course] ([CourseID], [Title], [Credits], [RegistrationFee], [StartDate], [EndDate]) VALUES (1050, N'Chemistry', 3, CAST(5374 AS Decimal(18, 0)), CAST(N'2025-01-20T02:32:59.353' AS DateTime), CAST(N'2025-02-20T02:32:59.353' AS DateTime))
INSERT [dbo].[Course] ([CourseID], [Title], [Credits], [RegistrationFee], [StartDate], [EndDate]) VALUES (2021, N'Composition', 3, CAST(2938 AS Decimal(18, 0)), CAST(N'2025-01-16T02:32:59.353' AS DateTime), CAST(N'2025-02-16T02:32:59.353' AS DateTime))
INSERT [dbo].[Course] ([CourseID], [Title], [Credits], [RegistrationFee], [StartDate], [EndDate]) VALUES (2042, N'Literature', 4, CAST(2039 AS Decimal(18, 0)), CAST(N'2025-01-07T02:32:59.353' AS DateTime), CAST(N'2025-02-07T02:32:59.353' AS DateTime))
INSERT [dbo].[Course] ([CourseID], [Title], [Credits], [RegistrationFee], [StartDate], [EndDate]) VALUES (3141, N'Trigonometry', 4, CAST(7839 AS Decimal(18, 0)), CAST(N'2025-01-02T02:32:59.353' AS DateTime), CAST(N'2025-02-02T02:32:59.353' AS DateTime))
INSERT [dbo].[Course] ([CourseID], [Title], [Credits], [RegistrationFee], [StartDate], [EndDate]) VALUES (4022, N'Microeconomics', 3, CAST(3098 AS Decimal(18, 0)), CAST(N'2025-01-01T02:32:59.353' AS DateTime), CAST(N'2025-02-01T02:32:59.353' AS DateTime))
INSERT [dbo].[Course] ([CourseID], [Title], [Credits], [RegistrationFee], [StartDate], [EndDate]) VALUES (4041, N'Macroeconomics', 3, CAST(1092 AS Decimal(18, 0)), CAST(N'2025-01-25T02:32:59.353' AS DateTime), CAST(N'2025-02-25T02:32:59.353' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[CourseInstructor] ON 

INSERT [dbo].[CourseInstructor] ([ID], [CourseID], [InstructorID]) VALUES (1, 1050, 4)
INSERT [dbo].[CourseInstructor] ([ID], [CourseID], [InstructorID]) VALUES (2, 1050, 3)
INSERT [dbo].[CourseInstructor] ([ID], [CourseID], [InstructorID]) VALUES (3, 4022, 5)
INSERT [dbo].[CourseInstructor] ([ID], [CourseID], [InstructorID]) VALUES (4, 4041, 5)
INSERT [dbo].[CourseInstructor] ([ID], [CourseID], [InstructorID]) VALUES (5, 1045, 2)
INSERT [dbo].[CourseInstructor] ([ID], [CourseID], [InstructorID]) VALUES (6, 3141, 3)
INSERT [dbo].[CourseInstructor] ([ID], [CourseID], [InstructorID]) VALUES (7, 2021, 1)
INSERT [dbo].[CourseInstructor] ([ID], [CourseID], [InstructorID]) VALUES (8, 2042, 1)
SET IDENTITY_INSERT [dbo].[CourseInstructor] OFF
GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([DepartmentID], [Name], [Budget], [StartDate], [InstructorID]) VALUES (1, N'English', CAST(350000.00 AS Decimal(18, 2)), CAST(N'2007-09-01T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Department] ([DepartmentID], [Name], [Budget], [StartDate], [InstructorID]) VALUES (2, N'Mathematics', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2007-09-01T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Department] ([DepartmentID], [Name], [Budget], [StartDate], [InstructorID]) VALUES (3, N'Engineering', CAST(350000.00 AS Decimal(18, 2)), CAST(N'2007-09-01T00:00:00.000' AS DateTime), 3)
INSERT [dbo].[Department] ([DepartmentID], [Name], [Budget], [StartDate], [InstructorID]) VALUES (4, N'Economics', CAST(100000.00 AS Decimal(18, 2)), CAST(N'2007-09-01T00:00:00.000' AS DateTime), 4)
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[Enrollment] ON 

INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (1, 1050, 1, 0)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (2, 4022, 1, 2)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (3, 4041, 1, 1)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (4, 1045, 2, 1)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (5, 3141, 2, 4)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (6, 2021, 2, 4)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (7, 1050, 3, NULL)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (8, 1050, 4, NULL)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (9, 4022, 4, 4)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (10, 4041, 5, 2)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (11, 1045, 6, NULL)
INSERT [dbo].[Enrollment] ([EnrollmentID], [CourseID], [StudentID], [Grade]) VALUES (12, 3141, 7, 0)
SET IDENTITY_INSERT [dbo].[Enrollment] OFF
GO
SET IDENTITY_INSERT [dbo].[Instructor] ON 

INSERT [dbo].[Instructor] ([ID], [LastName], [FirstMidName], [HireDate]) VALUES (1, N'Abercrombie', N'Kim', CAST(N'1995-03-11T00:00:00.000' AS DateTime))
INSERT [dbo].[Instructor] ([ID], [LastName], [FirstMidName], [HireDate]) VALUES (2, N'Fakhouri', N'Fadi', CAST(N'2002-07-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Instructor] ([ID], [LastName], [FirstMidName], [HireDate]) VALUES (3, N'Harui', N'Roger', CAST(N'1998-07-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Instructor] ([ID], [LastName], [FirstMidName], [HireDate]) VALUES (4, N'Kapoor', N'Candace', CAST(N'2001-01-15T00:00:00.000' AS DateTime))
INSERT [dbo].[Instructor] ([ID], [LastName], [FirstMidName], [HireDate]) VALUES (5, N'Zheng', N'Roger', CAST(N'2004-02-12T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Instructor] OFF
GO
INSERT [dbo].[OfficeAssignment] ([InstructorID], [Location]) VALUES (2, N'Smith 17')
INSERT [dbo].[OfficeAssignment] ([InstructorID], [Location]) VALUES (3, N'Gowan 27')
INSERT [dbo].[OfficeAssignment] ([InstructorID], [Location]) VALUES (4, N'Thompson 304')
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([ID], [LastName], [FirstMidName], [EnrollmentDate], [BirthDate]) VALUES (1, N'Alexander', N'Carson', CAST(N'2005-09-01T00:00:00.000' AS DateTime), CAST(N'2003-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Student] ([ID], [LastName], [FirstMidName], [EnrollmentDate], [BirthDate]) VALUES (2, N'Alonso', N'Meredith', CAST(N'2002-09-01T00:00:00.000' AS DateTime), CAST(N'2000-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Student] ([ID], [LastName], [FirstMidName], [EnrollmentDate], [BirthDate]) VALUES (3, N'Anand', N'Arturo', CAST(N'2003-09-01T00:00:00.000' AS DateTime), CAST(N'2001-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Student] ([ID], [LastName], [FirstMidName], [EnrollmentDate], [BirthDate]) VALUES (4, N'Barzdukas', N'Gytis', CAST(N'2002-09-01T00:00:00.000' AS DateTime), CAST(N'2000-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Student] ([ID], [LastName], [FirstMidName], [EnrollmentDate], [BirthDate]) VALUES (5, N'Li', N'Yan', CAST(N'2002-09-01T00:00:00.000' AS DateTime), CAST(N'2000-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Student] ([ID], [LastName], [FirstMidName], [EnrollmentDate], [BirthDate]) VALUES (6, N'Justice', N'Peggy', CAST(N'2001-09-01T00:00:00.000' AS DateTime), CAST(N'1999-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Student] ([ID], [LastName], [FirstMidName], [EnrollmentDate], [BirthDate]) VALUES (7, N'Norman', N'Laura', CAST(N'2003-09-01T00:00:00.000' AS DateTime), CAST(N'2001-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Student] ([ID], [LastName], [FirstMidName], [EnrollmentDate], [BirthDate]) VALUES (8, N'Olivetto', N'Nino', CAST(N'2005-09-01T00:00:00.000' AS DateTime), CAST(N'2003-09-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
ALTER TABLE [dbo].[CourseInstructor]  WITH CHECK ADD  CONSTRAINT [FK_CourseInstructor_Course] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([CourseID])
GO
ALTER TABLE [dbo].[CourseInstructor] CHECK CONSTRAINT [FK_CourseInstructor_Course]
GO
ALTER TABLE [dbo].[CourseInstructor]  WITH CHECK ADD  CONSTRAINT [FK_CourseInstructor_Instructor] FOREIGN KEY([InstructorID])
REFERENCES [dbo].[Instructor] ([ID])
GO
ALTER TABLE [dbo].[CourseInstructor] CHECK CONSTRAINT [FK_CourseInstructor_Instructor]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Instructor] FOREIGN KEY([InstructorID])
REFERENCES [dbo].[Instructor] ([ID])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Instructor]
GO
ALTER TABLE [dbo].[Enrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_Course] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([CourseID])
GO
ALTER TABLE [dbo].[Enrollment] CHECK CONSTRAINT [FK_Enrollment_Course]
GO
ALTER TABLE [dbo].[Enrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([ID])
GO
ALTER TABLE [dbo].[Enrollment] CHECK CONSTRAINT [FK_Enrollment_Student]
GO
ALTER TABLE [dbo].[OfficeAssignment]  WITH CHECK ADD  CONSTRAINT [FK_OfficeAssignment_Instructor] FOREIGN KEY([InstructorID])
REFERENCES [dbo].[Instructor] ([ID])
GO
ALTER TABLE [dbo].[OfficeAssignment] CHECK CONSTRAINT [FK_OfficeAssignment_Instructor]
GO
ALTER DATABASE [AcmeSchoolDB] SET  READ_WRITE 
GO
