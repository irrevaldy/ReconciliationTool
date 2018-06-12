USE [DbWDGatewayIDM]
GO

/****** Object:  StoredProcedure [dbo].[spVIDM_SearchRecon]    Script Date: 08/06/2018 16:20:03 ******/
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
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT FGATEWAY_TS, FTID, FMID, FSTORECODE, FAPPRCODE, FRRN, FPREPAIDCARDNUM, FAMOUNT FROM [DbWDGatewayIDM].[dbo].[TTRANSACTION]
where FGATEWAY_TS = '20170524133836'
and FTID = '80080052'
and FMID = '000080080088887'
and FSTORECODE = 'TRIE'
and FAPPRCODE = '764777'
and FRRN = '714430258751'
and FPREPAIDCARDNUM = '6032984018572614'
and FAMOUNT = '11111'
END

GO

