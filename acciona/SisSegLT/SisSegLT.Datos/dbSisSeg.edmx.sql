
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/24/2014 19:15:40
-- Generated from EDMX file: E:\Proyectos VS2013\Asodenic\SisSegLT\SisSegLT.Datos\dbSisSeg.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ASODENICDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[Cliente].[FK_AprobacionInstitucion_Cliente]', 'F') IS NOT NULL
    ALTER TABLE [Cliente].[AprobacionInstitucion] DROP CONSTRAINT [FK_AprobacionInstitucion_Cliente];
GO
IF OBJECT_ID(N'[Catalogo].[FK_Categoria_Factor]', 'F') IS NOT NULL
    ALTER TABLE [Catalogo].[Categoria] DROP CONSTRAINT [FK_Categoria_Factor];
GO
IF OBJECT_ID(N'[seguridad].[FK_Ciudad_Pais]', 'F') IS NOT NULL
    ALTER TABLE [seguridad].[Ciudad] DROP CONSTRAINT [FK_Ciudad_Pais];
GO
IF OBJECT_ID(N'[Catalogo].[FK_Clasificacion_Categoria]', 'F') IS NOT NULL
    ALTER TABLE [Catalogo].[Clasificacion] DROP CONSTRAINT [FK_Clasificacion_Categoria];
GO
IF OBJECT_ID(N'[Cliente].[FK_ClienteEvaluacion_Cliente]', 'F') IS NOT NULL
    ALTER TABLE [Cliente].[ClienteEvaluacion] DROP CONSTRAINT [FK_ClienteEvaluacion_Cliente];
GO
IF OBJECT_ID(N'[Cliente].[FK_Contacto_Cliente]', 'F') IS NOT NULL
    ALTER TABLE [Cliente].[Contacto] DROP CONSTRAINT [FK_Contacto_Cliente];
GO
IF OBJECT_ID(N'[Cliente].[FK_DatosNegocio_Cliente]', 'F') IS NOT NULL
    ALTER TABLE [Cliente].[DatosNegocio] DROP CONSTRAINT [FK_DatosNegocio_Cliente];
GO
IF OBJECT_ID(N'[Cliente].[FK_DetalleCliente_Cliente]', 'F') IS NOT NULL
    ALTER TABLE [Cliente].[DetalleCliente] DROP CONSTRAINT [FK_DetalleCliente_Cliente];
GO
IF OBJECT_ID(N'[Cliente].[FK_DocumentosNegocio_DatosNegocio]', 'F') IS NOT NULL
    ALTER TABLE [Cliente].[DocumentosNegocio] DROP CONSTRAINT [FK_DocumentosNegocio_DatosNegocio];
GO
IF OBJECT_ID(N'[Cliente].[FK_Domicilio_Cliente]', 'F') IS NOT NULL
    ALTER TABLE [Cliente].[Domicilio] DROP CONSTRAINT [FK_Domicilio_Cliente];
GO
IF OBJECT_ID(N'[Cliente].[FK_EvaluacionCategoriaClasificacion_EvaluacionCategoria]', 'F') IS NOT NULL
    ALTER TABLE [Cliente].[EvaluacionCategoriaClasificacion] DROP CONSTRAINT [FK_EvaluacionCategoriaClasificacion_EvaluacionCategoria];
GO
IF OBJECT_ID(N'[Cliente].[FK_IdCliente]', 'F') IS NOT NULL
    ALTER TABLE [Cliente].[ResumenTransaccion] DROP CONSTRAINT [FK_IdCliente];
GO
IF OBJECT_ID(N'[Cliente].[FK_IdClienteEvaluacion]', 'F') IS NOT NULL
    ALTER TABLE [Cliente].[EvaluacionCategoria] DROP CONSTRAINT [FK_IdClienteEvaluacion];
GO
IF OBJECT_ID(N'[seguridad].[FK_ItemMenu_ItemMenu]', 'F') IS NOT NULL
    ALTER TABLE [seguridad].[ItemMenu] DROP CONSTRAINT [FK_ItemMenu_ItemMenu];
GO
IF OBJECT_ID(N'[seguridad].[FK_ItemRol_ItemMenu]', 'F') IS NOT NULL
    ALTER TABLE [seguridad].[ItemRol] DROP CONSTRAINT [FK_ItemRol_ItemMenu];
GO
IF OBJECT_ID(N'[Cliente].[FK_NegocioProveedores_DatosNegocio]', 'F') IS NOT NULL
    ALTER TABLE [Cliente].[NegocioProveedores] DROP CONSTRAINT [FK_NegocioProveedores_DatosNegocio];
GO
IF OBJECT_ID(N'[Cliente].[FK_ReferenciaCrediticia_Cliente]', 'F') IS NOT NULL
    ALTER TABLE [Cliente].[ReferenciaCrediticia] DROP CONSTRAINT [FK_ReferenciaCrediticia_Cliente];
GO
IF OBJECT_ID(N'[Cliente].[FK_Referencias_Cliente]', 'F') IS NOT NULL
    ALTER TABLE [Cliente].[Referencias] DROP CONSTRAINT [FK_Referencias_Cliente];
GO
IF OBJECT_ID(N'[seguridad].[FK_Sucursal_Ciudad]', 'F') IS NOT NULL
    ALTER TABLE [seguridad].[Sucursal] DROP CONSTRAINT [FK_Sucursal_Ciudad];
