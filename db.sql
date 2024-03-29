USE [master]
GO
/****** Object:  Database [Arcade]    Script Date: 2/24/2020 5:52:50 PM ******/
CREATE DATABASE [Arcade]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Arcade', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Arcade.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Arcade_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Arcade_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Arcade] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Arcade].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Arcade] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Arcade] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Arcade] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Arcade] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Arcade] SET ARITHABORT OFF 
GO
ALTER DATABASE [Arcade] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Arcade] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Arcade] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Arcade] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Arcade] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Arcade] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Arcade] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Arcade] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Arcade] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Arcade] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Arcade] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Arcade] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Arcade] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Arcade] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Arcade] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Arcade] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Arcade] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Arcade] SET RECOVERY FULL 
GO
ALTER DATABASE [Arcade] SET  MULTI_USER 
GO
ALTER DATABASE [Arcade] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Arcade] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Arcade] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Arcade] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Arcade] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Arcade', N'ON'
GO
ALTER DATABASE [Arcade] SET QUERY_STORE = OFF
GO
USE [Arcade]
GO
/****** Object:  Table [dbo].[Computers]    Script Date: 2/24/2020 5:52:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Computers](
	[Id] [nvarchar](250) NOT NULL,
	[TypeId] [nvarchar](250) NOT NULL,
	[Number] [int] NOT NULL,
	[IsTerminated] [bit] NOT NULL,
 CONSTRAINT [PK_Computers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComputerTypes]    Script Date: 2/24/2020 5:52:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComputerTypes](
	[Id] [nvarchar](250) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[HourlyRate] [float] NOT NULL,
 CONSTRAINT [PK_ComputerTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 2/24/2020 5:52:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [nvarchar](255) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[IsTerminated] [bit] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Faults]    Script Date: 2/24/2020 5:52:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faults](
	[Code] [int] NOT NULL,
	[HttpStatusCode] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Faults] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Games]    Script Date: 2/24/2020 5:52:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Games](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ImageId] [nvarchar](450) NULL,
	[AgeLimit] [int] NULL,
	[Category] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Games] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IdentityRoleClaim`1]    Script Date: 2/24/2020 5:52:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityRoleClaim`1](
	[Id] [int] NOT NULL,
	[RoleId] [nvarchar](max) NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IdentityUserClaim`1]    Script Date: 2/24/2020 5:52:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserClaim`1](
	[Id] [int] NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IdentityUserLogin`1]    Script Date: 2/24/2020 5:52:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserLogin`1](
	[LoginProvider] [nvarchar](max) NULL,
	[ProviderKey] [nvarchar](max) NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IdentityUserRole`1]    Script Date: 2/24/2020 5:52:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserRole`1](
	[UserId] [nvarchar](max) NULL,
	[RoleId] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IdentityUserToken`1]    Script Date: 2/24/2020 5:52:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentityUserToken`1](
	[UserId] [nvarchar](max) NULL,
	[LoginProvider] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 2/24/2020 5:52:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [nvarchar](450) NOT NULL,
	[Path] [nvarchar](max) NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 2/24/2020 5:52:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[Id] [nvarchar](255) NOT NULL,
	[Amount] [float] NOT NULL,
	[EmployeeId] [nvarchar](255) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QueueNumberStorage]    Script Date: 2/24/2020 5:52:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QueueNumberStorage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LatestNumber] [int] NOT NULL,
 CONSTRAINT [PK_QueueNumberStorage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 2/24/2020 5:52:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [nvarchar](255) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[NormalizedName] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_IdentityRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 2/24/2020 5:52:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessions](
	[Id] [nvarchar](255) NOT NULL,
	[ComputerId] [nvarchar](250) NOT NULL,
	[StartDate] [datetime2](7) NULL,
	[Duration] [int] NOT NULL,
	[PaymentId] [nvarchar](255) NULL,
	[EndDate] [datetime2](7) NULL,
	[QueueNumber] [int] NULL,
	[QueueDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemSettings]    Script Date: 2/24/2020 5:52:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemSettings](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Value] [text] NOT NULL,
	[Type] [nvarchar](250) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2/24/2020 5:52:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [nvarchar](255) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[NormalizedUserName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[NormalizedEmail] [nvarchar](max) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Computers] ([Id], [TypeId], [Number], [IsTerminated]) VALUES (N'bb814c94-5906-4b72-855e-c6bb93b1225d', N'2b358a02-962d-4e85-812a-98e2f62ca9e3', 2, 0)
INSERT [dbo].[Computers] ([Id], [TypeId], [Number], [IsTerminated]) VALUES (N'f85300cb-07b7-4f12-9e37-f4183a07d31b', N'3a79f558-5196-4652-88b9-12dfd7a793d5', 1, 0)
INSERT [dbo].[Computers] ([Id], [TypeId], [Number], [IsTerminated]) VALUES (N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', N'2b358a02-962d-4e85-812a-98e2f62ca9e3', 1, 0)
INSERT [dbo].[ComputerTypes] ([Id], [Name], [HourlyRate]) VALUES (N'2b358a02-962d-4e85-812a-98e2f62ca9e3', N'Playstation', 412)
INSERT [dbo].[ComputerTypes] ([Id], [Name], [HourlyRate]) VALUES (N'3a79f558-5196-4652-88b9-12dfd7a793d5', N'VR Headset', 650.5)
INSERT [dbo].[ComputerTypes] ([Id], [Name], [HourlyRate]) VALUES (N'fb0ff264-38ca-4a0d-b51d-872a9c9177c1', N'PC', 500)
INSERT [dbo].[Employees] ([Id], [FirstName], [LastName], [IsTerminated]) VALUES (N'0a160b6d-5b5f-4604-911e-53a410aa76a6', N'Karen', N'Esiminchyan', 0)
INSERT [dbo].[Employees] ([Id], [FirstName], [LastName], [IsTerminated]) VALUES (N'90dc8b08-1405-4b08-b9ce-9173b06e7e37', N'Davit', N'Asryan', 0)
INSERT [dbo].[Faults] ([Code], [HttpStatusCode], [Description]) VALUES (1000, 404, N'Resource not found')
INSERT [dbo].[Faults] ([Code], [HttpStatusCode], [Description]) VALUES (1001, 400, N'Bad request')
INSERT [dbo].[Faults] ([Code], [HttpStatusCode], [Description]) VALUES (1002, 400, N'Image not provided')
INSERT [dbo].[Faults] ([Code], [HttpStatusCode], [Description]) VALUES (2001, 404, N'Invalid credentials')
INSERT [dbo].[Faults] ([Code], [HttpStatusCode], [Description]) VALUES (2002, 400, N'Invalid registration data')
INSERT [dbo].[Faults] ([Code], [HttpStatusCode], [Description]) VALUES (9000, 500, N'Unexpected error')
INSERT [dbo].[Games] ([Id], [Name], [ImageId], [AgeLimit], [Category]) VALUES (N'0a5c12bb-a7eb-4bd4-8958-2357b791fdb2', N'Blood & Truth', N'6a5c8b38-3295-4777-bd92-1ec983724c29', 12, N'First-person shooter')
INSERT [dbo].[Games] ([Id], [Name], [ImageId], [AgeLimit], [Category]) VALUES (N'849163b2-3b11-456b-81a0-4abbdff4bd40', N'Rick and Morty', N'9f7939f4-227b-4c60-8d54-061f5aa29433', 12, N'Simulation')
INSERT [dbo].[Games] ([Id], [Name], [ImageId], [AgeLimit], [Category]) VALUES (N'a54a762f-c1b7-40ee-818c-e3b8a1ab008e', N'Tetris Effect', N'76471bf2-9844-4052-80b9-8b2e53ff312c', 7, N'Puzzle')
INSERT [dbo].[Games] ([Id], [Name], [ImageId], [AgeLimit], [Category]) VALUES (N'c09b2803-1b29-49b2-acbc-0363e71a55fa', N'Beat Saber', N'cc74fada-3d57-43dc-846a-8ab824fac650', 16, N'Rhythm')
INSERT [dbo].[Games] ([Id], [Name], [ImageId], [AgeLimit], [Category]) VALUES (N'd38f9b86-a60c-40dc-b12e-e1925d407a5f', N'Job Simulator', N'c699a8d5-ad24-4495-a158-2c97064e0f35', 13, N'Simulation')
INSERT [dbo].[IdentityUserRole`1] ([UserId], [RoleId]) VALUES (N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', N'91352b3a-3f92-4aea-b35a-56efba083b0e')
INSERT [dbo].[IdentityUserRole`1] ([UserId], [RoleId]) VALUES (N'0a160b6d-5b5f-4604-911e-53a410aa76a6', N'bbc6652f-9c91-42c5-a18c-a8ad2af7d626')
INSERT [dbo].[IdentityUserRole`1] ([UserId], [RoleId]) VALUES (N'90dc8b08-1405-4b08-b9ce-9173b06e7e37', N'bbc6652f-9c91-42c5-a18c-a8ad2af7d626')
INSERT [dbo].[Images] ([Id], [Path]) VALUES (N'65dcd92a-134b-49a1-9b7d-f09faf265d53', N'assets\images/0ec4fa21-e2dd-4b04-b9a0-9b1258700bb9.jpg')
INSERT [dbo].[Images] ([Id], [Path]) VALUES (N'66962006-2fac-46b5-baf4-d518863495a3', N'assets\images/66284cc3-384d-441f-8a39-67aafc82e2a6.jpg')
INSERT [dbo].[Images] ([Id], [Path]) VALUES (N'6a5c8b38-3295-4777-bd92-1ec983724c29', N'assets\images/d4c9e896-1b7a-4831-b053-03b8ceeab1c7.jpg')
INSERT [dbo].[Images] ([Id], [Path]) VALUES (N'76471bf2-9844-4052-80b9-8b2e53ff312c', N'assets\images/b58b6a18-4f87-4998-bfc3-73de9ddad956.jpg')
INSERT [dbo].[Images] ([Id], [Path]) VALUES (N'9f7939f4-227b-4c60-8d54-061f5aa29433', N'assets/images/2530dc5f-fb39-444d-9533-c66a80b777cf.jpg')
INSERT [dbo].[Images] ([Id], [Path]) VALUES (N'c0f8cec6-cd73-4e6a-9cc5-22629030af17', N'assets\images/4191b6f5-7585-46e5-9c4c-3972088fe95a.jpg')
INSERT [dbo].[Images] ([Id], [Path]) VALUES (N'c699a8d5-ad24-4495-a158-2c97064e0f35', N'assets\images/ecaebca7-3b3b-43fd-b7b3-c4fad6038701.jpg')
INSERT [dbo].[Images] ([Id], [Path]) VALUES (N'cc74fada-3d57-43dc-846a-8ab824fac650', N'assets\images/1f4339b8-a4a4-4149-a17d-ed1eb9a4e462.jpg')
INSERT [dbo].[Images] ([Id], [Path]) VALUES (N'cf12e88f-8ef9-40e0-a307-d849fbba6354', N'assets\images/711dd3d7-df08-4662-8d9e-a3e3c44cdd98.jpg')
INSERT [dbo].[Images] ([Id], [Path]) VALUES (N'cf33a847-1617-4022-918a-c74b3418ff97', N'assets\images/ff0fb3f4-9ce5-45ad-b8ea-f45d96047227.jpg')
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'01fea4b8-a559-41bf-9e69-7139706b7541', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:09:01.3035632' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'074acf36-a917-4b4f-beb2-510379256e0f', 325.25, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-19T14:42:13.4196237' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'074e1b8f-ffba-4e99-bd19-442f3a44ced8', 325.25, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:04:00.3867924' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'0ce6bbcf-2a9a-4cdb-b277-345c3b6ba2e3', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:22:25.0472635' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'12982ff9-d2b9-4195-9c42-856d769b1a5f', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:20:30.1124834' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'1e0188bf-a707-4a13-8f9c-fbfb19dc55cd', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T18:24:17.3628001' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'211be998-36e3-4f0c-abbc-1ec391b58130', 650.5, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T15:45:47.3362499' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'2184638e-5caf-46d0-a8a6-8f5eca80f4dd', 650.5, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-19T15:13:00.6234708' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'23a73865-ea09-4c91-9c41-a66134892669', 325.25, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T16:37:11.1505407' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'27f5873c-5ae3-4dad-a942-81b9e94b1754', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:53:46.5835469' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'2f957ff8-2edc-47fb-8b02-a70dba6b662a', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-22T17:06:40.7130123' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'33365d4b-a689-45a5-be75-ac164e8ca503', 325.25, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T16:51:21.8827062' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'36c12fe6-b51b-4e5e-af3f-bed51db9b00d', 325.25, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T15:27:56.0953233' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'37c22007-f080-49a3-8456-618256457a76', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:18:40.6139139' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'3c83e97c-4c12-4b2e-a0d4-271b6378a6b7', 325.25, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T16:43:25.4443869' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'3caf108c-1252-4106-b704-31f127cbdd4a', 325.25, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-19T14:41:19.8735013' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'3e1eb267-3d7e-4be9-93a0-a8b45c49471e', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-19T18:15:54.4562145' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'44ec8923-e2d3-4e21-a3aa-76fa07d2dfaa', 412, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-22T17:19:52.1144472' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'474c9449-df22-4c15-9f32-75bafac64b76', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:33:40.1378159' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'481e1889-d739-43e6-ba75-b123920e63bc', 325.25, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-19T14:40:23.0472060' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'5c1eba48-672b-4589-9403-d4554eb7ebf0', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:39:46.3650245' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'6415b600-70a0-4762-a146-1fbd15c3ece3', 650.5, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-19T15:12:57.5278467' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'668d35ee-4520-41ea-a664-7e0884bf22fc', 325.25, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-19T14:47:01.3064654' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'6d28c48a-8b07-4db1-b653-7e235c1ab1e9', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:55:44.2014850' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'6f2001a8-bccd-4517-9e0d-2da6f59bc54f', 412, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:08:01.1352106' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'724dc141-9000-49a6-8116-fbe9747b7fa7', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:23:36.0399198' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'7586bbb1-f645-418c-9351-a8efee8697d5', 325.25, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T16:37:16.0532300' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'7ac0ac84-59b1-4755-933f-04071388a2d4', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-19T18:15:43.3583510' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'81a1ef5b-7388-4592-b792-f820a2dd8a99', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:59:14.7750088' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'85e6bfbb-8162-4442-bbb9-8c22752d6934', 325.25, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-19T14:42:55.3202271' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'927e6fbf-002d-43b9-8e02-4f445508a0f6', 325.25, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T16:54:01.9646398' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'9c6c036f-2a77-4995-9d52-a1591f459f99', 325.25, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-19T15:42:28.1471372' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'9f9898da-1170-4264-bbb3-f28042046168', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:14:35.0111104' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'a1e17a08-bcd5-486f-b1ca-61ef0bb007b8', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:47:59.8522802' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'ad8291e2-f3a3-44e6-ab53-4e778fb2a73a', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:16:29.9875986' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'b6f58293-122c-4d0f-90b0-4e654f59596b', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:42:52.4486325' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'b822b9d1-0328-42ba-879d-366688510c56', 325.25, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T16:53:37.4965779' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'b9b8186a-f492-40e6-ac80-601b475ffa1f', 650.5, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-19T15:14:02.4694168' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'c2351c2d-fbe9-4a9b-b8ce-a46484acebad', 824, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:07:56.6373175' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'c7e2de7e-0423-4ac2-854d-5a006a17d726', 325.25, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-19T14:57:06.2297568' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'c8d45ed0-d8d4-4c26-bc17-547145c9ff88', 824, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-19T15:19:29.3894393' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'cc7286e7-feda-4c2b-9b3b-8e953c098323', 650.5, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-19T14:45:43.5669562' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'cd221ac4-9ccf-44b0-babf-ce5efc75ae5a', 650.5, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T16:59:37.6301817' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'df142bf0-7dca-4dbc-bf7e-9d7ad0448ed1', 325.25, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-19T14:40:28.8261893' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'e57793f0-8ecf-4ffb-ad07-2f5531820040', 0, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-19T15:38:32.6039965' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'e8e370fc-b10a-4100-baa8-e81b7e8806b3', 325.25, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T16:40:59.5484399' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'ef1667ac-3b45-4771-a0a0-50493a996438', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:34:44.0600749' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'f0395179-e7a0-4f3f-8aef-5d1b78d46f6b', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:51:14.7493454' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'f4efcc78-6699-46b0-bc36-93fd8bc7c403', 206, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T17:40:31.9781096' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'f72e2d9b-bdc4-461d-b8c0-01610d9808da', 412, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-19T15:19:27.1117586' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'f8744a9d-a215-453e-b66a-a1f790245c40', 1301, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-22T17:06:53.8020057' AS DateTime2))
INSERT [dbo].[Payments] ([Id], [Amount], [EmployeeId], [Date]) VALUES (N'fe1cd151-d818-44f8-94da-835a6669bfaf', 325.25, N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', CAST(N'2020-02-18T16:39:29.0304894' AS DateTime2))
SET IDENTITY_INSERT [dbo].[QueueNumberStorage] ON 

INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1, 100)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (2, 101)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (3, 101)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (4, 101)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (5, 102)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (6, 103)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (7, 104)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (8, 105)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (9, 106)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (10, 107)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (11, 108)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (12, 109)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (13, 110)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (14, 111)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (15, 112)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (16, 113)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (17, 114)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (18, 115)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (19, 116)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (20, 117)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (21, 118)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (22, 119)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1012, 120)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1013, 121)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1014, 122)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1015, 123)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1016, 124)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1017, 125)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1018, 126)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1019, 127)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1020, 128)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1021, 129)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1022, 130)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1023, 131)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1024, 132)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1025, 133)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1026, 134)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1027, 135)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1028, 136)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1029, 137)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1030, 138)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1031, 139)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1032, 140)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1033, 141)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1034, 142)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1035, 143)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1036, 144)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1037, 145)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1038, 146)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1039, 147)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1040, 148)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1041, 149)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1042, 150)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1043, 151)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1044, 152)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1045, 153)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1046, 154)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1047, 155)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1048, 156)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1049, 157)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1050, 158)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1051, 159)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1052, 160)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1053, 161)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1054, 162)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1055, 163)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1056, 164)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1057, 165)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1058, 166)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1059, 167)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1060, 168)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1061, 169)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1062, 170)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1063, 171)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1064, 172)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1065, 173)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1066, 174)
INSERT [dbo].[QueueNumberStorage] ([Id], [LatestNumber]) VALUES (1067, 175)
SET IDENTITY_INSERT [dbo].[QueueNumberStorage] OFF
INSERT [dbo].[Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'6b6fb9a6-94ad-4826-b5e9-33baccd8f110', N'Tech', N'TECH', N'70983d18-db49-439f-b15f-28d5925ce4a8')
INSERT [dbo].[Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'91352b3a-3f92-4aea-b35a-56efba083b0e', N'Admin', N'ADMIN', N'c9c0b1f2-745a-44cf-9651-9d66fd17a548')
INSERT [dbo].[Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'bbc6652f-9c91-42c5-a18c-a8ad2af7d626', N'Sales', N'SALES', N'bba257a6-137e-45cd-8f0f-4e7f79d021f1')
INSERT [dbo].[Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'd459ea38-58f3-40de-a049-2ea904f767f3', N'Manager', N'MANAGER', N'2256685b-36a3-45d3-a8b8-d0c11937be63')
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'048d7fbb-6e0c-4747-88ac-d8ccee4c8f7d', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:01:37.4248917' AS DateTime2), 30, N'fe1cd151-d818-44f8-94da-835a6669bfaf', CAST(N'2020-02-19T19:01:41.0668777' AS DateTime2), 136, CAST(N'2020-02-18T16:39:29.0288566' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'0aa0de4f-c365-47c0-b531-6dff4223a1bf', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:01:07.5519424' AS DateTime2), 60, N'211be998-36e3-4f0c-abbc-1ec391b58130', CAST(N'2020-02-19T19:01:17.0146668' AS DateTime2), 134, CAST(N'2020-02-18T15:45:47.3028531' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'1154e868-6672-4e89-b670-c0e1fb8636fa', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-18T17:07:56.6373152' AS DateTime2), 120, N'c2351c2d-fbe9-4a9b-b8ce-a46484acebad', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'15351f46-dfef-4de2-b6c8-e63558b7931c', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T20:10:15.0475128' AS DateTime2), 1, NULL, CAST(N'2020-02-16T20:10:58.3372577' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'1bb58829-e10c-4bec-98e6-062238890bd6', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-18T15:27:56.0937055' AS DateTime2), 30, N'36c12fe6-b51b-4e5e-af3f-bed51db9b00d', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'1c1fa3e5-c33e-4409-8f75-fa643dca2954', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-17T16:21:08.5645287' AS DateTime2), 60, NULL, CAST(N'2020-02-17T16:21:12.6204821' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'1c6861bd-29d0-4601-9c0e-0c9333fa9c24', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-17T16:47:28.2747085' AS DateTime2), 30, NULL, CAST(N'2020-02-17T16:48:37.9723652' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'24acc789-7847-4068-bcc7-a007db2cdbfb', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T09:37:02.5735818' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 112, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'29a6d495-48bf-4071-b150-ff43ee4a4bb5', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:37:07.4178455' AS DateTime2), 60, N'cd221ac4-9ccf-44b0-babf-ce5efc75ae5a', CAST(N'2020-02-19T19:37:10.7493959' AS DateTime2), 142, CAST(N'2020-02-18T16:59:37.6280551' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'2a21c227-249d-4fbf-be23-35261cd8773b', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T09:12:02.5803497' AS DateTime2), 12, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 106, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'2deed685-35e4-49d0-8fbe-59387415a462', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:39:55.9638751' AS DateTime2), 30, N'81a1ef5b-7388-4592-b792-f820a2dd8a99', CAST(N'2020-02-19T19:40:04.4052685' AS DateTime2), 161, CAST(N'2020-02-18T17:59:14.7734312' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'2ed72436-846e-44cd-b391-61c8fcb9ff7d', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:40:10.9684192' AS DateTime2), 30, N'1e0188bf-a707-4a13-8f9c-fbfb19dc55cd', CAST(N'2020-02-19T19:40:16.1441092' AS DateTime2), 162, CAST(N'2020-02-18T18:24:17.3289285' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'31749a27-d527-44c3-90cb-48b1f014c163', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:37:52.4181576' AS DateTime2), 30, N'3caf108c-1252-4106-b704-31f127cbdd4a', CAST(N'2020-02-19T19:38:07.5423111' AS DateTime2), 164, CAST(N'2020-02-19T14:41:19.8711932' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'34284906-1c7d-4a59-8861-74c57a7eb329', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-15T22:23:52.9849522' AS DateTime2), 12, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'3bcca4bf-a629-46bd-b97b-851e1717c496', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:40:22.4180625' AS DateTime2), 0, N'e57793f0-8ecf-4ffb-ad07-2f5531820040', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 172, CAST(N'2020-02-19T15:38:32.6024580' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'3e0c39f3-d4ba-4248-801c-b68e90527aff', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-15T22:19:56.8218625' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'45826649-0f2c-4c16-a389-294ee38fc340', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T09:27:47.5806555' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 109, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'46530091-0ab0-4123-b234-5a5c446c8f1a', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T09:53:23.7672741' AS DateTime2), 5, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'47e02673-692e-47a3-823d-a10d541b0d5e', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T15:19:29.3894367' AS DateTime2), 120, N'c8d45ed0-d8d4-4c26-bc17-547145c9ff88', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'4beae196-c91a-47f8-8c8e-6a7752dc7ead', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-17T19:17:31.2513668' AS DateTime2), 60, NULL, CAST(N'2020-02-17T19:17:41.2937010' AS DateTime2), 130, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'4c4c3f7a-4649-4279-b7fa-1255058b4fdf', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T20:31:57.5088991' AS DateTime2), 1, NULL, CAST(N'2020-02-16T20:32:07.6425716' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'4e318b1f-d02b-4e7c-ab6f-8f47e4daa943', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-17T20:34:19.3439432' AS DateTime2), 30, NULL, CAST(N'2020-02-17T20:34:39.1075928' AS DateTime2), 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'4fce6e9d-464d-450f-b8d9-d517711f45bb', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-15T22:22:15.6824043' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 103, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'50ac66be-7e52-4247-bc5f-76d7a1a96a62', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-19T19:01:26.7155885' AS DateTime2), 30, N'3e1eb267-3d7e-4be9-93a0-a8b45c49471e', CAST(N'2020-02-19T19:01:34.4468845' AS DateTime2), 174, CAST(N'2020-02-19T18:15:54.4205149' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'50ded502-6b04-465f-9fad-e870ba88bcc9', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T19:51:27.3616391' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 121, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'5263b30b-5aaa-4f80-b789-591f2f24de5b', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:40:37.4169214' AS DateTime2), 30, N'9c6c036f-2a77-4995-9d52-a1591f459f99', CAST(N'2020-02-19T19:40:42.8396746' AS DateTime2), 173, CAST(N'2020-02-19T15:42:28.1456766' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'5361031e-88d6-47c3-8a6f-001eaba384c6', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-17T19:00:39.0716680' AS DateTime2), 123, NULL, CAST(N'2020-02-17T19:00:42.2068246' AS DateTime2), 126, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'5385cb48-223d-4375-b445-98933c2e4ecd', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:39:25.9636712' AS DateTime2), 30, N'27f5873c-5ae3-4dad-a942-81b9e94b1754', CAST(N'2020-02-19T19:39:34.3693918' AS DateTime2), 159, CAST(N'2020-02-18T17:53:46.5819573' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'58cd00a8-b162-490a-9eaa-c13ebb0df2a3', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:39:37.4221089' AS DateTime2), 30, N'c7e2de7e-0423-4ac2-854d-5a006a17d726', CAST(N'2020-02-19T19:39:44.5028537' AS DateTime2), 169, CAST(N'2020-02-19T14:57:06.2282826' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'5ac526da-25d9-4004-9724-67a561df58b4', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T09:29:02.5757446' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 110, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'5b161106-3990-458f-8c1e-c46d8dce2d73', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-16T11:16:52.9381134' AS DateTime2), 380, NULL, CAST(N'2020-02-16T11:16:59.2986183' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'5c462a82-b518-47f8-94db-7adb21ec5347', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T19:57:27.2801481' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 122, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'5c765839-a414-45fb-b68e-53a55aa9e471', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T09:25:29.1509853' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 107, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'60f031f1-1c97-43c8-a426-c011c76e0407', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:02:07.4208056' AS DateTime2), 30, N'3c83e97c-4c12-4b2e-a0d4-271b6378a6b7', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 138, CAST(N'2020-02-18T16:43:25.4428141' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'6428a609-7eb1-4cf8-b141-cec8991f195b', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:38:55.9687684' AS DateTime2), 30, N'a1e17a08-bcd5-486f-b1ca-61ef0bb007b8', CAST(N'2020-02-19T19:39:03.6476928' AS DateTime2), 157, CAST(N'2020-02-18T17:47:59.8504880' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'65791c5c-21cf-4fbe-ab26-c80d19fc6b27', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T11:17:47.2459305' AS DateTime2), 450, NULL, CAST(N'2020-02-16T11:18:13.0685322' AS DateTime2), 119, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'6994f4c7-904f-4947-ac48-87bf2e83a7ae', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:37:37.4200091' AS DateTime2), 30, N'df142bf0-7dca-4dbc-bf7e-9d7ad0448ed1', CAST(N'2020-02-19T19:37:42.1627529' AS DateTime2), 163, CAST(N'2020-02-19T14:40:28.8008123' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'6e21bb50-0f05-487a-874a-ae3fb7848ce3', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 60, N'44ec8923-e2d3-4e21-a3aa-76fa07d2dfaa', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 175, CAST(N'2020-02-22T17:19:52.0862360' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'73fc1769-36e7-4b5c-9fd2-594e77d8a320', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T09:45:01.4555644' AS DateTime2), 5, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'76768195-1405-4768-b3b5-656af0c35cff', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-17T21:15:37.2391058' AS DateTime2), 60, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'79d396f9-3b3d-4535-85bf-547e5516a33b', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T09:59:35.8933406' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 117, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'7af7fc8d-a3b4-4b26-87ae-7d9388755c1d', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:37:22.4193690' AS DateTime2), 30, N'074e1b8f-ffba-4e99-bd19-442f3a44ced8', CAST(N'2020-02-19T19:37:24.2449572' AS DateTime2), 143, CAST(N'2020-02-18T17:04:00.3851824' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'7ba0fc1f-5d93-4d69-b2fd-f67454b45ac8', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:32:07.4270388' AS DateTime2), 30, N'33365d4b-a689-45a5-be75-ac164e8ca503', CAST(N'2020-02-19T19:36:19.6378945' AS DateTime2), 139, CAST(N'2020-02-18T16:51:21.8813927' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'7c1032a2-1053-43ec-9f21-8e0db6a32a53', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:32:25.9692796' AS DateTime2), 30, N'12982ff9-d2b9-4195-9c42-856d769b1a5f', CAST(N'2020-02-19T19:36:21.0945794' AS DateTime2), 149, CAST(N'2020-02-18T17:20:30.1109586' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'7fd08165-4aa3-401d-951d-fda6f8c9df8b', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T20:00:42.1107356' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'8470f4e1-22dd-435a-8483-beea3b0cf961', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-17T19:11:25.2188299' AS DateTime2), 30, NULL, CAST(N'2020-02-17T19:14:31.4873288' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'84b2fbeb-ed0b-453c-b454-4fb8db4fed1d', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-15T22:36:00.6803037' AS DateTime2), 45, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 104, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'8622f450-d799-4edf-9335-2d8b5268cede', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-17T16:21:24.6598472' AS DateTime2), 1, NULL, CAST(N'2020-02-17T16:22:01.0569931' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'877cd310-fdb5-4cca-96bb-7b34cc0a59dc', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-16T20:29:20.1902302' AS DateTime2), 2, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 124, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'878a412e-986f-4e43-970e-6665d4a88722', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T20:30:07.0905398' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 123, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'8842bf8d-bce0-47ca-8706-ae7c6a1fcb3e', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T09:30:02.5767581' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 111, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'89359fa9-e128-4ecd-af49-78f89b2aaef1', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-15T22:21:00.6826988' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 102, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'8c4b0b6c-5fba-4548-9c61-858dde5ed8ed', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:36:55.9662766' AS DateTime2), 30, N'724dc141-9000-49a6-8116-fbe9747b7fa7', CAST(N'2020-02-19T19:36:57.1648096' AS DateTime2), 151, CAST(N'2020-02-18T17:23:36.0375811' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'8d764180-af87-4b0d-84a2-1d0d339f74f8', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:40:07.4222761' AS DateTime2), 60, N'b9b8186a-f492-40e6-ac80-601b475ffa1f', CAST(N'2020-02-19T19:40:14.8996716' AS DateTime2), 171, CAST(N'2020-02-19T15:14:02.4675426' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'8fe7922f-0398-43b4-848a-d6729d9f5018', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-17T19:16:52.7180735' AS DateTime2), 30, NULL, CAST(N'2020-02-17T19:17:22.2570918' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'95d96a37-7891-413a-af10-a1762a627e05', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T19:17:07.9713824' AS DateTime2), 3, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'96da2dee-f189-483e-9690-b509921379ae', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:38:10.9694512' AS DateTime2), 30, N'f4efcc78-6699-46b0-bc36-93fd8bc7c403', CAST(N'2020-02-19T19:38:17.7246429' AS DateTime2), 155, CAST(N'2020-02-18T17:40:31.9765138' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'9cc7fdab-aa7b-4737-8862-5d1501cb3034', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T10:43:55.6927624' AS DateTime2), 240, NULL, CAST(N'2020-02-16T11:14:08.5403420' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'9dc93ed8-08fd-411e-aa3a-52b999a1ec02', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T20:07:55.2243959' AS DateTime2), 1, NULL, CAST(N'2020-02-16T20:08:19.3425018' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'a4af1bb0-226a-4e8d-bb21-3523790d0394', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-19T15:19:27.1117559' AS DateTime2), 60, N'f72e2d9b-bdc4-461d-b8c0-01610d9808da', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'a4e77b6d-2170-4244-9ec3-6f0d428aee17', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:36:22.4247585' AS DateTime2), 30, N'b822b9d1-0328-42ba-879d-366688510c56', CAST(N'2020-02-19T19:36:44.2076187' AS DateTime2), 140, CAST(N'2020-02-18T16:53:37.4949915' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'a6176b15-3dcd-4290-8c51-46925f7bb601', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:39:07.4209497' AS DateTime2), 60, N'cc7286e7-feda-4c2b-9b3b-8e953c098323', CAST(N'2020-02-19T19:39:11.2632676' AS DateTime2), 167, CAST(N'2020-02-19T14:45:43.5653499' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'a633c73f-a6ec-426f-ac4d-faa2c3476601', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:37:10.9646366' AS DateTime2), 30, N'474c9449-df22-4c15-9f32-75bafac64b76', CAST(N'2020-02-19T19:37:14.3216816' AS DateTime2), 152, CAST(N'2020-02-18T17:33:40.1361911' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'a824f88b-cc81-4451-83fc-d14ae229c345', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-17T21:20:37.8717993' AS DateTime2), 60, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'a9eed0a3-20da-4d6a-b039-a80692ae2a5a', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T09:26:32.5801676' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 108, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'aa37c8c0-b2b6-428a-be9a-e22ebed2c97f', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:01:40.9741890' AS DateTime2), 30, N'9f9898da-1170-4264-bbb3-f28042046168', CAST(N'2020-02-19T19:01:43.4670319' AS DateTime2), 146, CAST(N'2020-02-18T17:14:35.0096748' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'ac6b79a3-23f4-46c1-96f2-f47d7a3ea556', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T09:32:00.3161875' AS DateTime2), 5, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'ac809342-df4d-49bf-a1a4-ef25e9c51b20', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:38:37.4217458' AS DateTime2), 30, N'85e6bfbb-8162-4442-bbb9-8c22752d6934', CAST(N'2020-02-19T19:38:55.9041966' AS DateTime2), 166, CAST(N'2020-02-19T14:42:55.3183822' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'ad473d3e-ad7c-42f9-9e09-8081da6f514b', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T15:12:57.5278439' AS DateTime2), 60, N'6415b600-70a0-4762-a146-1fbd15c3ece3', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'ada5bde1-a4c0-4039-afee-90b69f6bbfbc', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:36:52.4209070' AS DateTime2), 30, N'927e6fbf-002d-43b9-8e02-4f445508a0f6', CAST(N'2020-02-19T19:36:54.0383130' AS DateTime2), 141, CAST(N'2020-02-18T16:54:01.9626465' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'b59a4e11-77be-45a9-ba6a-395659443468', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:01:52.4215113' AS DateTime2), 30, N'e8e370fc-b10a-4100-baa8-e81b7e8806b3', CAST(N'2020-02-19T19:01:54.9862895' AS DateTime2), 137, CAST(N'2020-02-18T16:40:59.5466526' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'b7506d81-5ac2-4b8b-9c86-aa9da558fd06', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-17T18:59:46.6862646' AS DateTime2), 6, NULL, CAST(N'2020-02-17T19:00:24.4778195' AS DateTime2), 125, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'b81b58e5-13c0-402e-b842-a57b3a8328ba', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-18T16:37:11.1505345' AS DateTime2), 30, N'23a73865-ea09-4c91-9c41-a66134892669', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'b9afec2e-c21c-4bb7-b150-b6a1ce658c8d', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T11:16:28.9167406' AS DateTime2), 340, NULL, CAST(N'2020-02-16T11:17:45.6007805' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'ba93522a-ecf2-4c0a-85ae-b403bddc00e7', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T09:51:17.5735676' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 115, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'bace9ae0-8ddb-491b-94fa-dd245ba98966', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:01:11.0879973' AS DateTime2), 60, N'6f2001a8-bccd-4517-9e0d-2da6f59bc54f', CAST(N'2020-02-19T19:01:16.2975165' AS DateTime2), 144, CAST(N'2020-02-18T17:08:01.1335474' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'c0cab46b-2b66-47f9-9020-f507708a23a4', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T09:50:02.5792037' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 114, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'c0d48af9-e1d0-47d0-9808-ceedabe81829', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T19:55:26.5477023' AS DateTime2), 2, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'c2d8730c-a707-4522-ad88-e41eb7325599', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-17T19:05:48.4617866' AS DateTime2), 30, NULL, CAST(N'2020-02-17T19:06:24.6315054' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'c4201450-81f9-4db4-818e-dc1bce7092dd', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:39:40.9682313' AS DateTime2), 30, N'6d28c48a-8b07-4db1-b653-7e235c1ab1e9', CAST(N'2020-02-19T19:39:45.8058485' AS DateTime2), 160, CAST(N'2020-02-18T17:55:44.1987942' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'c54b6e4e-d520-43e8-8137-e94a465faa17', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T09:58:35.8903797' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 116, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'c9b207a8-922a-4ce6-b236-99f23e98755b', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-17T19:00:17.9558262' AS DateTime2), 60, NULL, CAST(N'2020-02-17T19:00:26.6807643' AS DateTime2), 127, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'cc15da77-f907-4fbd-b9a8-58400dadfa88', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T20:12:10.6322262' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'ccce5de7-b36b-494c-b07e-66389cfb11b0', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-22T17:06:40.7116577' AS DateTime2), 30, N'2f957ff8-2edc-47fb-8b02-a70dba6b662a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'd153974d-9647-451a-a39c-8a4b29512824', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-17T20:23:39.2889408' AS DateTime2), 120, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'd5bbf907-4355-478b-ae93-66826cf69235', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-17T20:26:34.8938965' AS DateTime2), 60, NULL, CAST(N'2020-02-17T20:43:06.9060213' AS DateTime2), 131, CAST(N'2020-02-17T20:24:04.6785217' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'd6d3397f-5dc8-4c12-b983-cd1e12247bd0', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:37:40.9662102' AS DateTime2), 30, N'5c1eba48-672b-4589-9403-d4554eb7ebf0', CAST(N'2020-02-19T19:38:06.1065585' AS DateTime2), 154, CAST(N'2020-02-18T17:39:46.3633075' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'd713b41a-7389-4263-85ba-6f07944b2154', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:01:25.9645995' AS DateTime2), 30, N'01fea4b8-a559-41bf-9e69-7139706b7541', CAST(N'2020-02-19T19:01:36.7307930' AS DateTime2), 145, CAST(N'2020-02-18T17:09:01.3020674' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'd8045c26-05d2-426f-b13f-699e150ce944', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T20:08:22.4108594' AS DateTime2), 1, NULL, CAST(N'2020-02-16T20:08:33.8783901' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'd8f0ca38-5ed0-4f86-a9b2-a380b9f28636', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:39:22.4195534' AS DateTime2), 30, N'668d35ee-4520-41ea-a664-7e0884bf22fc', CAST(N'2020-02-19T19:39:24.4977977' AS DateTime2), 168, CAST(N'2020-02-19T14:47:01.3051449' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'dcd46ff9-5d05-4273-95f2-b6561301e507', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-17T20:43:19.8909299' AS DateTime2), 30, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 132, CAST(N'2020-02-17T20:25:39.4188544' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'dfd7149f-bd2b-46af-8a12-4dc909193676', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T14:40:23.0455288' AS DateTime2), 30, N'481e1889-d739-43e6-ba75-b123920e63bc', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'e2152f7e-f5a1-4e5a-b6da-cc94cd331f7b', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:39:52.4175914' AS DateTime2), 60, N'2184638e-5caf-46d0-a8a6-8f5eca80f4dd', CAST(N'2020-02-19T19:39:55.1131360' AS DateTime2), 170, CAST(N'2020-02-19T15:13:00.6215865' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'e22b8299-59ad-492b-9758-2fd83c82735b', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-17T19:14:41.8782937' AS DateTime2), 60, NULL, CAST(N'2020-02-17T19:14:50.3856555' AS DateTime2), 129, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'e2696780-7a21-4711-b6a6-162be7b72b21', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:01:22.4210647' AS DateTime2), 30, N'7586bbb1-f645-418c-9351-a8efee8697d5', CAST(N'2020-02-19T19:01:35.4569219' AS DateTime2), 135, CAST(N'2020-02-18T16:37:16.0503602' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'e2abc5bc-65f3-4805-a18d-b6254b32ffca', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T08:59:47.7782451' AS DateTime2), 12, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 105, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'e3a70fb0-21d7-464c-bba5-fa44724e1a21', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-17T20:16:24.2246985' AS DateTime2), 45, NULL, CAST(N'2020-02-17T20:26:23.6130912' AS DateTime2), 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'e3ca3fdf-870b-4ae1-b52c-5b77fdac3abf', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:01:55.9659417' AS DateTime2), 30, N'ad8291e2-f3a3-44e6-ab53-4e778fb2a73a', CAST(N'2020-02-19T19:01:57.3207208' AS DateTime2), 147, CAST(N'2020-02-18T17:16:29.9863276' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'e41a5ba7-24c0-48b0-a188-a120cd3a2f58', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-19T19:38:22.4178170' AS DateTime2), 30, N'074acf36-a917-4b4f-beb2-510379256e0f', CAST(N'2020-02-19T19:38:25.8693522' AS DateTime2), 165, CAST(N'2020-02-19T14:42:13.4180046' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'e535d654-5a69-4256-9a90-9e3f044533fc', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-22T17:06:53.8020031' AS DateTime2), 120, N'f8744a9d-a215-453e-b66a-a1f790245c40', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'e70f1c6c-18f6-4eee-9817-52c014525655', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:39:10.9695690' AS DateTime2), 30, N'f0395179-e7a0-4f3f-8aef-5d1b78d46f6b', CAST(N'2020-02-19T19:39:13.0018767' AS DateTime2), 158, CAST(N'2020-02-18T17:51:14.7468423' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'e77d8d1b-29c3-45bd-a2a3-cbdbe13d2d79', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:38:25.9671854' AS DateTime2), 30, N'b6f58293-122c-4d0f-90b0-4e654f59596b', CAST(N'2020-02-19T19:38:54.3192408' AS DateTime2), 156, CAST(N'2020-02-18T17:42:52.4470684' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'e78e5475-5a0b-467b-823b-da7bf1c28068', N'f85300cb-07b7-4f12-9e37-f4183a07d31b', CAST(N'2020-02-16T20:27:11.9607789' AS DateTime2), 2, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'e920d7bc-1b49-4169-8ba6-a61ff9609d92', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:36:25.9705992' AS DateTime2), 30, N'0ce6bbcf-2a9a-4cdb-b277-345c3b6ba2e3', CAST(N'2020-02-19T19:36:45.8196225' AS DateTime2), 150, CAST(N'2020-02-18T17:22:25.0457839' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'e9c4cf85-cb36-4914-9c10-5c9d95d9b956', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:02:10.9666060' AS DateTime2), 30, N'37c22007-f080-49a3-8456-618256457a76', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 148, CAST(N'2020-02-18T17:18:40.6123259' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'ea68fe7f-79f1-4b69-a888-0634fcfc6a4d', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-19T18:15:43.3564399' AS DateTime2), 30, N'7ac0ac84-59b1-4755-933f-04071388a2d4', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'f19aa849-2c8d-4ed2-a6a4-9e28726ae9b6', N'fa502669-a4c9-44d9-b2c8-ec5d94dbd56c', CAST(N'2020-02-19T19:37:25.9695605' AS DateTime2), 30, N'ef1667ac-3b45-4771-a0a0-50493a996438', CAST(N'2020-02-19T19:37:36.0004117' AS DateTime2), 153, CAST(N'2020-02-18T17:34:44.0582393' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'f2751456-b6ff-480e-86a4-32fe2323e766', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T20:59:10.7782689' AS DateTime2), 60, NULL, CAST(N'2020-02-16T21:01:51.0185252' AS DateTime2), 0, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'f65460e7-bbb8-4343-9bc2-de03110774e8', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T11:14:15.7020978' AS DateTime2), 60, NULL, CAST(N'2020-02-16T11:14:25.5481388' AS DateTime2), 118, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'f72129d4-bc0e-4c24-a973-151d04dffde8', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T19:20:17.5591816' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 120, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'fb06b3f3-fb93-4dfb-9bf4-343835814d8f', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-16T09:38:02.5781924' AS DateTime2), 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 113, NULL)
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'fc6f14e2-9f5c-4843-b701-65d47cf3df09', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-19T18:58:26.3326653' AS DateTime2), 60, NULL, CAST(N'2020-02-19T19:01:15.5982363' AS DateTime2), 133, CAST(N'2020-02-17T21:20:40.1615997' AS DateTime2))
INSERT [dbo].[Sessions] ([Id], [ComputerId], [StartDate], [Duration], [PaymentId], [EndDate], [QueueNumber], [QueueDate]) VALUES (N'fe4e36c2-1f35-4c78-a299-341673d21ce9', N'bb814c94-5906-4b72-855e-c6bb93b1225d', CAST(N'2020-02-17T19:06:37.1389960' AS DateTime2), 60, NULL, CAST(N'2020-02-17T19:06:45.7758145' AS DateTime2), 128, NULL)
INSERT [dbo].[SystemSettings] ([Id], [Name], [Value], [Type]) VALUES (1, N'Queue Print Dimensions', N'400x400', N'System.String')
INSERT [dbo].[User] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'0a160b6d-5b5f-4604-911e-53a410aa76a6', N'k.esim', N'K.ESIM', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEE7IpILAxA62vzSGadfsXkQHRaWb/YHIPfVuZNYpZqIUI0o5Ve//PFSuMazQVshrtg==', N'WBCQHNY3CYA2PXIQXTZ3O237VJQBN4V5', N'3ac403ee-bc77-4292-91a3-38feee4acf8c', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[User] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'4b74d1b7-82de-4c9b-8ca3-d2ec541fb83d', N'vadrsa', N'VADRSA', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEHd2ENSrBrPx3LdLslSCWZe7kQR/pnQj4QVnpXNrTpUqU/GoYN262aeh/vsvJpDJYQ==', N'34NX7OYTTQVKXXV5TEONJ43JZ6M64UYI', N'e00495c6-7b97-4cb2-9af0-73d9d01387cc', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[User] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'90dc8b08-1405-4b08-b9ce-9173b06e7e37', N'dav.asryan', N'DAV.ASRYAN', NULL, NULL, 0, N'AQAAAAEAACcQAAAAEFIYgm+vqIGf3l+FTqgSph4laYWnjebp2d8g4Cca0neyPHkEhsMSOMMhQ82Wi9H57A==', N'F2IOJYMI42Y7XT7TOBKG37SNLTHW2H6B', N'a012bf4b-4be7-4561-b363-9f7a3440eb29', NULL, 0, 0, NULL, 1, 0)
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Games_ImageId]    Script Date: 2/24/2020 5:52:50 PM ******/
CREATE NONCLUSTERED INDEX [IX_Games_ImageId] ON [dbo].[Games]
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Computers] ADD  CONSTRAINT [DF_Computers_IsTerminated]  DEFAULT ((0)) FOR [IsTerminated]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_IsTerminated]  DEFAULT ((0)) FOR [IsTerminated]
GO
ALTER TABLE [dbo].[Games] ADD  CONSTRAINT [DF__Games__AgeLimit__7A672E12]  DEFAULT ((0)) FOR [AgeLimit]
GO
ALTER TABLE [dbo].[Computers]  WITH CHECK ADD  CONSTRAINT [FK_Computers_ComputerTypes] FOREIGN KEY([TypeId])
REFERENCES [dbo].[ComputerTypes] ([Id])
GO
ALTER TABLE [dbo].[Computers] CHECK CONSTRAINT [FK_Computers_ComputerTypes]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Employees] FOREIGN KEY([Id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Employees]
GO
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [FK_Sessions_Payments] FOREIGN KEY([PaymentId])
REFERENCES [dbo].[Payments] ([Id])
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [FK_Sessions_Payments]
GO
USE [master]
GO
ALTER DATABASE [Arcade] SET  READ_WRITE 
GO
