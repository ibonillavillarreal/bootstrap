SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Fernando M. Gallegos
-- Create date: 14/10/2014
-- Description:	Eliminacion de registros duplicados 
-- =============================================
CREATE PROCEDURE SP_EliminarDuplicados
AS
BEGIN
	SET NOCOUNT ON;

    -- Elimina los duplicados de la tabla dbo.DatosClienteImp
	delete tablaDatosCliente from (
		select fila = row_number() over(
			partition by NoCedula, FechaActualizacion order by NoCedula
		) from dbo.DatosClienteImp
	) as tablaDatosCliente where fila <> 1

	-- Elimina los duplicados de la tabla dbo.DetallePrestamosClienteImp
	delete tablaDetallePrestamosCliente from (
		select fila = row_number() over(
			partition by NoCedula, CodigoExpediente, NoPrestamo order by NoCedula
		) from dbo.DetallePrestamosClienteImp
	) as tablaDetallePrestamosCliente where fila <> 1
END
GO
