﻿ALTER TABLE [dbo].[netsqlazman_Settings] ADD CONSTRAINT [CK_Settings] CHECK (([SettingName] = 'Mode' and ([SettingValue] = 'Developer' or [SettingValue] = 'Administrator') or [SettingName] = 'LogErrors' and ([SettingValue] = 'True' or [SettingValue] = 'False') or [SettingName] = 'LogWarnings' and ([SettingValue] = 'True' or [SettingValue] = 'False') or [SettingName] = 'LogInformations' and ([SettingValue] = 'True' or [SettingValue] = 'False') or [SettingName] = 'LogOnEventLog' and ([SettingValue] = 'True' or [SettingValue] = 'False') or [SettingName] = 'LogOnDb' and ([SettingValue] = 'True' or [SettingValue] = 'False')))