GO
IF OBJECT_ID(N'[seguridad].[FK_Usuario_Sucursal]', 'F') IS NOT NULL
    ALTER TABLE [seguridad].[Usuario] DROP CONSTRAINT [FK_Usuario_Sucursal];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[Auditoria].[Auditoria]', 'U') IS NOT NULL
    DROP TABLE [Auditoria].[Auditoria];
GO
IF OBJECT_ID(N'[Catalogo].[Categoria]', 'U') IS NOT NULL
    DROP TABLE [Catalogo].[Categoria];
GO
IF OBJECT_ID(N'[Catalogo].[Clasificacion]', 'U') IS NOT NULL
    DROP TABLE [Catalogo].[Clasificacion];
GO
IF OBJECT_ID(N'[Catalogo].[Factor]', 'U') IS NOT NULL
    DROP TABLE [Catalogo].[Factor];
GO
IF OBJECT_ID(N'[Catalogo].[MatrizCalificacion]', 'U') IS NOT NULL
    DROP TABLE [Catalogo].[MatrizCalificacion];
GO
IF OBJECT_ID(N'[Catalogo].[Metodologia]', 'U') IS NOT NULL
    DROP TABLE [Catalogo].[Metodologia];
GO
IF OBJECT_ID(N'[Catalogo].[Profesion]', 'U') IS NOT NULL
    DROP TABLE [Catalogo].[Profesion];
GO
IF OBJECT_ID(N'[Cliente].[AprobacionInstitucion]', 'U') IS NOT NULL
    DROP TABLE [Cliente].[AprobacionInstitucion];
GO
IF OBJECT_ID(N'[Cliente].[Cliente]', 'U') IS NOT NULL
    DROP TABLE [Cliente].[Cliente];
GO
IF OBJECT_ID(N'[Cliente].[ClienteEvaluacion]', 'U') IS NOT NULL
    DROP TABLE [Cliente].[ClienteEvaluacion];
GO
IF OBJECT_ID(N'[Cliente].[Contacto]', 'U') IS NOT NULL
    DROP TABLE [Cliente].[Contacto];
GO
IF OBJECT_ID(N'[Cliente].[DatosNegocio]', 'U') IS NOT NULL
    DROP TABLE [Cliente].[DatosNegocio];
GO
IF OBJECT_ID(N'[Cliente].[DetalleCliente]', 'U') IS NOT NULL
    DROP TABLE [Cliente].[DetalleCliente];
GO
IF OBJECT_ID(N'[Cliente].[DocumentosNegocio]', 'U') IS NOT NULL
    DROP TABLE [Cliente].[DocumentosNegocio];
GO
IF OBJECT_ID(N'[Cliente].[Domicilio]', 'U') IS NOT NULL
    DROP TABLE [Cliente].[Domicilio];
GO
IF OBJECT_ID(N'[Cliente].[EvaluacionCategoria]', 'U') IS NOT NULL
    DROP TABLE [Cliente].[EvaluacionCategoria];
GO
IF OBJECT_ID(N'[Cliente].[EvaluacionCategoriaClasificacion]', 'U') IS NOT NULL
    DROP TABLE [Cliente].[EvaluacionCategoriaClasificacion];
GO
IF OBJECT_ID(N'[Cliente].[NegocioProveedores]', 'U') IS NOT NULL
    DROP TABLE [Cliente].[NegocioProveedores];
GO
IF OBJECT_ID(N'[Cliente].[ReferenciaCrediticia]', 'U') IS NOT NULL
    DROP TABLE [Cliente].[ReferenciaCrediticia];
GO
IF OBJECT_ID(N'[Cliente].[Referencias]', 'U') IS NOT NULL
    DROP TABLE [Cliente].[Referencias];
GO
IF OBJECT_ID(N'[Cliente].[ResumenTransaccion]', 'U') IS NOT NULL
    DROP TABLE [Cliente].[ResumenTransaccion];
GO
IF OBJECT_ID(N'[seguridad].[Ciudad]', 'U') IS NOT NULL
    DROP TABLE [seguridad].[Ciudad];
GO
IF OBJECT_ID(N'[seguridad].[ItemMenu]', 'U') IS NOT NULL
    DROP TABLE [seguridad].[ItemMenu];
GO
IF OBJECT_ID(N'[seguridad].[ItemRol]', 'U') IS NOT NULL
    DROP TABLE [seguridad].[ItemRol];
GO
IF OBJECT_ID(N'[seguridad].[Pais]', 'U') IS NOT NULL
    DROP TABLE [seguridad].[Pais];
GO
IF OBJECT_ID(N'[seguridad].[Rol]', 'U') IS NOT NULL
    DROP TABLE [seguridad].[Rol];
GO
IF OBJECT_ID(N'[seguridad].[Sucursal]', 'U') IS NOT NULL
    DROP TABLE [seguridad].[Sucursal];
GO
IF OBJECT_ID(N'[seguridad].[Usuario]', 'U') IS NOT NULL
    DROP TABLE [seguridad].[Usuario];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Ciudad'
CREATE TABLE [dbo].[Ciudad] (
    [IdCiudad] uniqueidentifier  NOT NULL,
    [IdPais] uniqueidentifier  NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [EsActivo] bit  NOT NULL
);
GO

-- Creating table 'ItemMenu'
CREATE TABLE [dbo].[ItemMenu] (
    [IdItemMenu] uniqueidentifier  NOT NULL,
    [IdItemMenuPadre] uniqueidentifier  NULL,
    [Ruta] varchar(256)  NOT NULL,
    [Texto] nvarchar(50)  NOT NULL,
    [Descripcion] nvarchar(256)  NOT NULL,
    [Visible] bit  NOT NULL,
    [FechaRegistro] datetime  NULL,
    [EsActivo] bit  NOT NULL
);
GO

