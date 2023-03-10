/*
Run this script on:

.\sql2k14.AzMan_MS_CI_AI_CF    -  This database will be modified

to synchronize it with:

.\sql2k14.AzMan_MS_CI_AI

You are recommended to back up your database before running this script

Script created by SQL Data Compare version 11.1.3 from Red Gate Software Ltd at 09/01/2018 05:33:40 p.m.

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

PRINT(N'Drop constraints from [dbo].[netsqlazman_StoreGroupsTable]')
ALTER TABLE [dbo].[netsqlazman_StoreGroupsTable] DROP CONSTRAINT [FK_StoreGroups_Stores]

PRINT(N'Drop constraint FK_StoreGroupMembers_StoreGroup from [dbo].[netsqlazman_StoreGroupMembersTable]')
ALTER TABLE [dbo].[netsqlazman_StoreGroupMembersTable] DROP CONSTRAINT [FK_StoreGroupMembers_StoreGroup]

PRINT(N'Add 53 rows to [dbo].[netsqlazman_StoreGroupsTable]')
SET IDENTITY_INSERT [dbo].[netsqlazman_StoreGroupsTable] ON
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (1, 1, 0xff2c66e24be57446a9d3286d1ca5ab3e, N'Jefatura_Producto_Banca', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (2, 1, 0x06546b59a95bf247a5eddde0f21612a6, N'Jefatura_Producto_Comunicaciones', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (3, 1, 0x29b6a633b3e8004cb93a3e6c0733913e, N'Jefatura_Producto_ETI', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (4, 1, 0x8681a6674cff794b95fe371c922121f1, N'Jefatura_Producto_Gestion', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (5, 1, 0x28f5f0d1cd656c4faa2ebf259dc8af9e, N'Jefatura_Producto_Ingles', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (6, 1, 0x6e6593bf1fccf74a9b8feb1e18605ddc, N'Jefatura_Producto_Secretariado', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (7, 1, 0x5718181af22d854d94b303749ab27b25, N'Larco_Caja', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (8, 1, 0x4b0e89e2b785624f95ac81f38c78ab3a, N'Larco_JefaturaAcademica', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (9, 1, 0x156bafd8af647b4b94e5f73e034287fe, N'Larco_JefaturaCobranza', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (10, 1, 0x56a80e4e6b0abf4ab581f1cf51ed4b51, N'Larco_SAA', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (11, 1, 0xf34fc56d835a2d478a03bd65f58aad88, N'Larco_SAD', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (12, 1, 0xc85820add5510548b744c15601c6d46a, N'Lima_Caja', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (13, 1, 0x87c0907ea73f184ebb408480b2cf2a0a, N'Lima_JefaturaAcademica', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (14, 1, 0x3caf1978cb410449b13f425f85b62413, N'Lima_JefaturaCobranza', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (15, 1, 0x76df7b888c458b42b60b23cd498facf0, N'Lima_SAA', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (16, 1, 0xa612d66270a41a43b13d78ef31489da2, N'Lima_SAD', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (17, 1, 0x19fec26d731bb649a6637edde4c9aab1, N'Santa_Caja', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (18, 1, 0x4ad200b02a9b26478f59b03e317f22c6, N'Santa_DireccionSede', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (19, 1, 0xc768134fd15c624cac0db22efa5a5cc8, N'Santa_JefaturaAcademica', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (20, 1, 0x90ddc78b91c27841bb87de21992b71e3, N'Santa_JefaturaCobranza', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (21, 1, 0xf0f921062f4a3a448a892453c274fc1d, N'Santa_SAA', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (22, 1, 0x623d4594f4d2a64596af0e508d654a41, N'Santa_SAD', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (23, 1, 0x66a848dd57986e42a68c9dbf57ee20c2, N'Secretaria_General', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (24, 1, 0xe66e2fc97f5a36419f966dc42a8871ee, N'Secretaria_General_Asistentes', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (25, 1, 0xdd456a8138381545812eca1a4ff7cc23, N'SJL_Caja', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (26, 1, 0xed0351b14ba798428b572e77fac89593, N'SJL_DireccionSede', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (27, 1, 0xff9b7493fc3c1f41b8a0c17faeb81e9f, N'SJL_JefaturaAcadémica', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (28, 1, 0xc816dfd3bb30034787d762b49edb7366, N'SJL_JefaturaCobranza', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (29, 1, 0x67fe366bde7b0449b2bd7cb8779ab5ab, N'SJL_SAA', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (30, 1, 0x071fc044853c84428037a65dd6ccb4e3, N'SJL_SAD', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (31, 1, 0xb7facd3efd2f5648883e8a2539148b8c, N'SJM_JefaturaAcademica', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (32, 1, 0xbb3f775aea8b264da04c69e391f005bc, N'Surco_Caja', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (33, 1, 0xe39c80397f3cd54caed247a5019bf360, N'Surco_JefaturaAcademica', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (34, 1, 0xfc54c5368746054a937f62de5e23d807, N'Surco_JefaturaCobranza', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (35, 1, 0x8d7d2f7dc3b7454c87d7eb60204c6592, N'Surco_SAA', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (36, 1, 0x37abf7e26d409e489ff705a092a97453, N'Surco_SAD', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (37, 1, 0x91cb0bdc37694a4585950677007389ee, N'TIERP_Desarrollo', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (38, 1, 0x510106fde1efe141a455d2e332e29620, N'TIERP_Proyectos', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (39, 1, 0x8cbb891154b337489045337f13e24d88, N'Todo_Academico', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (40, 1, 0x2dc6d2effb068146ba465d2492c049f9, N'Todo_ADMIN', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (41, 1, 0x785591600b371b439b5ce0123330dee9, N'Todo_Caja', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (42, 1, 0xf884e5815dc0ad478f966843c8f5054f, N'Todo_DireccionAcademica', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (43, 1, 0x150f6f8f1f625c4899b9fb687498e875, N'Todo_Finanzas', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (44, 1, 0xea4402b69f08af49ad8ac97de826fd8b, N'Todo_JefaturaCobranza', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (45, 1, 0x8c16a994b6faeb4fb6564069ec6d8617, N'Todo_Marketing', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (46, 1, 0xbb5aa527232f3042b59d1fedb76803a6, N'Todo_ProcesosYTecnologia', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (47, 1, 0xf5b8eb55773fab4dad4b45bb99c8463d, N'Todo_RRHH', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (48, 1, 0xa486e3bf19524a4085fc151ffb4d83da, N'Todo_SecretariaGeneral', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (49, 1, 0x157146fd31bf094487c18c0d8457a3e5, N'Todo_SISE', N'Todos los grupos y usuarios de SISE', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (50, 1, 0x38449d88da5a81458bf7f47158b23c2d, N'Todo_SoporteSedes', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (51, 1, 0xf9468e74c683ce47b8247d6ed25c8995, N'Trujillo_JefaturaAcademica', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (52, 1, 0x9d8b3187ff06ce48bec94280ed584223, N'Trujillo_SAA', N'', N'', 0)
INSERT INTO [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId], [StoreId], [objectSid], [Name], [Description], [LDapQuery], [GroupType]) VALUES (53, 1, 0x69236d11df6dd34eaf22635df1b7762c, N'Trujillo_SAD', N'', N'', 0)
SET IDENTITY_INSERT [dbo].[netsqlazman_StoreGroupsTable] OFF

PRINT(N'Add constraints to [dbo].[netsqlazman_StoreGroupsTable]')
ALTER TABLE [dbo].[netsqlazman_StoreGroupsTable] WITH NOCHECK ADD CONSTRAINT [FK_StoreGroups_Stores] FOREIGN KEY ([StoreId]) REFERENCES [dbo].[netsqlazman_StoresTable] ([StoreId]) ON DELETE CASCADE

PRINT(N'Add constraint FK_StoreGroupMembers_StoreGroup to [dbo].[netsqlazman_StoreGroupMembersTable]')
ALTER TABLE [dbo].[netsqlazman_StoreGroupMembersTable] WITH NOCHECK ADD CONSTRAINT [FK_StoreGroupMembers_StoreGroup] FOREIGN KEY ([StoreGroupId]) REFERENCES [dbo].[netsqlazman_StoreGroupsTable] ([StoreGroupId]) ON DELETE CASCADE
COMMIT TRANSACTION
GO
