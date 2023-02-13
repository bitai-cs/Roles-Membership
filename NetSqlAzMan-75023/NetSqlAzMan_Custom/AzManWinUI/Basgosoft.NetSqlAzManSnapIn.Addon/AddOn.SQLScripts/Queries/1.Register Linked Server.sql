/*************************************************************************************/
/* ATTENTION: REMEMBER TO CREATE A DATABASE FIRST (Tipical: NetSqlAzManStorage) !!!  */
/*            THIS SCRIPT DOES NOT CREATE DATABASE !!!!                              */
/*************************************************************************************/
use [Master];

/** ADD ADSI LINKED SERVER PROVIDER **/
    -- CHECK IF SERVER ALREADY EXISTS
IF NOT EXISTS ( SELECT  *
                FROM    master.dbo.sysservers
                WHERE   srvname = 'ADSI' ) 
   BEGIN
      EXEC sp_addlinkedserver 'ADSI', 'Active Directory Service Interfaces', 'ADSDSOObject', 'adsdatasource'
     /** REMEMBER: change security context credentials for this linked server to allow ADSI provider to estabilish a connection with your DOMAIN **/
   END
GO