-- Creating table 'ItemRol'
CREATE TABLE [dbo].[ItemRol] (
    [IdItemRol] uniqueidentifier  NOT NULL,
    [IdItemMenu] uniqueidentifier  NOT NULL,
    [IdRol] uniqueidentifier  NOT NULL,
    [FechaRegistro] datetime  NULL,
    [EsActivo] bit  NOT NULL
);
GO

-- Creating table 'Pais'
CREATE TABLE [dbo].[Pais] (
    [IdPais] uniqueidentifier  NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [Nacionalidad] nvarchar(100)  NULL,
    [EsActivo] bit  NOT NULL
);
GO

-- Creating table 'Rol'
CREATE TABLE [dbo].[Rol] (
    [IdRol] uniqueidentifier  NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [FechaRegistro] datetime  NULL,
    [EsActivo] bit  NOT NULL
);
GO

-- Creating table 'Sucursal'
CREATE TABLE [dbo].[Sucursal] (
    [IdSucursal] uniqueidentifier  NOT NULL,
    [IdCiudad] uniqueidentifier  NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [Codigo] nvarchar(20)  NOT NULL,
    [Direccion] nvarchar(150)  NOT NULL,
    [EsActivo] bit  NOT NULL
);
GO

-- Creating table 'Usuario'
CREATE TABLE [dbo].[Usuario] (
    [IdUsuario] uniqueidentifier  NOT NULL,
    [IdSucursal] uniqueidentifier  NOT NULL,
    [IdRol] uniqueidentifier  NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [Sexo] nvarchar(1)  NOT NULL,
    [Login] varchar(20)  NOT NULL,
    [Pass] nvarchar(512)  NOT NULL,
    [Cargo] nvarchar(100)  NOT NULL,
    [Codigo] nvarchar(20)  NOT NULL,
    [FechaRegistro] datetime  NULL,
    [EsActivo] bit  NOT NULL
);
GO

-- Creating table 'Auditoria'
CREATE TABLE [dbo].[Auditoria] (
    [IdAuditoria] uniqueidentifier  NOT NULL,
    [IdAfectado] uniqueidentifier  NOT NULL,
    [IdReferencia] uniqueidentifier  NULL,
    [FechaAfectacion] datetime  NOT NULL,
    [IdTipo] int  NOT NULL,
    [IdTabla] int  NOT NULL,
    [Comentario] nvarchar(max)  NOT NULL,
    [Usuario] nvarchar(50)  NULL,
    [UsuarioPC] nvarchar(50)  NULL,
    [UsuarioIP] nvarchar(50)  NULL
);
GO

-- Creating table 'Categoria'
CREATE TABLE [dbo].[Categoria] (
    [IdCategoria] uniqueidentifier  NOT NULL,
    [IdFactor] uniqueidentifier  NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [Ponderacion] int  NOT NULL,
    [EsActivo] bit  NOT NULL
);
GO

-- Creating table 'Clasificacion'
CREATE TABLE [dbo].[Clasificacion] (
    [IdClasificacion] uniqueidentifier  NOT NULL,
    [IdCategoria] uniqueidentifier  NOT NULL,
    [Nombre] nvarchar(500)  NOT NULL,
    [Puntuacion] int  NOT NULL,
    [EsActivo] bit  NOT NULL
);
GO

-- Creating table 'Factor'
CREATE TABLE [dbo].[Factor] (
    [IdFactor] uniqueidentifier  NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [EsActivo] bit  NOT NULL
);
GO

-- Creating table 'MatrizCalificacion'
CREATE TABLE [dbo].[MatrizCalificacion] (
    [IdMatrizCalificacion] uniqueidentifier  NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [ValorMin] decimal(10,2)  NOT NULL,
    [ValorMax] decimal(10,2)  NOT NULL,
    [Impacto] int  NOT NULL,
    [EsActivo] bit  NOT NULL,
    [Color] nvarchar(50)  NULL
);
GO

-- Creating table 'Metodologia'
CREATE TABLE [dbo].[Metodologia] (
    [IdMetodologia] uniqueidentifier  NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [EsActivo] bit  NOT NULL
);
GO

-- Creating table 'Profesion'
CREATE TABLE [dbo].[Profesion] (
    [IdProfesion] uniqueidentifier  NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [EsActivo] bit  NOT NULL,
    [FechaRegistro] datetime  NULL
);
GO

-- Creating table 'AprobacionInstitucion'
CREATE TABLE [dbo].[AprobacionInstitucion] (
    [IdAprobacionInstitucion] uniqueidentifier  NOT NULL,
    [IdCliente] uniqueidentifier  NOT NULL,
    [Descripcion] nvarchar(max)  NULL,
    [IdUsuario] uniqueidentifier  NULL,
    [FechaHoraVerificacion] datetime  NULL
);
GO

