USE [msdb]
GO

BEGIN TRANSACTION
DECLARE @ReturnCode INT
SELECT   @ReturnCode = 0
IF NOT EXISTS ( SELECT  name
                FROM    msdb.dbo.syscategories
                WHERE   name = N'Proceso de cambio de estado'
                        AND category_class = 1 )
   BEGIN
      EXEC @ReturnCode = msdb.dbo.sp_add_category @class = N'JOB',
         @type = N'LOCAL', @name = N'Proceso de cambio de estado'
      IF (@@ERROR <> 0
          OR @ReturnCode <> 0)
         GOTO QuitWithRollback

   END

DECLARE @jobId BINARY(16)
EXEC @ReturnCode = msdb.dbo.sp_add_job @job_name = N'LOGIN_job_SetExpiredSessions',
   @enabled = 1, @notify_level_eventlog = 2, @notify_level_email = 2,
   @notify_level_netsend = 0, @notify_level_page = 0, @delete_level = 0,
   @description = N'Verifica la vigencia de las sesiones y las marca como expirada por inactividad después de vencer el tiepo máximo de la sesión.',
   @category_name = N'Proceso de cambio de estado', @owner_login_name = N'sa',
   @notify_email_operator_name = N'SECMAN_OPERATOR', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0
    OR @ReturnCode <> 0)
   GOTO QuitWithRollback

EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id = @jobId,
   @step_name = N'LOGIN_step_SetExpiredLogins', @step_id = 1,
   @cmdexec_success_code = 0, @on_success_action = 1, @on_success_step_id = 0,
   @on_fail_action = 2, @on_fail_step_id = 0, @retry_attempts = 0,
   @retry_interval = 0, @os_run_priority = 0, @subsystem = N'TSQL',
   @command = N'EXECUTE [Basgosoft].[Login_pa_SetExpiredLogins];',
   @database_name = N'AzMan', @flags = 0
IF (@@ERROR <> 0
    OR @ReturnCode <> 0)
   GOTO QuitWithRollback

EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0
    OR @ReturnCode <> 0)
   GOTO QuitWithRollback

EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id = @jobId,
   @name = N'LOGIN_schd_CadaMinutoDelDia', @enabled = 1, @freq_type = 4,
   @freq_interval = 1, @freq_subday_type = 4, @freq_subday_interval = 1,
   @freq_relative_interval = 0, @freq_recurrence_factor = 0,
   @active_start_date = 20130821, @active_end_date = 99991231,
   @active_start_time = 0, @active_end_time = 235959,
   @schedule_uid = N'6d154b2e-958f-420d-8871-3bb0d9b07cb3'
IF (@@ERROR <> 0
    OR @ReturnCode <> 0)
   GOTO QuitWithRollback

EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId,
   @server_name = N'(local)'
IF (@@ERROR <> 0
    OR @ReturnCode <> 0)
   GOTO QuitWithRollback

COMMIT TRANSACTION

GOTO EndSave

QuitWithRollback:
IF (@@TRANCOUNT > 0)
   ROLLBACK TRANSACTION
EndSave:
GO