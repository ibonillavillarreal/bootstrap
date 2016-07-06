USE [ASODENICDB]
GO

/****** Object:  Table [dbo].[DatosClienteImp]    Script Date: 14/10/2014 09:24:01 p.m. ******/
DROP TABLE [dbo].[DatosClienteImp]
GO

/****** Object:  Table [dbo].[DatosClienteImp]    Script Date: 14/10/2014 09:24:01 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DatosClienteImp](
	[IdClienteImp] [uniqueidentifier] NOT NULL CONSTRAINT [DF_DatosClienteImp_IdClienteImp]  DEFAULT (newid()),
	[NoCedula] [nvarchar](50) NOT NULL,
	[Nombres] [nvarchar](100) NULL,
	[Apellidos] [nvarchar](100) NULL,
	[FechaInicio] [datetime] NULL,
	[Sexo] [char](1) NULL,
	[FechaNacimiento] [datetime] NULL,
	[EstadoCivil] [char](1) NULL,
	[TelefonoCliente] [nvarchar](50) NULL,
	[Domicilio] [nvarchar](850) NULL,
	[Departamento] [nvarchar](50) NULL,
	[Casa] [char](1) NULL,
	[TelefonoResidencia] [nvarchar](50) NULL,
	[CodigoProfesion] [int] NULL,
	[Profesion] [nvarchar](100) NULL,
	[NoMiembros] [nvarchar](50) NULL,
	[NoDependientes] [nvarchar](50) NULL,
	[CodigoTipoNegocio] [int] NULL,
	[NombreTipoNegocio] [nvarchar](100) NULL,
	[DireccionNegocio] [nvarchar](850) NULL,
	[CasaNegocio] [char](1) NULL,
	[Metodologia] [nchar](50) NULL,
	[NoExpediente] [nchar](15) NULL,
	[CodigoAgencia] [int] NULL,
	[CodigoPromotor] [int] NULL,
	[NombrePromotor] [nvarchar](100) NULL,
	[FechaActualizacion] [datetime] NOT NULL,
 CONSTRAINT [PK_DatosClienteImp] PRIMARY KEY CLUSTERED 
(
	[IdClienteImp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


