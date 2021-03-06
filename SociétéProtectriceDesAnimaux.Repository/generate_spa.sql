CREATE DATABASE SPA
GO

USE [master]
GO
CREATE LOGIN [SPA] WITH PASSWORD=N'chien', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

USE [SPA]
GO
/****** Object:  User [SPA]    Script Date: 27/07/2021 18:24:38 ******/
CREATE USER [SPA] FOR LOGIN [spa] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [SPA]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [SPA]
GO
/****** Object:  Table [dbo].[Animal]    Script Date: 27/07/2021 18:24:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Animal](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NULL,
	[Type] [int] NULL,
	LastFoodingTime DATETIME NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Niche]    Script Date: 27/07/2021 18:24:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Niche](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NicheAnimal]    Script Date: 27/07/2021 18:24:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NicheAnimal](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdNiche] [int] NOT NULL,
	[IdAnimal] [int] NOT NULL
) ON [PRIMARY]
GO
