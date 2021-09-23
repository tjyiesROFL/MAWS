using System;
using MAWS.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using CsvHelper;
using System.Threading.Tasks;

namespace MAWS.Services
{
    public class QueryDiscipline
    {
        private readonly ApplicationDbContext _db;

        public QueryDiscipline(ApplicationDbContext db)
        {
            _db = db;
        }

    }
}
