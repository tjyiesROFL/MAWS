using MAWS.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MAWS.Services
{
    public class QueryService
    {
        private readonly ApplicationDbContext _db;

        public QueryService(ApplicationDbContext db)
        {
            _db = db;
        }


    }
}
