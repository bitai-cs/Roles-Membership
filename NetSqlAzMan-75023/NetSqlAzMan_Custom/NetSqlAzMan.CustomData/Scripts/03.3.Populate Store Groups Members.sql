/*
Run this script on:

.\sql2k14.AzMan_MS_CI_AI_CF    -  This database will be modified

to synchronize it with:

.\sql2k14.AzMan_MS_CI_AI

You are recommended to back up your database before running this script

Script created by SQL Data Compare version 11.1.3 from Red Gate Software Ltd at 09/01/2018 05:38:47 p.m.

*/
		
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
GO
SET DATEFORMAT YMD
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRANSACTION
-- Pointer used for text / image updates. This might not be needed, but is declared here just in case
DECLARE @pv binary(16)

PRINT(N'Drop constraints from [dbo].[netsqlazman_StoreGroupMembersTable]')
ALTER TABLE [dbo].[netsqlazman_StoreGroupMembersTable] DROP CONSTRAINT [FK_StoreGroupMembers_StoreGroup]

PRINT(N'Add 45 rows to [dbo].[netsqlazman_StoreGroupMembersTable]')
SET IDENTITY_INSERT [dbo].[netsqlazman_StoreGroupMembersTable] ON
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (1, 39, 0x56a80e4e6b0abf4ab581f1cf51ed4b51, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (2, 39, 0xf34fc56d835a2d478a03bd65f58aad88, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (3, 39, 0x4b0e89e2b785624f95ac81f38c78ab3a, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (4, 39, 0xc768134fd15c624cac0db22efa5a5cc8, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (5, 39, 0xf0f921062f4a3a448a892453c274fc1d, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (6, 39, 0x623d4594f4d2a64596af0e508d654a41, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (7, 39, 0x87c0907ea73f184ebb408480b2cf2a0a, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (8, 39, 0x76df7b888c458b42b60b23cd498facf0, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (9, 39, 0xa612d66270a41a43b13d78ef31489da2, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (10, 39, 0xe39c80397f3cd54caed247a5019bf360, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (11, 39, 0x8d7d2f7dc3b7454c87d7eb60204c6592, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (12, 39, 0x37abf7e26d409e489ff705a092a97453, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (13, 39, 0xb7facd3efd2f5648883e8a2539148b8c, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (14, 39, 0xff9b7493fc3c1f41b8a0c17faeb81e9f, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (15, 39, 0xf9468e74c683ce47b8247d6ed25c8995, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (16, 39, 0x9d8b3187ff06ce48bec94280ed584223, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (17, 39, 0x69236d11df6dd34eaf22635df1b7762c, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (18, 39, 0x66a848dd57986e42a68c9dbf57ee20c2, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (19, 39, 0x67fe366bde7b0449b2bd7cb8779ab5ab, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (20, 39, 0x071fc044853c84428037a65dd6ccb4e3, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (21, 41, 0x19fec26d731bb649a6637edde4c9aab1, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (22, 41, 0x5718181af22d854d94b303749ab27b25, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (23, 44, 0x90ddc78b91c27841bb87de21992b71e3, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (24, 44, 0x156bafd8af647b4b94e5f73e034287fe, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (25, 44, 0x3caf1978cb410449b13f425f85b62413, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (26, 44, 0xc816dfd3bb30034787d762b49edb7366, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (27, 44, 0xfc54c5368746054a937f62de5e23d807, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (28, 46, 0x510106fde1efe141a455d2e332e29620, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (29, 46, 0x91cb0bdc37694a4585950677007389ee, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (30, 48, 0xe66e2fc97f5a36419f966dc42a8871ee, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (31, 48, 0x66a848dd57986e42a68c9dbf57ee20c2, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (32, 49, 0x8cbb891154b337489045337f13e24d88, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (33, 49, 0x8c16a994b6faeb4fb6564069ec6d8617, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (34, 49, 0xbb5aa527232f3042b59d1fedb76803a6, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (35, 49, 0xf884e5815dc0ad478f966843c8f5054f, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (36, 49, 0xa486e3bf19524a4085fc151ffb4d83da, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (37, 49, 0xea4402b69f08af49ad8ac97de826fd8b, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (38, 49, 0x785591600b371b439b5ce0123330dee9, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (39, 49, 0x2dc6d2effb068146ba465d2492c049f9, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (40, 49, 0x510106fde1efe141a455d2e332e29620, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (41, 49, 0x91cb0bdc37694a4585950677007389ee, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (42, 49, 0x4ad200b02a9b26478f59b03e317f22c6, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (43, 49, 0xf5b8eb55773fab4dad4b45bb99c8463d, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (44, 49, 0x38449d88da5a81458bf7f47158b23c2d, 0, 1, NULL)
INSERT INTO [dbo].[netsqlazman_StoreGroupMembersTable] ([StoreGroupMemberId], [StoreGroupId], [objectSid], [WhereDefined], [IsMember], [LDAPDomain]) VALUES (45, 49, 0x150f6f8f1f625c4899b9fb687498e875, 0, 1, NULL)
SET IDENTITY_INSERT [dbo].[netsqlazman_StoreGroupMembersTable] OFF

PRINT(N'Add constraints to [dbo].[netsqlazman_StoreGroupMembersTable]')
ALTER TABLE [dbo].[netsqlazman_StoreGroupMembersTable] WITH NOCHECK ADD CONSTRAINT [FK_StoreGroupMembers_StoreGroup] FOREIGN KEY ([StoreGroupId]) REFERENCES [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId]) ON DELETE CASCADE
COMMIT TRANSACTION
GO
