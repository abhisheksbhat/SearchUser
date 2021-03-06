USE [master]
GO
/****** Object:  Database [UserDB]    Script Date: 3/4/2019 10:50:09 PM ******/
CREATE DATABASE [UserDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UserDB', FILENAME = N'D:\Softwares\SQLServerSetUp\MSSQL14.MSSQLSERVER\MSSQL\DATA\UserDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'UserDB_log', FILENAME = N'D:\Softwares\SQLServerSetUp\MSSQL14.MSSQLSERVER\MSSQL\DATA\UserDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [UserDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UserDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UserDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [UserDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [UserDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [UserDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [UserDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [UserDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [UserDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [UserDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [UserDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [UserDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [UserDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [UserDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [UserDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [UserDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [UserDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [UserDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [UserDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [UserDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [UserDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [UserDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [UserDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [UserDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [UserDB] SET RECOVERY FULL 
GO
ALTER DATABASE [UserDB] SET  MULTI_USER 
GO
ALTER DATABASE [UserDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [UserDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [UserDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [UserDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [UserDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'UserDB', N'ON'
GO
ALTER DATABASE [UserDB] SET QUERY_STORE = OFF
GO
USE [UserDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [UserDB]
GO
/****** Object:  Table [dbo].[State]    Script Date: 3/4/2019 10:50:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[Name] [varchar](50) NOT NULL,
	[Code] [varchar](2) NOT NULL,
 CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/4/2019 10:50:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](20) NOT NULL,
	[StateCode] [varchar](2) NOT NULL,
	[Age] [int] NOT NULL,
	[Email] [varchar](30) NOT NULL,
	[LastName] [varchar](20) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[State] ([Name], [Code]) VALUES (N'California', N'CA')
GO
INSERT [dbo].[State] ([Name], [Code]) VALUES (N'Colorado', N'CO')
GO
INSERT [dbo].[State] ([Name], [Code]) VALUES (N'Florida', N'FL')
GO
INSERT [dbo].[State] ([Name], [Code]) VALUES (N'Georgia', N'GA')
GO
INSERT [dbo].[State] ([Name], [Code]) VALUES (N'Hawaii', N'HI')
GO
INSERT [dbo].[State] ([Name], [Code]) VALUES (N'Kansas', N'KS')
GO
INSERT [dbo].[State] ([Name], [Code]) VALUES (N'Michigan', N'MI')
GO
INSERT [dbo].[State] ([Name], [Code]) VALUES (N'Minnesota', N'MN')
GO
INSERT [dbo].[State] ([Name], [Code]) VALUES (N'Missouri', N'MO')
GO
INSERT [dbo].[State] ([Name], [Code]) VALUES (N'North Carolina', N'NC')
GO
INSERT [dbo].[State] ([Name], [Code]) VALUES (N'New Jersey', N'NJ')
GO
INSERT [dbo].[State] ([Name], [Code]) VALUES (N'New Mexico', N'NM')
GO
INSERT [dbo].[State] ([Name], [Code]) VALUES (N'New York', N'NY')
GO
INSERT [dbo].[State] ([Name], [Code]) VALUES (N'Ohio', N'OH')
GO
INSERT [dbo].[State] ([Name], [Code]) VALUES (N'Texas', N'TX')
GO
INSERT [dbo].[State] ([Name], [Code]) VALUES (N'Washington', N'WA')
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([ID], [FirstName], [StateCode], [Age], [Email], [LastName]) VALUES (1, N'Joe', N'NY', 23, N'joe.bloggs@gmail.com', N'Bloggs')
GO
INSERT [dbo].[User] ([ID], [FirstName], [StateCode], [Age], [Email], [LastName]) VALUES (2, N'Peter', N'NJ', 40, N'peter.doobes@gmail.com', N'Doobes')
GO
INSERT [dbo].[User] ([ID], [FirstName], [StateCode], [Age], [Email], [LastName]) VALUES (3, N'John', N'WA', 23, N'john.cittizen@gmail.com', N'Cittizen')
GO
INSERT [dbo].[User] ([ID], [FirstName], [StateCode], [Age], [Email], [LastName]) VALUES (4, N'Fred', N'NY', 34, N'fred.flinstone@pefin.com', N'Flinstone')
GO
INSERT [dbo].[User] ([ID], [FirstName], [StateCode], [Age], [Email], [LastName]) VALUES (5, N'Tony', N'NY', 23, N'tony.stark@starkindustries.com', N'Stark')
GO
INSERT [dbo].[User] ([ID], [FirstName], [StateCode], [Age], [Email], [LastName]) VALUES (6, N'John', N'NJ', 19, N'johndoe@pefin.com', N'Doe')
GO
INSERT [dbo].[User] ([ID], [FirstName], [StateCode], [Age], [Email], [LastName]) VALUES (7, N'Mason', N'MO', 25, N'mason@pefin.com', NULL)
GO
INSERT [dbo].[User] ([ID], [FirstName], [StateCode], [Age], [Email], [LastName]) VALUES (8, N'Jack', N'NJ', 89, N'jackd@pefin.com', N'D')
GO
INSERT [dbo].[User] ([ID], [FirstName], [StateCode], [Age], [Email], [LastName]) VALUES (9, N'Antony', N'MO', 72, N'antony@gmail.com', NULL)
GO
INSERT [dbo].[User] ([ID], [FirstName], [StateCode], [Age], [Email], [LastName]) VALUES (10, N'Scott', N'NY', 50, N'scottd@hotmail.com', N'D')
GO
INSERT [dbo].[User] ([ID], [FirstName], [StateCode], [Age], [Email], [LastName]) VALUES (11, N'Sue', N'NJ', 35, N'sue@gmail.com', N'K')
GO
INSERT [dbo].[User] ([ID], [FirstName], [StateCode], [Age], [Email], [LastName]) VALUES (12, N'Suven', N'CO', 38, N'suven@pifen.com', N'S')
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_State] FOREIGN KEY([StateCode])
REFERENCES [dbo].[State] ([Code])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_State]
GO
USE [master]
GO
ALTER DATABASE [UserDB] SET  READ_WRITE 
GO
