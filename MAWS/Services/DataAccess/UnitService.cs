using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using MAWS.IntermediateData;
using MAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace MAWS.Services.DataAccess
{
    // not sure if its a best practice to have other classes int IDataAccessServices<T> ?
    public class UnitService : IDataAccessServices<IntermediateUnit>
    {
        public string ClassName => nameof(UnitService);
        private ApplicationDbContext _db { get; set; }
        private CsvReader csv;
        private List<Tuple<Unit, string>> _unitTupleList = new List<Tuple<Unit, string>>();


        public UnitService(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }


        public async Task<List<IntermediateUnit>> GetListAsync()
        {
            List<IntermediateUnit> intrUnitList = new List<IntermediateUnit>();

            foreach (var unit in await _db.Unit.ToListAsync())
            {
                if (unit != null)
                {

                    var intrUnit = new IntermediateUnit();

                    intrUnit.UnitCode = unit.UnitCode;
                    intrUnit.UnitName = unit.UnitName;
                    intrUnit.Area = unit.Area;
                    intrUnit.PU_BaseHrsExtraFlag = unit.PU_BaseHrsExtraFlag;
                    intrUnit.PU_OtherTeachingFlag = unit.PU_OtherTeachingFlag;
                    intrUnit.ClientFlag = unit.ClientFlag;
                    intrUnit.ExamFlag = unit.ExamFlag;
                    intrUnit.LabFlag = unit.LabFlag;
                    intrUnit.FieldworkFlag = unit.FieldworkFlag;
                    intrUnit.Tier = unit.Tier;
                    intrUnit.UCMTierBaseHrs = unit.UCMTierBaseHrs;
                    intrUnit.ActiveFlag = unit.ActiveFlag;
                    intrUnit.ProjectFlag = unit.ProjectFlag;
                    intrUnit.CreditPoints = unit.CreditPoints;
                    intrUnit.CreditPointsRatio = unit.CreditPointsRatio;
                    intrUnit.TeachingPattern = unit.TeachingPattern;

                    intrUnitList.Add(intrUnit);
                        
                
                }
            }
            return intrUnitList;
        }

        // GetAsync if needed?

        public async Task<bool> AddAsync(IntermediateUnit intermediateUnit)
        {

            if(_db.Unit.Find(intermediateUnit.UnitCode)!= null)
            {

                var unit = await _db.Unit
                    .Where(r => r.UnitCode == intermediateUnit.UnitCode)
                    .FirstOrDefaultAsync();

                unit.UnitCode = intermediateUnit.UnitCode;
                unit.UnitName = intermediateUnit.UnitName;
                unit.Area = intermediateUnit.Area;
                unit.PU_BaseHrsExtraFlag = intermediateUnit.PU_BaseHrsExtraFlag;
                unit.PU_OtherTeachingFlag = intermediateUnit.PU_OtherTeachingFlag;
                unit.ClientFlag = intermediateUnit.ClientFlag;
                unit.ExamFlag = intermediateUnit.ExamFlag;
                unit.LabFlag = intermediateUnit.LabFlag;
                unit.FieldworkFlag = intermediateUnit.FieldworkFlag;
                unit.Tier = intermediateUnit.Tier;
                unit.UCMTierBaseHrs = intermediateUnit.UCMTierBaseHrs;
                unit.ActiveFlag = intermediateUnit.ActiveFlag;
                unit.ProjectFlag = intermediateUnit.ProjectFlag;
                unit.CreditPoints = intermediateUnit.CreditPoints;
                unit.CreditPointsRatio = intermediateUnit.CreditPointsRatio;
                unit.TeachingPattern = intermediateUnit.TeachingPattern;

                await _db.SaveChangesAsync();
                return true;
            }
            else
            {
                var disc = await _db.Discipline.FirstOrDefaultAsync();

                var unit = new Unit();

                unit.UnitCode = intermediateUnit.UnitCode;
                unit.UnitName = intermediateUnit.UnitName;
                unit.Area = intermediateUnit.Area;
                unit.PU_BaseHrsExtraFlag = intermediateUnit.PU_BaseHrsExtraFlag;
                unit.PU_OtherTeachingFlag = intermediateUnit.PU_OtherTeachingFlag;
                unit.ClientFlag = intermediateUnit.ClientFlag;
                unit.ExamFlag = intermediateUnit.ExamFlag;
                unit.LabFlag = intermediateUnit.LabFlag;
                unit.FieldworkFlag = intermediateUnit.FieldworkFlag;
                unit.Tier = intermediateUnit.Tier;
                unit.UCMTierBaseHrs = intermediateUnit.UCMTierBaseHrs;
                unit.ActiveFlag = intermediateUnit.ActiveFlag;
                unit.ProjectFlag = intermediateUnit.ProjectFlag;
                unit.CreditPoints = intermediateUnit.CreditPoints;
                unit.CreditPointsRatio = intermediateUnit.CreditPointsRatio;
                unit.TeachingPattern = intermediateUnit.TeachingPattern;

                disc.UnitList.Add(unit);
                return true;
            }
        }

        public async Task RemoveAsync(IntermediateUnit intermediateUnit)
        {
            var unit = await _db.Unit
                .Where(r => r.UnitCode == intermediateUnit.UnitCode)
                .FirstOrDefaultAsync();

            _db.Unit.Remove(unit);

            await _db.SaveChangesAsync();
        }

        //should move in academicstaffservice
        public async Task<List<string>> GetIdListAsync()
        {
            return await _db.AcademicStaff.Select(a => a.AcademicStaffID).ToListAsync();
        }

        public async Task UploadAsync(MemoryStream ms)
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
                unit.CreditPoints = int.Parse(csv.GetField("CreditPoints"));
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

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
        }
    }
}
