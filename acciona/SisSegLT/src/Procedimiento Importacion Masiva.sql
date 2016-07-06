USE [ASODENICDB]
GO

/****** Object:  StoredProcedure [dbo].[SP_ImportacionMasiva]    Script Date: 14/10/2014 09:22:29 p.m. ******/
DROP PROCEDURE [dbo].[SP_ImportacionMasiva]
GO

/****** Object:  StoredProcedure [dbo].[SP_ImportacionMasiva]    Script Date: 14/10/2014 09:20:32 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Fernando M. Gallegos
-- Create date: 11/10/2014
-- Description:	Permite la importacion masiva de registros y 
--              posteriormente elimina duplicados
-- =============================================
CREATE PROCEDURE [dbo].[SP_ImportacionMasiva] 
	@Tabla nvarchar(256), --Deberá tener uno de los valores (1 - DatosClienteImp; 2 - DetallePrestamosClienteImp; ? - No hara nada)
	@RutaCSV nvarchar(MAX),
	@RutaXML nvarchar(MAX)
AS
BEGIN
	SET NOCOUNT ON;

    -- Crear la sentencia SQL para ejecutarla posteriormente
	DECLARE @SqlTexto nvarchar(MAX)

	IF (@Tabla = '1')
		SELECT @Tabla = 'DatosClienteImp';
	ELSE IF (@Tabla = '2')
		SELECT @Tabla = 'DetallePrestamosClienteImp';
	
	SELECT @SqlTexto = 'BULK INSERT ';
	SELECT @SqlTexto = @SqlTexto + @Tabla;
	SELECT @SqlTexto = @SqlTexto + ' FROM ''';
	SELECT @SqlTexto = @SqlTexto + @RutaCSV;
	SELECT @SqlTexto = @SqlTexto + ''' WITH (FORMATFILE = ''';
	SELECT @SqlTexto = @SqlTexto + @RutaXML;
	SELECT @SqlTexto = @SqlTexto + ''')';

	EXEC(@SqlTexto)

	-- Eliminar duplicados automaticamente
	EXEC dbo.sp_EliminarDuplicados
END

GO


