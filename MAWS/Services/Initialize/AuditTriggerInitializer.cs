using MAWS.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MAWS.Services
{

    /// <summary>
    /// 
    /// Class initializes Database trigger functions & Audtitble values
    ///  
    /// 
    /// Jack
    /// 
    /// </summary>

    public class AuditTriggerInitializer
    {

        private readonly ApplicationDbContext _db;

        public AuditTriggerInitializer(ApplicationDbContext db)
        {

            _db = db;

        }

        public bool IsAuditTriggersActive()
        {
            if (_db.AuditTableStatus.Find(true) == null)
            {
                return false;
            }
            else 
            {
                return true;
            }
        }

        public async void InitializeAuditTriggers()
        {

            if (IsAuditTriggersActive())
            {

                InitializeAuditTriggers();
                _db.AuditTableStatus.Find(true).IsActive = true;
                await _db.SaveChangesAsync();

            }
            else { Console.WriteLine("Audit Trigger Status: Active"); }

        }

        public string GenerateOnCreateTriggerQuery(String tableName)
        {

            string onCreate = "CREATE FUNCTION " +
                tableName +
                "CreateAuditLog() RETURNS trigger AS $" +
                tableName +
                "Creation$\nBEGIN\nNEW.\"Create_DateTime\" := current_timestamp;" +
                "\nRETURN NEW;\nEND;\n$" +
                tableName +
                "Creation$ LANGUAGE plpgsql;\nCREATE TRIGGER " +
                tableName +
                "Creation BEFORE INSERT ON public.\"" +
                tableName +
                "\"" + "\nFOR EACH ROW EXECUTE PROCEDURE " +
                tableName +
                "CreateAuditLog();";

            return onCreate;
        }

        public string GenerateOnUpdateTriggerQuery(String tableName)
        {

            string onUpdate = "CREATE FUNCTION " +
                tableName +
                "UpdateAuditLog() RETURNS trigger AS $" +
                tableName +
                "Updation$\nBEGIN\nNEW.\"Update_DateTime\" := current_timestamp;" +
                "\nRETURN NEW;\nEND;\n$" +
                tableName +
                "Updation$ LANGUAGE plpgsql;\nCREATE TRIGGER " +
                tableName +
                "Updation BEFORE UPDATE ON public.\"" +
                tableName +
                "\"" + "\nFOR EACH ROW EXECUTE PROCEDURE " +
                tableName +
                "UpdateAuditLog();";


            return onUpdate;
        }

        public string GenerateOnUpdateDeleteAudit(String tableName)
        {
            string onUpdateDelete =
            "CREATE OR REPLACE FUNCTION process_" + tableName + "_audit() RETURNS TRIGGER AS $" + tableName + "_audit$\n" +
             "BEGIN\n" +
            "IF (TG_OP = 'DELETE') THEN \n" +
            "INSERT INTO \"" + tableName + "_audit\" SELECT current_timestamp, 'D',OLD.*; \n" +
            "ELSIF (TG_OP = 'UPDATE') THEN \n" +
            "INSERT INTO \"" + tableName + "_audit\" SELECT current_timestamp,'U',NEW.*;\n" +
            "END IF;\n" +
            "RETURN NULL;\n" +
             "END;\n" +
            "$" + tableName + "_audit$ LANGUAGE plpgsql;\n\n" +

            "CREATE TRIGGER " + tableName + "_audit\n" +
            "AFTER INSERT OR UPDATE OR DELETE ON \"" + tableName + "\"\n" +
            "FOR EACH ROW EXECUTE PROCEDURE process_" + tableName + "_audit();\n";

            return onUpdateDelete;
        }

        public void ExecuteAuditTriggerSQL()
        {

            //Trigger audits
            _db.Database.ExecuteSqlRaw(GenerateOnUpdateDeleteAudit("Research"));
            _db.Database.ExecuteSqlRaw(GenerateOnUpdateDeleteAudit("MiscTeachingActivity"));
            _db.Database.ExecuteSqlRaw(GenerateOnUpdateDeleteAudit("Service"));
            _db.Database.ExecuteSqlRaw(GenerateOnUpdateDeleteAudit("Supervision"));
            _db.Database.ExecuteSqlRaw(GenerateOnUpdateDeleteAudit("TeachingActivityAssignment"));


            // Trigger Values
            _db.Database.ExecuteSqlRaw(GenerateOnCreateTriggerQuery("AcademicStaff"));
            _db.Database.ExecuteSqlRaw(GenerateOnUpdateTriggerQuery("AcademicStaff"));

            _db.Database.ExecuteSqlRaw(GenerateOnCreateTriggerQuery("Research"));
            _db.Database.ExecuteSqlRaw(GenerateOnUpdateTriggerQuery("Research"));

            _db.Database.ExecuteSqlRaw(GenerateOnCreateTriggerQuery("Service"));
            _db.Database.ExecuteSqlRaw(GenerateOnUpdateTriggerQuery("Service"));

            _db.Database.ExecuteSqlRaw(GenerateOnCreateTriggerQuery("Supervision"));
            _db.Database.ExecuteSqlRaw(GenerateOnUpdateTriggerQuery("Supervision"));

            _db.Database.ExecuteSqlRaw(GenerateOnCreateTriggerQuery("Discipline"));
            _db.Database.ExecuteSqlRaw(GenerateOnUpdateTriggerQuery("Discipline"));

            _db.Database.ExecuteSqlRaw(GenerateOnCreateTriggerQuery("Unit"));
            _db.Database.ExecuteSqlRaw(GenerateOnUpdateTriggerQuery("Unit"));

            _db.Database.ExecuteSqlRaw(GenerateOnCreateTriggerQuery("UnitOffering"));
            _db.Database.ExecuteSqlRaw(GenerateOnUpdateTriggerQuery("UnitOffering"));

            _db.Database.ExecuteSqlRaw(GenerateOnCreateTriggerQuery("TeachingPattern"));
            _db.Database.ExecuteSqlRaw(GenerateOnUpdateTriggerQuery("TeachingPattern"));

            _db.Database.ExecuteSqlRaw(GenerateOnCreateTriggerQuery("TeachingActivity"));
            _db.Database.ExecuteSqlRaw(GenerateOnUpdateTriggerQuery("TeachingActivity"));

            _db.Database.ExecuteSqlRaw(GenerateOnCreateTriggerQuery("MiscTeachingActivity"));
            _db.Database.ExecuteSqlRaw(GenerateOnUpdateTriggerQuery("MiscTeachingActivity"));


        }
    }
}
