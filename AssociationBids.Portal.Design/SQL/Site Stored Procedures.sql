create PROCEDURE [dbo].[Site_Company_Exit]
    @Id INT = 0,
    @Name VARCHAR(50)
AS
BEGIN

    SELECT COUNT(c.CompanyKey)
    FROM Company AS c
    WHERE c.Name = @Name
          AND c.CompanyKey <> @Id
          


END

go 

create PROCEDURE [dbo].[site_Service_GetAll]

AS
SET NOCOUNT ON

select ServiceKey, Title from [Service] order by Title asc



go


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------





   
CREATE PROCEDURE [dbo].[site_Company_Insert]
	 
	@CompanyName    varchar(500),
	@LegalName		varchar(500),
	@TaxID 			varchar(500),
	@Address		varchar(500),
	@Address2		varchar(500),
	@City			varchar(500),
	@StateKey		varchar(500),
	@Zip			varchar(500),
	@Work			varchar(500),
	@Work2			varchar(500),
	@Fax			varchar(500),
	@Website		varchar(500),									
	@ServiceTitle1	varchar(500),
	@RadiusKey		varchar(500),
	@ServiceAddress	varchar(500),		
	@FileSize		varchar(500),
	@FileName		varchar(500),
	@companyvalue INT OUTPUT

AS
SET NOCOUNT ON



declare @vendorkey int
declare @companytype int
declare @status int
declare @modulekey int
declare @insurancekey int


select @companytype = LookUpKey from LookUp where Title = 'Company Vendor' 
select @status = LookUpKey from LookUp where Title = 'Pending'

        
	       select @modulekey = count(ModuleKey) from Module Where Controller = 'Registration' and Action = 'RegistrationInsert'
	       if(@modulekey = 0)
	       begin	    
	          insert into Module (ModuleKey,Controller,[Action],Title)values((select max(ModuleKey) + 1  from Module),'Registration','RegistrationInsert','Registration')		  
           end 


           insert into Company(Name,[State],LegalName,TaxID,[Address],Address2,City,Zip,Work,Work2,Fax,Website,[Status],CompanyTypeKey,CompanyID)
		                      values(@CompanyName,@StateKey,@LegalName,@TaxID,@Address,@Address2,@City,@Zip,@Work,@Work2,@Fax,@Website,@status,@companytype,@CompanyName)

           set @vendorkey = @@identity
		   select  @companyvalue = @@IDENTITY
		


		   INSERT INTO Insurance
                   (VendorKey, CompanyName, Work, Fax, Address, Address2, City, State, Zip, Status,StartDate,EndDate)
           VALUES (@vendorkey,@CompanyName,@Work,@Fax,@Address,@Address2,@City,@StateKey,@Zip,@status,GETDATE(),(SELECT DATEADD(year, 1, GETDATE())))


		   set @insurancekey  = @@IDENTITY

		 	   		    
		   insert into ServiceArea (VendorKey,[Address],Address2,City,[State],Zip,Radius)
	                   values (@vendorkey,@Address,@Address2,@City,@StateKey,@Zip,@RadiusKey)	            
		   


            select @modulekey = ModuleKey from Module Where Controller = 'Registration' and Action = 'RegistrationInsert'
			declare FileNamecur cursor for  
  
               select j.Item as FileName,k.Item as FileSize from 
               (select a.Item, ROW_NUMBER() over(order by (SELECT 1000)) rownum from dbo.SplitString(@FileName,'?') as a ) j
               inner join ( select a.Item, ROW_NUMBER() over(order by (SELECT 1000)) rownum from dbo.SplitString(@FileSize,',') as a)k
                on j.rownum = k.rownum

                 open FileNamecur     
                 declare @File as nvarchar(max), @Size as nvarchar(max)

           fetch next from FileNamecur into @File,@Size
                   while @@FETCH_STATUS = 0  
               begin 

               Insert Into Document(ModuleKey, ObjectKey, [FileName], FileSize,LastModificationTime)
               values(@modulekey, @insurancekey ,@File ,convert(varchar,@Size), GETDATE())

            fetch next from FileNamecur into @File,@Size  
                       end        
            close FileNamecur      
              deallocate FileNamecur
 
 declare  vendorcur cursor for

 select item from  SplitString(@ServiceTitle1,',')

 declare @servicekey int

 open vendorcur 

 fetch  next from vendorcur into @servicekey
    while @@FETCH_STATUS = 0  
    begin 
   insert into VendorService (servicekey,vendorkey)values(@servicekey,@vendorkey) 

 fetch  next from vendorcur into @servicekey
    end
 close vendorcur
 deallocate vendorcur



go


create procedure site_Payment_Insert


@CardNumber	nvarchar(max),
@StripeTokenID	nvarchar(max),
@AddedOn	datetime,
@errorcode  int output


as 
begin
INSERT INTO PaymentModel
                  (CardNumber, StripeTokenID, AddedOn)
VALUES     (@CardNumber,@StripeTokenID,GETDATE())

select @errorcode = @@ERROR

end


