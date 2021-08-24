use [AssociationBids]
go

SET IDENTITY_INSERT [dbo].[LookUpType] ON 

INSERT [dbo].[LookUpType] ([LookUpTypeKey], [Title]) VALUES (1, N'Status')
INSERT [dbo].[LookUpType] ([LookUpTypeKey], [Title]) VALUES (2, N'Access')
INSERT [dbo].[LookUpType] ([LookUpTypeKey], [Title]) VALUES (3, N'Task Status')
INSERT [dbo].[LookUpType] ([LookUpTypeKey], [Title]) VALUES (4, N'Task Priority')
INSERT [dbo].[LookUpType] ([LookUpTypeKey], [Title]) VALUES (5, N'Email Status')
INSERT [dbo].[LookUpType] ([LookUpTypeKey], [Title]) VALUES (6, N'Bid Request Status')
INSERT [dbo].[LookUpType] ([LookUpTypeKey], [Title]) VALUES (7, N'Bid Vendor Status')
INSERT [dbo].[LookUpType] ([LookUpTypeKey], [Title]) VALUES (8, N'Bid Status')
INSERT [dbo].[LookUpType] ([LookUpTypeKey], [Title]) VALUES (9, N'Message Status')
INSERT [dbo].[LookUpType] ([LookUpTypeKey], [Title]) VALUES (10, N'Company Type')
INSERT [dbo].[LookUpType] ([LookUpTypeKey], [Title]) VALUES (11, N'Resource Type')
INSERT [dbo].[LookUpType] ([LookUpTypeKey], [Title]) VALUES (12, N'Pricing Type')
INSERT [dbo].[LookUpType] ([LookUpTypeKey], [Title]) VALUES (13, N'Payment Type')
INSERT [dbo].[LookUpType] ([LookUpTypeKey], [Title]) VALUES (14, N'Frequency Type')
INSERT [dbo].[LookUpType] ([LookUpTypeKey], [Title]) VALUES (15, N'EmailType')
INSERT [dbo].[LookUpType] ([LookUpTypeKey], [Title]) VALUES (17, N'Notification Type')
SET IDENTITY_INSERT [dbo].[LookUpType] OFF
GO
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (100, 1, N'Pending', 1, 1)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (101, 1, N'Approved', 2, 2)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (102, 1, N'Unapproved', 4, 3)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (104, 1, N'Duplicate', 8, 4)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (200, 2, N'Create', 1, 1)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (201, 2, N'Read', 2, 2)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (202, 2, N'Update', 4, 3)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (203, 2, N'Delete', 8, 4)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (300, 3, N'Not Started', 1, 1)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (301, 3, N'In Progress', 2, 2)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (302, 3, N'Waiting', 16, 3)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (303, 3, N'Deferred', 8, 4)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (304, 3, N'Complete', 4, 5)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (305, 3, N'Closed', 32, 6)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (400, 4, N'Low', 1, 1)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (401, 4, N'Normal', 2, 2)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (402, 4, N'High', 4, 3)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (500, 5, N'New', 1, 1)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (501, 5, N'Pending', 2, 2)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (502, 5, N'Sent', 4, 3)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (600, 6, N'In Progress', 1, 1)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (601, 6, N'Submitted', 2, 2)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (602, 6, N'Completed', 4, 3)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (603, 6, N'Closed', 8, 4)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (604, 6, N'Cancelled', 0, 5)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (700, 7, N'In Progress', 1, 1)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (701, 7, N'Submitted', 2, 2)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (702, 7, N'Interested', 4, 3)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (703, 7, N'Not Interested', 8, 4)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (800, 8, N'In Progress', 1, 1)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (801, 8, N'Submitted', 2, 2)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (802, 8, N'Accepted', 4, 3)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (803, 8, N'Rejected', 8, 4)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (900, 9, N'New', 1, 1)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (901, 9, N'Read', 2, 2)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (902, 9, N'Deleted', 4, 3)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1000, 10, N'Administration', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1001, 10, N'Management Company', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1002, 10, N'Vendor', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1003, 10, N'Company Vendor', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1100, 11, N'Staff', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1101, 11, N'Contact', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1102, 11, N'Mailing Address', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1103, 11, N'Other', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1200, 12, N'Membership Fee', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1201, 12, N'No Bid Fee', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1202, 12, N'Bid Fee', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1300, 13, N'Check', NULL, 1)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1301, 13, N'ACH', NULL, 2)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1302, 13, N'Debit Card', NULL, 3)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1303, 13, N'Credit Card', NULL, 4)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1304, 13, N'Other', NULL, 5)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1400, 14, N'One-Time', NULL, 1)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1401, 14, N'Daily', NULL, 2)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1402, 14, N'Weekly', NULL, 3)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1403, 14, N'Every two weeks', NULL, 4)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1404, 14, N'Twice a month', NULL, 5)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1405, 14, N'Monthly', NULL, 6)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1406, 14, N'Quarterly', NULL, 7)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1407, 14, N'Twice a year', NULL, 8)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1408, 14, N'Annually', NULL, 9)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1410, 15, N'Manager registration', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1411, 15, N'Vendor registration', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1412, 15, N'Bid invitation', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1413, 15, N'Bid invitation reminde', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1414, 15, N'Bid withdrawal email', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1415, 15, N'Proposal reminder', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1416, 15, N'Bid award', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1417, 15, N'Bid rejection', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1418, 15, N'Comments email', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1419, 15, N'Incomplete bid fee', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1420, 15, N'Win fee', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1421, 15, N'Credit card about to expire', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1422, 15, N'Credit card expired', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1423, 15, N'Insurance renewal', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1424, 15, N'Insurance expired', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1425, 15, N'Membership renewal', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1426, 15, N'Membership expired', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1427, 15, N'Reset Password', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1428, 15, N'Vendor approval', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1429, 15, N'Vendor invitation', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1430, 15, N'Forgot Password', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1431, 15, N'Vendor Registration Confirm', 0, 0)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1432, 15, N'Bid Reject Email', 0, 0)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1433, 15, N'BidSubmissionReminder', 0, 0)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1434, 15, N'Bid Fine', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1435, 15, N'MembershipFees', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1436, 15, N'RefundRequest', NULL, NULL)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1701, 17, N'BidReqMsg', 1, 1)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1702, 17, N'BidReqStatus', 2, 2)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1703, 17, N'BidVendorStatus', 3, 3)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1704, 17, N'BidReqStatusAccept', 4, 4)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1705, 17, N'BidReqStatusReject', 5, 5)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1706, 17, N'BidReqStatusRejByAcceptOther', 6, 6)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1707, 17, N'VendorReg', 7, 7)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1708, 17, N'RefundReq', 8, 8)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1709, 17, N'RefundReqMsg', 9, 9)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1710, 17, N'CCExpiry', 10, 10)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1711, 17, N'CCExpired', 11, 11)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1712, 17, N'MembershipExpiry', 12, 12)
GO
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1713, 17, N'MembershipExpired', 13, 13)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1714, 17, N'InsuranceExpiry', 14, 14)
INSERT [dbo].[LookUp] ([LookUpKey], [LookUpTypeKey], [Title], [Value], [SortOrder]) VALUES (1715, 17, N'InsuranceExpired', 15, 15)
GO
SET IDENTITY_INSERT [dbo].[Pricing] ON 

