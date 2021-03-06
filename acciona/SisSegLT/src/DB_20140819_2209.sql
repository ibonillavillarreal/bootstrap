USE [master]
GO
/****** Object:  Database [ASODENICDB]    Script Date: 08/19/2014 22:10:23 ******/
CREATE DATABASE [ASODENICDB] ON  PRIMARY 
( NAME = N'ASODENICDB', FILENAME = N'D:\BD\ASODENIC\19082014\ASODENICDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'ASODENICDB_log', FILENAME = N'D:\BD\ASODENIC\19082014\ASODENICDB_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ASODENICDB] SET COMPATIBILITY_LEVEL = 100
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
ALTER DATABASE [ASODENICDB] SET AUTO_CREATE_STATISTICS ON
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
ALTER DATABASE [ASODENICDB] SET  READ_WRITE
GO
ALTER DATABASE [ASODENICDB] SET RECOVERY FULL
GO
ALTER DATABASE [ASODENICDB] SET  MULTI_USER
GO
ALTER DATABASE [ASODENICDB] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [ASODENICDB] SET DB_CHAINING OFF
GO
USE [ASODENICDB]
GO
/****** Object:  Schema [seguridad]    Script Date: 08/19/2014 22:10:23 ******/
CREATE SCHEMA [seguridad] AUTHORIZATION [dbo]
GO
/****** Object:  Schema [Catalogo]    Script Date: 08/19/2014 22:10:23 ******/
CREATE SCHEMA [Catalogo] AUTHORIZATION [dbo]
GO
/****** Object:  Schema [Auditoria]    Script Date: 08/19/2014 22:10:23 ******/
CREATE SCHEMA [Auditoria] AUTHORIZATION [dbo]
GO
/****** Object:  Table [seguridad].[ItemMenu]    Script Date: 08/19/2014 22:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [seguridad].[ItemMenu](
	[IdItemMenu] [uniqueidentifier] NOT NULL,
	[IdItemMenuPadre] [uniqueidentifier] NULL,
	[Ruta] [varchar](256) NOT NULL,
	[Texto] [nvarchar](50) NOT NULL,
	[Descripcion] [nvarchar](256) NOT NULL,
	[Visible] [bit] NOT NULL,
	[FechaRegistro] [datetime] NULL,
	[EsActivo] [bit] NOT NULL,
 CONSTRAINT [PK_ItemMenu] PRIMARY KEY CLUSTERED 
(
	[IdItemMenu] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [seguridad].[ItemMenu] ([IdItemMenu], [IdItemMenuPadre], [Ruta], [Texto], [Descripcion], [Visible], [FechaRegistro], [EsActivo]) VALUES (N'279f44cb-db18-4bca-8198-1f98557d139c', N'37eb62ff-8264-48e6-988d-4170e311b08f', N'~/Catalogos/Profesiones.aspx', N'Profesiones', N'Profesiones disponibles para los clientes', 1, CAST(0x0000A38C01660EE3 AS DateTime), 1)
INSERT [seguridad].[ItemMenu] ([IdItemMenu], [IdItemMenuPadre], [Ruta], [Texto], [Descripcion], [Visible], [FechaRegistro], [EsActivo]) VALUES (N'2085d909-f33d-437c-bd79-2fa0bc35142e', N'8e9a4dcc-6a9a-4e61-8d6f-8d7b81372d86', N'/Catalogos/Clientes/ClienteNuevo.aspx', N'Cliente Nuevo', N'Registro de nuevo cliente', 1, CAST(0x0000A3DD003256F2 AS DateTime), 1)
INSERT [seguridad].[ItemMenu] ([IdItemMenu], [IdItemMenuPadre], [Ruta], [Texto], [Descripcion], [Visible], [FechaRegistro], [EsActivo]) VALUES (N'37eb62ff-8264-48e6-988d-4170e311b08f', NULL, N'', N'Catálogos', N'Catálogos de datos necesarios para el sistema', 1, CAST(0x0000A3BE00EBB602 AS DateTime), 1)
INSERT [seguridad].[ItemMenu] ([IdItemMenu], [IdItemMenuPadre], [Ruta], [Texto], [Descripcion], [Visible], [FechaRegistro], [EsActivo]) VALUES (N'4635756c-7012-4007-8315-77f46390cefa', N'37eb62ff-8264-48e6-988d-4170e311b08f', N'~/Catalogos/Factores.aspx', N'Factores', N'Factores de Riesgo', 1, CAST(0x0000A38C015CF28E AS DateTime), 1)
INSERT [seguridad].[ItemMenu] ([IdItemMenu], [IdItemMenuPadre], [Ruta], [Texto], [Descripcion], [Visible], [FechaRegistro], [EsActivo]) VALUES (N'8e9a4dcc-6a9a-4e61-8d6f-8d7b81372d86', N'37eb62ff-8264-48e6-988d-4170e311b08f', N'~/Catalogos/MatrizCalificaciones.aspx', N'Matriz de Calificaciones', N'Catálogo de Matriz de Calificaciones', 1, CAST(0x0000A3DD00299D4F AS DateTime), 1)
INSERT [seguridad].[ItemMenu] ([IdItemMenu], [IdItemMenuPadre], [Ruta], [Texto], [Descripcion], [Visible], [FechaRegistro], [EsActivo]) VALUES (N'58223129-158f-488c-b9dc-ef85a8ad1fa1', NULL, N'~/Default.aspx', N'Inicio', N'Página principal del sistema', 0, CAST(0x0000A3FB0187097A AS DateTime), 1)
/****** Object:  Table [Catalogo].[Factor]    Script Date: 08/19/2014 22:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Factor](
	[IdFactor] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[EsActivo] [bit] NOT NULL,
 CONSTRAINT [PK_Factor] PRIMARY KEY CLUSTERED 
(
	[IdFactor] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [Catalogo].[Factor] ([IdFactor], [Nombre], [EsActivo]) VALUES (N'4e65c6b4-b9fb-4290-b195-c9a7b2e2d734', N'Factor 1', 1)
INSERT [Catalogo].[Factor] ([IdFactor], [Nombre], [EsActivo]) VALUES (N'e1a8f61a-9261-4a25-839b-cb96ba5373c8', N'Factor 2', 1)
/****** Object:  Table [Auditoria].[Auditoria]    Script Date: 08/19/2014 22:10:25 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
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
/****** Object:  Table [seguridad].[Rol]    Script Date: 08/19/2014 22:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [seguridad].[Rol](
	[IdRol] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[FechaRegistro] [datetime] NULL,
	[EsActivo] [bit] NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [seguridad].[Rol] ([IdRol], [Nombre], [FechaRegistro], [EsActivo]) VALUES (N'6301c811-b6b9-4685-9b8f-6edfc0a581e0', N'Usuario', CAST(0x0000A38C015AECEB AS DateTime), 1)
INSERT [seguridad].[Rol] ([IdRol], [Nombre], [FechaRegistro], [EsActivo]) VALUES (N'cd7429ed-2acb-4c12-9dee-9cb0bbb0de2d', N'Contabilidad', CAST(0x0000A38C015B1D53 AS DateTime), 1)
INSERT [seguridad].[Rol] ([IdRol], [Nombre], [FechaRegistro], [EsActivo]) VALUES (N'75ecc3ee-ba8c-4ee1-b1fc-b8da83b4d0dc', N'Riesgos', CAST(0x0000A38C015B1D59 AS DateTime), 1)
INSERT [seguridad].[Rol] ([IdRol], [Nombre], [FechaRegistro], [EsActivo]) VALUES (N'6b573de9-c72a-4048-b479-c63f6d75ec07', N'Administrador', CAST(0x0000A38C015B1D95 AS DateTime), 1)
/****** Object:  Table [Catalogo].[Profesion]    Script Date: 08/19/2014 22:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Profesion](
	[IdProfesion] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[EsActivo] [bit] NOT NULL,
	[FechaRegistro] [datetime] NULL,
 CONSTRAINT [PK_Profesion] PRIMARY KEY CLUSTERED 
(
	[IdProfesion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [Catalogo].[Profesion] ([IdProfesion], [Nombre], [EsActivo], [FechaRegistro]) VALUES (N'0dc7bfe2-868c-46c7-8b73-283f8a80805a', N'Comerciante', 0, NULL)
INSERT [Catalogo].[Profesion] ([IdProfesion], [Nombre], [EsActivo], [FechaRegistro]) VALUES (N'de9e34e9-8c67-4df9-bc89-67597cc90d44', N'Administrador de Empresas', 1, NULL)
INSERT [Catalogo].[Profesion] ([IdProfesion], [Nombre], [EsActivo], [FechaRegistro]) VALUES (N'2b25271b-3511-48a6-b3b7-8616cc946b7e', N'Ingeniero en Sistema', 1, NULL)
INSERT [Catalogo].[Profesion] ([IdProfesion], [Nombre], [EsActivo], [FechaRegistro]) VALUES (N'cbfeb62d-e8ce-41f8-8223-d76a90d20582', N'Peluche', 1, NULL)
/****** Object:  Table [seguridad].[Pais]    Script Date: 08/19/2014 22:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [seguridad].[Pais](
	[IdPais] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Nacionalidad] [nvarchar](100) NULL,
	[EsActivo] [bit] NOT NULL,
 CONSTRAINT [PK_Pais] PRIMARY KEY CLUSTERED 
(
	[IdPais] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [seguridad].[Pais] ([IdPais], [Nombre], [Nacionalidad], [EsActivo]) VALUES (N'af62e549-7291-4456-9840-a21f4bc4449e', N'Nicaragua', N'Nicaragüense', 1)
INSERT [seguridad].[Pais] ([IdPais], [Nombre], [Nacionalidad], [EsActivo]) VALUES (N'e62f5597-8b83-4e64-aa32-fad878c5813d', N'Costa Rica', N'Costarricense', 1)
INSERT [seguridad].[Pais] ([IdPais], [Nombre], [Nacionalidad], [EsActivo]) VALUES (N'cd20ac9a-4ef3-490a-9c76-fb2a7a85f819', N'Estados Unidos', N'Americano', 0)
/****** Object:  Table [Catalogo].[Metodologia]    Script Date: 08/19/2014 22:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Metodologia](
	[IdMetodologia] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[EsActivo] [bit] NOT NULL,
 CONSTRAINT [PK_Metodologia] PRIMARY KEY CLUSTERED 
(
	[IdMetodologia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [Catalogo].[Metodologia] ([IdMetodologia], [Nombre], [EsActivo]) VALUES (N'1f6e2a79-12c6-4eab-8aa7-19ef200ebab6', N'Bancos', 1)
INSERT [Catalogo].[Metodologia] ([IdMetodologia], [Nombre], [EsActivo]) VALUES (N'8ad2c36d-965c-434b-b6f7-6c9e3ef160b8', N'Grupo solidario', 0)
INSERT [Catalogo].[Metodologia] ([IdMetodologia], [Nombre], [EsActivo]) VALUES (N'1ce24ca4-81dc-463c-828e-ab43fc9a0445', N'Grupo Solidario', 1)
/****** Object:  Table [Catalogo].[MatrizCalificacion]    Script Date: 08/19/2014 22:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[MatrizCalificacion](
	[IdMatrizCalificacion] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[ValorMin] [decimal](10, 2) NOT NULL,
	[ValorMax] [decimal](10, 2) NOT NULL,
	[Impacto] [int] NOT NULL,
	[EsActivo] [bit] NOT NULL,
 CONSTRAINT [PK_MatrizCalificacion] PRIMARY KEY CLUSTERED 
(
	[IdMatrizCalificacion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Valor que indica la calificacion dada a un cliente en donde 1 = Bajo, 2= Medio, 3 = Alto' , @level0type=N'SCHEMA',@level0name=N'Catalogo', @level1type=N'TABLE',@level1name=N'MatrizCalificacion', @level2type=N'COLUMN',@level2name=N'Impacto'
GO
INSERT [Catalogo].[MatrizCalificacion] ([IdMatrizCalificacion], [Nombre], [ValorMin], [ValorMax], [Impacto], [EsActivo]) VALUES (N'2d34bfb3-ee9c-4778-848f-2aaa30c1a18c', N'Nombre personalizado 1', CAST(0.20 AS Decimal(10, 2)), CAST(0.60 AS Decimal(10, 2)), 1, 1)
INSERT [Catalogo].[MatrizCalificacion] ([IdMatrizCalificacion], [Nombre], [ValorMin], [ValorMax], [Impacto], [EsActivo]) VALUES (N'd730823d-d907-49b3-b75f-3fb4026a3297', N'Nombre personalizado 2', CAST(0.10 AS Decimal(10, 2)), CAST(0.20 AS Decimal(10, 2)), 2, 1)
/****** Object:  Table [seguridad].[ItemRol]    Script Date: 08/19/2014 22:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [seguridad].[ItemRol](
	[IdItemRol] [uniqueidentifier] NOT NULL,
	[IdItemMenu] [uniqueidentifier] NOT NULL,
	[IdRol] [uniqueidentifier] NOT NULL,
	[FechaRegistro] [datetime] NULL,
	[EsActivo] [bit] NOT NULL,
 CONSTRAINT [PK_ItemRol] PRIMARY KEY CLUSTERED 
(
	[IdItemRol] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'a3e8fb5d-d201-43da-b334-21467477b611', N'8e9a4dcc-6a9a-4e61-8d6f-8d7b81372d86', N'6b573de9-c72a-4048-b479-c63f6d75ec07', CAST(0x0000A3DD002C411B AS DateTime), 0)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'd8e5f860-f799-40fd-b5db-2434bf9863d5', N'8e9a4dcc-6a9a-4e61-8d6f-8d7b81372d86', N'cd7429ed-2acb-4c12-9dee-9cb0bbb0de2d', CAST(0x0000A3FB017E61BD AS DateTime), 0)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'867488b3-b62f-4ae1-b210-435546eba66f', N'58223129-158f-488c-b9dc-ef85a8ad1fa1', N'6b573de9-c72a-4048-b479-c63f6d75ec07', CAST(0x0000A3FB01884414 AS DateTime), 1)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'3bd560e2-68db-47a0-8766-6b0266e78591', N'2085d909-f33d-437c-bd79-2fa0bc35142e', N'75ecc3ee-ba8c-4ee1-b1fc-b8da83b4d0dc', CAST(0x0000A3DD00326357 AS DateTime), 1)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'37a66262-4cf2-4a07-a7fb-8839dbcb6ca5', N'37eb62ff-8264-48e6-988d-4170e311b08f', N'cd7429ed-2acb-4c12-9dee-9cb0bbb0de2d', CAST(0x0000A3BE018B2A71 AS DateTime), 0)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'4ffb2e78-0976-41d5-ab97-8d52af5cc8c9', N'37eb62ff-8264-48e6-988d-4170e311b08f', N'6b573de9-c72a-4048-b479-c63f6d75ec07', CAST(0x0000A3DD00044E40 AS DateTime), 0)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'7a650308-5aea-4d54-bf91-dc34453e716d', N'37eb62ff-8264-48e6-988d-4170e311b08f', N'75ecc3ee-ba8c-4ee1-b1fc-b8da83b4d0dc', CAST(0x0000A3DD00043BC4 AS DateTime), 0)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'e013019f-2437-4abc-af92-ea28e4828510', N'279f44cb-db18-4bca-8198-1f98557d139c', N'6b573de9-c72a-4048-b479-c63f6d75ec07', CAST(0x0000A38C01661B2F AS DateTime), 1)
INSERT [seguridad].[ItemRol] ([IdItemRol], [IdItemMenu], [IdRol], [FechaRegistro], [EsActivo]) VALUES (N'73bfa205-f57f-4c62-8e3d-f3fc56ddb102', N'4635756c-7012-4007-8315-77f46390cefa', N'6b573de9-c72a-4048-b479-c63f6d75ec07', CAST(0x0000A38C015D0925 AS DateTime), 1)
/****** Object:  Table [seguridad].[Ciudad]    Script Date: 08/19/2014 22:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [seguridad].[Ciudad](
	[IdCiudad] [uniqueidentifier] NOT NULL,
	[IdPais] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[EsActivo] [bit] NOT NULL,
 CONSTRAINT [PK_Ciudad] PRIMARY KEY CLUSTERED 
(
	[IdCiudad] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [seguridad].[Ciudad] ([IdCiudad], [IdPais], [Nombre], [EsActivo]) VALUES (N'65dc9c5a-7cc2-42b4-b915-23b56ff19412', N'af62e549-7291-4456-9840-a21f4bc4449e', N'León', 1)
INSERT [seguridad].[Ciudad] ([IdCiudad], [IdPais], [Nombre], [EsActivo]) VALUES (N'58c1a314-d5b3-4c67-a102-328844fe401b', N'e62f5597-8b83-4e64-aa32-fad878c5813d', N'Puntarenas', 1)
INSERT [seguridad].[Ciudad] ([IdCiudad], [IdPais], [Nombre], [EsActivo]) VALUES (N'8645ec42-bb90-4a3d-811b-a1b8436d6b75', N'af62e549-7291-4456-9840-a21f4bc4449e', N'Granada', 1)
INSERT [seguridad].[Ciudad] ([IdCiudad], [IdPais], [Nombre], [EsActivo]) VALUES (N'595e0785-5e71-40e7-8ccd-a86970238699', N'af62e549-7291-4456-9840-a21f4bc4449e', N'Managua', 1)
INSERT [seguridad].[Ciudad] ([IdCiudad], [IdPais], [Nombre], [EsActivo]) VALUES (N'7d1fc84d-c22d-4e48-aa1e-cb867a8c0c1f', N'e62f5597-8b83-4e64-aa32-fad878c5813d', N'San José', 1)
INSERT [seguridad].[Ciudad] ([IdCiudad], [IdPais], [Nombre], [EsActivo]) VALUES (N'1001eb5d-68bc-4d21-95ea-f9ce38ee9463', N'af62e549-7291-4456-9840-a21f4bc4449e', N'Masaya', 1)
/****** Object:  Table [Catalogo].[Categoria]    Script Date: 08/19/2014 22:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Categoria](
	[IdCategoria] [uniqueidentifier] NOT NULL,
	[IdFactor] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Ponderacion] [int] NOT NULL,
	[EsActivo] [bit] NOT NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [Catalogo].[Categoria] ([IdCategoria], [IdFactor], [Nombre], [Ponderacion], [EsActivo]) VALUES (N'357a1463-8b33-4158-8cb7-0c6e7f83499a', N'4e65c6b4-b9fb-4290-b195-c9a7b2e2d734', N'Categoría 1.2', 14, 1)
INSERT [Catalogo].[Categoria] ([IdCategoria], [IdFactor], [Nombre], [Ponderacion], [EsActivo]) VALUES (N'ac6cca08-a152-4467-8b65-3867ded00d47', N'e1a8f61a-9261-4a25-839b-cb96ba5373c8', N'Categoría 2.1', 21, 1)
INSERT [Catalogo].[Categoria] ([IdCategoria], [IdFactor], [Nombre], [Ponderacion], [EsActivo]) VALUES (N'18ecacc6-b67e-4b6b-8a5b-550d90984df7', N'4e65c6b4-b9fb-4290-b195-c9a7b2e2d734', N'Categoría 1.1', 13, 1)
/****** Object:  Table [Catalogo].[Clasificacion]    Script Date: 08/19/2014 22:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Catalogo].[Clasificacion](
	[IdClasificacion] [uniqueidentifier] NOT NULL,
	[IdCategoria] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Puntuacion] [int] NOT NULL,
	[EsActivo] [bit] NOT NULL,
 CONSTRAINT [PK_Clasificacion] PRIMARY KEY CLUSTERED 
(
	[IdClasificacion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [Catalogo].[Clasificacion] ([IdClasificacion], [IdCategoria], [Nombre], [Puntuacion], [EsActivo]) VALUES (N'3cd6275c-28bc-4490-b443-d644768e9013', N'357a1463-8b33-4158-8cb7-0c6e7f83499a', N'Clasificación 1.2.1', 9, 1)
INSERT [Catalogo].[Clasificacion] ([IdClasificacion], [IdCategoria], [Nombre], [Puntuacion], [EsActivo]) VALUES (N'87fce1f9-5748-4310-91e4-dc2addb7b69e', N'ac6cca08-a152-4467-8b65-3867ded00d47', N'Clasificación 2.1.1', 211, 0)
/****** Object:  Table [seguridad].[Sucursal]    Script Date: 08/19/2014 22:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [seguridad].[Sucursal](
	[IdSucursal] [uniqueidentifier] NOT NULL,
	[IdCiudad] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Codigo] [nvarchar](20) NOT NULL,
	[Direccion] [nvarchar](150) NOT NULL,
	[EsActivo] [bit] NOT NULL,
 CONSTRAINT [PK_Sucursal] PRIMARY KEY CLUSTERED 
(
	[IdSucursal] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [seguridad].[Sucursal] ([IdSucursal], [IdCiudad], [Nombre], [Codigo], [Direccion], [EsActivo]) VALUES (N'140ca5b0-0293-4c71-9d36-3c66a8ebe4e6', N'1001eb5d-68bc-4d21-95ea-f9ce38ee9463', N'Central de todo el país', N'001', N'De donde fue X, 200m norte', 1)
INSERT [seguridad].[Sucursal] ([IdSucursal], [IdCiudad], [Nombre], [Codigo], [Direccion], [EsActivo]) VALUES (N'82f9ddf9-f6d8-40b8-8534-9d4cda092675', N'1001eb5d-68bc-4d21-95ea-f9ce38ee9463', N'Masaya', N'', N'Por ahí', 1)
/****** Object:  Table [seguridad].[Usuario]    Script Date: 08/19/2014 22:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [seguridad].[Usuario](
	[IdUsuario] [uniqueidentifier] NOT NULL,
	[IdSucursal] [uniqueidentifier] NOT NULL,
	[IdRol] [uniqueidentifier] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Sexo] [nvarchar](1) NOT NULL,
	[Login] [varchar](20) NOT NULL,
	[Pass] [nvarchar](512) NOT NULL,
	[Cargo] [nvarchar](100) NOT NULL,
	[Codigo] [nvarchar](20) NOT NULL,
	[FechaRegistro] [datetime] NULL,
	[EsActivo] [bit] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [seguridad].[Usuario] ([IdUsuario], [IdSucursal], [IdRol], [Nombre], [Sexo], [Login], [Pass], [Cargo], [Codigo], [FechaRegistro], [EsActivo]) VALUES (N'344bb27c-d0cf-4f4b-af0b-411b6056a31f', N'82f9ddf9-f6d8-40b8-8534-9d4cda092675', N'6b573de9-c72a-4048-b479-c63f6d75ec07', N'Fernando M. Gallegos Gutiérrez', N'M', N'galher', N'123', N'Administrador', N'01', CAST(0x0000A2CC001E73C2 AS DateTime), 1)
INSERT [seguridad].[Usuario] ([IdUsuario], [IdSucursal], [IdRol], [Nombre], [Sexo], [Login], [Pass], [Cargo], [Codigo], [FechaRegistro], [EsActivo]) VALUES (N'182c324f-643b-43fa-b904-5115060c6f9c', N'140ca5b0-0293-4c71-9d36-3c66a8ebe4e6', N'6b573de9-c72a-4048-b479-c63f6d75ec07', N'Abraham Molina Navarro', N'M', N'amolina', N'123', N'Administrador del Sistema', N'002', CAST(0x0000A38C015C52C2 AS DateTime), 1)
/****** Object:  Default [DF_ItemMenu_IdItemMenu]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[ItemMenu] ADD  CONSTRAINT [DF_ItemMenu_IdItemMenu]  DEFAULT (newid()) FOR [IdItemMenu]
GO
/****** Object:  Default [DF_ItemMenu_Ruta]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[ItemMenu] ADD  CONSTRAINT [DF_ItemMenu_Ruta]  DEFAULT ('~/') FOR [Ruta]
GO
/****** Object:  Default [DF_ItemMenu_Texto]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[ItemMenu] ADD  CONSTRAINT [DF_ItemMenu_Texto]  DEFAULT ('') FOR [Texto]
GO
/****** Object:  Default [DF_ItemMenu_Descripcion]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[ItemMenu] ADD  CONSTRAINT [DF_ItemMenu_Descripcion]  DEFAULT ('') FOR [Descripcion]
GO
/****** Object:  Default [DF_ItemMenu_Visible]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[ItemMenu] ADD  CONSTRAINT [DF_ItemMenu_Visible]  DEFAULT ((0)) FOR [Visible]
GO
/****** Object:  Default [DF_ItemMenu_FechaRegistro]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[ItemMenu] ADD  CONSTRAINT [DF_ItemMenu_FechaRegistro]  DEFAULT (getdate()) FOR [FechaRegistro]
GO
/****** Object:  Default [DF_ItemMenu_EsActivo]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[ItemMenu] ADD  CONSTRAINT [DF_ItemMenu_EsActivo]  DEFAULT ((1)) FOR [EsActivo]
GO
/****** Object:  Default [DF_Factor_IdFactor]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Catalogo].[Factor] ADD  CONSTRAINT [DF_Factor_IdFactor]  DEFAULT (newid()) FOR [IdFactor]
GO
/****** Object:  Default [DF__Factor__EsActivo__123EB7A3]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Catalogo].[Factor] ADD  CONSTRAINT [DF__Factor__EsActivo__123EB7A3]  DEFAULT ((1)) FOR [EsActivo]
GO
/****** Object:  Default [DF_Auditoria_IdAuditoria]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Auditoria].[Auditoria] ADD  CONSTRAINT [DF_Auditoria_IdAuditoria]  DEFAULT (newid()) FOR [IdAuditoria]
GO
/****** Object:  Default [DF_Auditoria_FechaAfectacion]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Auditoria].[Auditoria] ADD  CONSTRAINT [DF_Auditoria_FechaAfectacion]  DEFAULT (getdate()) FOR [FechaAfectacion]
GO
/****** Object:  Default [DF_Auditoria_IdTipo]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Auditoria].[Auditoria] ADD  CONSTRAINT [DF_Auditoria_IdTipo]  DEFAULT ((0)) FOR [IdTipo]
GO
/****** Object:  Default [DF_Auditoria_IdTabla]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Auditoria].[Auditoria] ADD  CONSTRAINT [DF_Auditoria_IdTabla]  DEFAULT ((0)) FOR [IdTabla]
GO
/****** Object:  Default [DF_Auditoria_Comentario]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Auditoria].[Auditoria] ADD  CONSTRAINT [DF_Auditoria_Comentario]  DEFAULT ('') FOR [Comentario]
GO
/****** Object:  Default [DF_Auditoria_Usuario]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Auditoria].[Auditoria] ADD  CONSTRAINT [DF_Auditoria_Usuario]  DEFAULT (N'sis') FOR [Usuario]
GO
/****** Object:  Default [DF_Rol_IdRol]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Rol] ADD  CONSTRAINT [DF_Rol_IdRol]  DEFAULT (newid()) FOR [IdRol]
GO
/****** Object:  Default [DF_Rol_Nombre]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Rol] ADD  CONSTRAINT [DF_Rol_Nombre]  DEFAULT ('') FOR [Nombre]
GO
/****** Object:  Default [DF_Rol_FechaRegistro]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Rol] ADD  CONSTRAINT [DF_Rol_FechaRegistro]  DEFAULT (getdate()) FOR [FechaRegistro]
GO
/****** Object:  Default [DF_Rol_EsActivo]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Rol] ADD  CONSTRAINT [DF_Rol_EsActivo]  DEFAULT ((1)) FOR [EsActivo]
GO
/****** Object:  Default [DF_Profesion_Nombre]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Catalogo].[Profesion] ADD  CONSTRAINT [DF_Profesion_Nombre]  DEFAULT (N'') FOR [Nombre]
GO
/****** Object:  Default [DF_Profesion_EsActivo]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Catalogo].[Profesion] ADD  CONSTRAINT [DF_Profesion_EsActivo]  DEFAULT ((1)) FOR [EsActivo]
GO
/****** Object:  Default [DF_Profesion_FechaRegistro]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Catalogo].[Profesion] ADD  CONSTRAINT [DF_Profesion_FechaRegistro]  DEFAULT (getdate()) FOR [FechaRegistro]
GO
/****** Object:  Default [DF_Pais_IdPais]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Pais] ADD  CONSTRAINT [DF_Pais_IdPais]  DEFAULT (newid()) FOR [IdPais]
GO
/****** Object:  Default [DF_Pais_Gentilicio]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Pais] ADD  CONSTRAINT [DF_Pais_Gentilicio]  DEFAULT ('') FOR [Nacionalidad]
GO
/****** Object:  Default [DF_Pais_EsActivo]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Pais] ADD  CONSTRAINT [DF_Pais_EsActivo]  DEFAULT ((1)) FOR [EsActivo]
GO
/****** Object:  Default [DF_Metodologia_IdMetodologia]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Catalogo].[Metodologia] ADD  CONSTRAINT [DF_Metodologia_IdMetodologia]  DEFAULT (newid()) FOR [IdMetodologia]
GO
/****** Object:  Default [DF_MatrizCalificacion_IdMatrizCalificacion]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Catalogo].[MatrizCalificacion] ADD  CONSTRAINT [DF_MatrizCalificacion_IdMatrizCalificacion]  DEFAULT (newid()) FOR [IdMatrizCalificacion]
GO
/****** Object:  Default [DF_MatrizCalificacion_Nombre]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Catalogo].[MatrizCalificacion] ADD  CONSTRAINT [DF_MatrizCalificacion_Nombre]  DEFAULT ('') FOR [Nombre]
GO
/****** Object:  Default [DF__MatrizCal__EsAct__151B244E]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Catalogo].[MatrizCalificacion] ADD  CONSTRAINT [DF__MatrizCal__EsAct__151B244E]  DEFAULT ((1)) FOR [EsActivo]
GO
/****** Object:  Default [DF_Table_1_IdItemMenu]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[ItemRol] ADD  CONSTRAINT [DF_Table_1_IdItemMenu]  DEFAULT (newid()) FOR [IdItemRol]
GO
/****** Object:  Default [DF_ItemRol_FechaRegistro]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[ItemRol] ADD  CONSTRAINT [DF_ItemRol_FechaRegistro]  DEFAULT (getdate()) FOR [FechaRegistro]
GO
/****** Object:  Default [DF_ItemRol_EsActivo]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[ItemRol] ADD  CONSTRAINT [DF_ItemRol_EsActivo]  DEFAULT ((1)) FOR [EsActivo]
GO
/****** Object:  Default [DF_Ciudad_IdCiudad]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Ciudad] ADD  CONSTRAINT [DF_Ciudad_IdCiudad]  DEFAULT (newid()) FOR [IdCiudad]
GO
/****** Object:  Default [DF_Ciudad_Nombre]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Ciudad] ADD  CONSTRAINT [DF_Ciudad_Nombre]  DEFAULT ('') FOR [Nombre]
GO
/****** Object:  Default [DF_Ciudad_EsActivo]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Ciudad] ADD  CONSTRAINT [DF_Ciudad_EsActivo]  DEFAULT ((1)) FOR [EsActivo]
GO
/****** Object:  Default [DF_Categoria_IdCategoria]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Catalogo].[Categoria] ADD  CONSTRAINT [DF_Categoria_IdCategoria]  DEFAULT (newid()) FOR [IdCategoria]
GO
/****** Object:  Default [DF__Categoria__EsAct__0C85DE4D]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Catalogo].[Categoria] ADD  CONSTRAINT [DF__Categoria__EsAct__0C85DE4D]  DEFAULT ((1)) FOR [EsActivo]
GO
/****** Object:  Default [DF_Clasificacion_IdClasificacion]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Catalogo].[Clasificacion] ADD  CONSTRAINT [DF_Clasificacion_IdClasificacion]  DEFAULT (newid()) FOR [IdClasificacion]
GO
/****** Object:  Default [DF__Clasifica__EsAct__0F624AF8]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Catalogo].[Clasificacion] ADD  CONSTRAINT [DF__Clasifica__EsAct__0F624AF8]  DEFAULT ((1)) FOR [EsActivo]
GO
/****** Object:  Default [DF_Sucursal_IdSucursal]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Sucursal] ADD  CONSTRAINT [DF_Sucursal_IdSucursal]  DEFAULT (newid()) FOR [IdSucursal]
GO
/****** Object:  Default [DF_Sucursal_Nombre]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Sucursal] ADD  CONSTRAINT [DF_Sucursal_Nombre]  DEFAULT ('') FOR [Nombre]
GO
/****** Object:  Default [DF_Sucursal_Codigo]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Sucursal] ADD  CONSTRAINT [DF_Sucursal_Codigo]  DEFAULT ('') FOR [Codigo]
GO
/****** Object:  Default [DF_Sucursal_Direccion]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Sucursal] ADD  CONSTRAINT [DF_Sucursal_Direccion]  DEFAULT ('') FOR [Direccion]
GO
/****** Object:  Default [DF_Sucursal_EsActivo]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Sucursal] ADD  CONSTRAINT [DF_Sucursal_EsActivo]  DEFAULT ((1)) FOR [EsActivo]
GO
/****** Object:  Default [DF_Usuario_IdUsuario]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Usuario] ADD  CONSTRAINT [DF_Usuario_IdUsuario]  DEFAULT (newid()) FOR [IdUsuario]
GO
/****** Object:  Default [DF_Usuario_Nombre]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Usuario] ADD  CONSTRAINT [DF_Usuario_Nombre]  DEFAULT ('') FOR [Nombre]
GO
/****** Object:  Default [DF_Usuario_Login]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Usuario] ADD  CONSTRAINT [DF_Usuario_Login]  DEFAULT ('') FOR [Login]
GO
/****** Object:  Default [DF_Usuario_Pass]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Usuario] ADD  CONSTRAINT [DF_Usuario_Pass]  DEFAULT ('') FOR [Pass]
GO
/****** Object:  Default [DF_Usuario_Cargo]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Usuario] ADD  CONSTRAINT [DF_Usuario_Cargo]  DEFAULT ('') FOR [Cargo]
GO
/****** Object:  Default [DF_Usuario_Codigo]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Usuario] ADD  CONSTRAINT [DF_Usuario_Codigo]  DEFAULT ('') FOR [Codigo]
GO
/****** Object:  Default [DF_Usuario_FechaRegistro]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Usuario] ADD  CONSTRAINT [DF_Usuario_FechaRegistro]  DEFAULT (getdate()) FOR [FechaRegistro]
GO
/****** Object:  Default [DF_Usuario_EsActivo]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Usuario] ADD  CONSTRAINT [DF_Usuario_EsActivo]  DEFAULT ((1)) FOR [EsActivo]
GO
/****** Object:  ForeignKey [FK_ItemMenu_ItemMenu]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[ItemMenu]  WITH CHECK ADD  CONSTRAINT [FK_ItemMenu_ItemMenu] FOREIGN KEY([IdItemMenuPadre])
REFERENCES [seguridad].[ItemMenu] ([IdItemMenu])
GO
ALTER TABLE [seguridad].[ItemMenu] CHECK CONSTRAINT [FK_ItemMenu_ItemMenu]
GO
/****** Object:  ForeignKey [FK_ItemRol_ItemMenu]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[ItemRol]  WITH CHECK ADD  CONSTRAINT [FK_ItemRol_ItemMenu] FOREIGN KEY([IdItemMenu])
REFERENCES [seguridad].[ItemMenu] ([IdItemMenu])
GO
ALTER TABLE [seguridad].[ItemRol] CHECK CONSTRAINT [FK_ItemRol_ItemMenu]
GO
/****** Object:  ForeignKey [FK_Ciudad_Pais]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Ciudad]  WITH CHECK ADD  CONSTRAINT [FK_Ciudad_Pais] FOREIGN KEY([IdPais])
REFERENCES [seguridad].[Pais] ([IdPais])
GO
ALTER TABLE [seguridad].[Ciudad] CHECK CONSTRAINT [FK_Ciudad_Pais]
GO
/****** Object:  ForeignKey [FK_Categoria_Factor]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Catalogo].[Categoria]  WITH CHECK ADD  CONSTRAINT [FK_Categoria_Factor] FOREIGN KEY([IdFactor])
REFERENCES [Catalogo].[Factor] ([IdFactor])
GO
ALTER TABLE [Catalogo].[Categoria] CHECK CONSTRAINT [FK_Categoria_Factor]
GO
/****** Object:  ForeignKey [FK_Clasificacion_Categoria]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [Catalogo].[Clasificacion]  WITH CHECK ADD  CONSTRAINT [FK_Clasificacion_Categoria] FOREIGN KEY([IdCategoria])
REFERENCES [Catalogo].[Categoria] ([IdCategoria])
GO
ALTER TABLE [Catalogo].[Clasificacion] CHECK CONSTRAINT [FK_Clasificacion_Categoria]
GO
/****** Object:  ForeignKey [FK_Sucursal_Ciudad]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Sucursal]  WITH CHECK ADD  CONSTRAINT [FK_Sucursal_Ciudad] FOREIGN KEY([IdCiudad])
REFERENCES [seguridad].[Ciudad] ([IdCiudad])
GO
ALTER TABLE [seguridad].[Sucursal] CHECK CONSTRAINT [FK_Sucursal_Ciudad]
GO
/****** Object:  ForeignKey [FK_Usuario_Sucursal]    Script Date: 08/19/2014 22:10:25 ******/
ALTER TABLE [seguridad].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Sucursal] FOREIGN KEY([IdSucursal])
REFERENCES [seguridad].[Sucursal] ([IdSucursal])
GO
ALTER TABLE [seguridad].[Usuario] CHECK CONSTRAINT [FK_Usuario_Sucursal]
GO
