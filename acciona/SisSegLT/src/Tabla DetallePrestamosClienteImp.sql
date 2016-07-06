USE [ASODENICDB]
GO

/****** Object:  Table [dbo].[DetallePrestamosClienteImp]    Script Date: 14/10/2014 09:24:28 p.m. ******/
DROP TABLE [dbo].[DetallePrestamosClienteImp]
GO

/****** Object:  Table [dbo].[DetallePrestamosClienteImp]    Script Date: 14/10/2014 09:24:28 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DetallePrestamosClienteImp](
	[IdDetalleClienteImp] [uniqueidentifier] NOT NULL CONSTRAINT [DF_DetallePrestamosClienteImp_IdDetalleClienteImp]  DEFAULT (newid()),
	[NoCedula] [nvarchar](50) NOT NULL,
	[CodigoExpediente] [nvarchar](50) NOT NULL,
	[NombreExpediente] [nvarchar](100) NULL,
	[NoPrestamo] [nvarchar](50) NOT NULL,
	[FechaAprobacion] [datetime] NULL,
	[FechaCancelacion] [datetime] NULL,
	[MontoAprobado] [float] NULL,
	[FechaRegistro] [datetime] NULL CONSTRAINT [DF_DetallePrestamosClienteImp_FechaRegistro]  DEFAULT (getdate()),
 CONSTRAINT [PK_DetallePrestamosClienteImp] PRIMARY KEY CLUSTERED 
(
	[IdDetalleClienteImp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


