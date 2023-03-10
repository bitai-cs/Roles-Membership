/*
Run this script on:

.\sql2k14.AzMan_MS_CI_AI_CF    -  This database will be modified

to synchronize it with:

.\sql2k14.AzMan_MS_CI_AI

You are recommended to back up your database before running this script

Script created by SQL Data Compare version 11.1.3 from Red Gate Software Ltd at 09/01/2018 05:41:59 p.m.

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

PRINT(N'Drop constraints from [dbo].[netsqlazman_AuthorizationsTable]')
ALTER TABLE [dbo].[netsqlazman_AuthorizationsTable] DROP CONSTRAINT [FK_Authorizations_Items]

PRINT(N'Drop constraint FK_AuthorizationAttributes_Authorizations from [dbo].[netsqlazman_AuthorizationAttributesTable]')
ALTER TABLE [dbo].[netsqlazman_AuthorizationAttributesTable] DROP CONSTRAINT [FK_AuthorizationAttributes_Authorizations]

PRINT(N'Add 1 row to [dbo].[netsqlazman_AuthorizationsTable]')
SET IDENTITY_INSERT [dbo].[netsqlazman_AuthorizationsTable] ON
INSERT INTO [dbo].[netsqlazman_AuthorizationsTable] ([AuthorizationId], [ItemId], [ownerSid], [ownerSidWhereDefined], [objectSid], [objectSidWhereDefined], [AuthorizationType], [ValidFrom], [ValidTo], [LDAPDomain]) VALUES (1, 49, 0x01050000000000051500000019350872a77f0e3bc63fd75b19440000, 2, 0x01050000000000051500000019350872a77f0e3bc63fd75b19440000, 3, 3, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[netsqlazman_AuthorizationsTable] OFF

PRINT(N'Add constraints to [dbo].[netsqlazman_AuthorizationsTable]')
ALTER TABLE [dbo].[netsqlazman_AuthorizationsTable] WITH NOCHECK ADD CONSTRAINT [FK_Authorizations_Items] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[netsqlazman_ItemsTable] ([ItemId]) ON DELETE CASCADE

PRINT(N'Add constraint FK_AuthorizationAttributes_Authorizations to [dbo].[netsqlazman_AuthorizationAttributesTable]')
ALTER TABLE [dbo].[netsqlazman_AuthorizationAttributesTable] WITH NOCHECK ADD CONSTRAINT [FK_AuthorizationAttributes_Authorizations] FOREIGN KEY ([AuthorizationId]) REFERENCES [dbo].[netsqlazman_AuthorizationsTable] ([AuthorizationId]) ON DELETE CASCADE
COMMIT TRANSACTION
GO
