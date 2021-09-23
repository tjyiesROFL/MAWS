//using System;
//using MAWS.Models;
//using MAWS.IntermediateData;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Claims;
//using CsvHelper;
//using System.Threading.Tasks;

//namespace MAWS.Services
//{
//    public class QueryUnits
//    {
//        private readonly ApplicationDbContext _db;

//        public QueryUnits(ApplicationDbContext db)
//        {
//            _db = db;
//        }

//        public async Task<List<IntermediateUnit>> GetUnitsAsync()
//        {

//            List<IntermediateUnit> _unitList = new List<IntermediateUnit>();

//            foreach (var unit in await _db.Unit.ToListAsync())
//            {
//                IntermediateUnit tempUnit = new IntermediateUnit(unit);
//                _unitList.Add(tempUnit);
//            }

//            return _unitList;

//        }

//        public async Task<List<Unit>> GetUnitWithUnitOfferingsAsync()
//        {

//            List<Unit> _UnitList = await _db.Unit.ToListAsync();

//            foreach (var entry in _UnitList)
//            {
//                await _db.Entry(entry)
//                    .Collection(u => u.UnitOfferingList)
//                    .LoadAsync();
//            }
//            return _UnitList;
//        }
//    }
//}
