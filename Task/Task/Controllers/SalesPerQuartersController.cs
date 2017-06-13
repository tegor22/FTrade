using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Task.Models;

namespace Task.Controllers
{
    public class SalesPerQuartersController : ApiController
    {
        // GET: api/SalesPerQuarters
        public IQueryable<StructQuarters> Get(DateTime StartDate, DateTime EndDate)
        {
            int startYear = StartDate.Year;
            int endYear = EndDate.Year;
            DatabaseTaskEntities context = new DatabaseTaskEntities();
            var result = context.Sales.Select(i => new Sale() { Id = i.Id, Price = i.Price, Date = i.Date })
                .Where(i => i.Date >= StartDate && i.Date <= EndDate);
            var fimleResult = result.GroupBy(i => new { QuarterNumber = SqlFunctions.DatePart("quarter", i.Date), YearNumber = SqlFunctions.DatePart("year", i.Date) })
                .Select(grp => new StructQuarters() { NumberQuarter = grp.Key.QuarterNumber, NumberYear = grp.Key.YearNumber, Price = grp.Sum(i => i.Price), CountPrice = grp.Count() });
            return fimleResult;
        }

        // GET: api/SalesPerQuarters/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SalesPerQuarters
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SalesPerQuarters/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SalesPerQuarters/5
        public void Delete(int id)
        {
        }
    }
}
