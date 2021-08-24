USE [master]
GO

ALTER DATABASE [AssociationBids] 
	SET SINGLE_USER WITH ROLLBACK IMMEDIATE

RESTORE DATABASE [AssociationBids] 
	FROM  
		DISK = N'C:\Data\BackUp\AssociationBids.QA_05282021.bak' 
	WITH  
		FILE = 1,  
		MOVE N'AssociationBids' TO N'C:\DATA\AssociationBids.mdf',  
		MOVE N'AssociationBids_log' TO N'C:\DATA\AssociationBids_log.ldf',  
		NOUNLOAD,  REPLACE,  STATS = 5

ALTER DATABASE [AssociationBids] SET MULTI_USER
GO


------------------------------------------------------------------------------
/*

USE [master]
GO

ALTER DATABASE [AssociationBids] 
	SET SINGLE_USER WITH ROLLBACK IMMEDIATE

RESTORE DATABASE [AssociationBids] 
	FROM  
		DISK = N'C:\Data\BackUp\AssociationBids_05272021.bak' 
	WITH  
		FILE = 1,  
		MOVE N'AssociationBids' TO N'C:\DATA\AssociationBids.mdf',  
		MOVE N'AssociationBids_log' TO N'C:\DATA\AssociationBids_log.ldf',  
		NOUNLOAD,  REPLACE,  STATS = 5

ALTER DATABASE [AssociationBids] SET MULTI_USER
GO

*/