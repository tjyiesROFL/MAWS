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
    public class UploadResearch : IUploadData
    {
        public string ClassName => nameof(UploadResearch);
        private ApplicationDbContext _db { get; set; }
        private CsvReader csv;
        private List<Tuple<Research, string>> _researchTupleList = new List<Tuple<Research, string>>();


        public UploadResearch(ApplicationDbContext dbContext)
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


        private Tuple<Research,string> ReadFieldsFromCsv(CsvReader csv)
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
