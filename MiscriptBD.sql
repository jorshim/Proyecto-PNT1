USE [master]
GO

CREATE DATABASE [ClinicaDatabaseV2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ClinicaDatabaseV2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ClinicaDatabaseV2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ClinicaDatabaseV2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ClinicaDatabaseV2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ClinicaDatabaseV2] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ClinicaDatabaseV2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ClinicaDatabaseV2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET ARITHABORT OFF 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET RECOVERY FULL 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET  MULTI_USER 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ClinicaDatabaseV2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ClinicaDatabaseV2] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ClinicaDatabaseV2', N'ON'
GO
ALTER DATABASE [ClinicaDatabaseV2] SET QUERY_STORE = OFF
GO
USE [ClinicaDatabaseV2]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estudios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TipoEstudio] [int] NOT NULL,
 CONSTRAINT [PK_Estudios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medicos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Matricula] [int] NOT NULL,
	[Nombre] [nvarchar](30) NOT NULL,
	[Apellido] [nvarchar](30) NOT NULL,
	[Especialidad] [int] NOT NULL,
 CONSTRAINT [PK_Medicos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pacientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Documento] [int] NOT NULL,
	[Nombre] [nvarchar](30) NOT NULL,
	[Apellido] [nvarchar](30) NOT NULL,
	[FechaDeNacimiento] [datetime2](7) NOT NULL,
	[ObraSocial] [nvarchar](15) NOT NULL,
	[NroAfiliado] [int] NOT NULL,
	[Telefono] [nvarchar](max) NOT NULL,
	[CorreoElectronico] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Pacientes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TurnoConsultaMedica](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPaciente] [int] NOT NULL,
	[DiasDisponibles] [int] NOT NULL,
	[HorasDisponibles] [int] NOT NULL,
	[IdMedico] [int] NOT NULL,
	[FechaConsultaMedica] [nvarchar](max) NULL,
	[DocumentoPaciente] [int] NOT NULL,
 CONSTRAINT [PK_TurnoConsultaMedica] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TurnoPracticaMedica](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPaciente] [int] NOT NULL,
	[DiasDisponibles] [int] NOT NULL,
	[HorasDisponibles] [int] NOT NULL,
	[IdPracticaMedica] [int] NOT NULL,
	[FechaConsultaMedica] [nvarchar](max) NULL,
	[DocumentoPaciente] [int] NOT NULL,
 CONSTRAINT [PK_TurnoPracticaMedica] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211109160843_jar_Proyecto_PNT1.V2.Context.ClinicaDatabaseV2Context', N'3.1.19')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211109224220_jar_Proyecto_PNT1.V2.Context.ClinicaDatabaseV2Context02', N'3.1.20')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211121234315_jar_Proyecto_PNT1.V2.Context.ClinicaDatabaseV2Context03', N'3.1.21')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20211121234816_jar_Proyecto_PNT1.V2ContextClinicaDatabaseV2Context04', N'3.1.21')
GO
SET IDENTITY_INSERT [dbo].[Medicos] ON 

