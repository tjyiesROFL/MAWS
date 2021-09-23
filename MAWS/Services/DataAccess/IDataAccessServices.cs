using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MAWS.Services.DataAccess
{
    public interface IDataAccessServices<T>
    {
        public string ClassName { get; }
        public Task UploadAsync(MemoryStream ms);
        public Task<List<T>> GetListAsync();
        public Task<bool> AddAsync(T data);
        public Task RemoveAsync(T data);

        //remove?
        public Task<List<string>> GetIdListAsync();
    }
}