INSERT [dbo].[Pricing] ([PricingKey], [CompanyKey], [PricingTypeKey], [StartAmount], [EndAmount], [Fee], [LastModificationTime], [SortOrder], [FeeType]) VALUES (19, 15, 1200, NULL, NULL, 100.0000, CAST(N'1900-01-01T07:50:50.000' AS DateTime), NULL, N'Fixed')
INSERT [dbo].[Pricing] ([PricingKey], [CompanyKey], [PricingTypeKey], [StartAmount], [EndAmount], [Fee], [LastModificationTime], [SortOrder], [FeeType]) VALUES (20, 15, 1202, NULL, 5000.0000, 75.0000, CAST(N'1900-01-01T07:51:23.000' AS DateTime), NULL, N'Fixed')
INSERT [dbo].[Pricing] ([PricingKey], [CompanyKey], [PricingTypeKey], [StartAmount], [EndAmount], [Fee], [LastModificationTime], [SortOrder], [FeeType]) VALUES (21, 15, 1202, 5001.0000, 1000000.0000, 10.0000, CAST(N'1900-01-01T07:51:54.000' AS DateTime), NULL, N'Percentage')
SET IDENTITY_INSERT [dbo].[Pricing] OFF
GO
SET IDENTITY_INSERT [dbo].[EmailTemplate] ON 

INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1066, N'Vendor Invitation', N'Association Bids Invitation for [CompanyName]', N'<p>Hi [MemberName],</p>

<p>Your company has been invited to join the Association Bids Vendor community. By completing the registration via the link below, you will gain access to a fast growing network of properies that source all of their service and maintance needs through the Association Bids community. Each year millions of dollars of services will be won on this platform, and we welcome you to bid and win your share.</p>

<p><a href="[VendorRegistrationLink]">Click here</a> to register.</p>

<p>&nbsp;</p>

<p>Company Name : [CompanyName]</p>

<p>Email : [Email]</p>

<p>Contact Person : [ContactPerson]</p>

<p>Thanks,</p>

<p>&nbsp;</p>

<p>Association Bids Team</p>

<p>&nbsp;</p>

<p>Please add this sender/email address to your safe list.</p>
', CAST(N'2020-09-10T13:32:36.547' AS DateTime), 1429)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1067, N'Vendor Registration', N'Association Bids registration sumbitted for [CompanyName]', N'<p>Hi [MemberName],</p>

<p>New vendor has registered, please check.</p>

<p>Company Name:&nbsp;&nbsp;[CompanyName]</p>

<p>Legal Name:&nbsp;&nbsp;[LegalName]</p>

<p>Tax ID:&nbsp;[TaxId]</p>

<p>Web Site:&nbsp;&nbsp;[WebSite]</p>

