USE [master]
GO
/****** Object:  Database [ASODENICDB]    Script Date: 12/08/2014 11:27:53 p.m. ******/
CREATE DATABASE [ASODENICDB] ON  PRIMARY 
( NAME = N'ASODENICDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.GALHERSQLSERVER\MSSQL\DATA\ASODENICDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'ASODENICDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.GALHERSQLSERVER\MSSQL\DATA\ASODENICDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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
EXEC sys.sp_db_vardecimal_storage_format N'ASODENICDB', N'ON'
GO
USE [ASODENICDB]
GO
/****** Object:  Schema [seguridad]    Script Date: 12/08/2014 11:27:53 p.m. ******/
CREATE SCHEMA [seguridad]
GO
/****** Object:  Table [seguridad].[Ciudad]    Script Date: 12/08/2014 11:27:53 p.m. ******/
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
/****** Object:  Table [seguridad].[ItemMenu]    Script Date: 12/08/2014 11:27:54 p.m. ******/
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
/****** Object:  Table [seguridad].[ItemRol]    Script Date: 12/08/2014 11:27:54 p.m. ******/
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
/****** Object:  Table [seguridad].[Pais]    Script Date: 12/08/2014 11:27:54 p.m. ******/
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
/****** Object:  Table [seguridad].[Rol]    Script Date: 12/08/2014 11:27:54 p.m. ******/
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
/****** Object:  Table [seguridad].[Sucursal]    Script Date: 12/08/2014 11:27:54 p.m. ******/
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
/****** Object:  Table [seguridad].[Usuario]    Script Date: 12/08/2014 11:27:54 p.m. ******/
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
INSERT [seguridad].[Ciudad] ([IdCiudad], [IdPais], [Nombre], [EsActivo]) VALUES (N'65dc9c5a-7cc2-42b4-b915-23b56ff19412', N'af62e549-7291-4456-9840-a21f4bc4449e', N'León', 1)
INSERT [seguridad].[Ciudad] ([IdCiudad], [IdPais], [Nombre], [EsActivo]) VALUES (N'58c1a314-d5b3-4c67-a102-328844fe401b', N'e62f5597-8b83-4e64-aa32-fad878c5813d', N'Puntarenas', 1)
INSERT [seguridad].[Ciudad] ([IdCiudad], [IdPais], [Nombre], [EsActivo]) VALUES (N'8645ec42-bb90-4a3d-811b-a1b8436d6b75', N'af62e549-7291-4456-9840-a21f4bc4449e', N'Granada', 1)
INSERT [seguridad].[Ciudad] ([IdCiudad], [IdPais], [Nombre], [EsActivo]) VALUES (N'595e0785-5e71-40e7-8ccd-a86970238699', N'af62e549-7291-4456-9840-a21f4bc4449e', N'Managua', 1)
INSERT [seguridad].[Ciudad] ([IdCiudad], [IdPais], [Nombre], [EsActivo]) VALUES (N'7d1fc84d-c22d-4e48-aa1e-cb867a8c0c1f', N'e62f5597-8b83-4e64-aa32-fad878c5813d', N'San José', 1)
INSERT [seguridad].[Ciudad] ([IdCiudad], [IdPais], [Nombre], [EsActivo]) VALUES (N'1001eb5d-68bc-4d21-95ea-f9ce38ee9463', N'af62e549-7291-4456-9840-a21f4bc4449e', N'Masaya', 1)
INSERT [seguridad].[ItemMenu] ([IdItemMenu], [IdItemMenuPadre], [Ruta], [Texto], [Descripcion], [Visible], [FechaRegistro], [EsActivo]) VALUES (N'2085d909-f33d-437c-bd79-2fa0bc35142e', N'8e9a4dcc-6a9a-4e61-8d6f-8d7b81372d86', N'/Catalogos/Clientes/ClienteNuevo.aspx', N'Cliente Nuevo', N'Registro de nuevo cliente', 1, CAST(N'2014-08-11 03:03:16.860' AS DateTime), 1)
INSERT [seguridad].[ItemMenu] ([IdItemMenu], [IdItemMenuPadre], [Ruta], [Texto], [Descripcion], [Visible], [FechaRegistro], [EsActivo]) VALUES (N'37eb62ff-8264-48e6-988d-4170e311b08f', NULL, N'/Catalogos', N'Catálogos', N'Catálogos de datos necesarios para el sistema', 1, CAST(N'2014-08-10 14:18:11.847' AS DateTime), 1)
INSERT [seguridad].[ItemMenu] ([IdItemMenu], [IdItemMenuPadre], [Ruta], [Texto], [Descripcion], [Visible], [FechaRegistro], [EsActivo]) VALUES (N'8e9a4dcc-6a9a-4e61-8d6f-8d7b81372d86', N'37eb62ff-8264-48e6-988d-4170e311b08f', N'/Catalogos/Clientes.aspx', N'Clientes', N'Catálogo de clientes', 1, CAST(N'2014-08-11 02:31:30.823' AS DateTime), 1)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'a3e8fb5d-d201-43da-b334-21467477b611', N'8e9a4dcc-6a9a-4e61-8d6f-8d7b81372d86', N'6b573de9-c72a-4048-b479-c63f6d75ec07', CAST(N'2014-08-11 02:41:07.503' AS DateTime), 0)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'd8e5f860-f799-40fd-b5db-2434bf9863d5', N'8e9a4dcc-6a9a-4e61-8d6f-8d7b81372d86', N'cd7429ed-2acb-4c12-9dee-9cb0bbb0de2d', CAST(N'2014-08-12 23:12:12.577' AS DateTime), 0)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'3bd560e2-68db-47a0-8766-6b0266e78591', N'2085d909-f33d-437c-bd79-2fa0bc35142e', N'75ecc3ee-ba8c-4ee1-b1fc-b8da83b4d0dc', CAST(N'2014-08-11 03:03:27.437' AS DateTime), 1)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'37a66262-4cf2-4a07-a7fb-8839dbcb6ca5', N'37eb62ff-8264-48e6-988d-4170e311b08f', N'cd7429ed-2acb-4c12-9dee-9cb0bbb0de2d', CAST(N'2014-08-10 23:58:45.283' AS DateTime), 0)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'4ffb2e78-0976-41d5-ab97-8d52af5cc8c9', N'37eb62ff-8264-48e6-988d-4170e311b08f', N'6b573de9-c72a-4048-b479-c63f6d75ec07', CAST(N'2014-08-11 00:15:40.587' AS DateTime), 0)
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
INSERT [seguridad].[Usuario] ([IdUsuario], [IdSucursal], [IdRol], [Nombre], [Sexo], [Login], [Pass], [Cargo], [Codigo], [FechaRegistro], [EsActivo]) VALUES (N'344bb27c-d0cf-4f4b-af0b-411b6056a31f', N'82f9ddf9-f6d8-40b8-8534-9d4cda092675', N'6b573de9-c72a-4048-b479-c63f6d75ec07', N'Fernando M. Gallegos Gutiérrez', N'M', N'galher', N'Mchl6676*-', N'Administrador', N'', CAST(N'2014-08-02 01:50:52.380' AS DateTime), 1)
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
USE [master]
GO
ALTER DATABASE [ASODENICDB] SET  READ_WRITE 
GO