INSERT [dbo].[Medicos] ([Id], [Matricula], [Nombre], [Apellido], [Especialidad]) VALUES (4, 111112, N'Ana', N'Diaz', 0)
INSERT [dbo].[Medicos] ([Id], [Matricula], [Nombre], [Apellido], [Especialidad]) VALUES (5, 1123343, N'Pablo', N'Smith', 1)
INSERT [dbo].[Medicos] ([Id], [Matricula], [Nombre], [Apellido], [Especialidad]) VALUES (6, 1123211, N'Adriana', N'Sosa', 2)
INSERT [dbo].[Medicos] ([Id], [Matricula], [Nombre], [Apellido], [Especialidad]) VALUES (7, 1123667, N'Camilo', N'Paz', 3)
INSERT [dbo].[Medicos] ([Id], [Matricula], [Nombre], [Apellido], [Especialidad]) VALUES (8, 1198932, N'Rosa', N'Herrera', 4)
INSERT [dbo].[Medicos] ([Id], [Matricula], [Nombre], [Apellido], [Especialidad]) VALUES (9, 1157465, N'Ana Clara', N'Bonano', 5)
INSERT [dbo].[Medicos] ([Id], [Matricula], [Nombre], [Apellido], [Especialidad]) VALUES (10, 1173623, N'Ezequiel', N'Alvarez', 6)
INSERT [dbo].[Medicos] ([Id], [Matricula], [Nombre], [Apellido], [Especialidad]) VALUES (11, 1231145, N'Vanesa', N'Gordillo', 7)
INSERT [dbo].[Medicos] ([Id], [Matricula], [Nombre], [Apellido], [Especialidad]) VALUES (12, 1236143, N'Jorge', N'Carrascal', 3)
SET IDENTITY_INSERT [dbo].[Medicos] OFF
GO
SET IDENTITY_INSERT [dbo].[Pacientes] ON 

INSERT [dbo].[Pacientes] ([Id], [Documento], [Nombre], [Apellido], [FechaDeNacimiento], [ObraSocial], [NroAfiliado], [Telefono], [CorreoElectronico]) VALUES (1, 36528288, N'jorge', N'rodriguez', CAST(N'2021-11-03T18:45:00.0000000' AS DateTime2), N'432432432432432', 111111, N'01158136794', N'jor.rodri92@gmail.com')
INSERT [dbo].[Pacientes] ([Id], [Documento], [Nombre], [Apellido], [FechaDeNacimiento], [ObraSocial], [NroAfiliado], [Telefono], [CorreoElectronico]) VALUES (4, 36528282, N'jorge', N'diaz', CAST(N'2021-11-04T19:49:00.0000000' AS DateTime2), N'4324324324324', 1111112, N'123456', N'1234@gmail.com')
INSERT [dbo].[Pacientes] ([Id], [Documento], [Nombre], [Apellido], [FechaDeNacimiento], [ObraSocial], [NroAfiliado], [Telefono], [CorreoElectronico]) VALUES (5, 11111111, N'Peppa', N'Pig', CAST(N'2021-11-02T22:26:00.0000000' AS DateTime2), N'OSDE310', 123456, N'00000000', N'peppa@pig.com')
INSERT [dbo].[Pacientes] ([Id], [Documento], [Nombre], [Apellido], [FechaDeNacimiento], [ObraSocial], [NroAfiliado], [Telefono], [CorreoElectronico]) VALUES (6, 95501505, N'Juana', N'Perez', CAST(N'1988-09-03T00:45:00.0000000' AS DateTime2), N'OSDE310', 654321, N'1165658787', N'jperez@yahoo.com.ar')
INSERT [dbo].[Pacientes] ([Id], [Documento], [Nombre], [Apellido], [FechaDeNacimiento], [ObraSocial], [NroAfiliado], [Telefono], [CorreoElectronico]) VALUES (7, 32456987, N'Claudia', N'Gomez', CAST(N'1972-09-03T00:45:00.0000000' AS DateTime2), N'OSDE410', 889001, N'1112348787', N'cgomez@icloud.com')
INSERT [dbo].[Pacientes] ([Id], [Documento], [Nombre], [Apellido], [FechaDeNacimiento], [ObraSocial], [NroAfiliado], [Telefono], [CorreoElectronico]) VALUES (8, 11233443, N'Roberto', N'Santillana', CAST(N'1965-09-03T00:45:00.0000000' AS DateTime2), N'GALENO100', 97845, N'1156987432', N'titosantillana@gmail.com')
INSERT [dbo].[Pacientes] ([Id], [Documento], [Nombre], [Apellido], [FechaDeNacimiento], [ObraSocial], [NroAfiliado], [Telefono], [CorreoElectronico]) VALUES (9, 22654789, N'Florencia', N'Salas', CAST(N'1977-09-03T00:45:00.0000000' AS DateTime2), N'GALENO200', 332344, N'1143219876', N'fsalas@yahoo.com.ar')
INSERT [dbo].[Pacientes] ([Id], [Documento], [Nombre], [Apellido], [FechaDeNacimiento], [ObraSocial], [NroAfiliado], [Telefono], [CorreoElectronico]) VALUES (10, 15677844, N'Marcelo', N'Gutman', CAST(N'1973-09-03T00:45:00.0000000' AS DateTime2), N'OSDE210', 776321, N'1123345667', N'mgutman@icloud.com')
INSERT [dbo].[Pacientes] ([Id], [Documento], [Nombre], [Apellido], [FechaDeNacimiento], [ObraSocial], [NroAfiliado], [Telefono], [CorreoElectronico]) VALUES (11, 30987123, N'Camila', N'Fiorini', CAST(N'1990-09-03T00:45:00.0000000' AS DateTime2), N'SWISSMEDICAL10', 443887, N'1160129345', N'cfiorini@yahoo.com.ar')
INSERT [dbo].[Pacientes] ([Id], [Documento], [Nombre], [Apellido], [FechaDeNacimiento], [ObraSocial], [NroAfiliado], [Telefono], [CorreoElectronico]) VALUES (12, 39008711, N'Miguel', N'Caviria', CAST(N'1995-09-03T00:45:00.0000000' AS DateTime2), N'OSDE310', 812340, N'1161092877', N'miguelc@gmail.com')
INSERT [dbo].[Pacientes] ([Id], [Documento], [Nombre], [Apellido], [FechaDeNacimiento], [ObraSocial], [NroAfiliado], [Telefono], [CorreoElectronico]) VALUES (13, 23723459, N'Fatima', N'Rodriguez', CAST(N'1978-09-03T00:45:00.0000000' AS DateTime2), N'OSDE210', 123558, N'1166657899', N'fatima.rodriguez@yahoo.com.ar')
INSERT [dbo].[Pacientes] ([Id], [Documento], [Nombre], [Apellido], [FechaDeNacimiento], [ObraSocial], [NroAfiliado], [Telefono], [CorreoElectronico]) VALUES (14, 30901108, N'Pablo', N'Pardini', CAST(N'1982-09-03T00:45:00.0000000' AS DateTime2), N'OSDE410', 534798, N'1160902331', N'ppardini@icloud.com')
SET IDENTITY_INSERT [dbo].[Pacientes] OFF
GO
SET IDENTITY_INSERT [dbo].[TurnoConsultaMedica] ON 

