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
    public class SalesPerMonthController : ApiController
    {
        // GET: api/SalesPerMonth
        public IQueryable<StructMonth> Get(DateTime StartDate, DateTime EndDate)
        {
            int StartYear = StartDate.Year;
            int EndYear = EndDate.Year;
            int StartMonth = StartDate.Month;
            int EndMonth = EndDate.Month;
            DatabaseTaskEntities context = new DatabaseTaskEntities();
            var result = context.Sales.Select(i => new Sale() { Id = i.Id, Price = i.Price, Date = i.Date })
                .Where(i => i.Date >= StartDate && i.Date <= EndDate);
            var fimleResult = result.GroupBy(i => new { MonthNumber = SqlFunctions.DatePart("month", i.Date), YearNumber = SqlFunctions.DatePart("year", i.Date) })
                .Select(grp => new StructMonth() { NumberMonth = grp.Key.MonthNumber, NumberYear = grp.Key.YearNumber, Price = grp.Sum(i => i.Price), CountPrice = grp.Count() });
            return fimleResult;
        }

        // GET: api/SalesPerMonth/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SalesPerMonth
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SalesPerMonth/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SalesPerMonth/5
        public void Delete(int id)
        {
        }
    }
}
