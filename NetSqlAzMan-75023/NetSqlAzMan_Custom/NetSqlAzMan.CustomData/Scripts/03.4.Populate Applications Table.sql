/*
Run this script on:

.\sql2k14.AzMan_MS_CI_AI_CF    -  This database will be modified

to synchronize it with:

.\sql2k14.AzMan_MS_CI_AI

You are recommended to back up your database before running this script

Script created by SQL Data Compare version 11.1.3 from Red Gate Software Ltd at 09/01/2018 05:39:41 p.m.

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

PRINT(N'Drop constraints from [dbo].[netsqlazman_ApplicationsTable]')
ALTER TABLE [dbo].[netsqlazman_ApplicationsTable] DROP CONSTRAINT [FK_Applications_Stores]

PRINT(N'Drop constraint FK_ApplicationAttributes_Applications from [dbo].[netsqlazman_ApplicationAttributesTable]')
ALTER TABLE [dbo].[netsqlazman_ApplicationAttributesTable] DROP CONSTRAINT [FK_ApplicationAttributes_Applications]

PRINT(N'Drop constraint FK_ApplicationGroups_Applications from [dbo].[netsqlazman_ApplicationGroupsTable]')
ALTER TABLE [dbo].[netsqlazman_ApplicationGroupsTable] DROP CONSTRAINT [FK_ApplicationGroups_Applications]

PRINT(N'Drop constraint FK_ApplicationPermissions_ApplicationsTable from [dbo].[netsqlazman_ApplicationPermissionsTable]')
ALTER TABLE [dbo].[netsqlazman_ApplicationPermissionsTable] DROP CONSTRAINT [FK_ApplicationPermissions_ApplicationsTable]

PRINT(N'Drop constraint FK_Items_Applications from [dbo].[netsqlazman_ItemsTable]')
ALTER TABLE [dbo].[netsqlazman_ItemsTable] DROP CONSTRAINT [FK_Items_Applications]

PRINT(N'Add 11 rows to [dbo].[netsqlazman_ApplicationsTable]')
SET IDENTITY_INSERT [dbo].[netsqlazman_ApplicationsTable] ON
INSERT INTO [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId], [StoreId], [Name], [Description]) VALUES (1, 1, N'ACADEMICO', N'')
INSERT INTO [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId], [StoreId], [Name], [Description]) VALUES (2, 1, N'BOLSA_EMPLEO', N'')
INSERT INTO [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId], [StoreId], [Name], [Description]) VALUES (3, 1, N'CAMPUS_ADMINISTRATIVO', N'')
INSERT INTO [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId], [StoreId], [Name], [Description]) VALUES (4, 1, N'CONTROL_SESIONES_DOCENTES', N'')
INSERT INTO [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId], [StoreId], [Name], [Description]) VALUES (5, 1, N'GESTION_HONORARIOS_4TA', N'')
INSERT INTO [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId], [StoreId], [Name], [Description]) VALUES (6, 1, N'INCIDENCIAS', N'')
INSERT INTO [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId], [StoreId], [Name], [Description]) VALUES (7, 1, N'LOGIN', N'')
INSERT INTO [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId], [StoreId], [Name], [Description]) VALUES (8, 1, N'REG_PRACT_PROFESIONALES', N'')
INSERT INTO [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId], [StoreId], [Name], [Description]) VALUES (9, 1, N'SEGUIMIENTO_DOCENTES', N'')
INSERT INTO [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId], [StoreId], [Name], [Description]) VALUES (10, 1, N'TRAMITES_ALUMNADO', N'')
INSERT INTO [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId], [StoreId], [Name], [Description]) VALUES (11, 1, N'TRANSMISION_DATOS_WS_PRONABEC', N'')
SET IDENTITY_INSERT [dbo].[netsqlazman_ApplicationsTable] OFF

PRINT(N'Add constraints to [dbo].[netsqlazman_ApplicationsTable]')
ALTER TABLE [dbo].[netsqlazman_ApplicationsTable] WITH NOCHECK ADD CONSTRAINT [FK_Applications_Stores] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[netsqlazman_StoresTable] ([StoreId]) ON DELETE CASCADE

PRINT(N'Add constraint FK_ApplicationAttributes_Applications to [dbo].[netsqlazman_ApplicationAttributesTable]')
ALTER TABLE [dbo].[netsqlazman_ApplicationAttributesTable] WITH NOCHECK ADD CONSTRAINT [FK_ApplicationAttributes_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId]) ON DELETE CASCADE

PRINT(N'Add constraint FK_ApplicationGroups_Applications to [dbo].[netsqlazman_ApplicationGroupsTable]')
ALTER TABLE [dbo].[netsqlazman_ApplicationGroupsTable] WITH NOCHECK ADD CONSTRAINT [FK_ApplicationGroups_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId]) ON DELETE CASCADE

PRINT(N'Add constraint FK_ApplicationPermissions_ApplicationsTable to [dbo].[netsqlazman_ApplicationPermissionsTable]')
ALTER TABLE [dbo].[netsqlazman_ApplicationPermissionsTable] WITH NOCHECK ADD CONSTRAINT [FK_ApplicationPermissions_ApplicationsTable] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId]) ON DELETE CASCADE

PRINT(N'Add constraint FK_Items_Applications to [dbo].[netsqlazman_ItemsTable]')
ALTER TABLE [dbo].[netsqlazman_ItemsTable] WITH NOCHECK ADD CONSTRAINT [FK_Items_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[netsqlazman_ApplicationsTable] ([ApplicationId]) ON DELETE CASCADE
COMMIT TRANSACTION
GO
