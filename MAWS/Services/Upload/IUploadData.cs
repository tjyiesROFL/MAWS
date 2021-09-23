using System;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using MAWS.Models;

namespace MAWS.Services.UploadData
{
    public interface IUploadData
    {
        public string ClassName { get; }
        public Task Upload(MemoryStream ms);
        //public void ReadFieldsFromCsv();
        //public bool IsValid();
    }
}
