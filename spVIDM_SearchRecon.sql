USE [DbWDGatewayIDM]
GO

/****** Object:  StoredProcedure [dbo].[spVIDM_SearchRecon]    Script Date: 06/08/2018 10:40:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Irrevaldy
-- Create date: 7 Juni 2018
-- Description:	Search Recon Tool
-- =============================================
CREATE PROCEDURE [dbo].[spVIDM_SearchRecon] 
	-- Add the parameters for the stored procedure here
	@FTRX_TS varchar(50), 
	@FTID varchar(50),
	@FMID varchar(50), 
	@FSTORECODE varchar(50),
	@FAPPRCODE varchar(50), 
	@FRRN varchar(50),
	@FPREPAIDCARDNUM varchar(50),
	@FCARDNUM varchar(50),
	@FSTATUS varchar(50), 
	@FAMOUNT nvarchar(50)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT FTRX_TS, FTID, FMID, FSTORECODE, FAPPRCODE, FRRN, FPREPAIDCARDNUM, FCARDNUM, FSTATUS, FAMOUNT FROM [DbWDGatewayIDM].[dbo].[TTRANSACTION] with (INDEX(idxTrx_TrxGatewayTS))
where FTRX_TS >= LEFT(@FTRX_TS, 8) + '000000' AND FTRX_TS <=  LEFT(@FTRX_TS, 8) + '235959'
and FTID LIKE '%'+@FTID+'%'
and FMID LIKE '%'+@FMID+'%'
and FSTORECODE LIKE '%'+@FSTORECODE+'%'
AND FAPPRCODE LIKE '%'+@FAPPRCODE+'%'
		AND FRRN LIKE '%'+@FRRN+'%'
		AND FPREPAIDCARDNUM LIKE '%'+@FPREPAIDCARDNUM+'%'
		AND FCARDNUM LIKE '%'+@FCARDNUM+'%'
and FSTATUS LIKE '%'+@FSTATUS+'%'
and FAMOUNT = @FAMOUNT
END

GO

