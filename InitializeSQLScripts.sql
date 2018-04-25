USE [master]
GO
/****** Object:  Database [tnext]    Script Date: 18.04.2018 10:08:41 ******/
CREATE DATABASE [tnext]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'tnext', FILENAME = N'/var/opt/mssql/data/tnext.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'tnext_log', FILENAME = N'/var/opt/mssql/data/tnext_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [tnext] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [tnext].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [tnext] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [tnext] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [tnext] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [tnext] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [tnext] SET ARITHABORT OFF 
GO
ALTER DATABASE [tnext] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [tnext] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [tnext] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [tnext] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [tnext] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [tnext] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [tnext] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [tnext] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [tnext] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [tnext] SET  DISABLE_BROKER 
GO
ALTER DATABASE [tnext] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [tnext] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [tnext] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [tnext] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [tnext] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [tnext] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [tnext] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [tnext] SET RECOVERY FULL 
GO
ALTER DATABASE [tnext] SET  MULTI_USER 
GO
ALTER DATABASE [tnext] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [tnext] SET DB_CHAINING OFF 
GO
ALTER DATABASE [tnext] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [tnext] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [tnext] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [tnext] SET QUERY_STORE = OFF
GO
USE [tnext]
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
USE [tnext]
GO
/****** Object:  Table [dbo].[ApplicationGroup]    Script Date: 18.04.2018 10:08:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [nvarchar](100) NULL,
	[GroupName] [nvarchar](255) NULL,
 CONSTRAINT [PK_ApplicationGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Configuration]    Script Date: 18.04.2018 10:08:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Configuration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Group] [nvarchar](255) NULL,
	[ApplicationName] [nvarchar](100) NOT NULL,
	[Environment] [nvarchar](20) NOT NULL,
	[Key] [nvarchar](255) NOT NULL,
	[Value] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_Configuration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HealthServiceInformation]    Script Date: 18.04.2018 10:08:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HealthServiceInformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[HealthUrl] [nvarchar](255) NOT NULL,
	[Protochol] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parameter]    Script Date: 18.04.2018 10:08:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parameter](
	[Id] [int] NOT NULL,
	[Group] [nvarchar](50) NOT NULL,
	[Key] [nvarchar](50) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[Code1] [nvarchar](max) NULL,
	[Code2] [nvarchar](max) NULL,
	[Code3] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequestLog]    Script Date: 18.04.2018 10:08:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RequestId] [varchar](100) NULL,
	[Host] [varchar](250) NULL,
	[Url] [varchar](250) NULL,
	[ClientIp] [varchar](50) NULL,
	[RequestHeaders] [text] NULL,
	[RequestBody] [text] NULL,
	[ResponseHeaders] [text] NULL,
	[ResponseBody] [text] NULL,
	[RequestTime] [datetime] NULL,
	[ResponseTime] [datetime] NULL,
	[ResponseStatus] [text] NULL,
	[Description] [text] NULL,
	[IsSuccess] [bit] NULL,
 CONSTRAINT [PK_RequestLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 85) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Configuration] ON 
GO
INSERT [dbo].[Configuration] ([Id], [Group], [ApplicationName], [Environment], [Key], [Value]) VALUES (4, N'ALL_MICROSERVICES', N'', N'LOCAL', N'tNext.Microservices.Campaign.Api.Url', N'http://localhost/tNext.Microservices.Campaign')
GO
INSERT [dbo].[Configuration] ([Id], [Group], [ApplicationName], [Environment], [Key], [Value]) VALUES (7, N'ALL_MICROSERVICES', N'', N'LOCAL', N'tNext.Microservices.Configuration.Api.Url', N'http://localhost/tNext.Microservices.Configuration')
GO
INSERT [dbo].[Configuration] ([Id], [Group], [ApplicationName], [Environment], [Key], [Value]) VALUES (8, N'ALL_MICROSERVICES', N'', N'LOCAL', N'tNext.Microservices.Customer.Api.Url', N'http://localhost/tNext.Microservices.Customer')
GO
INSERT [dbo].[Configuration] ([Id], [Group], [ApplicationName], [Environment], [Key], [Value]) VALUES (9, N'ALL_MICROSERVICES', N'', N'LOCAL', N'tNext.Microservices.Parameter.Api.Url', N'http://localhost/tNext.Microservices.Parameter')
GO
INSERT [dbo].[Configuration] ([Id], [Group], [ApplicationName], [Environment], [Key], [Value]) VALUES (10, N'ALL_MICROSERVICES', N'', N'LOCAL', N'tNext.Microservices.Environment.Api.Url', N'http://localhost/tNext.Microservices.Environment')
GO
INSERT [dbo].[Configuration] ([Id], [Group], [ApplicationName], [Environment], [Key], [Value]) VALUES (11, N'ALL_MICROSERVICES', N'', N'LOCAL', N'tNext.Microservices.Health.Api.Url', N'http://localhost/tNext.Microservices.Health')
GO
INSERT [dbo].[Configuration] ([Id], [Group], [ApplicationName], [Environment], [Key], [Value]) VALUES (12, N'ALL_MICROSERVICES', N'', N'LOCAL', N'tNext.Bff.Mobile.Api.Url', N'http://localhost/tNext.Bff.Mobile')
GO
SET IDENTITY_INSERT [dbo].[Configuration] OFF
GO
/****** Object:  StoredProcedure [dbo].[tNext_sp_GetConfiguration]    Script Date: 18.04.2018 10:08:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[tNext_sp_GetConfiguration]
	@ApplicationName	VARCHAR(100)='XXX',
	@Environment		VARCHAR(100)='XXX',
	@Key				VARCHAR(100)=NULL
AS
BEGIN
      SELECT [Id]
			,[Group]
			,[ApplicationName]
			,[Environment]
			,[Key]
			,[Value]  
	  FROM	[Configuration] (NOLOCK)
	  WHERE (ApplicationName=@ApplicationName			
			OR [Group] IN (
				SELECT DISTINCT(GroupName) FROM [dbo].[ApplicationGroup] WITH(NOLOCK) 
				WHERE ApplicationName=@ApplicationName
			)
			OR [Group] = 'ALL_MICROSERVICES'
			)
			AND Environment=@Environment
			AND ( @Key IS NULL OR @Key = [Key] )	  
END



GO
/****** Object:  StoredProcedure [dbo].[tNext_sp_GetHealthData]    Script Date: 18.04.2018 10:08:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[tNext_sp_GetHealthData]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		 [Name]
		,[HealthUrl]
		,[Protochol]

	FROM [dbo].[HealthServiceInformation] (NOLOCK)
END


GO
/****** Object:  StoredProcedure [dbo].[tNext_sp_GetParameter]    Script Date: 18.04.2018 10:08:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[tNext_sp_GetParameter] 

			@Group nvarchar(50)='',
			@Key nvarchar(50)=NULL
--EXEC [dbo].[sp_GetParameter] 'sehir'
AS
BEGIN
   
	SET NOCOUNT ON;

	SELECT	[Id]
			,[Group]
			,[Key]
			,[Value]
			,[Code1]
			,[Code2]
			,[Code3]
			,[Description]
	FROM	[dbo].[Parameter] with(nolock)
	WHERE 
			(ISNULL( @Key,'') ='' AND  [Group] = @Group )
			OR 
			( ISNULL( @Key,'')<>'' AND  [Group] = @Group AND [Key]=@Key )
			OR
			@Group = 'ALL'

END


GO
USE [master]
GO
ALTER DATABASE [tnext] SET  READ_WRITE 
GO
