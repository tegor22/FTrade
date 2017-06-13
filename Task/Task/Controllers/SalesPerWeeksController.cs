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
    public class SalesPerWeeksController : ApiController
    {
        // GET: api/SalesPerWeeks
        public IQueryable<StructWeeks> Get(DateTime StartDate, DateTime EndDate)
        {
            int startMonth = StartDate.Month;
            int endMonth = EndDate.Month;
            int startYear = StartDate.Year;
            int endYear = EndDate.Year;
            DatabaseTaskEntities context = new DatabaseTaskEntities();
            var result = context.Sales.Select(i => new Sale() { Id = i.Id, Date = i.Date, Price = i.Price })
                .Where(q => q.Date >= StartDate && q.Date <= EndDate).OrderBy(i => i.Date);
            var finleResult = result.GroupBy(i => new { WeekNumber = SqlFunctions.DatePart("week", i.Date), MonthNumber = SqlFunctions.DatePart("month", i.Date),  YearNumber = SqlFunctions.DatePart("year", i.Date) })
                .Select(grp => new StructWeeks() { NumberWeek = grp.Key.WeekNumber, NumberYear = grp.Key.YearNumber, NumberMonth = grp.Key.MonthNumber, CountPrice = grp.Count(), Price = grp.Sum(i => i.Price) });
            return finleResult;
        }

        // GET: api/SalesPerWeeks/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SalesPerWeeks
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SalesPerWeeks/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SalesPerWeeks/5
        public void Delete(int id)
        {
        }
    }
}
