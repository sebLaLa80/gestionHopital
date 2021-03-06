USE [master]
GO
/****** Object:  Database [northern_lights_hospital]    Script Date: 2020-11-24 4:22:52 PM ******/
CREATE DATABASE [northern_lights_hospital]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'northern_lights_hospital', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\northern_lights_hospital.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'northern_lights_hospital_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\northern_lights_hospital_log.ldf' , SIZE = 784KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [northern_lights_hospital] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [northern_lights_hospital].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [northern_lights_hospital] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET ARITHABORT OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [northern_lights_hospital] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [northern_lights_hospital] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [northern_lights_hospital] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET  ENABLE_BROKER 
GO
ALTER DATABASE [northern_lights_hospital] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [northern_lights_hospital] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET RECOVERY FULL 
GO
ALTER DATABASE [northern_lights_hospital] SET  MULTI_USER 
GO
ALTER DATABASE [northern_lights_hospital] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [northern_lights_hospital] SET DB_CHAINING OFF 
GO
ALTER DATABASE [northern_lights_hospital] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [northern_lights_hospital] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'northern_lights_hospital', N'ON'
GO
USE [northern_lights_hospital]
GO
/****** Object:  Table [dbo].[Admission]    Script Date: 2020-11-24 4:22:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admission](
	[IDAdmission] [nchar](10) NOT NULL,
	[ChirurgieProgrammee] [nchar](30) NULL,
	[DateAdmission] [date] NULL,
	[DateChirurgie] [date] NULL,
	[DateDuConge] [date] NULL,
	[Televiseur] [bit] NOT NULL,
	[Telephone] [bit] NOT NULL,
	[NSS] [nchar](9) NULL,
	[NumeroLit] [nchar](10) NULL,
	[IDMedecin] [nchar](10) NULL,
 CONSTRAINT [PK_Admission] PRIMARY KEY CLUSTERED 
(
	[IDAdmission] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Assurance]    Script Date: 2020-11-24 4:22:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assurance](
	[IDAssurance] [nchar](10) NOT NULL,
	[NomCompagnie] [nchar](20) NULL,
 CONSTRAINT [PK_Assurance] PRIMARY KEY CLUSTERED 
(
	[IDAssurance] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Departement]    Script Date: 2020-11-24 4:22:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departement](
	[IDDepartement] [nchar](10) NOT NULL,
	[NomDepartement] [nchar](20) NULL,
 CONSTRAINT [PK_Departement] PRIMARY KEY CLUSTERED 
(
	[IDDepartement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Lit]    Script Date: 2020-11-24 4:22:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lit](
	[NumeroLit] [nchar](10) NULL,
	[Occupe] [bit] NOT NULL,
	[IDType] [nchar](10) NOT NULL,
	[IDDepartement] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Lit] PRIMARY KEY CLUSTERED 
(
	[IDType] ASC,
	[IDDepartement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Medecin]    Script Date: 2020-11-24 4:22:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medecin](
	[IDMedecin] [nchar](10) NOT NULL,
	[Nom] [nchar](20) NULL,
	[Prenom] [nchar](20) NULL,
 CONSTRAINT [PK_Medecin] PRIMARY KEY CLUSTERED 
(
	[IDMedecin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Patient]    Script Date: 2020-11-24 4:22:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[NSS] [nchar](9) NOT NULL,
	[DateNaissance] [date] NULL,
	[Nom] [nchar](20) NULL,
	[Prenom] [nchar](20) NULL,
	[Adresse] [nchar](30) NULL,
	[Ville] [nchar](10) NULL,
	[Province] [nchar](10) NULL,
	[CodePostal] [nchar](10) NULL,
	[Telephone] [nchar](11) NULL,
	[IDAssurance] [nchar](10) NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[NSS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TypeLit]    Script Date: 2020-11-24 4:22:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeLit](
	[IDType] [nchar](10) NOT NULL,
	[Description] [nchar](20) NULL,
 CONSTRAINT [PK_TypeLit] PRIMARY KEY CLUSTERED 
(
	[IDType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[Admission] ([IDAdmission], [ChirurgieProgrammee], [DateAdmission], [DateChirurgie], [DateDuConge], [Televiseur], [Telephone], [NSS], [NumeroLit], [IDMedecin]) VALUES (N'1         ', N'Rhinoplastie                  ', CAST(N'2020-11-24' AS Date), CAST(N'2020-11-25' AS Date), CAST(N'2020-11-26' AS Date), 1, 1, N'111111111', N'1         ', N'666       ')
INSERT [dbo].[Assurance] ([IDAssurance], [NomCompagnie]) VALUES (N'1         ', N'Great West          ')
INSERT [dbo].[Assurance] ([IDAssurance], [NomCompagnie]) VALUES (N'2         ', N'OnMeurtTousUnJour   ')
INSERT [dbo].[Assurance] ([IDAssurance], [NomCompagnie]) VALUES (N'3         ', N'CheapInsu           ')
INSERT [dbo].[Departement] ([IDDepartement], [NomDepartement]) VALUES (N'1         ', N'Chirurgie           ')
INSERT [dbo].[Departement] ([IDDepartement], [NomDepartement]) VALUES (N'2         ', N'Psychiatrie         ')
INSERT [dbo].[Lit] ([NumeroLit], [Occupe], [IDType], [IDDepartement]) VALUES (N'1         ', 1, N'1         ', N'1         ')
INSERT [dbo].[Lit] ([NumeroLit], [Occupe], [IDType], [IDDepartement]) VALUES (N'2         ', 0, N'2         ', N'2         ')
INSERT [dbo].[Medecin] ([IDMedecin], [Nom], [Prenom]) VALUES (N'666       ', N'Pepper              ', N'Alfred              ')
INSERT [dbo].[Medecin] ([IDMedecin], [Nom], [Prenom]) VALUES (N'777       ', N'Dufour              ', N'Julie               ')
INSERT [dbo].[Patient] ([NSS], [DateNaissance], [Nom], [Prenom], [Adresse], [Ville], [Province], [CodePostal], [Telephone], [IDAssurance]) VALUES (N'111111111', CAST(N'1970-01-01' AS Date), N'Tremblay            ', N'Paul                ', N'123 rue Pins                  ', N'Laval     ', N'QC        ', N'H0G 0H0   ', N'14502221111', N'1         ')
INSERT [dbo].[Patient] ([NSS], [DateNaissance], [Nom], [Prenom], [Adresse], [Ville], [Province], [CodePostal], [Telephone], [IDAssurance]) VALUES (N'222222222', CAST(N'1980-02-02' AS Date), N'Filali              ', N'Othmane             ', N'456 Cote St-Luc               ', N'Montreal  ', N'QC        ', N'F2R 1J6   ', N'15145555555', N'2         ')
INSERT [dbo].[TypeLit] ([IDType], [Description]) VALUES (N'1         ', N'prive               ')
INSERT [dbo].[TypeLit] ([IDType], [Description]) VALUES (N'2         ', N'semi-prive          ')
INSERT [dbo].[TypeLit] ([IDType], [Description]) VALUES (N'3         ', N'standard            ')
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Lit]    Script Date: 2020-11-24 4:22:52 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Lit] ON [dbo].[Lit]
(
	[NumeroLit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Lit] FOREIGN KEY([NumeroLit])
REFERENCES [dbo].[Lit] ([NumeroLit])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Lit]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Medecin] FOREIGN KEY([IDMedecin])
REFERENCES [dbo].[Medecin] ([IDMedecin])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Medecin]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Patient] FOREIGN KEY([NSS])
REFERENCES [dbo].[Patient] ([NSS])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Patient]
GO
ALTER TABLE [dbo].[Lit]  WITH CHECK ADD  CONSTRAINT [FK_Lit_Departement] FOREIGN KEY([IDDepartement])
REFERENCES [dbo].[Departement] ([IDDepartement])
GO
ALTER TABLE [dbo].[Lit] CHECK CONSTRAINT [FK_Lit_Departement]
GO
ALTER TABLE [dbo].[Lit]  WITH CHECK ADD  CONSTRAINT [FK_Lit_TypeLit] FOREIGN KEY([IDType])
REFERENCES [dbo].[TypeLit] ([IDType])
GO
ALTER TABLE [dbo].[Lit] CHECK CONSTRAINT [FK_Lit_TypeLit]
GO
ALTER TABLE [dbo].[Patient]  WITH CHECK ADD  CONSTRAINT [FK_Patient_Assurance] FOREIGN KEY([IDAssurance])
REFERENCES [dbo].[Assurance] ([IDAssurance])
GO
ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [FK_Patient_Assurance]
GO
USE [master]
GO
ALTER DATABASE [northern_lights_hospital] SET  READ_WRITE 
GO
