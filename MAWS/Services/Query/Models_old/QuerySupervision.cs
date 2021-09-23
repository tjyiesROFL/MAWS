using MAWS.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MAWS.Services
{
    public class QuerySupervision
    {
        private readonly ApplicationDbContext _db;

        public QuerySupervision(ApplicationDbContext db)
        {
            _db = db;
        }


    }
}
