using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using MAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace MAWS.Services.UploadData
{
    public class UploadDiscipline : IUploadData
    {
        public string ClassName  => nameof(UploadDiscipline);

        private ApplicationDbContext _db { get; set; }
        private CsvReader csv;
        private List<Tuple<Discipline, string>> _disciplineTupleList = new List<Tuple<Discipline, string>>();
        public UploadDiscipline(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task Upload(MemoryStream ms)
        {
            using (var reader = new System.IO.StreamReader(ms))
            {
                ms.Position = 0;
                using (csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        Tuple<Discipline, string> record = ReadFieldsFromCsv();
                        if(IsDisciplineValid(record.Item1))
                        {
                            _disciplineTupleList.Add(record);
                        }
                    }
                }
            }

            await AddDisciplineListAsync();
        }

        private bool IsDisciplineValid(Discipline _discipline)
        {

            //if (!_db.Discipline.Any(o => o.DisciplineID == record.DisciplineID)) { disciplineList.Add(record); }

            if (_discipline.DisciplineID.Length > 6) { return false; }

            return true;
        }

        private Tuple<Discipline, string> ReadFieldsFromCsv()
        {
            Discipline discipline = new Discipline();
            try
            {
                string HeadOfDiscipline = csv.GetField("HeadOfDiscipline");
                discipline.DisciplineID= csv.GetField("DisciplineID");
                discipline.DisciplineName = csv.GetField("DisciplineName");
                discipline.School = csv.GetField("School");
                discipline.ActiveFlag = bool.Parse(csv.GetField("ActiveFlag"));
                discipline.WOCT_FirstHrsPH = double.Parse(csv.GetField("WOCT_FirstHrsPH"));
                discipline.WOCT_RepeatHRsPH = double.Parse(csv.GetField("WOCT_RepeatHRsPH"));
                discipline.SGT_FirstSessionCount = double.Parse(csv.GetField("SGT_FirstSessionCount"));
                discipline.SGT_FirstHrsPH = double.Parse(csv.GetField("SGT_FirstHrsPH"));
                discipline.SGT_SubsequentHrsPH = double.Parse(csv.GetField("SGT_SubsequentHrsPH"));
                discipline.SGT_MarkingHrsPS = double.Parse(csv.GetField("SGT_MarkingHrsPS"));
                discipline.Marking_ExamHrsPS = double.Parse(csv.GetField("Marking_ExamHrsPS"));
                discipline.OUAE_AttentionHrsPS = double.Parse(csv.GetField("OUAE_AttentionHrsPS"));
                discipline.OUAE_MarkingHrsPS = double.Parse(csv.GetField("OUAE_MarkingHrsPS"));
                discipline.CP_3ptRatio = double.Parse(csv.GetField("CP_3ptRatio"));
                discipline.CP_6ptRatio = double.Parse(csv.GetField("CP_6ptRatio"));
                discipline.CP_9ptRatio = double.Parse(csv.GetField("CP_9ptRatio"));
                discipline.CP_12ptRatio = double.Parse(csv.GetField("CP_12ptRatio"));
                discipline.U_BaseHrs_Tier1 = double.Parse(csv.GetField("U_BaseHrs_Tier1"));
                discipline.U_BaseHrs_Tier2 = double.Parse(csv.GetField("U_BaseHrs_Tier2"));
                discipline.U_BaseHrs_Tier3 = double.Parse(csv.GetField("U_BaseHrs_Tier3"));
                discipline.UCM_ExternalHrs = double.Parse(csv.GetField("UCM_ExternalHrs"));
                discipline.UCM_BaseStudents = double.Parse(csv.GetField("UCM_BaseStudents"));
                discipline.UCM_AdditionalHrsPerStudent = double.Parse(csv.GetField("UCM_AdditionalHrsPerStudent"));
                discipline.UCM_NewUCHrs = double.Parse(csv.GetField("UCM_NewUCHrs"));
                discipline.UCM_UpdateHrs_Minor = double.Parse(csv.GetField("UCM_UpdateHrs_Minor"));
                discipline.UCM_UpdateHrs_Major = double.Parse(csv.GetField("UCM_UpdateHrs_Major"));
                discipline.UCM_DevelopNewUnitBaseHrs = double.Parse(csv.GetField("UCM_DevelopNewUnitBaseHrs"));
                discipline.UCM_DevelopNewUnitDiscretionHrsMax = double.Parse(csv.GetField("UCM_DevelopNewUnitDiscretionHrsMax"));
                discipline.UCM_DevelopNewUnitDigitallyEnhancedHrs = double.Parse(csv.GetField("UCM_DevelopNewUnitDigitallyEnhancedHrs"));
                discipline.PU_BaseHrs = double.Parse(csv.GetField("PU_BaseHrs"));
                discipline.PU_AdditionalClassHrs = double.Parse(csv.GetField("PU_AdditionalClassHrs"));
                discipline.PU_BaseHrsExtra = double.Parse(csv.GetField("PU_BaseHrsExtra"));
                discipline.PU_OtherTeaching= double.Parse(csv.GetField("PU_OtherTeaching"));
                discipline.PU_SupervisorHrsPP= double.Parse(csv.GetField("PU_SupervisorHrsPP"));
                discipline.PU_StaffAsClientHrs= double.Parse(csv.GetField("PU_StaffAsClientHrs"));
                discipline.PU_StaffAsClientHrsTNE= double.Parse(csv.GetField("PU_StaffAsClientHrsTNE"));
                discipline.UCM_TNE_SetupHrs= double.Parse(csv.GetField("UCM_TNE_SetupHrs"));
                discipline.UCM_TNE_CMBaseHrs= double.Parse(csv.GetField("UCM_TNE_CMBaseHrs"));
                discipline.UCM_TNE_CMBaseStudents= double.Parse(csv.GetField("UCM_TNE_CMBaseStudents"));
                discipline.UCM_TNE_CMBaseAffiliates= double.Parse(csv.GetField("UCM_TNE_CMBaseAffiliates"));
                discipline.UCM_TNE_CMAdditionalHrsPerAffiliate= double.Parse(csv.GetField("UCM_TNE_CMAdditionalHrsPerAffiliate"));







                return new Tuple<Discipline, string>(discipline, HeadOfDiscipline);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CSV Reader Error has occured: " + ex.Data["CsvHelper"]);
                return null;
            }
        }

        private async Task AddDisciplineListAsync()
        {

            foreach (var record in _disciplineTupleList)
            {
                AcademicStaff headOfDiscipline = await _db.AcademicStaff.Where(b => b.AcademicStaffID == record.Item2).FirstOrDefaultAsync();

                if (headOfDiscipline.Discipline == null)
                {
                    headOfDiscipline.Discipline = new Discipline();
                }
                headOfDiscipline.Discipline = record.Item1;
            }

            try { await _db.SaveChangesAsync(); }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }
    }
}