GO
/****** Object:  StoredProcedure [dbo].[USP_Document_Delete]    Script Date: 5/21/2020 5:40:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--USP_Resource_Delete 1006,0
create PROCEDURE [dbo].[site_Document_Delete]
	@propertyKey int,
	@ResourceKey int,
	@errorCode int output
	

AS
SET NOCOUNT ON
  delete  from  PropertyResource   where  PropertyKey =  @propertyKey and  ResourceKey = @ResourceKey

select @errorCode  = @@ERROR
-- Get the Error Code for the statement just executed.


--------------------------------------------------------------------------


GO
/****** Object:  StoredProcedure [dbo].[USP_Document_Insert]    Script Date: 5/21/2020 5:41:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  create PROCEDURE [dbo].[site_Document_Insert]             
     @PropertyKey int, 
	@FileName varchar(max), 
	@FileSize varchar(max),
	@errorCode int output

AS            
BEGIN 

 declare @modulekey int  
   select @modulekey = ModuleKey from Module Where Controller = 'PMProperties' and Action = 'PMPropertyAdd'
   
 SET NOCOUNT ON;            
 Declare @LastID As int        
 Declare @strQuery As nvarchar(max) = ''             		
declare FileNamecur cursor for   

   select j.Item as FileName,k.Item as FileSize from 
               (select a.Item, ROW_NUMBER() over(order by (SELECT 1000)) rownum from dbo.SplitString(@FileName,',') as a ) j
               inner join ( select a.Item, ROW_NUMBER() over(order by (SELECT 1000)) rownum from dbo.SplitString(@FileSize,',') as a)k
                on j.rownum = k.rownum
   open FileNamecur     
                 declare @File as nvarchar(max), @Size as nvarchar(max)
fetch next from FileNamecur into @File,@Size
                   while @@FETCH_STATUS = 0  
begin     	 
   Insert Into Document(ModuleKey, ObjectKey, [FileName], FileSize,LastModificationTime)
               values(@modulekey, @PropertyKey ,@File ,@Size, GETDATE())
                      
 End  
fetch next from FileNamecur into @File,@Size
end
close FileNamecur
deallocate FileNamecur                     
 select @errorCode  = @@ERROR
-----------------------------------------------------------------------------------------





GO
/****** Object:  StoredProcedure [dbo].[USP_Document_SelectAll]    Script Date: 5/21/2020 5:43:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[site_Document_SelectAll]

@PropertyKey int

AS
SET NOCOUNT ON
begin
select  FileName  from Document where ObjectKey = @PropertyKey
end



---------------------------------------------------------------------------------------------------


GO
/****** Object:  StoredProcedure [dbo].[USP_GetGroupkey]    Script Date: 5/21/2020 5:44:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--USP_Resource_Delete 1006,0
create  PROCEDURE [dbo].[site_GetGroupkey]

	@ResourceKey nvarchar(max),
	@Groupkey int output
	

 
As
begin

 select @Groupkey = GroupKey  from [Group] Where  Title = @ResourceKey

-- Get the Error Code for the statement just executed.
end

------------------------------------------------------------------------------------------------------------------






GO
/****** Object:  StoredProcedure [dbo].[USP_Manager_SelectAll]    Script Date: 5/21/2020 5:44:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[site_Manager_SelectAll]

@Groupkey int

AS
SET NOCOUNT ON

if @Groupkey = 0
begin 
Select GroupKey,Title From [Group] order by title asc    
end
else
begin
select * from PropertyResource  as pr inner join [Group]  as g on g.GroupKey = pr.ResourceKey where pr.PropertyKey = @Groupkey
end

 




--------------------------------------------------------------------------------------------------------------------------------



GO
/****** Object:  StoredProcedure [dbo].[USP_Manger_Update]    Script Date: 5/21/2020 5:45:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--USP_Resource_Delete 1006,0
create PROCEDURE [dbo].[site_Manger_Update]
	@propertyKey int,
	@ResourceKey nvarchar(max),
	@errorCode int output
	

AS
SET NOCOUNT ON


Declare @qry nvarchar(max)
   declare managercur cursor for 
           select Item from dbo.SplitString(@ResourceKey,',')     

             open managercur
           declare @resourcevalue int

  fetch next from managercur into @resourcevalue
  while @@FETCH_STATUS = 0
  begin  
   Set @qry = 'Insert Into PropertyResource(PropertyKey, ResourceKey, DateAdded, Status)values('''+convert(varchar, @propertyKey)+''','''+ convert(varchar, @resourcevalue) +''', GETDATE(), 1)'                                         

exec(@qry)
print(@qry)

fetch next from managercur into @resourcevalue

end
close managercur
deallocate managercur


select @errorCode  = @@ERROR
-- Get the Error Code for the statement just executed.

-------------------------------------------------------------------------------------------



GO
/****** Object:  StoredProcedure [dbo].[USP_Property_Delete]    Script Date: 5/21/2020 5:46:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--USP_Resource_Delete 1006,0
create PROCEDURE [dbo].[site_Property_Delete]
	@propertyKey int,
	@errorCode int output
	

AS
SET NOCOUNT ON
delete from [PropertyResource] where PropertyKey = @propertyKey
DELETE FROM Property WHERE PropertyKey = @propertyKey



select @errorCode  = @@ERROR
-- Get the Error Code for the statement just executed.

-------------------------------------------------------------------------------------------------------------


GO
/****** Object:  StoredProcedure [dbo].[USP_Property_Insert]    Script Date: 5/21/2020 5:47:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- USP_Property_Insert 15,'shyam',25,'ahmedabad','ahmedabad','ahmedabad','HI','0512151',5,5,'desc',1,' Tasksheet  23-04-2020.docx,Tasksheet  24-04-2020.docx,Tasksheet  30-04-2020.docx','21811,21821,22471',1,20
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

create PROCEDURE [dbo].[site_Property_Insert]
	 
	@CompanyKey int, 
	@Title varchar(150), 
	@NumberOfUnits int, 
	@Address varchar(100),
	@Address2 varchar(100),
	@City varchar(50), 
	@State varchar(2),
	@Zip varchar(11),
	@BidRequestAmount money,
	@MinimumInsuranceAmount money,
	@Description varchar(max),
	@Status int,
	@FileName varchar(max), 
	@FileSize varchar(max), 
	@ResourceKey nvarchar(max),
	@Propertyvalue int output

AS
SET NOCOUNT ON

 Declare @qry nvarchar(max)   
    Insert into Property(CompanyKey,Title, NumberOfUnits, [Address], [Address2], City, [State], Zip,
	BidRequestAmount, MinimumInsuranceAmount, [Description], [Status]) 
	values 
	(15,@Title, @NumberOfUnits, @Address, @Address2, @City, @State, @Zip, 
	@BidRequestAmount, @MinimumInsuranceAmount, @Description, 1)
	select @Propertyvalue = @@IDENTITY
	   declare @PropertyKey int = @@identity   	
       declare @modulekey int
	   select @modulekey = count(ModuleKey) from Module Where Controller = 'PMProperties' and Action = 'PMPropertyAdd'
	   if(@modulekey = 0)
	   begin
	        insert into Module (ModuleKey,Controller,[Action],Title)values((select max(ModuleKey) + 1  from Module),'PMProperties','PMPropertyAdd','Properties')
		  
            end 
	        select @modulekey = ModuleKey from Module Where Controller = 'PMProperties' and Action = 'PMPropertyAdd'
			declare FileNamecur cursor for  
  
               select j.Item as FileName,k.Item as FileSize from 
               (select a.Item, ROW_NUMBER() over(order by (SELECT 1000)) rownum from dbo.SplitString(@FileName,',') as a ) j
               inner join ( select a.Item, ROW_NUMBER() over(order by (SELECT 1000)) rownum from dbo.SplitString(@FileSize,',') as a)k
                on j.rownum = k.rownum

                 open FileNamecur     
                 declare @File as nvarchar(max), @Size as nvarchar(max)

           fetch next from FileNamecur into @File,@Size
                   while @@FETCH_STATUS = 0  
               begin 

               Insert Into Document(ModuleKey, ObjectKey, [FileName], FileSize,LastModificationTime)
               values(@modulekey, @PropertyKey ,@File ,@Size, GETDATE())

            fetch next from FileNamecur into @File,@Size  
                       end        
            close FileNamecur      
              deallocate FileNamecur
		  	       
      declare managercur cursor for 
           select Item from dbo.SplitString(@ResourceKey,',')     

             open managercur
           declare @resourcevalue int

fetch next from managercur into @resourcevalue
while @@FETCH_STATUS = 0
begin  
Set @qry = 'Insert Into PropertyResource(PropertyKey, ResourceKey, DateAdded, Status)values('''+convert(varchar, @PropertyKey)+''','''+ convert(varchar, @resourcevalue) +''', GETDATE(), 1)'                                         

exec(@qry)
print(@qry)

fetch next from managercur into @resourcevalue

end
close managercur
deallocate managercur



---------------------------------------------------------------------------------------------------------------------------------------

GO
/****** Object:  StoredProcedure [dbo].[USP_Property_propertyupdate]    Script Date: 5/21/2020 5:48:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[site_Property_propertyupdate]
	
	@title VARCHAR(150),
	@numberOfUnits VARCHAR(150),
	@address VARCHAR(100),
	@address2 VARCHAR(100),
	@city VARCHAR(50),
	@state VARCHAR(2),
	@zip VARCHAR(11),
	@bidRequestAmount money,
	@minimumInsuranceAmount money,
	@description VARCHAR(MAX),
	@status int,
	@PropertyKey INT

AS
SET NOCOUNT ON

Update [Property] set  Title = @title, NumberOfUnits = @numberOfUnits, Address = @address, Address2 = @address2, City = @city, State = @state, Zip = @zip, BidRequestAmount = @bidRequestAmount,
	MinimumInsuranceAmount = @minimumInsuranceAmount, Description = @description, Status = @status
	where PropertyKey = @PropertyKey




------------------------------------------------------------------------------------------------------------





GO
/****** Object:  StoredProcedure [dbo].[USP_Property_SelectIndexPaging]    Script Date: 5/21/2020 5:51:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- USP_Property_SelectIndexPaging 50,1,'','order by u.Title desc'
create PROCEDURE [dbo].[site_Property_SelectIndexPaging]
@PageSize int,                           
@PageIndex int,                           
@Search nvarchar(max),                              
@Sort nvarchar(max)    
AS
BEGIN
  declare @qrywhere varchar(max)                    
  declare @qrytotal varchar(max)                     
  declare @qry varchar(max)    
    
  set @qrywhere = 'where (u.Title like  ''%'+ @Search +'%'') or (u.Address like ''%'+ @Search +'%'') 
  or (u.NumberOfUnits like ''%'+ @Search +'%'') 
  or ( '''+@Search+''' = '''' )' 
  
  set @qrytotal =   'declare @total int  select  @total  = count(*) from [Property] as u
   '+@qrywhere + ''

  set @qry = 'select * from (  select  u.PropertyKey as PropertyKey, u.Title as Title,u.Address as Address,NumberOfUnits as NumberOfUnits, @total  as TotalRecord,
   row_number() over('+@Sort+') as  rownum  from [Property] as u '+@qrywhere+'
   
  ) i where rownum between  '+Convert(nvarchar(10),( (@pageIndex-1) * @pageSize  ) + 1 )+ ' and '  + Convert(varchar(10),( (@pageIndex-1) * @pageSize  ) + @pageSize) 
      print(@qrytotal + @qry)             
      exec( @qrytotal + @qry)          
END

---------------------------------------------------------------------

GO
/****** Object:  StoredProcedure [dbo].[USP_Property_SelectOneByPropertyKey]    Script Date: 5/21/2020 5:51:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[site_Property_SelectOneByPropertyKey]
	@PropertyKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

select Gp.GroupKey, * 

From Property as ps 
inner  join  PropertyResource pR on pr.PropertyKey = ps.PropertyKey 
inner  Join  [Group] Gp on Gp.GroupKey = pr.ResourceKey
where ps.PropertyKey = @PropertyKey



			
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
print(@errorCode)



 ---------------------------------------------





GO
/****** Object:  StoredProcedure [dbo].[USP_Property_Update]    Script Date: 5/21/2020 5:52:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ********************************
-- * Stored Procedure Code
-- ********************************
create PROCEDURE [dbo].[site_Property_Update]
	@PropertyKey INT,
	@companyKey INT,
	@title VARCHAR(150),
	@numberOfUnits int,
	@address VARCHAR(100),
	@address2 VARCHAR(100),
	@city VARCHAR(50),
	@state VARCHAR(2),
	@zip VARCHAR(11),
	@bidRequestAmount money,
	@minimumInsuranceAmount money,
	@description VARCHAR(MAX),
	@status INT,
	@errorCode  INT OUTPUT
AS
SET NOCOUNT ON

UPDATE [Property]
SET
	[CompanyKey] = @companyKey,
	[Title] = @title,
	[NumberOfUnits] = @numberOfUnits,
	[Address] = @address,
	[Address2] = @address2,
	[City] = @city,
	[State] = @state,
	[Zip] = @zip,
	[BidRequestAmount] = @bidRequestAmount,
	[MinimumInsuranceAmount] = @minimumInsuranceAmount,
	[Description] = @description,

	[Status] = @status
WHERE [Property].[PropertyKey] = @PropertyKey

-- Get the Error Code for the statement just executed.

SELECT @errorCode = @@ERROR
---------------------------------------------------------------------------------


GO
/****** Object:  StoredProcedure [dbo].[USP_Company_SelectAll]    Script Date: 5/21/2020 7:27:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[site_Company_SelectAll]

AS
SET NOCOUNT ON

select CompanyKey, CompanyID, Name from company

-- Get the Error Code for the statement just executed.





USE [AssociationBids]
GO
/****** Object:  Table [dbo].[EmailTemplate]    Script Date: 5/21/2020 7:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmailTemplate](
	[EmailTemplateKey] [int] IDENTITY(1,1) NOT NULL,
	[EmailTitle] [varchar](200) NULL,
	[EmailSubject] [varchar](max) NULL,
	[Body] [varchar](max) NULL,
	[DateAdded] [datetime] NULL,
	[lookUpType] [int] NULL,
 CONSTRAINT [PK_EmailTemplate] PRIMARY KEY CLUSTERED 
(
	[EmailTemplateKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PushNotificationTemplate]    Script Date: 5/21/2020 7:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PushNotificationTemplate](
	[PushNotificaionTemplateKey] [int] IDENTITY(1,1) NOT NULL,
	[PushNotificationTitle] [varchar](100) NULL,
	[PushNotificationType] [varchar](50) NULL,
	[Body] [varchar](500) NULL,
	[DateAdded] [datetime] NULL,
	[NTSubject] [varchar](max) NULL,
 CONSTRAINT [PK_NotificationTemplate] PRIMARY KEY CLUSTERED 
(
	[PushNotificaionTemplateKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[EmailTemplate] ON 

INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (5, N'EmailSend', N'this is the update program', N'this is the commant', CAST(N'2020-05-16 19:13:52.173' AS DateTime), NULL)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (6, N'EmailDroping', N'Can you Send this Email Sending', N'i am email send this templet', CAST(N'2020-05-16 19:15:18.647' AS DateTime), NULL)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (7, N'test', N'test1', N'test2', CAST(N'2020-05-17 22:03:37.333' AS DateTime), NULL)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (8, N'vino', N'v', N'v', CAST(N'2020-05-17 22:04:40.630' AS DateTime), NULL)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (9, N'vionpd', N'vinod', N'vinoyyttet', CAST(N'2020-05-18 16:23:13.307' AS DateTime), NULL)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (10, N'vinod', N'vinod', N'hhhuyyyuyuyutt', CAST(N'2020-05-18 17:12:01.410' AS DateTime), NULL)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (11, N'vinod', N'hhhhjiiuhuhbbb h', N' jhbhugyugyugyu', CAST(N'2020-05-18 17:56:48.480' AS DateTime), NULL)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (12, N'vi o', N'vinooo', N'nyugyugy', CAST(N'2020-05-18 18:15:12.943' AS DateTime), NULL)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (13, N'vinod', N'voiiinkjn', N'uihuhuhuhyu', CAST(N'2020-05-18 18:20:33.823' AS DateTime), 100)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (14, N'vinod', N'voiiinkjn', N'uihuhuhuhyu', CAST(N'2020-05-18 18:21:50.730' AS DateTime), 100)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (15, N'vinod', N'voiiinkjn', N'uihuhuhuhyu', CAST(N'2020-05-18 18:22:42.750' AS DateTime), 102)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (16, N'vinmooo', N'uuuuu', N'hhhh', CAST(N'2020-05-18 18:28:10.740' AS DateTime), 102)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (17, N'vinmooo', N'uuuuu', N'hhhh', CAST(N'2020-05-18 18:28:53.180' AS DateTime), 102)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (18, N'vinnuu', N'vnod', N'vinuu', CAST(N'2020-05-18 18:31:02.957' AS DateTime), 200)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (19, N'vinnuu', N'vnod', N'vinuu', CAST(N'2020-05-18 18:33:04.477' AS DateTime), 200)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (20, N'vinod', N'vinioo', N'vuiii', CAST(N'2020-05-18 18:50:46.733' AS DateTime), 201)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (21, N'jyotsna', N'voiiinkjn', N'hii i am so happy', CAST(N'2020-05-18 18:52:06.330' AS DateTime), 102)
SET IDENTITY_INSERT [dbo].[EmailTemplate] OFF
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD FOREIGN KEY([lookUpType])
REFERENCES [dbo].[LookUp] ([LookUpKey])
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD FOREIGN KEY([lookUpType])
REFERENCES [dbo].[LookUp] ([LookUpKey])
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD FOREIGN KEY([lookUpType])
REFERENCES [dbo].[LookUp] ([LookUpKey])
GO
ALTER TABLE [dbo].[EmailTemplate]  WITH CHECK ADD FOREIGN KEY([lookUpType])
REFERENCES [dbo].[LookUp] ([LookUpKey])
GO
/****** Object:  StoredProcedure [dbo].[site__PushNotificationTemplate_SelectIndexPaging]    Script Date: 5/21/2020 7:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- USP_Resource_SelectIndexPaging 50,1,'fdfdf','order by FirstName desc'
CREATE PROCEDURE [dbo].[site__PushNotificationTemplate_SelectIndexPaging]
@PageSize int,                           
@PageIndex int,                           
@Search nvarchar(max),                              
@Sort nvarchar(max)       
AS
BEGIN
  declare @qrywhere varchar(max)                    
  declare @qrytotal varchar(max)                     
  declare @qry varchar(max)    
    
  set @qrywhere = 'where (PushNotificationTitle like  ''%'+ @Search +'%'') or ( '''+@Search+''' = '''' )' 
  
  set @qrytotal =   'declare @total int  select  @total  = count(*) from [EmailTemplate] '+@qrywhere + '' 

--  set @qry = '  SELECT Em.EmailTitle, lk.Title ,Em.EmailSubject,Em.Body  
--FROM EmailTemplate as Em
--INNER JOIN [LookUp] as lk ON lk.LookUpTypeKey =Em.EmailTemplateKey
   
--  '+@qrywhere+'
-- ) i where rownum between  '
--      print(@qrytotal + @qry)             
--      exec( @qrytotal + @qry)     


set @qry = '   select * from ( SELECT NT.PushNotificaionTemplateKey  ,NT.PushNotificationTitle  ,NT.PushNotificationType ,NT.NTSubject,NT.Body  
FROM PushNotificationTemplate as NT
   '+@qrywhere+'
  ) i where rownum between  '+Convert(nvarchar(10),( (@pageIndex-1) * @pageSize  ) + 1 ) + ' and '  + Convert(varchar(10),( (@pageIndex-1) * @pageSize  ) + @pageSize) 
      print(@qrytotal + @qry)             
      exec( @qrytotal + @qry)  


	      
END


GO
/****** Object:  StoredProcedure [dbo].[site_EmailTemplate_Delete]    Script Date: 5/21/2020 7:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Create PROCEDURE [dbo].[site_EmailTemplate_Delete]
	 
	@EmailTemplateKey int,
	@errorCode int output
AS
SET NOCOUNT ON
begin
	delete EmailTemplate  where EmailTemplateKey  = @EmailTemplateKey
	
	
	--delete GroupMember where ResourceKey = @ResourceKey

	select @errorCode  = @@ERROR

	End
GO
/****** Object:  StoredProcedure [dbo].[site_EmailTemplate_Edit]    Script Date: 5/21/2020 7:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[site_EmailTemplate_Edit]
	@EmailTemplateKey int,
	@EmailTitle varchar(200), 
	@EmailSubject varchar(max), 
	@Body varchar(max),
	@LookUpKey int

AS
SET NOCOUNT ON
begin
Update EmailTemplate set EmailTitle = @EmailTitle,lookUpType=@LookUpKey, EmailSubject = @EmailSubject, Body = @Body, DateAdded = GETDATE ()
	where EmailTemplateKey = @EmailTemplateKey

	end
GO
/****** Object:  StoredProcedure [dbo].[site_EmailTemplate_Insert]    Script Date: 5/21/2020 7:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- USP_Property_Insert 15,'shyam',25,'ahmedabad','ahmedabad','ahmedabad','HI','0512151',5,5,'desc',1,' Tasksheet  23-04-2020.docx,Tasksheet  24-04-2020.docx,Tasksheet  30-04-2020.docx','21811,21821,22471',1,20
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[site_EmailTemplate_Insert]
	 
	
	@EmailTitle varchar(200), 
	@EmailSubject varchar(max), 
	@Body varchar(max),
	@LookupType int,
	@Propertyvalue int output
	
AS
begin  
    Insert into EmailTemplate(EmailTitle,LookupType, EmailSubject, Body,DateAdded)values(@EmailTitle,@LookupType, @EmailSubject, @Body, GETDATE ())
	declare @EmailTemplateKey int = @@identity  
	select @Propertyvalue = @@IDENTITY
end


--[USP_EmailTemplet_Insert] 'test','test1','test2'




GO
/****** Object:  StoredProcedure [dbo].[site_EmailTemplate_SelectAll]    Script Date: 5/21/2020 7:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[site_EmailTemplate_SelectAll]
--@errorCode INT OUTPUT
AS
begin
SET NOCOUNT ON
SELECT Em.EmailTemplateKey ,EmailTitle, lk.Title ,Em.EmailSubject,Em.Body  
FROM EmailTemplate as Em inner join [LookUp] as LK on LK.LookUpKey =Em.lookUpType
end


-- Get the Error Code for the statement just executed.
	--SELECT @errorCode = @@ERROR

GO
/****** Object:  StoredProcedure [dbo].[site_EmailTemplate_SelectIndexPaging]    Script Date: 5/21/2020 7:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- USP_Resource_SelectIndexPaging 50,1,'fdfdf','order by FirstName desc'
CREATE PROCEDURE [dbo].[site_EmailTemplate_SelectIndexPaging]
@PageSize int,                           
@PageIndex int,                           
@Search nvarchar(max),                              
@Sort nvarchar(max)       
AS
BEGIN
  declare @qrywhere varchar(max)                    
  declare @qrytotal varchar(max)                     
  declare @qry varchar(max)    
    
  set @qrywhere = 'where (EmailTitle like  ''%'+ @Search +'%'') or ( '''+@Search+''' = '''' )' 
  
  set @qrytotal =   'declare @total int  select  @total  = count(*) from [EmailTemplate] '+@qrywhere + '' 

--  set @qry = '  SELECT Em.EmailTitle, lk.Title ,Em.EmailSubject,Em.Body  
--FROM EmailTemplate as Em
--INNER JOIN [LookUp] as lk ON lk.LookUpTypeKey =Em.EmailTemplateKey
   
--  '+@qrywhere+'
-- ) i where rownum between  '
--      print(@qrytotal + @qry)             
--      exec( @qrytotal + @qry)     


set @qry = '   select * from (  select Em.EmailTemplateKey,Em.EmailTitle, lk.Title,Em.EmailSubject,Em.Body, @total  as TotalRecord,
   row_number() over('+@Sort+') as  rownum from [EmailTemplate] as Em 
   INNER JOIN [LookUp] as lk ON lk.LookUpKey =Em.lookUpType
   '+@qrywhere+'
  ) i where rownum between  '+Convert(nvarchar(10),( (@pageIndex-1) * @pageSize  ) + 1 ) + ' and '  + Convert(varchar(10),( (@pageIndex-1) * @pageSize  ) + @pageSize) 
      print(@qrytotal + @qry)             
      exec( @qrytotal + @qry)  


	      
END


GO
/****** Object:  StoredProcedure [dbo].[site_EmailTemplate_SelectOneByEmailTemplateKey]    Script Date: 5/21/2020 7:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[site_EmailTemplate_SelectOneByEmailTemplateKey]
	@EmailTemplateKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
select E.EmailTemplateKey,E.EmailTitle,L.Title  ,E.EmailSubject ,E.Body  from EmailTemplate as E right join LookUp as L on l.LookUpKey =e.lookUpType    where EmailTemplateKey= @EmailTemplateKey  


			
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
print(@errorCode)



 





GO
/****** Object:  StoredProcedure [dbo].[site_EmailTemplate_update]    Script Date: 5/21/2020 7:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[site_EmailTemplate_update]
	
    @EmailTitle varchar(150), 
	@EmailSubject int, 
	@Body varchar(100),
	@DateAdded datetime,
	@EmailTemplateKey INT,
	@lookUpType int
AS
SET NOCOUNT ON
begin
Update EmailTemplate set  EmailTitle = @EmailTitle, lookUpType=@lookUpType,EmailSubject = @EmailSubject, Body = @Body, DateAdded = @DateAdded
	where EmailTemplateKey = @EmailTemplateKey

	End
GO
/****** Object:  StoredProcedure [dbo].[site_Notification_SelectAll]    Script Date: 5/21/2020 7:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[site_Notification_SelectAll]
--@errorCode INT OUTPUT
AS
begin
SET NOCOUNT ON
SELECT NT.PushNotificaionTemplateKey  ,NT.PushNotificationTitle  ,NT.PushNotificationType ,NT.Body  
FROM PushNotificationTemplate as NT
end


-- Get the Error Code for the statement just executed.
	--SELECT @errorCode = @@ERROR

GO
/****** Object:  StoredProcedure [dbo].[site_Notification_SelectOneByNotificationKey]    Script Date: 5/21/2020 7:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[site_Notification_SelectOneByNotificationKey]
	@EmailTemplateKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON
 SELECT PushNotificaionTemplateKey  ,PushNotificationTitle  ,PushNotificationType ,NTSubject,Body  
FROM PushNotificationTemplate 
			
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
print(@errorCode)



 





GO
/****** Object:  StoredProcedure [dbo].[site_notificationTemplate_Edit]    Script Date: 5/21/2020 7:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[site_notificationTemplate_Edit]
	@PushNotificaionTemplateKey int,
	@PushNotificationTitle varchar(200), 
	@PushNotificationType varchar(max), 
	@Body varchar(max),
	@NTSubject varchar(max),
	@DateAdded datetime
AS
SET NOCOUNT ON
begin
Update PushNotificationTemplate set PushNotificationTitle = @PushNotificationTitle,NTSubject=@NTSubject,PushNotificationType=@PushNotificationType,  Body = @Body, DateAdded = GETDATE ()
	where PushNotificaionTemplateKey = @PushNotificaionTemplateKey

	end
GO
/****** Object:  StoredProcedure [dbo].[site_PushNotificationTemplate_Delete]    Script Date: 5/21/2020 7:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Create PROCEDURE [dbo].[site_PushNotificationTemplate_Delete]
	 
	@PushNotificaionTemplateKey int,
	@errorCode int output
AS
SET NOCOUNT ON
begin
	delete PushNotificationTemplate  where PushNotificaionTemplateKey  = @PushNotificaionTemplateKey
	
	
	--delete GroupMember where ResourceKey = @ResourceKey

	select @errorCode  = @@ERROR

	End
GO
/****** Object:  StoredProcedure [dbo].[site_PushNotificationTemplate_Insert]    Script Date: 5/21/2020 7:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- USP_Property_Insert 15,'shyam',25,'ahmedabad','ahmedabad','ahmedabad','HI','0512151',5,5,'desc',1,' Tasksheet  23-04-2020.docx,Tasksheet  24-04-2020.docx,Tasksheet  30-04-2020.docx','21811,21821,22471',1,20
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[site_PushNotificationTemplate_Insert]
	 
	
	@PushNotificaionTemplateKey int,
	@PushNotificationTitle varchar(200), 
	@PushNotificationType varchar(max),
	@NTSubject varchar(max), 
	@Body varchar(max),
	@DateAdded datetime,
	@Notificationvalue int output
	
AS
begin  
    Insert into PushNotificationTemplate(PushNotificationTitle,PushNotificationType,NTSubject,Body,DateAdded)values(@PushNotificationTitle,@PushNotificationType,@NTSubject,@Body,GETDATE ())
	 
	select @Notificationvalue = @@IDENTITY
end


--[USP_EmailTemplet_Insert] 'test','test1','test2'




GO
/****** Object:  StoredProcedure [dbo].[site_PushNotificationTemplate_update]    Script Date: 5/21/2020 7:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[site_PushNotificationTemplate_update]
	
    @PushNotificaionTemplateKey int,
	@PushNotificationTitle varchar(200), 
	@PushNotificationType varchar(max), 
	@NTSubject varchar(max),
	@Body varchar(max),
	@DateAdded datetime
AS
SET NOCOUNT ON
begin
Update PushNotificationTemplate set  PushNotificationTitle =@PushNotificationTitle,NTSubject=@NTSubject, PushNotificationType=@PushNotificationType, Body = @Body, DateAdded = @DateAdded
	where PushNotificaionTemplateKey =@PushNotificaionTemplateKey

	End
GO


GO
/****** Object:  StoredProcedure [dbo].[site_Resource_SelectIndexPaging]    Script Date: 5/20/2020 2:03:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[site_Resource_SelectIndexPaging]
@PageSize int,                           
@PageIndex int,                           
@Search nvarchar(max),                              
@Sort nvarchar(max)    
AS
BEGIN
  declare @qrywhere varchar(max)                    
  declare @qrytotal varchar(max)                     
  declare @qry varchar(max)    
    
  set @qrywhere = 'where (FirstName like  ''%'+ @Search +'%'') or (LastName like ''%'+ @Search +'%'')  or (CellPhone like ''%'+ @Search +'%'')  or (Email like ''%'+ @Search +'%'') 
   or ( '''+@Search+''' = '''' ) or  FirstName + '' '' + LastName like ''%'+@Search+'%'' or LastName + '' '' + FirstName like ''%'+@Search+'%'' '
  
  set @qrytotal =   'declare @total int  select  @total  = count(*) from [Resource] '+@qrywhere + '' 

  set @qry = '   select * from (  select rs.FirstName as FirstName,rs.ResourceKey as ResourceKey,rs.LastName as LastName,rs.CellPhone as Phone,rs.Email as Email,
   rs.Status as Status, @total  as TotalRecord,
   row_number() over('+@Sort+') as  rownum from [Resource] as rs 
   
   '+@qrywhere+'
  ) i where rownum between  '+Convert(nvarchar(10),( (@pageIndex-1) * @pageSize  ) + 1 ) + ' and '  + Convert(varchar(10),( (@pageIndex-1) * @pageSize  ) + @pageSize) 
      print(@qrytotal + @qry)             
      exec( @qrytotal + @qry)          
END


-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



GO
/****** Object:  StoredProcedure [dbo].[site_Resource_StaffDirectionInsert]    Script Date: 5/20/2020 2:09:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[site_Resource_StaffDirectionInsert]
	 
	@CompanyKey int, 
	@FirstName varchar(50), 
	@LastName varchar(50), 
	@Email varchar(150), 
	@Email2 varchar(150),
	@CellPhone varchar(50),
	@Work varchar(50),
	@Work2 varchar(50),
	@Fax varchar(50),
	@Address varchar(100),
	@Address2 varchar(100),
	@City varchar(50), 
	@State varchar(50),
	@Status int,
	@Zip varchar(11),
	@PrimaryContact bit,
	@Description varchar(50),
	@Username varchar(150),
	@Password varchar(150),
	@GroupId varchar(150), 
	@FileName varchar(150),
	@FileSize int,
	@ResourceValue INT OUTPUT

AS
SET NOCOUNT ON

declare @ResourceTypeKeyvalue int 
select @ResourceTypeKeyvalue =  CompanyTypeKey from company where CompanyKey = @CompanyKey


Insert into Resource (CompanyKey, ResourceTypeKey, FirstName, LastName, Email, Email2, CellPhone,
	Work, Work2, Fax, Address, Address2, City, State, Zip, PrimaryContact, Description, DateAdded, 
	LastModificationTime, Status) 
	values 
	(@CompanyKey, @ResourceTypeKeyvalue, @FirstName, @LastName, @Email, @Email2, @CellPhone, 
	@Work, @Work2, @Fax, @Address, @Address2, @City, @State, @Zip, @PrimaryContact, @Description, getdate(), 
	CONVERT(VARCHAR(8),GETDATE(),108), @Status)
	
	   declare @ResourceKey int = @@identity 	
       Insert into [user] (ResourceKey, Username, Password, 
	   DateAdded, LastModificationTime,FirstTimeAccess, Status) values (@ResourceKey, @Username, @Password, GetDate(),GETDATE(),1, @Status)




	if(@FileName != '')
	begin
	   declare @modulekey int

	   select @modulekey  = count(ModuleKey) from Module Where Controller = 'StaffDirectory' and Action = 'StaffDirectoryAdd'

	   if(@modulekey > 0)
	   begin 
			select @modulekey  = ModuleKey from Module Where Controller = 'StaffDirectory' and Action = 'StaffDirectoryAdd'
			insert into document (ModuleKey, ObjectKey, FileName, FileSize, LastModificationTime) values (@modulekey, @ResourceKey, @FileName, @FileSize, getdate())
	   end
	   else 
	   begin	   
	        select @modulekey  = max(ModuleKey) from Module
	        insert into Module (ModuleKey,title,Controller,[Action])
            values (@modulekey + 1,'StaffDirectoryAdd','StaffDirectory','StaffDirectoryAdd')

			insert into document (ModuleKey, ObjectKey, FileName, FileSize, LastModificationTime) values (@modulekey, @ResourceKey, @FileName, @FileSize, getdate())
	      
	   end
	end
          declare Managercur cursor for    
         select Value from dbo.Split_Int(@GroupId,',')     
         open Managercur    
         declare @groupidal as int 

         fetch next from Managercur into @groupidal    
         while @@FETCH_STATUS = 0    
         begin  

                 
        declare @PropertyTypeKeyvalue int 
        select @PropertyTypeKeyvalue  =  CompanyTypeKey from company where CompanyKey = @CompanyKey


Insert into GroupMember(GroupKey,ResourceKey) 
	values 
	(@groupidal, @ResourceKey)
	
	 fetch next from Managercur into @groupidal    
end    
close Managercur    
deallocate Managercur   


select @ResourceValue = @@IDENTITY



--------------------------------------------------------------------------------------------------------------------------------------------------------------------------



GO
/****** Object:  StoredProcedure [dbo].[site_Service_GetAll]    Script Date: 5/20/2020 2:51:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[site_Service_GetAll]

AS
SET NOCOUNT ON

select ServiceKey, Title from [Service] order by Title asc



--------------------------------------------------------------------------------------------------------------------------------------------------------------------------


GO
/****** Object:  StoredProcedure [dbo].[site_Resource_StaffSelectOneByResourceKey]    Script Date: 5/20/2020 2:53:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[site_Resource_StaffSelectOneByResourceKey]
	@resourceKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

if not exists(Select * from GroupMember where ResourceKey = @resourceKey)
			Begin
				select rs.resourcekey,FirstName, LastName, Email, Email2, CellPhone, Work, Work2, Fax, Address, Address2, City, State,Zip, PrimaryContact, Description, ur.UserName,
				ur.Password, 0 as GroupKey, isnull(DM.FileName, '') as FileName
				From [Resource] as rs 
				inner join [user] as ur on rs.ResourceKey = ur.ResourceKey 
				left join [Document] as DM on DM.ObjectKey = rs.ResourceKey where rs.ResourceKey = @resourceKey
			End
		else
		Begin
		select rs.resourcekey,FirstName, LastName, Email, Email2, CellPhone, Work, Work2, Fax, Address, Address2, City, State,Zip, PrimaryContact, Description, ur.UserName,
		ur.Password, Gp.GroupKey, isnull(DM.FileName, '') as FileName
			 From [Resource] as rs 
			inner join [user] as ur on rs.ResourceKey = ur.ResourceKey
			inner join [GroupMember] as Gp on Gp.ResourceKey = rs.ResourceKey
			left join [Document] as DM on DM.ObjectKey = rs.ResourceKey where rs.ResourceKey = @resourceKey 
		
		End
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



GO
/****** Object:  StoredProcedure [dbo].[site_Resource_StaffEdit]    Script Date: 5/20/2020 2:56:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[site_Resource_StaffEdit]
	 
	@FirstName varchar(50), 
	@LastName varchar(50), 
	@Email varchar(150), 
	@Email2 varchar(150),
	@CellPhone varchar(50),
	@Work varchar(50),
	@Work2 varchar(50),
	@Fax varchar(50),
	@Address varchar(100),
	@Address2 varchar(100),
	@City varchar(50), 
	@State varchar(50),
	@Status int,
	@Zip varchar(11),
	@PrimaryContact bit,
	@Description varchar(50),
	@ResourceKey INT

AS
SET NOCOUNT ON

Update [Resource] set FirstName = @FirstName, LastName = @LastName, Email = @Email, Email2 = @Email2, CellPhone = @CellPhone, Work = @Work, Work2 = @Work2, Fax = @Fax,
	Address = @Address, Address2 = @Address2, City = @City, State = @State, Status = @Status, Zip = @Zip, PrimaryContact = @PrimaryContact, Description = @Description 
	where ResourceKey = @ResourceKey


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



GO
/****** Object:  StoredProcedure [dbo].[site_Resource_StaffGroupEdit]    Script Date: 5/20/2020 2:58:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[site_Resource_StaffGroupEdit]
	 
	@GroupId varchar(150), 
	@ResourceKey INT 
AS
SET NOCOUNT ON

DELETE FROM GroupMember WHERE ResourceKey = @ResourceKey

declare Managercur cursor for    
 select Value from dbo.Split_Int(@GroupId,',')     
open Managercur    
declare @groupidal as int 

fetch next from Managercur into @groupidal    
while @@FETCH_STATUS = 0    
begin  

Insert into GroupMember(GroupKey,ResourceKey) 
	values 
	(@groupidal, @ResourceKey)
	
	 fetch next from Managercur into @groupidal    
end    
close Managercur    
deallocate Managercur   


------------------------------------------------------------------------------------------------------------------------------------------------------------------------



GO
/****** Object:  StoredProcedure [dbo].[site_Resource_StaffGroupUser]    Script Date: 5/20/2020 3:02:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Create PROCEDURE [dbo].[site_Resource_StaffGroupUser]
	 
	@FileName varchar(150),
	@FileSize int,
	@UserName varchar(150), 
	@ResourceKey INT 
AS
SET NOCOUNT ON


update [user] set UserName = @UserName where ResourceKey = @ResourceKey 
if(@FileName != '')
	begin
	   declare @modulekey int

	   select @modulekey  = count(ModuleKey) from Module Where Controller = 'StaffDirectory' and Action = 'StaffDirectoryAdd'

	   if(@modulekey > 0)
	   begin 
			select @modulekey  = ModuleKey from Module Where Controller = 'StaffDirectory' and Action = 'StaffDirectoryAdd'
			
			if exists (Select * from Document where ObjectKey = @ResourceKey)
			begin
				update Document set FileName = @FileName, FileSize = @FileSize where ObjectKey = @ResourceKey
			end
			else
			begin
				insert into document (ModuleKey,ObjectKey,FileName, FileSize, LastModificationTime)  values (@modulekey,@ResourceKey, @FileName, @FileSize, getdate())
			end
	   end
	   else 
	   begin	   
	        select @modulekey  = max(ModuleKey) from Module
	        insert into Module (ModuleKey,title,Controller,[Action])
            values (@modulekey + 1,'StaffDirectoryAdd','StaffDirectory','StaffDirectoryAdd')

			insert into document (ModuleKey, ObjectKey, FileName, FileSize, LastModificationTime) values (@modulekey, @ResourceKey, @FileName, @FileSize, getdate())
	      
	   end
	end
update [User] set UserName = @UserName where ResourceKey = @ResourceKey


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



GO
/****** Object:  StoredProcedure [dbo].[site_User_StaffResetPassword]    Script Date: 5/20/2020 3:05:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[site_User_StaffResetPassword]
	@resourceKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

update [User] set TokenReset = NEWID(), ResetExpirationDate = getdate() where ResourceKey = @resourceKey

select us.Username, rs.Email from [resource] as rs 
inner join [user] as us on rs.ResourceKey = us.ResourceKey where rs.ResourceKey = @resourceKey
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


GO
/****** Object:  StoredProcedure [dbo].[site_Resource_CheckDuplicatedEmail]    Script Date: 5/20/2020 3:07:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[site_Resource_CheckDuplicatedEmail]
	 
	@Email varchar(150), 
	@Status int OUTPUT

AS
SET NOCOUNT ON

	if not exists (Select * from [Resource] where Email = @Email)
		begin
		SET @Status = 1
		end
	else
		begin
		SET @Status = 0
		end


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



GO
/****** Object:  StoredProcedure [dbo].[site_Resource_StaffDelete]    Script Date: 5/20/2020 3:09:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[site_Resource_StaffDelete]
	 
	@ResourceKey int,
	@errorCode int output
AS
SET NOCOUNT ON
	delete [User] where ResourceKey = @ResourceKey
	delete [Resource] where ResourceKey = @ResourceKey
	

	select @errorCode  = @@ERROR










-----------------------------------------------------------------------------------------------------------------------------------

GO
/****** Object:  StoredProcedure [dbo].[site_Property_GetAll]    Script Date: 5/20/2020 1:03:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[site_Property_GetAll]

AS
SET NOCOUNT ON

select PropertyKey, Title from [Property] order by Title asc






-----------------------------------------------------------------------------------------------------------------------------------

GO
/****** Object:  StoredProcedure [dbo].[site_Vendor_SelectIndexPaging_Vendor_SelectIndexPaging]    Script Date: 5/20/2020 1:00:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- USP_Vendor_SelectIndexPaging 50,1,'','order by s.Title desc'
create PROCEDURE [dbo].[site_Vendor_SelectIndexPaging]
@PageSize int,                           
@PageIndex int,                           
@Search nvarchar(max),                              
@Sort nvarchar(max)    
AS
BEGIN
  declare @qrywhere varchar(max)                    
  declare @qrytotal varchar(max)                     
  declare @qry varchar(max)    
    
  set @qrywhere = 'where (u.Name like  ''%'+ @Search +'%'') or (u.Work like ''%'+ @Search +'%'') 
 or (s.Title like ''%'+ @Search +'%'') 
  or ( '''+@Search+''' = '''' )' 
  
  set @qrytotal =   'declare @total int  select  @total  = count(*) from [Company] as u
   inner join [VendorService] as vs on vs.VendorKey  = u.CompanyKey 
    inner join [Service] as s on s.ServiceKey = vs.ServiceKey

   '+@qrywhere + ''

set @qry = '   select * from (  select u.Name, u.CompanyKey , s.ServiceKey, u.Work, s.Title as Title,
   row_number() over('+@Sort+') as  rownum, @total  as TotalRecord from [Company] u
    inner join [VendorService] as vs on vs.VendorKey  = u.CompanyKey 
    inner join [Service] as s on s.ServiceKey = vs.ServiceKey
   '+@qrywhere+'
   
  ) i where rownum between  '+Convert(nvarchar(10),( (@pageIndex-1) * @pageSize  ) + 1 )+ ' and '  + Convert(varchar(10),( (@pageIndex-1) * @pageSize  ) + @pageSize) 
      print(@qrytotal + @qry)             
      exec( @qrytotal + @qry)          
END



----------------------------------------------------------------------------------------------------------------------------------------

GO
/****** Object:  StoredProcedure [dbo].[site_Vendor_Insert]    Script Date: 5/20/2020 12:50:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


create PROCEDURE [dbo].[site_Vendor_Insert]
	 
	@CompanyName    varchar(500),
	@Address		varchar(500),
	@Address2		varchar(500),
	@City			varchar(500),
	@StateKey		varchar(500),
	@Zip			varchar(500),
	@Work			varchar(500),
	@Work2			varchar(500),
	@Email          varchar(500),
	@Description    varchar(500),
	@CellPhone      varchar(500),
	@Fax			varchar(500),
	@Website		varchar(500),									
	@ServiceTitle1	varchar(500),
	@Title      	varchar(500),
	@InsuranceAmount MONEY,
	@PolicyNumber   varchar(150),
	@StartDate       DateTime,
	@EndDate         DateTime,
	@RenewalDate      DateTime,
	@companyvalue INT OUTPUT

AS
SET NOCOUNT ON

declare @vendorkey int
declare @companytype int
declare @status int
declare @insurancekey int


select @companytype = LookUpKey from LookUp where Title = 'Company Vendor' 
select @status = LookUpKey from LookUp where Title = 'Pending'

  insert into Company(Name,[State],[Address],Address2,City,Zip,Work,Work2,Fax,Website,[Status],[Description],CompanyTypeKey,CompanyID)
		                      values(@CompanyName,@StateKey,@Address,@Address2,@City,@Zip,@Work,@Work2,@Fax,@Website,@status,@Description,@companytype,@CompanyName)

           set @vendorkey = @@identity
		   select  @companyvalue = @@IDENTITY


		   INSERT INTO Insurance
                   (VendorKey, CompanyName,PolicyNumber,InsuranceAmount, Work, Fax, Address, Address2,CellPhone, City,Email,State, Zip, Status,StartDate,EndDate,RenewalDate)
           VALUES (@vendorkey,@CompanyName,@PolicyNumber,@InsuranceAmount,@Work,@Fax,@Address,@Address2,@CellPhone,@City,@Email,@StateKey,@Zip,@status,GETDATE(),GETDATE(),(SELECT DATEADD(year, 1, GETDATE())))


		   set @insurancekey  = @@IDENTITY
 declare  vendorcur cursor for

 select item from  SplitString(@ServiceTitle1,',')

 declare @servicekey int

 open vendorcur 

 fetch  next from vendorcur into @servicekey
    while @@FETCH_STATUS = 0  
    begin 
    insert into VendorService (servicekey,vendorkey)values(@servicekey,@vendorkey) 

 fetch  next from vendorcur into @servicekey
    end
 close vendorcur
 deallocate vendorcur


  declare  propertycur cursor for

 select item from  SplitString(@Title,',')

 declare @propertykey int

 open propertycur 

 fetch  next from propertycur into @propertykey
    while @@FETCH_STATUS = 0  
    begin 
    insert into Property(PropertyKey,CompanyKey)values(@propertykey,@companyvalue) 

 fetch  next from propertycur into @propertykey
    end
 close propertycur
 deallocate propertycur



------------------------------------------------------------------------------------------------------------------------------------

GO
/****** Object:  StoredProcedure [dbo].[site_vendor_SelectOneByvendorKey]    Script Date: 5/20/2020 12:54:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[site_vendor_SelectOneByvendorKey]
	@CompanyKey INT,
	@errorCode INT OUTPUT
AS
SET NOCOUNT ON

select Gp.InsuranceKey, Gp.CellPhone, * 

From Company as ps 
inner  join  [Resource] pR on pr.CompanyKey = ps.CompanyKey 
inner  Join  [Insurance] Gp on Gp.InsuranceKey = pr.CompanyKey
where ps.CompanyKey = @CompanyKey



			
-- Get the Error Code for the statement just executed.
SELECT @errorCode = @@ERROR
print(@errorCode)

go

 








 





 
           

		   


						   











