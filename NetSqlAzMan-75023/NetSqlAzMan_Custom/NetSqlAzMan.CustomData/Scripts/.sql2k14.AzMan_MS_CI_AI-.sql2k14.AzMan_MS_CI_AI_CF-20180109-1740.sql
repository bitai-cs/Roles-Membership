/*
Run this script on:

.\sql2k14.AzMan_MS_CI_AI_CF    -  This database will be modified

to synchronize it with:

.\sql2k14.AzMan_MS_CI_AI

You are recommended to back up your database before running this script

Script created by SQL Data Compare version 11.1.3 from Red Gate Software Ltd at 09/01/2018 05:40:38 p.m.

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

PRINT(N'Drop constraints from [dbo].[netsqlazman_ApplicationGroupsTable]')
ALTER TABLE [dbo].[netsqlazman_ApplicationGroupsTable] DROP CONSTRAINT [FK_ApplicationGroups_Applications]

PRINT(N'Drop constraint FK_ApplicationGroupMembers_ApplicationGroup from [dbo].[netsqlazman_ApplicationGroupMembersTable]')
ALTER TABLE [dbo].[netsqlazman_ApplicationGroupMembersTable] DROP CONSTRAINT [FK_ApplicationGroupMembers_ApplicationGroup]

PRINT(N'Add 1 row to [dbo].[netsqlazman_ApplicationGroupsTable]')
SET IDENTITY_INSERT [dbo].[netsqlazman_ApplicationGroupsTable] ON
INSERT INTO [dbo].[netsqlazman_ApplicationGroupsTable] ([ApplicationGroupId], [ApplicationId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (1, 7, 0x0030255b5259694597d62362eb4a7703, N'_Windows', N'', N'', 0)
SET IDENTITY_INSERT [dbo].[netsqlazman_ApplicationGroupsTable] OFF

PRINT(N'Add constraints to [dbo].[netsqlazman_ApplicationGroupsTable]')
ALTER TABLE [dbo].[netsqlazman_ApplicationGroupsTable] WITH NOCHECK ADD CONSTRAINT [FK_ApplicationGroups_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId]) ON DELETE CASCADE

PRINT(N'Add constraint FK_ApplicationGroupMembers_ApplicationGroup to [dbo].[netsqlazman_ApplicationGroupMembersTable]')
ALTER TABLE [dbo].[netsqlazman_ApplicationGroupMembersTable] WITH NOCHECK ADD CONSTRAINT [FK_ApplicationGroupMembers_ApplicationGroup] FOREIGN KEY ([ApplicationGroupId]) REFERENCES [dbo].[netsqlazman_ApplicationGroupsTable] ([ApplicationGroupId]) ON DELETE CASCADE
COMMIT TRANSACTION
GO