<p>Services:&nbsp;&nbsp;[Service]</p>

<p>&nbsp;</p>

<p>&nbsp;</p>

<p>Thanks,</p>

<p>&nbsp;</p>

<p>Association Bids Team</p>
', CAST(N'2020-09-10T15:13:19.850' AS DateTime), 1411)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1068, N'Email for reset password', N'Association Bids Password reset request for [MemberName]', N'<p>Hi [MemberName],</p>

<p>We have received your request to reset password at Assocation Bids.</p>

<p><a href="[ResetPasswordlink]">Click here</a> to reset your password.</p>

<p>[SendDate]</p>

<p>[LinkExpiryDate]</p>

<p>[MemberEmail]</p>

<p>[MemberCompanyName]</p>

<p>Thanks,</p>

<p>Association Bids Team</p>
', CAST(N'2020-09-10T18:19:52.270' AS DateTime), 1427)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1069, N'Vendor Approval', N'Association Bids Vendor Approval for [CompanyName]', N'<p>Hi [MemberName],</p>

<p>Your registration has been&nbsp;approved successfully.&nbsp;</p>

<p><a href="[ResetPasswordlink]">Click here</a> to reset your password and you can login as a vendor.</p>

<p>&nbsp;</p>

<p>Company Name: [CompanyName]</p>

<p>&nbsp;</p>

<p>Thanks</p>

<p>Association Bids Team</p>
', CAST(N'2020-09-10T18:48:24.383' AS DateTime), 1428)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1070, N'Bid Reject Email', N'[BidName] Bid Rejected', N'<p>Hi [MemberName],</p>

<p>Your bid has been rejected by&nbsp;Assocation Bids.</p>

<p>Company Name = [CompanyName]</p>

<p>First NAme = [FirstName]</p>

<p>Email Address = [EmailAdderess]</p>

<p>Bid Name = [BidName]</p>

<p>&nbsp;</p>

<p>Thanks</p>

<p>&nbsp;</p>

<p>Association Bids Team</p>

<p>&nbsp;</p>
', CAST(N'2020-09-10T20:48:51.403' AS DateTime), 1432)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1071, N'Manager Registration', N'Association Bids Property Manager Registration for [MemberName]', N'<p>Hi [MemberName],</p>

<p>Please complete your registration as a Property Manager for Assocation Bids by establishing your password via the link below.</p>

<p><a href="[ResetPasswordlink]">Click here</a> to set your password.</p>

<p>Thanks</p>

<p>&nbsp;</p>

<p>Association Bids Team</p>

<p>&nbsp;</p>
', CAST(N'2020-09-10T21:00:39.107' AS DateTime), 1410)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1072, N'Bid Invitation', N'[CompanyName] invitation to Bid on [BidName] ', N'<p>Hi [MemberName],</p>

<p>We are inviting you for Bid on [Services]&nbsp; services for&nbsp;[Property]</p>

<p>[ResponseDueDate] and&nbsp;[BidDueDate]</p>

<p>[Title]</p>

<p>[VendorName]</p>

<p>[CompanyName]&nbsp;</p>

<p>&nbsp;</p>

<p>Thanks</p>

<p>Association Bids Team</p>
', CAST(N'2020-09-10T21:02:04.780' AS DateTime), 1412)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1073, N'Bid Invitation Reminder', N'[CompanyName] Reminder to Bid on [BidName] ', N'<p>Hi [MemberName],</p>

<p>Your&nbsp; [Title]&nbsp;&nbsp;&nbsp; Due&nbsp; Date is&nbsp;[ResponseDueDate] . Please Check&nbsp; It&nbsp;Immediately.</p>

<p>Thanks</p>

<p>&nbsp;</p>

<p>Association Bids Team</p>

<p>&nbsp;</p>
', CAST(N'2020-09-10T21:03:05.817' AS DateTime), 1413)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1074, N'Bid Withdrawal Email', N'Email for Bid Withdrawal Email', N'<p>Hi [MemberName],</p>

<p>We Bid Withdrawal Email at Assocation Bids.</p>

<p>Thanks</p>

<p>Association Bids Team</p>
', CAST(N'2020-06-24T18:24:57.050' AS DateTime), 1414)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1075, N'Proposal Reminder', N'Email for Proposal Reminder', N'<p>Hi [MemberName],</p>

<p>Your&nbsp;[BidName] bid response is due&nbsp;[ResponseDueDate]. Please <a href="[ProposalReminderLink]">click here</a> to respond.</p>

<p>Thanks</p>

<p>&nbsp;</p>

<p>Association Bids Team</p>

<p>&nbsp;</p>
', CAST(N'2020-09-16T18:14:26.640' AS DateTime), 1415)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1076, N'Bid Award', N'Congratulations [CompanyName] has been awarded [BidName] ', N'<p>Hi [MemberName],</p>

<p>Congratulations [CompanyName] has been awarded [BidName]. Your Association Bids win fee has been invoiced. Please contact the property manager at [Property] to schedule and complete the work.</p>

