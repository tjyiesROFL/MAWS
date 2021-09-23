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
    public class AcademicResearchService : IDataAccessServices<IntermediateResearch> 
    {
        public string ClassName => nameof(AcademicResearchService);
        private ApplicationDbContext _db { get; set; }
        private CsvReader csv;
        private List<Tuple<Research, string>> _researchTupleList = new List<Tuple<Research, string>>();


        public AcademicResearchService(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        
        public async Task<List<IntermediateResearch>> GetListAsync()
        {
            List<IntermediateResearch> intrResearchList = new List<IntermediateResearch>();
            var res = await _db.AcademicStaff.Include(a => a.ReasearchList).ToListAsync();

            foreach (var staff in res)
            {
                if (staff != null)
                {
                    foreach (var entry in staff.ReasearchList)
                    {
                        if (staff != null)
                        {
                            var intrResearch = new IntermediateResearch();

                            intrResearch.ResearchID = entry.ResearchID;
                            intrResearch.AcademicStaffID = staff.AcademicStaffID;
                            intrResearch.FirstName = staff.FirstName;
                            intrResearch.Surname = staff.Surname;
                            intrResearch.Year = entry.Year;
                            intrResearch.ECR_Pc = entry.ECR_Pc;
                            intrResearch.Income_Pc = entry.Income_Pc;
                            intrResearch.Completions_Pc = entry.Completions_Pc;
                            intrResearch.Pubs_Pc = entry.Pubs_Pc;
                            intrResearch.RCI_Pc = entry.RCI_Pc;
                            intrResearch.Discretionary_Pc = entry.Discretionary_Pc ?? 0;
                            intrResearch.Percentage = entry.Percentage ?? 0;

                            intrResearchList.Add(intrResearch);
                        }
                    }
                }
            }
            return intrResearchList; 
        }

        // GetAsync if needed?

        public async Task<bool> AddAsync(IntermediateResearch intermediateResearch)
        {
            var research = await _db.Research
                .Where(r => r.ResearchID == intermediateResearch.ResearchID)
                .FirstOrDefaultAsync();
            
            if (research != null)
            {
                research.Completions_Pc = intermediateResearch.Completions_Pc;
                research.Discretionary_Pc = intermediateResearch.Discretionary_Pc;
                research.ECR_Pc = intermediateResearch.ECR_Pc;
                //research.Fifteen_Pc = intermediateResearch.
                research.Income_Pc = intermediateResearch.Income_Pc;
                research.Pubs_Pc = intermediateResearch.Pubs_Pc;
                research.RCI_Pc = intermediateResearch.RCI_Pc;
                research.Percentage = intermediateResearch.Percentage;

                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task RemoveAsync(IntermediateResearch intermediateResearch)
        {
            var research = await _db.Research
                .Where(r => r.ResearchID == intermediateResearch.ResearchID)
                .FirstOrDefaultAsync();

            _db.Research.Remove(research);

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
                    await csv.ReadAsync();
                    csv.ReadHeader();
                    while (await csv.ReadAsync())
                    {
                        var record = ReadFieldsFromCsv(csv);
                        //if (IsResearchValid(record.Item1))
                        //{
                        _researchTupleList.Add(record);
                        //}
                    }
                }
            }
            await AddResearchListAsync();
        }



        private bool IsResearchValid(Research _research)
        {

            if (_research.Year.ToString().Length > 4) { return false; }
            if (_research.Fifteen_Pc.ToString().Length > 4) { return false; }
            if (_research.ECR_Pc.ToString().Length > 4) { return false; }
            if (_research.Income_Pc.ToString().Length > 4) { return false; }
            if (_research.Completions_Pc.ToString().Length > 4) { return false; }
            if (_research.Pubs_Pc.ToString().Length > 4) { return false; }
            if (_research.RCI_Pc.ToString().Length > 4) { return false; }

            //if (_research.Discretionary_Pc.ToString().Length > 4) { return false; } //set 0 as default value
            //if (_research.Discretionary_Comments.Length > 255) { return false; }
            //if (_research.Percentage.ToString().Length > 4) { return false; } //set 0 as default value
            //if (_research.IS_CURRENT ) { return false; }

            return true;
        }


        private Tuple<Research, string> ReadFieldsFromCsv(CsvReader csv)
        {
            Research research = new Research();
            string AcademicStaffID;
            try
            {
                AcademicStaffID = csv.GetField("StaffID");
                research.AcademicStaffID = csv.GetField("StaffID");
                research.Year = int.Parse(csv.GetField("Year"));
                research.Fifteen_Pc = double.Parse(csv.GetField("15-PC"));
                research.ECR_Pc = double.Parse(csv.GetField("ECR-PC"));
                research.Income_Pc = double.Parse(csv.GetField("Income-PC"));
                research.Completions_Pc = double.Parse(csv.GetField("Completions-PC"));
                research.Pubs_Pc = double.Parse(csv.GetField("Pubs-PC"));
                research.RCI_Pc = double.Parse(csv.GetField("RCI-PC"));
                research.Discretionary_Comments = csv.GetField("Discretion-Comments");
                research.Discretionary_Pc = double.Parse(csv.GetField("Discretion-PC"));
                research.IS_CURRENT = bool.Parse(csv.GetField("IS_CURRENT"));
                return new Tuple<Research, string>(research, AcademicStaffID);
            }
            catch (Exception ex)
            {
                Console.WriteLine("CSV Reader Error has occured: " + ex.Data["CsvHelper"]);
                return null;
            }
        }

        private async Task AddResearchListAsync()
        {
            //find AND retrieve AcademicStaff Object with related AcademicStaffID
            //initialize the Research list if it has not yet already been initialized
            //Add the Research entry to the list 
            //Save context after all entries in research list added

            foreach (var record in _researchTupleList)
            {

                AcademicStaff academicStaff = await _db.AcademicStaff.Where(b => b.AcademicStaffID == record.Item2).FirstOrDefaultAsync();
                if (academicStaff != null)
                {
                    if (academicStaff.ReasearchList == null)
                    {
                        academicStaff.ReasearchList = new List<Research>();
                    }
                    academicStaff.ReasearchList.Add(record.Item1);
                }
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
