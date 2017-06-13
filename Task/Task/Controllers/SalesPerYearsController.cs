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
    public class SalesPerYearsController : ApiController
    {
        // GET: api/SalesPerYears
        public IQueryable<StructYears> Get(DateTime StartDate, DateTime EndDate)
        {
            int StartYear = StartDate.Year;
            int EndYear = EndDate.Year;
            DatabaseTaskEntities context = new DatabaseTaskEntities();
            var result = context.Sales.GroupBy(i => SqlFunctions.DatePart("year", i.Date))
            .Select(grp => new StructYears() { NumberYear = grp.Key.Value, CountPrice = grp.Count(), Price = grp.Sum(q => q.Price) })
            .Where(q => q.NumberYear >= StartYear && q.NumberYear <= EndYear);
            return result;
        }

        // GET: api/SalesPerYears/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SalesPerYears
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SalesPerYears/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SalesPerYears/5
        public void Delete(int id)
        {
        }
    }
}