INSERT [dbo].[TurnoConsultaMedica] ([Id], [IdPaciente], [DiasDisponibles], [HorasDisponibles], [IdMedico], [FechaConsultaMedica], [DocumentoPaciente]) VALUES (2, 1, 1, 4, 6, N'28/11/2022 4:00', 36528288)
INSERT [dbo].[TurnoConsultaMedica] ([Id], [IdPaciente], [DiasDisponibles], [HorasDisponibles], [IdMedico], [FechaConsultaMedica], [DocumentoPaciente]) VALUES (4, 1, 4, 0, 2, N'02/12/2022 0:00', 36528288)
SET IDENTITY_INSERT [dbo].[TurnoConsultaMedica] OFF
GO
SET IDENTITY_INSERT [dbo].[TurnoPracticaMedica] ON 

INSERT [dbo].[TurnoPracticaMedica] ([Id], [IdPaciente], [DiasDisponibles], [HorasDisponibles], [IdPracticaMedica], [FechaConsultaMedica], [DocumentoPaciente]) VALUES (1, 1, 1, 0, 0, N'29/11/2022 0:00', 36528288)
SET IDENTITY_INSERT [dbo].[TurnoPracticaMedica] OFF
GO
USE [master]
GO
ALTER DATABASE [ClinicaDatabaseV2] SET  READ_WRITE 
GO
