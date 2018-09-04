USE [DbDwhIDM]
GO

/****** Object:  StoredProcedure [dbo].[spVIDM_SearchRecon]    Script Date: 10/08/2018 16:10:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Irrevaldy
-- Create date: 7 Juni 2018
-- Description:	Search Recon Tool
-- =============================================
ALTER PROCEDURE [dbo].[spVIDM_SearchRecon] 
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

	IF(LEFT(@FTRX_TS, 4) = '2018')
	BEGIN
	-- Insert statements for procedure here
	SELECT FTRX_TS, FTID, FMID, FSTORECODE, FAPPRCODE, FRRN, FPREPAIDCARDNUM, FCARDNUM, FSTATUS, FAMOUNT FROM [DbDwhIDM].[dbo].[TTRANSACTION_V2] with (INDEX(idxTrx_TrxGatewayTS_V2))
where FTRX_TS >= LEFT(@FTRX_TS, 8) + '000000' AND FTRX_TS <=  LEFT(@FTRX_TS, 8) + '235959'
AND
(
	@FTID = ''
	OR
	(
		@FTID <> ''
		AND
		FTID LIKE '%'+@FTID
	)
)
AND
(
	@FMID = ''
	OR
	(
		@FMID <> ''
		AND
		FMID LIKE '%'+@FMID
	)
)
AND
(
	@FSTORECODE = ''
	OR
	(
		@FSTORECODE <> ''
		/*AND
		FSTORECODE LIKE '%'+@FSTORECODE*/
	)
)
AND
(
	@FAPPRCODE = ''
	OR
	(
		@FAPPRCODE <> ''
		AND
		FAPPRCODE LIKE '%'+ @FAPPRCODE
	)
)
AND
(
	@FRRN = ''
	OR
	(
		@FRRN <> ''
		AND
		FRRN LIKE '%'+ @FRRN
	)
)
AND/*
(
	@FPREPAIDCARDNUM = ''
	OR
	(
		@FPREPAIDCARDNUM <> ''
		AND
		FPREPAIDCARDNUM = @FPREPAIDCARDNUM
	)
)
AND
(
	@FCARDNUM = ''
	OR
	(
		@FCARDNUM <> ''
		AND
		FCARDNUM = @FCARDNUM
	)
)
AND*/
(
	@FSTATUS = ''
	OR
	(
		@FSTATUS <> ''
		AND
		FSTATUS = @FSTATUS
	)
)
AND
(
	@FAMOUNT = ''
	OR
	(
		@FAMOUNT <> ''
		AND
		FAMOUNT = @FAMOUNT
	)
)
--and FAMOUNT = @FAMOUNT
	END

	ELSE
	BEGIN
	-- Insert statements for procedure here
	SELECT FTRX_TS, FTID, FMID, FSTORECODE, FAPPRCODE, FRRN, FPREPAIDCARDNUM, FCARDNUM, FSTATUS, FAMOUNT FROM [DbDwhIDM].[dbo].[TTRANSACTION] with (INDEX(idxTrx_TrxGatewayTS))
where FTRX_TS >= LEFT(@FTRX_TS, 8) + '000000' AND FTRX_TS <=  LEFT(@FTRX_TS, 8) + '235959'
AND
(
	@FTID = ''
	OR
	(
		@FTID <> ''
		AND
		FTID LIKE '%'+@FTID
	)
)
AND
(
	@FMID = ''
	OR
	(
		@FMID <> ''
		AND
		FMID LIKE '%'+@FMID
	)
)
AND
(
	@FSTORECODE = ''
	OR
	(
		@FSTORECODE <> ''
		AND
		FSTORECODE LIKE '%'+@FSTORECODE
	)
)
AND
(
	@FAPPRCODE = ''
	OR
	(
		@FAPPRCODE <> ''
		AND
		FAPPRCODE LIKE '%'+ @FAPPRCODE
	)
)
AND
(
	@FRRN = ''
	OR
	(
		@FRRN <> ''
		AND
		FRRN LIKE '%'+ @FRRN
	)
)
AND/*
(
	@FPREPAIDCARDNUM = ''
	OR
	(
		@FPREPAIDCARDNUM <> ''
		AND
		FPREPAIDCARDNUM = @FPREPAIDCARDNUM
	)
)
AND
(
	@FCARDNUM = ''
	OR
	(
		@FCARDNUM <> ''
		AND
		FCARDNUM = @FCARDNUM
	)
)
AND*/
(
	@FSTATUS = ''
	OR
	(
		@FSTATUS <> ''
		AND
		FSTATUS = @FSTATUS
	)
)
AND
(
	@FAMOUNT = ''
	OR
	(
		@FAMOUNT <> ''
		AND
		FAMOUNT = @FAMOUNT
	)
)
--and FAMOUNT = @FAMOUNT
	END
    
END
GO