-- Creating table 'Cliente'
CREATE TABLE [dbo].[Cliente] (
    [IdCliente] uniqueidentifier  NOT NULL,
    [IdUsuario] uniqueidentifier  NOT NULL,
    [IdSucursal] uniqueidentifier  NOT NULL,
    [NombreCompleto] nvarchar(100)  NOT NULL,
    [NoIdentificacion] nvarchar(15)  NOT NULL,
    [EstadoPerfil] int  NOT NULL,
    [FechaPerfil] datetime  NOT NULL,
    [EsActivo] bit  NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    [Usuario] nvarchar(50)  NOT NULL,
    [UserIP] nvarchar(50)  NOT NULL,
    [UserPC] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ClienteEvaluacion'
CREATE TABLE [dbo].[ClienteEvaluacion] (
    [IdClienteEvaluacion] uniqueidentifier  NOT NULL,
    [IdCliente] uniqueidentifier  NOT NULL,
    [IdUsuario] uniqueidentifier  NULL,
    [IdMetodologia] uniqueidentifier  NULL,
    [Descripcion] nvarchar(max)  NULL,
    [Puntaje] decimal(10,2)  NULL,
    [NoExpediente] nvarchar(10)  NULL,
    [NoCredito] nvarchar(10)  NULL,
    [FechaHoraEvaluacion] datetime  NULL,
    [EsActivo] bit  NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    [Usuario] nvarchar(50)  NOT NULL,
    [UserPC] nvarchar(50)  NOT NULL,
    [UserIP] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'DocumentosNegocio'
CREATE TABLE [dbo].[DocumentosNegocio] (
    [IdDocumentoNegocio] uniqueidentifier  NOT NULL,
    [TipoRegistros] nvarchar(50)  NOT NULL,
    [Institucion] nvarchar(100)  NOT NULL,
    [FechaEmision] datetime  NULL,
    [FechaVencimiento] datetime  NOT NULL,
    [IdDatosNegocio] uniqueidentifier  NULL,
    [EsActivo] bit  NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    [Usuario] nvarchar(50)  NOT NULL,
    [UserPC] nvarchar(50)  NOT NULL,
    [UserIP] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Domicilio'
CREATE TABLE [dbo].[Domicilio] (
    [IdDomicilio] uniqueidentifier  NOT NULL,
    [IdCliente] uniqueidentifier  NOT NULL,
    [Descripcion] nvarchar(800)  NOT NULL,
    [EsAlquilada] bit  NOT NULL,
    [EsPropia] bit  NOT NULL,
    [Familiar] bit  NOT NULL,
    [TiempoResidir] int  NOT NULL,
    [EsActivo] bit  NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    [Usuario] nvarchar(50)  NOT NULL,
    [UserPC] nvarchar(50)  NOT NULL,
    [UserIP] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'EvaluacionCategoria'
CREATE TABLE [dbo].[EvaluacionCategoria] (
    [IdEvaluacionCategoria] uniqueidentifier  NOT NULL,
    [IdCategoria] uniqueidentifier  NOT NULL,
    [IdClienteEvaluacion] uniqueidentifier  NOT NULL,
    [CalculoRiesgo] decimal(10,2)  NOT NULL,
    [EsActivo] bit  NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    [Usuario] nvarchar(50)  NOT NULL,
    [UserPC] nvarchar(50)  NOT NULL,
    [UserIP] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'EvaluacionCategoriaClasificacion'
CREATE TABLE [dbo].[EvaluacionCategoriaClasificacion] (
    [IdEvaluacionCategoriaCategoria] uniqueidentifier  NOT NULL,
    [IdEvaluacionCategoria] uniqueidentifier  NOT NULL,
    [IdClasificacion] uniqueidentifier  NOT NULL,
    [EsActivo] bit  NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    [Usuario] nvarchar(50)  NOT NULL,
    [UserPC] nvarchar(50)  NOT NULL,
    [UserIP] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'NegocioProveedores'
CREATE TABLE [dbo].[NegocioProveedores] (
    [IdNegocioProveedor] uniqueidentifier  NOT NULL,
    [IdDatosNegocio] uniqueidentifier  NOT NULL,
    [Nombre] nvarchar(100)  NOT NULL,
    [EsCliente] bit  NOT NULL,
    [EsActivo] bit  NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    [Usuario] nvarchar(50)  NOT NULL,
    [UserPC] nvarchar(50)  NOT NULL,
    [UserIP] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ResumenTransaccion'
CREATE TABLE [dbo].[ResumenTransaccion] (
    [IdTransaccionesInstitucion] uniqueidentifier  NOT NULL,
    [IdCliente] uniqueidentifier  NOT NULL,
    [IdMetodologia] uniqueidentifier  NOT NULL,
    [CantidadPrestamos] int  NOT NULL,
    [MontoMinimo] decimal(10,2)  NULL,
    [MontoMaximo] decimal(10,2)  NULL,
    [FechaInicioCredito] datetime  NULL,
    [FechaFinCredito] datetime  NULL,
    [MontoPromedio] decimal(10,2)  NULL,
    [MaximoDiasMora] int  NULL,
    [EsActivo] bit  NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    [Usuario] nvarchar(50)  NOT NULL,
    [UserPC] nvarchar(50)  NOT NULL,
    [UserIP] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'DetalleCliente'
CREATE TABLE [dbo].[DetalleCliente] (
    [IdDetalleCliente] uniqueidentifier  NOT NULL,
    [IdCliente] uniqueidentifier  NOT NULL,
    [FechaNacimiento] datetime  NOT NULL,
    [EstadoCivil] nvarchar(50)  NOT NULL,
    [IdPaisNacimiento] uniqueidentifier  NOT NULL,
    [Nacionalidad] nvarchar(100)  NOT NULL,
    [Alias] nvarchar(100)  NULL,
    [CorreoElectronico] nvarchar(50)  NULL,
    [IdProfesion] uniqueidentifier  NULL,
    [Ocupacion] nvarchar(100)  NULL,
    [MiembrosFamilia] int  NULL,
    [Ingresos] float  NULL,
    [EsActivo] bit  NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    [Usuario] nvarchar(50)  NOT NULL,
    [UserIP] nvarchar(50)  NOT NULL,
    [UserPC] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Contacto'
CREATE TABLE [dbo].[Contacto] (
    [IdContacto] uniqueidentifier  NOT NULL,
    [IdCliente] uniqueidentifier  NOT NULL,
    [TipoContacto] int  NOT NULL,
    [Descripcion] nvarchar(250)  NOT NULL,
    [EsActivo] bit  NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    [Usuario] nvarchar(50)  NOT NULL,
    [UserPC] nvarchar(50)  NOT NULL,
    [UserIP] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ReferenciaCrediticia'
CREATE TABLE [dbo].[ReferenciaCrediticia] (
    [IdReferenciaCrediticia] uniqueidentifier  NOT NULL,
    [IdCliente] uniqueidentifier  NOT NULL,
    [Banco] nvarchar(100)  NULL,
    [Monto] decimal(19,4)  NULL,
    [Plazo] nvarchar(100)  NULL,
    [EsActivo] bit  NULL,
    [FechaRegistro] datetime  NULL,
    [Usuario] nvarchar(100)  NULL,
    [UsuarioPC] nvarchar(100)  NULL,
    [UsuarioIP] nvarchar(100)  NULL
);
GO

-- Creating table 'Referencias'
CREATE TABLE [dbo].[Referencias] (
    [IdReferencia] uniqueidentifier  NOT NULL,
    [IdCliente] uniqueidentifier  NOT NULL,
    [NombreCompleto] nvarchar(100)  NOT NULL,
    [NoIdentificacion] nvarchar(14)  NOT NULL,
    [IdProfesion] uniqueidentifier  NULL,
    [Telefono] nvarchar(50)  NOT NULL,
    [Tiempo] nvarchar(50)  NULL,
    [CentroLaboral] nvarchar(100)  NULL,
    [Direccion] nvarchar(300)  NOT NULL,
    [EsActivo] bit  NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    [Usuario] nvarchar(50)  NOT NULL,
    [UserPC] nvarchar(50)  NOT NULL,
    [UserIP] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'DatosNegocio'
CREATE TABLE [dbo].[DatosNegocio] (
    [IdDatosNegocio] uniqueidentifier  NOT NULL,
    [IdCliente] uniqueidentifier  NOT NULL,
    [UbicacionNegocio] nvarchar(500)  NOT NULL,
    [Tiempo] nvarchar(350)  NOT NULL,
    [TipoNegocio] nvarchar(200)  NOT NULL,
    [EsPropio] bit  NOT NULL,
    [Alquila] bit  NOT NULL,
    [Familiar] bit  NOT NULL,
    [IngresoVolumen] float  NOT NULL,
    [EsActivo] bit  NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    [Usuario] nvarchar(50)  NOT NULL,
    [UserPC] nvarchar(50)  NOT NULL,
    [UserIP] nvarchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdCiudad] in table 'Ciudad'
ALTER TABLE [dbo].[Ciudad]
ADD CONSTRAINT [PK_Ciudad]
    PRIMARY KEY CLUSTERED ([IdCiudad] ASC);
GO

-- Creating primary key on [IdItemMenu] in table 'ItemMenu'
ALTER TABLE [dbo].[ItemMenu]
ADD CONSTRAINT [PK_ItemMenu]
    PRIMARY KEY CLUSTERED ([IdItemMenu] ASC);
GO

-- Creating primary key on [IdItemRol] in table 'ItemRol'
ALTER TABLE [dbo].[ItemRol]
ADD CONSTRAINT [PK_ItemRol]
    PRIMARY KEY CLUSTERED ([IdItemRol] ASC);
GO

-- Creating primary key on [IdPais] in table 'Pais'
ALTER TABLE [dbo].[Pais]
ADD CONSTRAINT [PK_Pais]
    PRIMARY KEY CLUSTERED ([IdPais] ASC);
GO

-- Creating primary key on [IdRol] in table 'Rol'
ALTER TABLE [dbo].[Rol]
ADD CONSTRAINT [PK_Rol]
    PRIMARY KEY CLUSTERED ([IdRol] ASC);
GO

-- Creating primary key on [IdSucursal] in table 'Sucursal'
ALTER TABLE [dbo].[Sucursal]
ADD CONSTRAINT [PK_Sucursal]
    PRIMARY KEY CLUSTERED ([IdSucursal] ASC);
GO

-- Creating primary key on [IdUsuario] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [PK_Usuario]
    PRIMARY KEY CLUSTERED ([IdUsuario] ASC);
GO

-- Creating primary key on [IdAuditoria] in table 'Auditoria'
ALTER TABLE [dbo].[Auditoria]
ADD CONSTRAINT [PK_Auditoria]
    PRIMARY KEY CLUSTERED ([IdAuditoria] ASC);
GO

-- Creating primary key on [IdCategoria] in table 'Categoria'
ALTER TABLE [dbo].[Categoria]
ADD CONSTRAINT [PK_Categoria]
    PRIMARY KEY CLUSTERED ([IdCategoria] ASC);
GO

-- Creating primary key on [IdClasificacion] in table 'Clasificacion'
ALTER TABLE [dbo].[Clasificacion]
ADD CONSTRAINT [PK_Clasificacion]
    PRIMARY KEY CLUSTERED ([IdClasificacion] ASC);
GO

-- Creating primary key on [IdFactor] in table 'Factor'
ALTER TABLE [dbo].[Factor]
ADD CONSTRAINT [PK_Factor]
    PRIMARY KEY CLUSTERED ([IdFactor] ASC);
GO

-- Creating primary key on [IdMatrizCalificacion] in table 'MatrizCalificacion'
ALTER TABLE [dbo].[MatrizCalificacion]
ADD CONSTRAINT [PK_MatrizCalificacion]
    PRIMARY KEY CLUSTERED ([IdMatrizCalificacion] ASC);
GO

-- Creating primary key on [IdMetodologia] in table 'Metodologia'
ALTER TABLE [dbo].[Metodologia]
ADD CONSTRAINT [PK_Metodologia]
    PRIMARY KEY CLUSTERED ([IdMetodologia] ASC);
GO

-- Creating primary key on [IdProfesion] in table 'Profesion'
ALTER TABLE [dbo].[Profesion]
ADD CONSTRAINT [PK_Profesion]
    PRIMARY KEY CLUSTERED ([IdProfesion] ASC);
GO

-- Creating primary key on [IdAprobacionInstitucion] in table 'AprobacionInstitucion'
ALTER TABLE [dbo].[AprobacionInstitucion]
ADD CONSTRAINT [PK_AprobacionInstitucion]
    PRIMARY KEY CLUSTERED ([IdAprobacionInstitucion] ASC);
GO

-- Creating primary key on [IdCliente] in table 'Cliente'
ALTER TABLE [dbo].[Cliente]
ADD CONSTRAINT [PK_Cliente]
    PRIMARY KEY CLUSTERED ([IdCliente] ASC);
GO

-- Creating primary key on [IdClienteEvaluacion] in table 'ClienteEvaluacion'
ALTER TABLE [dbo].[ClienteEvaluacion]
ADD CONSTRAINT [PK_ClienteEvaluacion]
    PRIMARY KEY CLUSTERED ([IdClienteEvaluacion] ASC);
GO

-- Creating primary key on [IdDocumentoNegocio] in table 'DocumentosNegocio'
ALTER TABLE [dbo].[DocumentosNegocio]
ADD CONSTRAINT [PK_DocumentosNegocio]
    PRIMARY KEY CLUSTERED ([IdDocumentoNegocio] ASC);
GO

-- Creating primary key on [IdDomicilio] in table 'Domicilio'
ALTER TABLE [dbo].[Domicilio]
ADD CONSTRAINT [PK_Domicilio]
    PRIMARY KEY CLUSTERED ([IdDomicilio] ASC);
GO

-- Creating primary key on [IdEvaluacionCategoria] in table 'EvaluacionCategoria'
ALTER TABLE [dbo].[EvaluacionCategoria]
ADD CONSTRAINT [PK_EvaluacionCategoria]
    PRIMARY KEY CLUSTERED ([IdEvaluacionCategoria] ASC);
GO

-- Creating primary key on [IdEvaluacionCategoriaCategoria] in table 'EvaluacionCategoriaClasificacion'
ALTER TABLE [dbo].[EvaluacionCategoriaClasificacion]
ADD CONSTRAINT [PK_EvaluacionCategoriaClasificacion]
    PRIMARY KEY CLUSTERED ([IdEvaluacionCategoriaCategoria] ASC);
GO

-- Creating primary key on [IdNegocioProveedor] in table 'NegocioProveedores'
ALTER TABLE [dbo].[NegocioProveedores]
ADD CONSTRAINT [PK_NegocioProveedores]
    PRIMARY KEY CLUSTERED ([IdNegocioProveedor] ASC);
GO

-- Creating primary key on [IdTransaccionesInstitucion] in table 'ResumenTransaccion'
ALTER TABLE [dbo].[ResumenTransaccion]
ADD CONSTRAINT [PK_ResumenTransaccion]
    PRIMARY KEY CLUSTERED ([IdTransaccionesInstitucion] ASC);
GO

-- Creating primary key on [IdDetalleCliente] in table 'DetalleCliente'
ALTER TABLE [dbo].[DetalleCliente]
ADD CONSTRAINT [PK_DetalleCliente]
    PRIMARY KEY CLUSTERED ([IdDetalleCliente] ASC);
GO

-- Creating primary key on [IdContacto] in table 'Contacto'
ALTER TABLE [dbo].[Contacto]
ADD CONSTRAINT [PK_Contacto]
    PRIMARY KEY CLUSTERED ([IdContacto] ASC);
GO

-- Creating primary key on [IdReferenciaCrediticia] in table 'ReferenciaCrediticia'
ALTER TABLE [dbo].[ReferenciaCrediticia]
ADD CONSTRAINT [PK_ReferenciaCrediticia]
    PRIMARY KEY CLUSTERED ([IdReferenciaCrediticia] ASC);
GO

-- Creating primary key on [IdReferencia] in table 'Referencias'
ALTER TABLE [dbo].[Referencias]
ADD CONSTRAINT [PK_Referencias]
    PRIMARY KEY CLUSTERED ([IdReferencia] ASC);
GO

-- Creating primary key on [IdDatosNegocio] in table 'DatosNegocio'
ALTER TABLE [dbo].[DatosNegocio]
ADD CONSTRAINT [PK_DatosNegocio]
    PRIMARY KEY CLUSTERED ([IdDatosNegocio] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdPais] in table 'Ciudad'
ALTER TABLE [dbo].[Ciudad]
ADD CONSTRAINT [FK_Ciudad_Pais]
    FOREIGN KEY ([IdPais])
    REFERENCES [dbo].[Pais]
        ([IdPais])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Ciudad_Pais'
CREATE INDEX [IX_FK_Ciudad_Pais]
ON [dbo].[Ciudad]
    ([IdPais]);
GO

-- Creating foreign key on [IdCiudad] in table 'Sucursal'
ALTER TABLE [dbo].[Sucursal]
ADD CONSTRAINT [FK_Sucursal_Ciudad]
    FOREIGN KEY ([IdCiudad])
    REFERENCES [dbo].[Ciudad]
        ([IdCiudad])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Sucursal_Ciudad'
CREATE INDEX [IX_FK_Sucursal_Ciudad]
ON [dbo].[Sucursal]
    ([IdCiudad]);
GO

-- Creating foreign key on [IdItemMenuPadre] in table 'ItemMenu'
ALTER TABLE [dbo].[ItemMenu]
ADD CONSTRAINT [FK_ItemMenu_ItemMenu]
    FOREIGN KEY ([IdItemMenuPadre])
    REFERENCES [dbo].[ItemMenu]
        ([IdItemMenu])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemMenu_ItemMenu'
CREATE INDEX [IX_FK_ItemMenu_ItemMenu]
ON [dbo].[ItemMenu]
    ([IdItemMenuPadre]);
GO

-- Creating foreign key on [IdItemMenu] in table 'ItemRol'
ALTER TABLE [dbo].[ItemRol]
ADD CONSTRAINT [FK_ItemRol_ItemMenu]
    FOREIGN KEY ([IdItemMenu])
    REFERENCES [dbo].[ItemMenu]
        ([IdItemMenu])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemRol_ItemMenu'
CREATE INDEX [IX_FK_ItemRol_ItemMenu]
ON [dbo].[ItemRol]
    ([IdItemMenu]);
GO

-- Creating foreign key on [IdRol] in table 'ItemRol'
ALTER TABLE [dbo].[ItemRol]
ADD CONSTRAINT [FK_ItemRol_Rol]
    FOREIGN KEY ([IdRol])
    REFERENCES [dbo].[Rol]
        ([IdRol])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemRol_Rol'
CREATE INDEX [IX_FK_ItemRol_Rol]
ON [dbo].[ItemRol]
    ([IdRol]);
GO

-- Creating foreign key on [IdRol] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [FK_Usuario_Rol]
    FOREIGN KEY ([IdRol])
    REFERENCES [dbo].[Rol]
        ([IdRol])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Usuario_Rol'
CREATE INDEX [IX_FK_Usuario_Rol]
ON [dbo].[Usuario]
    ([IdRol]);
GO

-- Creating foreign key on [IdSucursal] in table 'Usuario'
ALTER TABLE [dbo].[Usuario]
ADD CONSTRAINT [FK_Usuario_Sucursal]
    FOREIGN KEY ([IdSucursal])
    REFERENCES [dbo].[Sucursal]
        ([IdSucursal])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Usuario_Sucursal'
CREATE INDEX [IX_FK_Usuario_Sucursal]
ON [dbo].[Usuario]
    ([IdSucursal]);
GO

-- Creating foreign key on [IdFactor] in table 'Categoria'
ALTER TABLE [dbo].[Categoria]
ADD CONSTRAINT [FK_Categoria_Factor]
    FOREIGN KEY ([IdFactor])
    REFERENCES [dbo].[Factor]
        ([IdFactor])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Categoria_Factor'
CREATE INDEX [IX_FK_Categoria_Factor]
ON [dbo].[Categoria]
    ([IdFactor]);
GO

-- Creating foreign key on [IdCategoria] in table 'Clasificacion'
ALTER TABLE [dbo].[Clasificacion]
ADD CONSTRAINT [FK_Clasificacion_Categoria]
    FOREIGN KEY ([IdCategoria])
    REFERENCES [dbo].[Categoria]
        ([IdCategoria])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Clasificacion_Categoria'
CREATE INDEX [IX_FK_Clasificacion_Categoria]
ON [dbo].[Clasificacion]
    ([IdCategoria]);
GO

-- Creating foreign key on [IdCliente] in table 'AprobacionInstitucion'
ALTER TABLE [dbo].[AprobacionInstitucion]
ADD CONSTRAINT [FK_AprobacionInstitucion_Cliente]
    FOREIGN KEY ([IdCliente])
    REFERENCES [dbo].[Cliente]
        ([IdCliente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AprobacionInstitucion_Cliente'
CREATE INDEX [IX_FK_AprobacionInstitucion_Cliente]
ON [dbo].[AprobacionInstitucion]
    ([IdCliente]);
GO

-- Creating foreign key on [IdCliente] in table 'ClienteEvaluacion'
ALTER TABLE [dbo].[ClienteEvaluacion]
ADD CONSTRAINT [FK_ClienteEvaluacion_Cliente]
    FOREIGN KEY ([IdCliente])
    REFERENCES [dbo].[Cliente]
        ([IdCliente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClienteEvaluacion_Cliente'
CREATE INDEX [IX_FK_ClienteEvaluacion_Cliente]
ON [dbo].[ClienteEvaluacion]
    ([IdCliente]);
GO

-- Creating foreign key on [IdCliente] in table 'Domicilio'
ALTER TABLE [dbo].[Domicilio]
ADD CONSTRAINT [FK_Domicilio_Cliente]
    FOREIGN KEY ([IdCliente])
    REFERENCES [dbo].[Cliente]
        ([IdCliente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Domicilio_Cliente'
CREATE INDEX [IX_FK_Domicilio_Cliente]
ON [dbo].[Domicilio]
    ([IdCliente]);
GO

-- Creating foreign key on [IdCliente] in table 'ResumenTransaccion'
ALTER TABLE [dbo].[ResumenTransaccion]
ADD CONSTRAINT [FK_IdCliente]
    FOREIGN KEY ([IdCliente])
    REFERENCES [dbo].[Cliente]
        ([IdCliente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IdCliente'
CREATE INDEX [IX_FK_IdCliente]
ON [dbo].[ResumenTransaccion]
    ([IdCliente]);
GO

-- Creating foreign key on [IdClienteEvaluacion] in table 'EvaluacionCategoria'
ALTER TABLE [dbo].[EvaluacionCategoria]
ADD CONSTRAINT [FK_IdClienteEvaluacion]
    FOREIGN KEY ([IdClienteEvaluacion])
    REFERENCES [dbo].[ClienteEvaluacion]
        ([IdClienteEvaluacion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_IdClienteEvaluacion'
CREATE INDEX [IX_FK_IdClienteEvaluacion]
ON [dbo].[EvaluacionCategoria]
    ([IdClienteEvaluacion]);
GO

-- Creating foreign key on [IdEvaluacionCategoria] in table 'EvaluacionCategoriaClasificacion'
ALTER TABLE [dbo].[EvaluacionCategoriaClasificacion]
ADD CONSTRAINT [FK_EvaluacionCategoriaClasificacion_EvaluacionCategoria]
    FOREIGN KEY ([IdEvaluacionCategoria])
    REFERENCES [dbo].[EvaluacionCategoria]
        ([IdEvaluacionCategoria])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EvaluacionCategoriaClasificacion_EvaluacionCategoria'
CREATE INDEX [IX_FK_EvaluacionCategoriaClasificacion_EvaluacionCategoria]
ON [dbo].[EvaluacionCategoriaClasificacion]
    ([IdEvaluacionCategoria]);
GO

-- Creating foreign key on [IdCliente] in table 'DetalleCliente'
ALTER TABLE [dbo].[DetalleCliente]
ADD CONSTRAINT [FK_DetalleCliente_Cliente]
    FOREIGN KEY ([IdCliente])
    REFERENCES [dbo].[Cliente]
        ([IdCliente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DetalleCliente_Cliente'
CREATE INDEX [IX_FK_DetalleCliente_Cliente]
ON [dbo].[DetalleCliente]
    ([IdCliente]);
GO

-- Creating foreign key on [IdCliente] in table 'Contacto'
ALTER TABLE [dbo].[Contacto]
ADD CONSTRAINT [FK_Contacto_Cliente]
    FOREIGN KEY ([IdCliente])
    REFERENCES [dbo].[Cliente]
        ([IdCliente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Contacto_Cliente'
CREATE INDEX [IX_FK_Contacto_Cliente]
ON [dbo].[Contacto]
    ([IdCliente]);
GO

-- Creating foreign key on [IdCliente] in table 'ReferenciaCrediticia'
ALTER TABLE [dbo].[ReferenciaCrediticia]
ADD CONSTRAINT [FK_ReferenciaCrediticia_Cliente]
    FOREIGN KEY ([IdCliente])
    REFERENCES [dbo].[Cliente]
        ([IdCliente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReferenciaCrediticia_Cliente'
CREATE INDEX [IX_FK_ReferenciaCrediticia_Cliente]
ON [dbo].[ReferenciaCrediticia]
    ([IdCliente]);
GO

-- Creating foreign key on [IdCliente] in table 'Referencias'
ALTER TABLE [dbo].[Referencias]
ADD CONSTRAINT [FK_Referencias_Cliente]
    FOREIGN KEY ([IdCliente])
    REFERENCES [dbo].[Cliente]
        ([IdCliente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Referencias_Cliente'
CREATE INDEX [IX_FK_Referencias_Cliente]
ON [dbo].[Referencias]
    ([IdCliente]);
GO

-- Creating foreign key on [IdCliente] in table 'DatosNegocio'
ALTER TABLE [dbo].[DatosNegocio]
ADD CONSTRAINT [FK_DatosNegocio_Cliente]
    FOREIGN KEY ([IdCliente])
    REFERENCES [dbo].[Cliente]
        ([IdCliente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DatosNegocio_Cliente'
CREATE INDEX [IX_FK_DatosNegocio_Cliente]
ON [dbo].[DatosNegocio]
    ([IdCliente]);
GO

-- Creating foreign key on [IdDatosNegocio] in table 'DocumentosNegocio'
ALTER TABLE [dbo].[DocumentosNegocio]
ADD CONSTRAINT [FK_DocumentosNegocio_DatosNegocio]
    FOREIGN KEY ([IdDatosNegocio])
    REFERENCES [dbo].[DatosNegocio]
        ([IdDatosNegocio])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DocumentosNegocio_DatosNegocio'
CREATE INDEX [IX_FK_DocumentosNegocio_DatosNegocio]
ON [dbo].[DocumentosNegocio]
    ([IdDatosNegocio]);
GO

-- Creating foreign key on [IdDatosNegocio] in table 'NegocioProveedores'
ALTER TABLE [dbo].[NegocioProveedores]
ADD CONSTRAINT [FK_NegocioProveedores_DatosNegocio]
    FOREIGN KEY ([IdDatosNegocio])
    REFERENCES [dbo].[DatosNegocio]
        ([IdDatosNegocio])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_NegocioProveedores_DatosNegocio'
CREATE INDEX [IX_FK_NegocioProveedores_DatosNegocio]
ON [dbo].[NegocioProveedores]
    ([IdDatosNegocio]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------