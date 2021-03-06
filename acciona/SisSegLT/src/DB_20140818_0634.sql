USE [master]
GO
/****** Object:  Database [ASODENICDB]    Script Date: 18/08/2014 06:34:23 a.m. ******/
CREATE DATABASE [ASODENICDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ASODENICDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.GALHERSQLSERVER\MSSQL\DATA\ASODENICDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'ASODENICDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.GALHERSQLSERVER\MSSQL\DATA\ASODENICDB_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ASODENICDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ASODENICDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ASODENICDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ASODENICDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ASODENICDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ASODENICDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ASODENICDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ASODENICDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ASODENICDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ASODENICDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ASODENICDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ASODENICDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ASODENICDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ASODENICDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ASODENICDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ASODENICDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ASODENICDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ASODENICDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ASODENICDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ASODENICDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ASODENICDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ASODENICDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ASODENICDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ASODENICDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ASODENICDB] SET RECOVERY FULL 
GO
ALTER DATABASE [ASODENICDB] SET  MULTI_USER 
GO
ALTER DATABASE [ASODENICDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ASODENICDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ASODENICDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ASODENICDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ASODENICDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ASODENICDB', N'ON'
GO
USE [ASODENICDB]
GO
/****** Object:  Schema [Auditoria]    Script Date: 18/08/2014 06:34:23 a.m. ******/
CREATE SCHEMA [Auditoria]
GO
/****** Object:  Schema [Catalogo]    Script Date: 18/08/2014 06:34:23 a.m. ******/
CREATE SCHEMA [Catalogo]
GO
/****** Object:  Schema [seguridad]    Script Date: 18/08/2014 06:34:23 a.m. ******/
CREATE SCHEMA [seguridad]
GO
/****** Object:  Table [Auditoria].[Auditoria]    Script Date: 18/08/2014 06:34:23 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Auditoria].[Auditoria](
	[IdAuditoria] [uniqueidentifier] NOT NULL,
	[IdAfectado] [uniqueidentifier] NOT NULL,
	[IdReferencia] [uniqueidentifier] NULL,
	[FechaAfectacion] [datetime] NOT NULL,
	[IdTipo] [int] NOT NULL,
	[IdTabla] [int] NOT NULL,
	[Comentario] [nvarchar](max) NOT NULL,
	[Usuario] [nvarchar](50) NULL,
	[UsuarioPC] [nvarchar](50) NULL,
	[UsuarioIP] [nvarchar](50) NULL,
 CONSTRAINT [PK_Auditoria] PRIMARY KEY CLUSTERED 
(
	[IdAuditoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[Categoria]    Script Date: 18/08/2014 06:34:23 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Categoria](
	[IdCategoria] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Categoria_IdCategoria]  DEFAULT (newid()),
	[IdFactor] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Ponderacion] [int] NOT NULL,
	[EsActivo] [bit] NOT NULL CONSTRAINT [DF__Categoria__EsAct__0C85DE4D]  DEFAULT ((1)),
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[Clasificacion]    Script Date: 18/08/2014 06:34:23 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Clasificacion](
	[IdClasificacion] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Clasificacion_IdClasificacion]  DEFAULT (newid()),
	[IdCategoria] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Puntuacion] [int] NOT NULL,
	[EsActivo] [bit] NOT NULL CONSTRAINT [DF__Clasifica__EsAct__0F624AF8]  DEFAULT ((1)),
 CONSTRAINT [PK_Clasificacion] PRIMARY KEY CLUSTERED 
(
	[IdClasificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[Factor]    Script Date: 18/08/2014 06:34:23 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Factor](
	[IdFactor] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Factor_IdFactor]  DEFAULT (newid()),
	[Nombre] [nvarchar](100) NOT NULL,
	[EsActivo] [bit] NOT NULL CONSTRAINT [DF__Factor__EsActivo__123EB7A3]  DEFAULT ((1)),
 CONSTRAINT [PK_Factor] PRIMARY KEY CLUSTERED 
(
	[IdFactor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[MatrizCalificacion]    Script Date: 18/08/2014 06:34:23 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[MatrizCalificacion](
	[IdMatrizCalificacion] [uniqueidentifier] NOT NULL CONSTRAINT [DF_MatrizCalificacion_IdMatrizCalificacion]  DEFAULT (newid()),
	[Nombre] [nvarchar](100) NOT NULL CONSTRAINT [DF_MatrizCalificacion_Nombre]  DEFAULT (''),
	[ValorMin] [decimal](10, 2) NOT NULL,
	[ValorMax] [decimal](10, 2) NOT NULL,
	[Impacto] [int] NOT NULL,
	[EsActivo] [bit] NOT NULL CONSTRAINT [DF__MatrizCal__EsAct__151B244E]  DEFAULT ((1)),
 CONSTRAINT [PK_MatrizCalificacion] PRIMARY KEY CLUSTERED 
(
	[IdMatrizCalificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [Catalogo].[Metodologia]    Script Date: 18/08/2014 06:34:23 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Metodologia](
	[IdMetodologia] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Metodologia_IdMetodologia]  DEFAULT (newid()),
	[Nombre] [nvarchar](100) NOT NULL,
	[EsActivo] [bit] NOT NULL,
 CONSTRAINT [PK_Metodologia] PRIMARY KEY CLUSTERED 
(
	[IdMetodologia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [seguridad].[Ciudad]    Script Date: 18/08/2014 06:34:23 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [seguridad].[Ciudad](
	[IdCiudad] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Ciudad_IdCiudad]  DEFAULT (newid()),
	[IdPais] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL CONSTRAINT [DF_Ciudad_Nombre]  DEFAULT (''),
	[EsActivo] [bit] NOT NULL CONSTRAINT [DF_Ciudad_EsActivo]  DEFAULT ((1)),
 CONSTRAINT [PK_Ciudad] PRIMARY KEY CLUSTERED 
(
	[IdCiudad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [seguridad].[ItemMenu]    Script Date: 18/08/2014 06:34:23 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [seguridad].[ItemMenu](
	[IdItemMenu] [uniqueidentifier] NOT NULL CONSTRAINT [DF_ItemMenu_IdItemMenu]  DEFAULT (newid()),
	[IdItemMenuPadre] [uniqueidentifier] NULL,
	[Ruta] [varchar](256) NOT NULL CONSTRAINT [DF_ItemMenu_Ruta]  DEFAULT ('~/'),
	[Texto] [nvarchar](50) NOT NULL CONSTRAINT [DF_ItemMenu_Texto]  DEFAULT (''),
	[Descripcion] [nvarchar](256) NOT NULL CONSTRAINT [DF_ItemMenu_Descripcion]  DEFAULT (''),
	[Visible] [bit] NOT NULL CONSTRAINT [DF_ItemMenu_Visible]  DEFAULT ((0)),
	[FechaRegistro] [datetime] NULL CONSTRAINT [DF_ItemMenu_FechaRegistro]  DEFAULT (getdate()),
	[EsActivo] [bit] NOT NULL CONSTRAINT [DF_ItemMenu_EsActivo]  DEFAULT ((1)),
 CONSTRAINT [PK_ItemMenu] PRIMARY KEY CLUSTERED 
(
	[IdItemMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [seguridad].[ItemRol]    Script Date: 18/08/2014 06:34:23 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [seguridad].[ItemRol](
	[IdItemRol] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Table_1_IdItemMenu]  DEFAULT (newid()),
	[IdItemMenu] [uniqueidentifier] NOT NULL,
	[IdRol] [uniqueidentifier] NOT NULL,
	[FechaRegistro] [datetime] NULL CONSTRAINT [DF_ItemRol_FechaRegistro]  DEFAULT (getdate()),
	[EsActivo] [bit] NOT NULL CONSTRAINT [DF_ItemRol_EsActivo]  DEFAULT ((1)),
 CONSTRAINT [PK_ItemRol] PRIMARY KEY CLUSTERED 
(
	[IdItemRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [seguridad].[Pais]    Script Date: 18/08/2014 06:34:23 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [seguridad].[Pais](
	[IdPais] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Pais_IdPais]  DEFAULT (newid()),
	[Nombre] [nvarchar](100) NOT NULL,
	[Nacionalidad] [nvarchar](100) NULL CONSTRAINT [DF_Pais_Gentilicio]  DEFAULT (''),
	[EsActivo] [bit] NOT NULL CONSTRAINT [DF_Pais_EsActivo]  DEFAULT ((1)),
 CONSTRAINT [PK_Pais] PRIMARY KEY CLUSTERED 
(
	[IdPais] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [seguridad].[Rol]    Script Date: 18/08/2014 06:34:23 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [seguridad].[Rol](
	[IdRol] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Rol_IdRol]  DEFAULT (newid()),
	[Nombre] [nvarchar](100) NOT NULL CONSTRAINT [DF_Rol_Nombre]  DEFAULT (''),
	[FechaRegistro] [datetime] NULL CONSTRAINT [DF_Rol_FechaRegistro]  DEFAULT (getdate()),
	[EsActivo] [bit] NOT NULL CONSTRAINT [DF_Rol_EsActivo]  DEFAULT ((1)),
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [seguridad].[Sucursal]    Script Date: 18/08/2014 06:34:23 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [seguridad].[Sucursal](
	[IdSucursal] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Sucursal_IdSucursal]  DEFAULT (newid()),
	[IdCiudad] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL CONSTRAINT [DF_Sucursal_Nombre]  DEFAULT (''),
	[Codigo] [nvarchar](20) NOT NULL CONSTRAINT [DF_Sucursal_Codigo]  DEFAULT (''),
	[Direccion] [nvarchar](150) NOT NULL CONSTRAINT [DF_Sucursal_Direccion]  DEFAULT (''),
	[EsActivo] [bit] NOT NULL CONSTRAINT [DF_Sucursal_EsActivo]  DEFAULT ((1)),
 CONSTRAINT [PK_Sucursal] PRIMARY KEY CLUSTERED 
(
	[IdSucursal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [seguridad].[Usuario]    Script Date: 18/08/2014 06:34:23 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [seguridad].[Usuario](
	[IdUsuario] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Usuario_IdUsuario]  DEFAULT (newid()),
	[IdSucursal] [uniqueidentifier] NOT NULL,
	[IdRol] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL CONSTRAINT [DF_Usuario_Nombre]  DEFAULT (''),
	[Sexo] [nvarchar](1) NOT NULL,
	[Login] [varchar](20) NOT NULL CONSTRAINT [DF_Usuario_Login]  DEFAULT (''),
	[Pass] [nvarchar](512) NOT NULL CONSTRAINT [DF_Usuario_Pass]  DEFAULT (''),
	[Cargo] [nvarchar](100) NOT NULL CONSTRAINT [DF_Usuario_Cargo]  DEFAULT (''),
	[Codigo] [nvarchar](20) NOT NULL CONSTRAINT [DF_Usuario_Codigo]  DEFAULT (''),
	[FechaRegistro] [datetime] NULL CONSTRAINT [DF_Usuario_FechaRegistro]  DEFAULT (getdate()),
	[EsActivo] [bit] NOT NULL CONSTRAINT [DF_Usuario_EsActivo]  DEFAULT ((1)),
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [Catalogo].[Categoria] ([IdCategoria], [IdFactor], [Nombre], [Ponderacion], [EsActivo]) VALUES (N'357a1463-8b33-4158-8cb7-0c6e7f83499a', N'4e65c6b4-b9fb-4290-b195-c9a7b2e2d734', N'Categoría 1.2', 12, 1)
INSERT [Catalogo].[Categoria] ([IdCategoria], [IdFactor], [Nombre], [Ponderacion], [EsActivo]) VALUES (N'ac6cca08-a152-4467-8b65-3867ded00d47', N'e1a8f61a-9261-4a25-839b-cb96ba5373c8', N'Categoría 2.1', 21, 1)
INSERT [Catalogo].[Categoria] ([IdCategoria], [IdFactor], [Nombre], [Ponderacion], [EsActivo]) VALUES (N'18ecacc6-b67e-4b6b-8a5b-550d90984df7', N'4e65c6b4-b9fb-4290-b195-c9a7b2e2d734', N'Categoría 1.1', 11, 1)
INSERT [Catalogo].[Clasificacion] ([IdClasificacion], [IdCategoria], [Nombre], [Puntuacion], [EsActivo]) VALUES (N'3cd6275c-28bc-4490-b443-d644768e9013', N'357a1463-8b33-4158-8cb7-0c6e7f83499a', N'Clasificación 1.2.1', 121, 1)
INSERT [Catalogo].[Clasificacion] ([IdClasificacion], [IdCategoria], [Nombre], [Puntuacion], [EsActivo]) VALUES (N'87fce1f9-5748-4310-91e4-dc2addb7b69e', N'ac6cca08-a152-4467-8b65-3867ded00d47', N'Clasificación 2.1.1', 211, 0)
INSERT [Catalogo].[Factor] ([IdFactor], [Nombre], [EsActivo]) VALUES (N'4e65c6b4-b9fb-4290-b195-c9a7b2e2d734', N'Factor 1', 1)
INSERT [Catalogo].[Factor] ([IdFactor], [Nombre], [EsActivo]) VALUES (N'e1a8f61a-9261-4a25-839b-cb96ba5373c8', N'Factor 2', 1)
INSERT [Catalogo].[MatrizCalificacion] ([IdMatrizCalificacion], [Nombre], [ValorMin], [ValorMax], [Impacto], [EsActivo]) VALUES (N'2d34bfb3-ee9c-4778-848f-2aaa30c1a18c', N'Nombre personalizado 1', CAST(0.20 AS Decimal(10, 2)), CAST(0.60 AS Decimal(10, 2)), 1, 1)
INSERT [Catalogo].[MatrizCalificacion] ([IdMatrizCalificacion], [Nombre], [ValorMin], [ValorMax], [Impacto], [EsActivo]) VALUES (N'd730823d-d907-49b3-b75f-3fb4026a3297', N'Nombre personalizado 2', CAST(0.10 AS Decimal(10, 2)), CAST(0.20 AS Decimal(10, 2)), 2, 1)
INSERT [Catalogo].[Metodologia] ([IdMetodologia], [Nombre], [EsActivo]) VALUES (N'1f6e2a79-12c6-4eab-8aa7-19ef200ebab6', N'Bancos', 1)
INSERT [Catalogo].[Metodologia] ([IdMetodologia], [Nombre], [EsActivo]) VALUES (N'8ad2c36d-965c-434b-b6f7-6c9e3ef160b8', N'Grupo solidario', 0)
INSERT [Catalogo].[Metodologia] ([IdMetodologia], [Nombre], [EsActivo]) VALUES (N'1ce24ca4-81dc-463c-828e-ab43fc9a0445', N'Grupo Solidario', 1)
INSERT [seguridad].[Ciudad] ([IdCiudad], [IdPais], [Nombre], [EsActivo]) VALUES (N'65dc9c5a-7cc2-42b4-b915-23b56ff19412', N'af62e549-7291-4456-9840-a21f4bc4449e', N'León', 1)
INSERT [seguridad].[Ciudad] ([IdCiudad], [IdPais], [Nombre], [EsActivo]) VALUES (N'58c1a314-d5b3-4c67-a102-328844fe401b', N'e62f5597-8b83-4e64-aa32-fad878c5813d', N'Puntarenas', 1)
INSERT [seguridad].[Ciudad] ([IdCiudad], [IdPais], [Nombre], [EsActivo]) VALUES (N'8645ec42-bb90-4a3d-811b-a1b8436d6b75', N'af62e549-7291-4456-9840-a21f4bc4449e', N'Granada', 1)
INSERT [seguridad].[Ciudad] ([IdCiudad], [IdPais], [Nombre], [EsActivo]) VALUES (N'595e0785-5e71-40e7-8ccd-a86970238699', N'af62e549-7291-4456-9840-a21f4bc4449e', N'Managua', 1)
INSERT [seguridad].[Ciudad] ([IdCiudad], [IdPais], [Nombre], [EsActivo]) VALUES (N'7d1fc84d-c22d-4e48-aa1e-cb867a8c0c1f', N'e62f5597-8b83-4e64-aa32-fad878c5813d', N'San José', 1)
INSERT [seguridad].[Ciudad] ([IdCiudad], [IdPais], [Nombre], [EsActivo]) VALUES (N'1001eb5d-68bc-4d21-95ea-f9ce38ee9463', N'af62e549-7291-4456-9840-a21f4bc4449e', N'Masaya', 1)
INSERT [seguridad].[ItemMenu] ([IdItemMenu], [IdItemMenuPadre], [Ruta], [Texto], [Descripcion], [Visible], [FechaRegistro], [EsActivo]) VALUES (N'2085d909-f33d-437c-bd79-2fa0bc35142e', N'8e9a4dcc-6a9a-4e61-8d6f-8d7b81372d86', N'/Catalogos/Clientes/ClienteNuevo.aspx', N'Cliente Nuevo', N'Registro de nuevo cliente', 1, CAST(N'2014-08-11 03:03:16.860' AS DateTime), 1)
INSERT [seguridad].[ItemMenu] ([IdItemMenu], [IdItemMenuPadre], [Ruta], [Texto], [Descripcion], [Visible], [FechaRegistro], [EsActivo]) VALUES (N'37eb62ff-8264-48e6-988d-4170e311b08f', NULL, N'', N'Catálogos', N'Catálogos de datos necesarios para el sistema', 1, CAST(N'2014-08-10 14:18:11.847' AS DateTime), 1)
INSERT [seguridad].[ItemMenu] ([IdItemMenu], [IdItemMenuPadre], [Ruta], [Texto], [Descripcion], [Visible], [FechaRegistro], [EsActivo]) VALUES (N'70adb95f-84f5-411c-8809-79ff4efce409', N'37eb62ff-8264-48e6-988d-4170e311b08f', N'~/Catalogos/Metodologias.aspx', N'Metodologías', N'Catálogo de metodologías para las evaluaciones de riesgo', 1, CAST(N'2014-08-17 00:09:50.563' AS DateTime), 1)
INSERT [seguridad].[ItemMenu] ([IdItemMenu], [IdItemMenuPadre], [Ruta], [Texto], [Descripcion], [Visible], [FechaRegistro], [EsActivo]) VALUES (N'dc7f9276-06d5-4107-bd9b-87ca76d62ee2', N'37eb62ff-8264-48e6-988d-4170e311b08f', N'~/Catalogos/Factores.aspx', N'Factores', N'Factores de riesgo y las categorías que permite, junto con las Clasificaciones respectivas', 1, CAST(N'2014-08-17 23:07:40.977' AS DateTime), 1)
INSERT [seguridad].[ItemMenu] ([IdItemMenu], [IdItemMenuPadre], [Ruta], [Texto], [Descripcion], [Visible], [FechaRegistro], [EsActivo]) VALUES (N'8e9a4dcc-6a9a-4e61-8d6f-8d7b81372d86', N'37eb62ff-8264-48e6-988d-4170e311b08f', N'~/Catalogos/MatrizCalificaciones.aspx', N'Matriz de Calificaciones', N'Catálogo de Matriz de Calificaciones', 1, CAST(N'2014-08-11 02:31:30.823' AS DateTime), 1)
INSERT [seguridad].[ItemMenu] ([IdItemMenu], [IdItemMenuPadre], [Ruta], [Texto], [Descripcion], [Visible], [FechaRegistro], [EsActivo]) VALUES (N'58223129-158f-488c-b9dc-ef85a8ad1fa1', NULL, N'~/Default.aspx', N'Inicio', N'Página principal del sistema', 0, CAST(N'2014-08-12 23:43:43.340' AS DateTime), 1)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'4b2074ea-3a04-4c6c-b0bf-0314b9b810c5', N'58223129-158f-488c-b9dc-ef85a8ad1fa1', N'cd7429ed-2acb-4c12-9dee-9cb0bbb0de2d', CAST(N'2014-08-13 01:34:56.950' AS DateTime), 1)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'a3e8fb5d-d201-43da-b334-21467477b611', N'8e9a4dcc-6a9a-4e61-8d6f-8d7b81372d86', N'6b573de9-c72a-4048-b479-c63f6d75ec07', CAST(N'2014-08-11 02:41:07.503' AS DateTime), 0)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'd8e5f860-f799-40fd-b5db-2434bf9863d5', N'8e9a4dcc-6a9a-4e61-8d6f-8d7b81372d86', N'cd7429ed-2acb-4c12-9dee-9cb0bbb0de2d', CAST(N'2014-08-12 23:12:12.577' AS DateTime), 0)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'017f05ad-acd2-4c1f-b9fa-2c0264252eb0', N'70adb95f-84f5-411c-8809-79ff4efce409', N'6b573de9-c72a-4048-b479-c63f6d75ec07', CAST(N'2014-08-17 00:17:22.097' AS DateTime), 1)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'867488b3-b62f-4ae1-b210-435546eba66f', N'58223129-158f-488c-b9dc-ef85a8ad1fa1', N'6b573de9-c72a-4048-b479-c63f6d75ec07', CAST(N'2014-08-12 23:48:11.800' AS DateTime), 1)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'05694d9d-18b7-4c68-9e12-64bdcf601cd0', N'dc7f9276-06d5-4107-bd9b-87ca76d62ee2', N'6b573de9-c72a-4048-b479-c63f6d75ec07', CAST(N'2014-08-17 23:07:56.890' AS DateTime), 1)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'3bd560e2-68db-47a0-8766-6b0266e78591', N'2085d909-f33d-437c-bd79-2fa0bc35142e', N'75ecc3ee-ba8c-4ee1-b1fc-b8da83b4d0dc', CAST(N'2014-08-11 03:03:27.437' AS DateTime), 1)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'37a66262-4cf2-4a07-a7fb-8839dbcb6ca5', N'37eb62ff-8264-48e6-988d-4170e311b08f', N'cd7429ed-2acb-4c12-9dee-9cb0bbb0de2d', CAST(N'2014-08-10 23:58:45.283' AS DateTime), 0)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'4ffb2e78-0976-41d5-ab97-8d52af5cc8c9', N'37eb62ff-8264-48e6-988d-4170e311b08f', N'6b573de9-c72a-4048-b479-c63f6d75ec07', CAST(N'2014-08-11 00:15:40.587' AS DateTime), 0)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'942e5353-1b7e-4a89-b243-a1144d096331', N'8e9a4dcc-6a9a-4e61-8d6f-8d7b81372d86', N'cd7429ed-2acb-4c12-9dee-9cb0bbb0de2d', CAST(N'2014-08-16 01:04:23.637' AS DateTime), 1)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'2c674ec6-cd89-434f-8c21-aeb4f19a19b4', N'8e9a4dcc-6a9a-4e61-8d6f-8d7b81372d86', N'6b573de9-c72a-4048-b479-c63f6d75ec07', CAST(N'2014-08-15 23:51:27.527' AS DateTime), 1)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'7a650308-5aea-4d54-bf91-dc34453e716d', N'37eb62ff-8264-48e6-988d-4170e311b08f', N'75ecc3ee-ba8c-4ee1-b1fc-b8da83b4d0dc', CAST(N'2014-08-11 00:15:24.813' AS DateTime), 0)
INSERT [seguridad].[Pais] ([IdPais], [Nombre], [Nacionalidad], [EsActivo]) VALUES (N'af62e549-7291-4456-9840-a21f4bc4449e', N'Nicaragua', N'Nicaragüense', 1)
INSERT [seguridad].[Pais] ([IdPais], [Nombre], [Nacionalidad], [EsActivo]) VALUES (N'e62f5597-8b83-4e64-aa32-fad878c5813d', N'Costa Rica', N'Costarricense', 1)
INSERT [seguridad].[Pais] ([IdPais], [Nombre], [Nacionalidad], [EsActivo]) VALUES (N'cd20ac9a-4ef3-490a-9c76-fb2a7a85f819', N'Estados Unidos', N'Americano', 0)
INSERT [seguridad].[Rol] ([IdRol], [Nombre], [FechaRegistro], [EsActivo]) VALUES (N'6301c811-b6b9-4685-9b8f-6edfc0a581e0', N'Usuario', CAST(N'2014-07-27 01:33:20.507' AS DateTime), 0)
INSERT [seguridad].[Rol] ([IdRol], [Nombre], [FechaRegistro], [EsActivo]) VALUES (N'cd7429ed-2acb-4c12-9dee-9cb0bbb0de2d', N'Contabilidad', CAST(N'2014-07-27 01:34:38.750' AS DateTime), 1)
INSERT [seguridad].[Rol] ([IdRol], [Nombre], [FechaRegistro], [EsActivo]) VALUES (N'75ecc3ee-ba8c-4ee1-b1fc-b8da83b4d0dc', N'Riesgos', CAST(N'2014-07-27 02:04:45.480' AS DateTime), 0)
INSERT [seguridad].[Rol] ([IdRol], [Nombre], [FechaRegistro], [EsActivo]) VALUES (N'6b573de9-c72a-4048-b479-c63f6d75ec07', N'Administrador', CAST(N'2014-07-16 01:05:34.723' AS DateTime), 1)
INSERT [seguridad].[Sucursal] ([IdSucursal], [IdCiudad], [Nombre], [Codigo], [Direccion], [EsActivo]) VALUES (N'140ca5b0-0293-4c71-9d36-3c66a8ebe4e6', N'1001eb5d-68bc-4d21-95ea-f9ce38ee9463', N'Central', N'', N'De donde fue X, 200m norte', 1)
INSERT [seguridad].[Sucursal] ([IdSucursal], [IdCiudad], [Nombre], [Codigo], [Direccion], [EsActivo]) VALUES (N'82f9ddf9-f6d8-40b8-8534-9d4cda092675', N'1001eb5d-68bc-4d21-95ea-f9ce38ee9463', N'Masaya', N'', N'Por ahí', 1)
INSERT [seguridad].[Usuario] ([IdUsuario], [IdSucursal], [IdRol], [Nombre], [Sexo], [Login], [Pass], [Cargo], [Codigo], [FechaRegistro], [EsActivo]) VALUES (N'666b0fe6-4104-4fa8-9e19-0f917cf8b4bd', N'140ca5b0-0293-4c71-9d36-3c66a8ebe4e6', N'cd7429ed-2acb-4c12-9dee-9cb0bbb0de2d', N'Abraham Molina', N'M', N'amolina', N'123', N'Prix', N'02', CAST(N'2014-08-13 01:33:18.787' AS DateTime), 1)
INSERT [seguridad].[Usuario] ([IdUsuario], [IdSucursal], [IdRol], [Nombre], [Sexo], [Login], [Pass], [Cargo], [Codigo], [FechaRegistro], [EsActivo]) VALUES (N'344bb27c-d0cf-4f4b-af0b-411b6056a31f', N'82f9ddf9-f6d8-40b8-8534-9d4cda092675', N'6b573de9-c72a-4048-b479-c63f6d75ec07', N'Fernando M. Gallegos Gutiérrez', N'M', N'galher', N'123', N'Administrador', N'01', CAST(N'2014-08-02 01:50:52.380' AS DateTime), 1)
ALTER TABLE [Auditoria].[Auditoria] ADD  CONSTRAINT [DF_Auditoria_IdAuditoria]  DEFAULT (newid()) FOR [IdAuditoria]
GO
ALTER TABLE [Auditoria].[Auditoria] ADD  CONSTRAINT [DF_Auditoria_FechaAfectacion]  DEFAULT (getdate()) FOR [FechaAfectacion]
GO
ALTER TABLE [Auditoria].[Auditoria] ADD  CONSTRAINT [DF_Auditoria_IdTipo]  DEFAULT ((0)) FOR [IdTipo]
GO
ALTER TABLE [Auditoria].[Auditoria] ADD  CONSTRAINT [DF_Auditoria_IdTabla]  DEFAULT ((0)) FOR [IdTabla]
GO
ALTER TABLE [Auditoria].[Auditoria] ADD  CONSTRAINT [DF_Auditoria_Comentario]  DEFAULT ('') FOR [Comentario]
GO
ALTER TABLE [Auditoria].[Auditoria] ADD  CONSTRAINT [DF_Auditoria_Usuario]  DEFAULT (N'sis') FOR [Usuario]
GO
ALTER TABLE [Catalogo].[Categoria]  WITH CHECK ADD  CONSTRAINT [FK_Categoria_Factor] FOREIGN KEY([IdFactor])
REFERENCES [Catalogo].[Factor] ([IdFactor])
GO
ALTER TABLE [Catalogo].[Categoria] CHECK CONSTRAINT [FK_Categoria_Factor]
GO
ALTER TABLE [Catalogo].[Clasificacion]  WITH CHECK ADD  CONSTRAINT [FK_Clasificacion_Categoria] FOREIGN KEY([IdCategoria])
REFERENCES [Catalogo].[Categoria] ([IdCategoria])
GO
ALTER TABLE [Catalogo].[Clasificacion] CHECK CONSTRAINT [FK_Clasificacion_Categoria]
GO
ALTER TABLE [seguridad].[Ciudad]  WITH CHECK ADD  CONSTRAINT [FK_Ciudad_Pais] FOREIGN KEY([IdPais])
REFERENCES [seguridad].[Pais] ([IdPais])
GO
ALTER TABLE [seguridad].[Ciudad] CHECK CONSTRAINT [FK_Ciudad_Pais]
GO
ALTER TABLE [seguridad].[ItemMenu]  WITH CHECK ADD  CONSTRAINT [FK_ItemMenu_ItemMenu] FOREIGN KEY([IdItemMenuPadre])
REFERENCES [seguridad].[ItemMenu] ([IdItemMenu])
GO
ALTER TABLE [seguridad].[ItemMenu] CHECK CONSTRAINT [FK_ItemMenu_ItemMenu]
GO
ALTER TABLE [seguridad].[ItemRol]  WITH CHECK ADD  CONSTRAINT [FK_ItemRol_ItemMenu] FOREIGN KEY([IdItemMenu])
REFERENCES [seguridad].[ItemMenu] ([IdItemMenu])
GO
ALTER TABLE [seguridad].[ItemRol] CHECK CONSTRAINT [FK_ItemRol_ItemMenu]
GO
ALTER TABLE [seguridad].[ItemRol]  WITH CHECK ADD  CONSTRAINT [FK_ItemRol_Rol] FOREIGN KEY([IdRol])
REFERENCES [seguridad].[Rol] ([IdRol])
GO
ALTER TABLE [seguridad].[ItemRol] CHECK CONSTRAINT [FK_ItemRol_Rol]
GO
ALTER TABLE [seguridad].[Sucursal]  WITH CHECK ADD  CONSTRAINT [FK_Sucursal_Ciudad] FOREIGN KEY([IdCiudad])
REFERENCES [seguridad].[Ciudad] ([IdCiudad])
GO
ALTER TABLE [seguridad].[Sucursal] CHECK CONSTRAINT [FK_Sucursal_Ciudad]
GO
ALTER TABLE [seguridad].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY([IdRol])
REFERENCES [seguridad].[Rol] ([IdRol])
GO
ALTER TABLE [seguridad].[Usuario] CHECK CONSTRAINT [FK_Usuario_Rol]
GO
ALTER TABLE [seguridad].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Sucursal] FOREIGN KEY([IdSucursal])
REFERENCES [seguridad].[Sucursal] ([IdSucursal])
GO
ALTER TABLE [seguridad].[Usuario] CHECK CONSTRAINT [FK_Usuario_Sucursal]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador único de la tabla' , @level0type=N'SCHEMA',@level0name=N'Auditoria', @level1type=N'TABLE',@level1name=N'Auditoria', @level2type=N'COLUMN',@level2name=N'IdAuditoria'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador del registro afectado' , @level0type=N'SCHEMA',@level0name=N'Auditoria', @level1type=N'TABLE',@level1name=N'Auditoria', @level2type=N'COLUMN',@level2name=N'IdAfectado'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Identificador que sirve de referencia, es decir, un registro de auditoría refiere a una inserción, modificación o eliminación (lógica), pero IdReferencia se puede usar a conveniencia para poner el Id de un registro de una tabla principal, por ejemplo, se elimina una evaluación, se registra la acción como auditoría pero se puede guardar como referencia el Id del Cliente al que refiere.' , @level0type=N'SCHEMA',@level0name=N'Auditoria', @level1type=N'TABLE',@level1name=N'Auditoria', @level2type=N'COLUMN',@level2name=N'IdReferencia'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fecha en que se realizó la afectación (inserción, modificación, eliminación, etc).' , @level0type=N'SCHEMA',@level0name=N'Auditoria', @level1type=N'TABLE',@level1name=N'Auditoria', @level2type=N'COLUMN',@level2name=N'FechaAfectacion'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor numérico que indica el tipo de afectación, Inserción=1; Modificación=2; Eliminación (lógica)=3; Ninguno=0;' , @level0type=N'SCHEMA',@level0name=N'Auditoria', @level1type=N'TABLE',@level1name=N'Auditoria', @level2type=N'COLUMN',@level2name=N'IdTipo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor numérico con que se identifica a una tabla en la base de datos, para ello revise los adjuntos de la documentación del sistema para ver los valores que corresponden a cada tabla' , @level0type=N'SCHEMA',@level0name=N'Auditoria', @level1type=N'TABLE',@level1name=N'Auditoria', @level2type=N'COLUMN',@level2name=N'IdTabla'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Comentario del analista, se puede usar para justificar una eliminación o modificación si la programación lo permite o las reglas de negocio lo establecen' , @level0type=N'SCHEMA',@level0name=N'Auditoria', @level1type=N'TABLE',@level1name=N'Auditoria', @level2type=N'COLUMN',@level2name=N'Comentario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Login del usuario que realizó la afectación en el sistema' , @level0type=N'SCHEMA',@level0name=N'Auditoria', @level1type=N'TABLE',@level1name=N'Auditoria', @level2type=N'COLUMN',@level2name=N'Usuario'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre del equipo en que se realizó la afectación' , @level0type=N'SCHEMA',@level0name=N'Auditoria', @level1type=N'TABLE',@level1name=N'Auditoria', @level2type=N'COLUMN',@level2name=N'UsuarioPC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ip del equipo en que se realizó la afectación' , @level0type=N'SCHEMA',@level0name=N'Auditoria', @level1type=N'TABLE',@level1name=N'Auditoria', @level2type=N'COLUMN',@level2name=N'UsuarioIP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'Auditoria', @level1type=N'TABLE',@level1name=N'Auditoria'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor que indica la calificacion dada a un cliente en donde 1 = Bajo, 2= Medio, 3 = Alto' , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'TABLE',@level1name=N'MatrizCalificacion', @level2type=N'COLUMN',@level2name=N'Impacto'
GO
USE [master]
GO
ALTER DATABASE [ASODENICDB] SET  READ_WRITE 
GO
