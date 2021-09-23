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
    public class UploadUnit :IUploadData
    {
        public string ClassName => nameof(UploadUnit);

        private ApplicationDbContext _db { get; set; }
        private CsvReader csv;
        private List<Tuple<Unit, string>> _unitTupleList = new List<Tuple<Unit, string>>();

        public UploadUnit(ApplicationDbContext dbContext)
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
                        var record = ReadFieldsFromCsv();
                        if (IsUnitValid(record.Item1))
                        {
                            _unitTupleList.Add(record);
                        }
                    }
                }
            }
            await AddUnitListAsync();
        }

        private bool IsUnitValid(Unit _unit)
        {

            if (_unit.UnitCode.Length > 12) { return false; }
            if (_unit.UnitName.Length > 255) { return false; }
            if (_unit.Area.Length > 6) { return false; }
            if (_unit.Tier.ToString().Length > 1) { return false; }
            if (_unit.UCMTierBaseHrs.ToString().Length > 6) { return false; }
            if (_unit.CreditPoints.ToString().Length > 2) { return false; }
            if (_unit.CreditPointsRatio.ToString().Length > 1) { return false; }
            //if (_unit.ActiveFlag) { return false; }
            //if (_unit.ProjectFlag) { return false; }
            //if (_unit.PU_BaseHrsExtraFlag) { return false; }
            //if (_unit.PU_OtherTeachingFlag) { return false; }
            //if (_unit.ClientFlag) { return false; }
            //if (_unit.ExamFlag) { return false; }
            //if (_unit.LabFlag) { return false; }
            //if (_unit.FieldworkFlag) { return false; }


            //if (!_db.Unit.Any(o => o.UnitCode == record.UnitCode)) { unitList.Add(record); }

            return true;
        }

        private Tuple<Unit, string> ReadFieldsFromCsv()
        {

            Unit unit = new Unit();
            try
            {
                var disciplineID = csv.GetField("DisciplineID");
                unit.UnitCode = csv.GetField("UnitCode");
                unit.UnitName = csv.GetField("UnitName");
                unit.Area = csv.GetField("Area");
                unit.ActiveFlag = bool.Parse(csv.GetField("ActiveFlag"));
                unit.ProjectFlag = bool.Parse(csv.GetField("ProjectFlag"));
                unit.PU_BaseHrsExtraFlag = bool.Parse(csv.GetField("PU_BaseHrsExtraFlag"));
                unit.PU_OtherTeachingFlag = bool.Parse(csv.GetField("PU_OtherTeachingFlag"));
                unit.ClientFlag = bool.Parse(csv.GetField("ClientFlag"));
                unit.ExamFlag = bool.Parse(csv.GetField("ExamFlag"));
                unit.LabFlag = bool.Parse(csv.GetField("LabFlag"));
                unit.FieldworkFlag = bool.Parse(csv.GetField("FieldworkFlag"));
                unit.Tier = int.Parse(csv.GetField("Tier"));
                unit.UCMTierBaseHrs = double.Parse(csv.GetField("UCMTierBaseHrs"));
                unit.CreditPoints = int.Parse(csv.GetField("CreditPoints"));
                unit.CreditPointsRatio = double.Parse(csv.GetField("CreditPointsRatio"));
                return new Tuple<Unit, string>(unit, disciplineID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CSV Reader Error has occured: " + ex.Data["CsvHelper"]);
                return null;
            }
        }

        private async Task AddUnitListAsync()
        {
            foreach (var record in _unitTupleList)
            {
                Discipline discipline = await _db.Discipline.Where(b => b.DisciplineID == record.Item2).FirstOrDefaultAsync();
                if (discipline.UnitList == null)
                {
                    discipline.UnitList = new List<Unit>();
                }
                discipline.UnitList.Add(record.Item1);
            }

            try {
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }
    }
}
