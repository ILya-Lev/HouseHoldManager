SET IDENTITY_INSERT [dbo].[Tarifs] ON
INSERT INTO [dbo].[Tarifs] ([Id], [ApplicableSince], [ApplicableTill]) VALUES (1, N'2014-08-01 00:00:00', N'2015-03-30 00:00:00')
INSERT INTO [dbo].[Tarifs] ([Id], [ApplicableSince], [ApplicableTill]) VALUES (2, N'2015-04-01 00:00:00', N'2015-08-31 00:00:00')
INSERT INTO [dbo].[Tarifs] ([Id], [ApplicableSince], [ApplicableTill]) VALUES (3, N'2015-09-01 00:00:00', N'2016-02-29 00:00:00')
INSERT INTO [dbo].[Tarifs] ([Id], [ApplicableSince], [ApplicableTill]) VALUES (4, N'2016-03-01 00:00:00', N'2016-08-31 00:00:00')
INSERT INTO [dbo].[Tarifs] ([Id], [ApplicableSince], [ApplicableTill]) VALUES (5, N'2016-09-01 00:00:00', NULL)
SET IDENTITY_INSERT [dbo].[Tarifs] OFF
