/*
Run this script on:

.\sql2k14.AzMan_MS_CI_AI_CF    -  This database will be modified

to synchronize it with:

.\sql2k14.AzMan_MS_CI_AI

You are recommended to back up your database before running this script

Script created by SQL Data Compare version 11.1.3 from Red Gate Software Ltd at 09/01/2018 05:32:09 p.m.

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

PRINT(N'Drop constraint FK_Applications_Stores from [dbo].[netsqlazman_ApplicationsTable]')
ALTER TABLE [dbo].[netsqlazman_ApplicationsTable] DROP CONSTRAINT [FK_Applications_Stores]

PRINT(N'Drop constraint FK_StoreAttributes_Stores from [dbo].[netsqlazman_StoreAttributesTable]')
ALTER TABLE [dbo].[netsqlazman_StoreAttributesTable] DROP CONSTRAINT [FK_StoreAttributes_Stores]

PRINT(N'Drop constraint FK_StoreGroups_Stores from [dbo].[netsqlazman_StoreGroupsTable]')
ALTER TABLE [dbo].[netsqlazman_StoreGroupsTable] DROP CONSTRAINT [FK_StoreGroups_Stores]

PRINT(N'Drop constraint FK_StorePermissions_StoresTable from [dbo].[netsqlazman_StorePermissionsTable]')
ALTER TABLE [dbo].[netsqlazman_StorePermissionsTable] DROP CONSTRAINT [FK_StorePermissions_StoresTable]

PRINT(N'Add 1 row to [dbo].[netsqlazman_StoresTable]')
SET IDENTITY_INSERT [dbo].[netsqlazman_StoresTable] ON
INSERT INTO [dbo].[netsqlazman_StoresTable] ([StoreId], [Name], [Description]) VALUES (1, N'SISE', N'')
SET IDENTITY_INSERT [dbo].[netsqlazman_StoresTable] OFF

PRINT(N'Add constraint FK_Applications_Stores to [dbo].[netsqlazman_ApplicationsTable]')
ALTER TABLE [dbo].[netsqlazman_ApplicationsTable] WITH NOCHECK ADD CONSTRAINT [FK_Applications_Stores] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[netsqlazman_StoresTable] ([StoreId]) ON DELETE CASCADE

PRINT(N'Add constraint FK_StoreAttributes_Stores to [dbo].[netsqlazman_StoreAttributesTable]')
ALTER TABLE [dbo].[netsqlazman_StoreAttributesTable] WITH NOCHECK ADD CONSTRAINT [FK_StoreAttributes_Stores] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[netsqlazman_StoresTable] ([StoreId]) ON DELETE CASCADE

PRINT(N'Add constraint FK_StoreGroups_Stores to [dbo].[netsqlazman_StoreGroupsTable]')
ALTER TABLE [dbo].[netsqlazman_StoreGroupsTable] WITH NOCHECK ADD CONSTRAINT [FK_StoreGroups_Stores] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[netsqlazman_StoresTable] ([StoreId]) ON DELETE CASCADE

PRINT(N'Add constraint FK_StorePermissions_StoresTable to [dbo].[netsqlazman_StorePermissionsTable]')
ALTER TABLE [dbo].[netsqlazman_StorePermissionsTable] WITH NOCHECK ADD CONSTRAINT [FK_StorePermissions_StoresTable] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[netsqlazman_StoresTable] ([StoreId]) ON DELETE CASCADE
COMMIT TRANSACTION
GO