<p>We inform you that your Bid [Title] Charge Amount is $[ChargeAmount].</p>

<p>It has been deducted&nbsp;from your account.</p>

<p>CompanyName =&nbsp;[CompanyName]</p>

<p>First Name =&nbsp;[FirstName]</p>

<p>EmailAdderess = [EmailAdderess]</p>

<p>BidName =&nbsp;[BidName]</p>

<p>Property: [Property]</p>

<p>Property Contact Detail: [PropertyContactDetail]</p>

<p>&nbsp;</p>

<p>Thanks</p>

<p>Association Bids Team</p>
', CAST(N'2020-09-16T12:35:27.593' AS DateTime), 1416)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1077, N'Bid Rejection', N'Email for Bid Rejection', N'<p>Hi [MemberName],</p>

<p>Bid has been declined by Vendor.</p>

<p>CompanyName = [CompanyName]</p>

<p>FirstName = [FirstName]</p>

<p>EmailAdderess = [EmailAdderess]</p>

<p>BidName = [BidName]</p>

<p>Thanks</p>

<p>Association Bids Team</p>
', CAST(N'2020-07-10T15:33:52.100' AS DateTime), 1417)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1078, N'Comments Email', N'Email for Comments', N'<p>Hi [MemberName],</p>

<p>We Comments Email at Assocation Bids.</p>

<p>Thanks</p>

<p>Association Bids Team</p>
', CAST(N'2020-07-24T12:04:03.937' AS DateTime), 1418)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1079, N'Incomplete Bid Fee', N'Incomplete Bid Fee Charged for [BidName]', N'<p>Hi [MemberName],</p>

<p>Per our terms of service, we have charged an incomplete Bid Fee for [BidName]. Please review the invoice via your vendor dashboard if you would like more details.</p>

<p>Thanks</p>

<p>&nbsp;</p>

<p>Association Bids Team</p>

<p>&nbsp;</p>
', CAST(N'2020-09-10T21:13:24.950' AS DateTime), 1419)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1080, N'Win Fee', N' Email for Win Fee', N'<p>Hi [MemberName],</p>

<p>We inform you that your Bid [Title] Charge Amount is $[ChargeAmount].00.</p>

<p>It has been deducted&nbsp;from your account .</p>

<p>Assocation Bids</p>

<p>Thanks Association Bids Team</p>
', CAST(N'2020-07-23T15:44:12.387' AS DateTime), 1420)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1081, N'Credit card about to Expire', N'Association Bids Credit card about to Expire', N'<p>Hi [MemberName],</p>

<p>The Credit card [CreditCardNo] associated to your Association Bids profile is about to Expire on [CreditCardExpireDate]. Please log in and update your credit card details to avoid missing out on opportunites to bid on new work.</p>

<p><a href="[LogInLink]">Log In</a></p>

<p>&nbsp;</p>

<p>Thanks</p>

<p>Association Bids Team</p>
', CAST(N'2020-09-16T15:15:51.403' AS DateTime), 1421)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1082, N'Credit card Expired', N'Association Bids Credit card has Expired', N'<p>Hi [MemberName],</p>

<p>The Credit card linked to your Association Bids profile expired on&nbsp;[CreditCardExpireDate]. Please log in and update your credit card details to avoid missing out on opportunites to bid on new work.</p>

<p><a href="[LogInLink]">Log In</a></p>

<p>Thanks</p>

<p>Association Bids Team</p>
', CAST(N'2020-09-11T11:39:37.943' AS DateTime), 1422)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1083, N'Insurance renewal', N'Association Bids Insurance renewal', N'<p>Hi [MemberName],</p>

<p>The Insurance policy&nbsp;[PolicyNumber]&nbsp;linked to your Association Bids profile is due for renewal&nbsp;[InsuranceRenewalDate]. Please&nbsp;<a href="[InsuranceRenewallink]">Click here</a> to&nbsp;renewal your Insurance</p>

<p>Thanks</p>

<p>&nbsp;</p>

<p>Association Bids Team</p>

<p>&nbsp;</p>
', CAST(N'2020-09-11T11:42:03.173' AS DateTime), 1423)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1084, N'Insurance Expired', N'Association Bids Insurance Expired', N'<p>Hi [MemberName],</p>

<p>The Insurance policy&nbsp;[PolicyNumber]&nbsp;linked to your Association Bids profile expired onl&nbsp;[InsuranceRenewalDate]. Please&nbsp;<a href="[InsuranceRenewallink]">Click here</a> to&nbsp;renewal your Insurance</p>

<p>Thanks</p>

<p>Association Bids Team</p>
', CAST(N'2020-09-11T11:42:55.643' AS DateTime), 1424)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1085, N'Membership Renewal', N'Association Bids Membership Renewal', N'<p>Hi [MemberName],</p>

<p>Your Association Bids Membership is due for renewal&nbsp;on [RenewalDate]&nbsp; No action is needed, your account will auto renew.</p>

<p>Thanks</p>

<p>&nbsp;</p>

<p>Association Bids Team</p>

<p>&nbsp;</p>
', CAST(N'2020-09-10T21:09:47.767' AS DateTime), 1425)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1086, N'Membership Expired', N'Association Bids Membership Expired', N'<p>Hi [MemberName],</p>

<p>Your&nbsp;Association Bids Membership expired today. Please <a href="[LogInLink]">Log In</a> to renew your membership.</p>

<p>Thanks</p>

<p>&nbsp;</p>

<p>Association Bids Team</p>

<p>&nbsp;</p>
', CAST(N'2020-09-11T11:44:52.267' AS DateTime), 1426)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1087, N'Incomplete Bid Fee', N'Email for Incomplete Bid Fee', N'<p>Hi [MemberName],</p>

<p>We have inform your Bid Fee are&nbsp;Incomplete at Assocation Bids.</p>

<p>Thanks</p>

<p>Association Bids Team</p>
', CAST(N'2020-06-24T19:01:32.113' AS DateTime), 1419)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (1088, N'Forgot Password', N'Association Bids Password Reset request for [MemberName]', N'<p>Hi [MemberName],</p>

<p>We have received your request to reset password at Assocation Bids.</p>

<p><a href="[ResetPasswordlink]">Click here</a> to reset your password.</p>

<p>[SendDate]</p>

<p>[LinkExpiryDate]</p>

<p>[MemberEmail]</p>

<p>[MemberCompanyName]</p>

<p>Thanks,</p>

<p>Association Bids Team</p>
', CAST(N'2020-09-10T16:22:22.510' AS DateTime), 1430)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (2067, N'Vendor Registration Confirm', N'Association Bids Registration Complete for [CompanyName]', N'<p>Hi [MemberName],</p>

<p>Your registration is complete and in review by the Association Bids Team. You will recive email confirmation when your registration is approved and you are ready to start winning new work from the Association Bids community.</p>

<p>Thanks</p>

<p>&nbsp;</p>

<p>Association Bids Team</p>

<p>&nbsp;</p>
', CAST(N'2020-09-10T21:06:48.833' AS DateTime), 1431)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (2068, N'Refund Request ', N'Association Bids Refund Request', N'<p>Hi [MemberName],</p>

<p>You have Refund Request of [VendorName] for [Title] about&nbsp;[ChargeAmount].</p>

<p>&nbsp;</p>

<p>Thanks</p>

<p>Association Bids Team</p>

<p>&nbsp;</p>
', CAST(N'2020-09-16T17:01:49.773' AS DateTime), 1433)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (2070, N'Membership Fees', N'Association Bids Membership Fee', N'<p>Hi [MemberName],</p>

<p>Your Association Bids membership&nbsp;fee&nbsp;of $[ChargeAmount] has been charged to your credit card on file. Please access your Vendor portal for more information and to review the invoice.</p>

<p>Assocation Bids</p>

<p>&nbsp;</p>

<p>Thanks Association Bids Team</p>

<p>&nbsp;</p>
', CAST(N'2020-09-10T21:05:02.010' AS DateTime), 1435)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (2071, N'Bid  Fine ', N'Bid Fine', N'<p>Hi [MemberName],</p>

<p>We inform you that your [Title]&nbsp;is&nbsp;not&nbsp;submitted , and&nbsp;due&nbsp;date&nbsp;is&nbsp;passed away.</p>

<p>We&nbsp;bid charge Amount for bid Fine [ChargeAmount] it has been Dedecut From your account .</p>

<p>Assocation Bids.</p>

<p>Thanks</p>

<p>Association Bids Team</p>
', CAST(N'2020-07-23T18:11:57.990' AS DateTime), 1434)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (2072, N'Refund Request Acknowledgement', N'Association Bids Refund Request', N'<p>Hi [MemberName],</p>

<p>We acknowledge that you requested a refund of bid [Title] for the amount of [ChargeAmount]. We are working on your request.</p>

<p>&nbsp;</p>

<p>Thanks</p>

<p>Association Bids Team</p>
', CAST(N'2020-09-16T15:23:08.557' AS DateTime), 1436)
INSERT [dbo].[EmailTemplate] ([EmailTemplateKey], [EmailTitle], [EmailSubject], [Body], [DateAdded], [lookUpType]) VALUES (2074, N'Refund Request Acknowledgement', N'Association Bids Refund Request', N'<p>Hi [MemberName],</p>    <p>We acknowledge that you requested a refund of bid [Title] for the amount of [ChargeAmount]. We are working on your request.</p>    <p>&nbsp;</p>    <p>Thanks</p>    <p>Association Bids Team</p>  ', CAST(N'2020-09-16T15:23:08.557' AS DateTime), 1436)
SET IDENTITY_INSERT [dbo].[EmailTemplate] OFF
GO
SET IDENTITY_INSERT [dbo].[Group] ON 

INSERT [dbo].[Group] ([GroupKey], [Title], [Description]) VALUES (1, N'Administrator', NULL)
INSERT [dbo].[Group] ([GroupKey], [Title], [Description]) VALUES (2, N'Supervisor', NULL)
INSERT [dbo].[Group] ([GroupKey], [Title], [Description]) VALUES (3, N'Property Manager', NULL)
INSERT [dbo].[Group] ([GroupKey], [Title], [Description]) VALUES (4, N'Staff', NULL)
INSERT [dbo].[Group] ([GroupKey], [Title], [Description]) VALUES (5, N'Vendor', NULL)
INSERT [dbo].[Group] ([GroupKey], [Title], [Description]) VALUES (6, N'Guest', NULL)
INSERT [dbo].[Group] ([GroupKey], [Title], [Description]) VALUES (7, N'Other', NULL)
SET IDENTITY_INSERT [dbo].[Group] OFF
GO
SET IDENTITY_INSERT [dbo].[GroupMember] ON 

INSERT [dbo].[GroupMember] ([GroupMemberKey], [GroupKey], [ResourceKey]) VALUES (1002, 1, 1062)
INSERT [dbo].[GroupMember] ([GroupMemberKey], [GroupKey], [ResourceKey]) VALUES (1003, 1, 1063)
INSERT [dbo].[GroupMember] ([GroupMemberKey], [GroupKey], [ResourceKey]) VALUES (1004, 1, 4254)
INSERT [dbo].[GroupMember] ([GroupMemberKey], [GroupKey], [ResourceKey]) VALUES (1038, 3, 7359)
INSERT [dbo].[GroupMember] ([GroupMemberKey], [GroupKey], [ResourceKey]) VALUES (1039, 1, 7360)
INSERT [dbo].[GroupMember] ([GroupMemberKey], [GroupKey], [ResourceKey]) VALUES (1040, 3, 7361)
INSERT [dbo].[GroupMember] ([GroupMemberKey], [GroupKey], [ResourceKey]) VALUES (1041, 3, 7362)
INSERT [dbo].[GroupMember] ([GroupMemberKey], [GroupKey], [ResourceKey]) VALUES (1042, 3, 7363)
INSERT [dbo].[GroupMember] ([GroupMemberKey], [GroupKey], [ResourceKey]) VALUES (1043, 3, 7364)
INSERT [dbo].[GroupMember] ([GroupMemberKey], [GroupKey], [ResourceKey]) VALUES (1044, 3, 7365)
SET IDENTITY_INSERT [dbo].[GroupMember] OFF
GO
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (1, N'Home', N'Home', N'Index', N'home.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (2, N'Billing', N'Billing', N'Index', N'billing.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (3, N'Settings', N'Settings', N'Index', N'settings.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (100, N'Bid Requests', N'BidRequest', N'Index', N'bid-request.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (101, N'Bids', N'Bid', N'Index', N'bid.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (102, N'Properties', N'Property', N'Index', N'property.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (103, N'Staff', N'Staff', N'Index', N'staff.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (104, N'Vendors', N'Vendor', N'Index', N'vendor.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (105, N'Verify Vendors', N'VendorVerify', N'Index', N'vendor-verify.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (106, N'Work Orders', N'WorkOrder', N'Index', N'work-order.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (200, N'Invoices', N'Invoice', N'Index', N'invoice.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (201, N'Payments', N'Payment', N'Index', N'payment.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (300, N'Account', N'Account', N'Index', N'password.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (301, N'Credit Cards', N'CreditCard', N'Index', N'credit-card.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (302, N'Insurance', N'Insurance', N'Index', N'insurance.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (303, N'Profile', N'Profile', N'Index', N'user.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (304, N'Services', N'CompanyService', N'Index', N'service.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (305, N'Service Areas', N'ServiceArea', N'Index', N'service-area.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (701, N'Company', N'Company', N'Index', N'', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (702, N'Documents', N'Document', N'Index', N'', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (703, N'Messages', N'Message', N'Index', N'', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (704, N'Notes', N'Note', N'Index', N'', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (705, N'Register', N'Register', N'Index', N'', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (706, N'Resources', N'Resource', N'Index', N'', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (707, N'Reminders', N'Reminder', N'Index', N'', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (708, N'Tasks', N'Task', N'Index', N'', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (709, N'Reward Points', N'Rewards', N'Index', N'reward-points.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (710, N'BisRequest', N'PMBidRequests', N'PMBidRequestAdd', NULL, NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (711, N'StaffDirectoryAdd', N'StaffDirectory', N'StaffDirectoryAdd', NULL, NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (712, N'Profile Image', N'PMProperties', N'Profile', N'216058-200.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (713, N'Properties', N'PMProperties', N'PMPropertyAdd', NULL, NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (714, N'Registration', N'Registration', N'RegistrationInsert', NULL, NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (715, N'Profile Image', N'PMDashboard', N'Profile', N'f2d67d8b0b75a420095546ab6036614d.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (716, N'Profile Image', N'vProfile', N'VProfile', N'15682.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (717, N'WorkOrders', N'PMWorkOrders', N'PMWorkOrdersAdd', NULL, NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (718, N'Profile Image', N'PMDashboard', N'Profile', N'person.jpg', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (719, N'Profile Image', N'PMProperties', N'Profile', N'person.jpg', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (720, N'Profile Image', N'PMProperties', N'Profile', N'person.jpg', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (721, N'Profile Image', N'PMProperties', N'Profile', N'counselor.png', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (722, N'Profile Image', N'vProfile', N'VProfile', N'brick.jpeg', NULL)
INSERT [dbo].[Module] ([ModuleKey], [Title], [Controller], [Action], [Image], [Description]) VALUES (723, N'Profile Image', N'PMProperties', N'Profile', N'imagesV79V66VV.jpg', NULL)
GO
SET IDENTITY_INSERT [dbo].[Portal] ON 

INSERT [dbo].[Portal] ([PortalKey], [PortalID], [Title], [Url], [SiteImage], [HomePageImage], [Stylesheet], [Description], [NotificationSetting], [DateAdded], [LastModificationTime], [Status]) VALUES (1, N'portal', N'Association Bids Portal', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-06-08T15:53:20.380' AS DateTime), CAST(N'2020-06-08T15:53:20.380' AS DateTime), 2)
INSERT [dbo].[Portal] ([PortalKey], [PortalID], [Title], [Url], [SiteImage], [HomePageImage], [Stylesheet], [Description], [NotificationSetting], [DateAdded], [LastModificationTime], [Status]) VALUES (2, N'company', N'Company Portal', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-06-08T15:53:20.380' AS DateTime), CAST(N'2020-06-08T15:53:20.380' AS DateTime), 2)
INSERT [dbo].[Portal] ([PortalKey], [PortalID], [Title], [Url], [SiteImage], [HomePageImage], [Stylesheet], [Description], [NotificationSetting], [DateAdded], [LastModificationTime], [Status]) VALUES (3, N'vendor', N'Vendor Portal', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2020-06-08T15:53:20.380' AS DateTime), CAST(N'2020-06-08T15:53:20.380' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[Portal] OFF
GO
SET IDENTITY_INSERT [dbo].[PushNotificationTemplate] ON 

INSERT [dbo].[PushNotificationTemplate] ([PushNotificaionTemplateKey], [PushNotificationTitle], [Body], [DateAdded], [NTSubject], [PushNotificationType]) VALUES (1, N'Pending Request', N'You request is pending. Please complete it by submitting mentioned doucments.', CAST(N'2020-06-15T19:20:11.867' AS DateTime), N'Pending Request', 100)
INSERT [dbo].[PushNotificationTemplate] ([PushNotificaionTemplateKey], [PushNotificationTitle], [Body], [DateAdded], [NTSubject], [PushNotificationType]) VALUES (2, N'New Bid Request [BidName]', N'<p>You have new bid request&nbsp;[BidName] from&nbsp;[CompanyName]</p>

<p>Your Response due date is [ResponseDueDate]</p>

<p>Bid due date is [BidDueDate]&nbsp;</p>
', CAST(N'2020-12-17T22:50:33.343' AS DateTime), N'New Bid Request [BidName]', 1702)
INSERT [dbo].[PushNotificationTemplate] ([PushNotificaionTemplateKey], [PushNotificationTitle], [Body], [DateAdded], [NTSubject], [PushNotificationType]) VALUES (3, N'Bid Accepted', N'<p>Your bid&nbsp;[BidName] has been accepted by&nbsp;[CompanyName].</p>
', CAST(N'2020-12-18T13:18:02.583' AS DateTime), N'Bid Accepted', 1704)
INSERT [dbo].[PushNotificationTemplate] ([PushNotificaionTemplateKey], [PushNotificationTitle], [Body], [DateAdded], [NTSubject], [PushNotificationType]) VALUES (4, N'Credit Card Expiration', N'<p>Your credit card number&nbsp;[CCNumber] is about to expire on&nbsp;[CCExpiryDate].</p>
', CAST(N'2020-12-18T13:20:48.957' AS DateTime), N'Credit Card Expiration', 1710)
INSERT [dbo].[PushNotificationTemplate] ([PushNotificaionTemplateKey], [PushNotificationTitle], [Body], [DateAdded], [NTSubject], [PushNotificationType]) VALUES (5, N'Credit Card Expired', N'<p>Your credit card has been expired.</p>
', CAST(N'2020-12-18T13:22:13.960' AS DateTime), N'Credit Card Expired', 1711)
INSERT [dbo].[PushNotificationTemplate] ([PushNotificaionTemplateKey], [PushNotificationTitle], [Body], [DateAdded], [NTSubject], [PushNotificationType]) VALUES (6, N'New Message', N'<p>You have a new message from&nbsp;[CompanyName].</p>
', CAST(N'2020-12-18T13:23:20.570' AS DateTime), N'New Message', 1701)
SET IDENTITY_INSERT [dbo].[PushNotificationTemplate] OFF
GO
SET IDENTITY_INSERT [dbo].[Service] ON 

INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (1, NULL, N'Accountant/CPA', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (2, NULL, N'Air Conditioning', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (3, NULL, N'Architects/Architectural Review', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (4, NULL, N'Asphalt Paving/Maintenace/Repair', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (5, NULL, N'Attorneys', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (6, NULL, N'Balcony Restoration', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (7, NULL, N'Banking/Financial Services', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (8, NULL, N'Builders/Developers', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (9, NULL, N'Cable/Internet/Phone', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (10, NULL, N'Carpentry', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (11, NULL, N'Carpet Cleaning', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (12, NULL, N'Carpet Installation', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (13, NULL, N'Catch Basin Cleaning', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (14, NULL, N'Collections', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (15, NULL, N'Concierge Services', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (16, NULL, N'Concrete Repair', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (17, NULL, N'Construction', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (18, NULL, N'Consulting', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (19, NULL, N'Credit Reporting', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (20, NULL, N'Deck Products and Services', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (21, NULL, N'Disaster Planning', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (22, NULL, N'Electrical Service', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (23, NULL, N'Elevators', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (24, NULL, N'Emergency Restoration Services', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (25, NULL, N'Engineers', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (26, NULL, N'Environmental & Safety Inspections', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (27, NULL, N'Environmental Services', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (28, NULL, N'Fire Safety Equipment', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (29, NULL, N'Flooring', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (30, NULL, N'Fountains', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (31, NULL, N'General Contractor', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (32, NULL, N'Grout and Tile Cleaning/Stone Polishing', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (33, NULL, N'Gutters', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (34, NULL, N'Heating, Ventilating, Air Conditioning', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (35, NULL, N'Insulation', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (36, NULL, N'Insurance', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (37, NULL, N'Irrigation', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (38, NULL, N'Lake and Pond Management', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (39, NULL, N'Landscaping/Lawn Care', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (40, NULL, N'Laundry Room Equipment/Maintenance', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (41, NULL, N'Lender', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (42, NULL, N'Lighting', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (43, NULL, N'Locksmith', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (44, NULL, N'Mailing Services', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (45, NULL, N'Maintenance', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (46, NULL, N'Marketing', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (47, NULL, N'Masonry', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (48, NULL, N'Newsletters/Publications/Printing', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (49, NULL, N'Painting Services and Retailers', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (50, NULL, N'Panel Brick Repair', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (51, NULL, N'Parking/Towing', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (52, NULL, N'Pest Control', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (53, NULL, N'Pet Waste Removal', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (54, NULL, N'Plumbing', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (55, NULL, N'Pool Services', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (56, NULL, N'Recreational/Playground Equipment', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (57, NULL, N'Reserve Studies', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (58, NULL, N'Restoration Services', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (59, NULL, N'Roofing', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (60, NULL, N'Roofing Manufacturer', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (61, NULL, N'Sealcoating', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (62, NULL, N'Security Products and Services', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (63, NULL, N'Siding', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (64, NULL, N'Snow Removal', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (65, NULL, N'Tree Care Services', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (66, NULL, N'Utility/Solar/Energy Services', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (67, NULL, N'Ventilating', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (68, NULL, N'Waste Management Services', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (69, NULL, N'Waterproofing', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (70, NULL, N'Websites / Internet Service', NULL)
INSERT [dbo].[Service] ([ServiceKey], [ParentServiceKey], [Title], [Tags]) VALUES (71, NULL, N'Windows and Doors', NULL)
SET IDENTITY_INSERT [dbo].[Service] OFF
GO
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'AK', N'Alaska')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'AL', N'Alabama')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'AR', N'Arkansas')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'AZ', N'Arizona')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'CA', N'California')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'CO', N'Colorado')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'CT', N'Connecticut')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'DC', N'Washington, D.C.')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'DE', N'Delaware')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'FL', N'Florida')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'GA', N'Georgia')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'HI', N'Hawaii')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'IA', N'Iowa')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'ID', N'Idaho')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'IL', N'Illinois')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'IN', N'Indiana')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'KS', N'Kansas')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'KY', N'Kentucky')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'LA', N'Louisiana ')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'MA', N'Massachusetts')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'MD', N'Maryland')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'ME', N'Maine')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'MI', N'Michigan')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'MN', N'Minnesota')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'MO', N'Missouri')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'MS', N'Mississippi')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'MT', N'Montana')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'NC', N'North Carolina')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'ND', N'North Dakota')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'NE', N'Nebraska')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'NH', N'New Hampshire')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'NJ', N'New Jersey')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'NM', N'New Mexico')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'NV', N'Nevada')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'NY', N'New York')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'OH', N'Ohio')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'OK', N'Oklahoma')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'OR', N'Oregon')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'PA', N'Pennsylvania')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'RI', N'Rhode Island')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'SC', N'South Carolina')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'SD', N'South Dakota')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'TN', N'Tennessee')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'TX', N'Texas')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'UT', N'Utah')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'VA', N'Virginia')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'VT', N'Vermont')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'WA', N'Washington')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'WI', N'Wisconsin')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'WV', N'West Virginia')
INSERT [dbo].[State] ([StateKey], [Title]) VALUES (N'WY', N'Wyoming')
GO
