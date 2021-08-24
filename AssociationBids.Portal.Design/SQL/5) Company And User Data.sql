use [AssociationBids]
go

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Company] ON 

GO
INSERT [dbo].[Company] ([CompanyKey], [ParentCompanyKey], [RelatedCompanyKey], [CompanyTypeKey], [PortalKey], [CompanyID], [Name], [LegalName], [TaxID], [Work], [Work2], [Fax], [Address], [Address2], [City], [State], [Zip], [Website], [Description], [BidRequestResponseDays], [BidSubmitDays], [BidRequestAmount], [NotificationPreference], [DateAdded], [LastModificationTime], [Status]) VALUES (15, 15, 15, 1000, 1, N'100', N'Administor Company', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2019-01-01 00:00:00.000' AS DateTime), CAST(N'2019-01-01 00:00:00.000' AS DateTime), 1)
GO
INSERT [dbo].[Company] ([CompanyKey], [ParentCompanyKey], [RelatedCompanyKey], [CompanyTypeKey], [PortalKey], [CompanyID], [Name], [LegalName], [TaxID], [Work], [Work2], [Fax], [Address], [Address2], [City], [State], [Zip], [Website], [Description], [BidRequestResponseDays], [BidSubmitDays], [BidRequestAmount], [NotificationPreference], [DateAdded], [LastModificationTime], [Status]) VALUES (3216, NULL, NULL, 1001, NULL, N'Property Company', N'Property Company', NULL, NULL, N'657567868', N'456757', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-06-07 15:03:58.107' AS DateTime), CAST(N'2020-06-07 15:03:58.107' AS DateTime), 1)
GO
INSERT [dbo].[Company] ([CompanyKey], [ParentCompanyKey], [RelatedCompanyKey], [CompanyTypeKey], [PortalKey], [CompanyID], [Name], [LegalName], [TaxID], [Work], [Work2], [Fax], [Address], [Address2], [City], [State], [Zip], [Website], [Description], [BidRequestResponseDays], [BidSubmitDays], [BidRequestAmount], [NotificationPreference], [DateAdded], [LastModificationTime], [Status]) VALUES (3217, NULL, NULL, 1003, NULL, N'Vendor Company', N'Vendor Company', N'Vendor Company', N'155151', N'Any', NULL, NULL, N'ghodasar', NULL, N'ahmedabad', N'AR', N'3852225', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-06-07 15:13:02.823' AS DateTime), CAST(N'2020-06-07 15:13:02.823' AS DateTime), 501)
GO
SET IDENTITY_INSERT [dbo].[Company] OFF
GO
SET IDENTITY_INSERT [dbo].[Resource] ON 

GO
INSERT [dbo].[Resource] ([ResourceKey], [CompanyKey], [ResourceTypeKey], [FirstName], [LastName], [Title], [Email], [Email2], [CellPhone], [HomePhone], [HomePhone2], [Work], [Work2], [Fax], [Address], [Address2], [City], [State], [Zip], [PrimaryContact], [Description], [DateAdded], [LastModificationTime], [Status], [RegistrationToken]) VALUES (1062, 15, 1000, N'Admin', N'Administrator', NULL, N'Administrator@gmail.com', NULL, N'9876543210', N'0123456789', N'0123456789', N'0123456789', N'0123456789', N'9876543210', N'Porbandar', N'Ahemadabad', N'Porbandar', N'IN', N'123456', 1, NULL, CAST(N'2020-04-29 20:51:26.510' AS DateTime), CAST(N'2020-04-29 21:11:18.623' AS DateTime), 1, NULL)
GO
INSERT [dbo].[Resource] ([ResourceKey], [CompanyKey], [ResourceTypeKey], [FirstName], [LastName], [Title], [Email], [Email2], [CellPhone], [HomePhone], [HomePhone2], [Work], [Work2], [Fax], [Address], [Address2], [City], [State], [Zip], [PrimaryContact], [Description], [DateAdded], [LastModificationTime], [Status], [RegistrationToken]) VALUES (1063, 3216, 1001, N'Property', N'Manager', N'Mr.', N'propertymanager@gmail.com', NULL, N'0987654321', N'9876543210', N'0123456789', N'345345345345', N'123456789', NULL, N'Address 1', N'Address 2', N'City', N'AK', N'55345', 1, NULL, CAST(N'2020-04-29 20:56:20.220' AS DateTime), CAST(N'2020-06-08 17:13:07.270' AS DateTime), 1, NULL)
GO
INSERT [dbo].[Resource] ([ResourceKey], [CompanyKey], [ResourceTypeKey], [FirstName], [LastName], [Title], [Email], [Email2], [CellPhone], [HomePhone], [HomePhone2], [Work], [Work2], [Fax], [Address], [Address2], [City], [State], [Zip], [PrimaryContact], [Description], [DateAdded], [LastModificationTime], [Status], [RegistrationToken]) VALUES (4254, 3217, 1002, NULL, NULL, N'Mr.', N'vendor@gmail.com', N'Vendor@gmail.com', NULL, NULL, NULL, NULL, NULL, NULL, N'hfhfghg', NULL, N'hfhgfh', N'IN', N'gfhfhf', 1, NULL, CAST(N'2020-06-08 16:20:16.353' AS DateTime), CAST(N'2020-06-09 18:20:15.700' AS DateTime), 2, NULL)
GO
SET IDENTITY_INSERT [dbo].[Resource] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

GO
INSERT [dbo].[User] ([UserKey], [ResourceKey], [Username], [Password], [TokenReset], [ResetExpirationDate], [AccountLocked], [FirstTimeAccess], [DateAdded], [LastModificationTime], [Status]) VALUES (3, 1062, N'Administrator', N'Oe58gljdfcwjVTIy6GQJc1Gh370CamYQwVbQATi6pgM=', NULL, NULL, NULL, NULL, CAST(N'2020-06-08 16:31:55.660' AS DateTime), CAST(N'2020-06-08 16:31:55.660' AS DateTime), 1)
GO
INSERT [dbo].[User] ([UserKey], [ResourceKey], [Username], [Password], [TokenReset], [ResetExpirationDate], [AccountLocked], [FirstTimeAccess], [DateAdded], [LastModificationTime], [Status]) VALUES (2, 1063, N'Property', N'Oe58gljdfcwjVTIy6GQJc1Gh370CamYQwVbQATi6pgM=', NULL, NULL, NULL, NULL, CAST(N'2020-06-08 16:31:04.577' AS DateTime), CAST(N'2020-06-08 16:31:04.577' AS DateTime), 1)
GO
INSERT [dbo].[User] ([UserKey], [ResourceKey], [Username], [Password], [TokenReset], [ResetExpirationDate], [AccountLocked], [FirstTimeAccess], [DateAdded], [LastModificationTime], [Status]) VALUES (16, 4254, N'vendor', N'Oe58gljdfcwjVTIy6GQJc1Gh370CamYQwVbQATi6pgM=', NULL, NULL, NULL, NULL, CAST(N'2020-06-09 15:11:54.707' AS DateTime), CAST(N'2020-06-09 15:11:54.707' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO