using MAWS.Models;
using MAWS.IntermediateData;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MAWS.Services
{
    public class QueryTeachingActivities
    {
        private readonly ApplicationDbContext _db;

        public QueryTeachingActivities(ApplicationDbContext db)
        {
            _db = db;
        }

        




    }
}
