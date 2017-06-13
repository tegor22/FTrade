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
    public class SalesPerDaysController : ApiController
    {
        // GET: api/SalesPerDays
        public IQueryable<StructDays> Get(DateTime StartDate, DateTime EndDate)
        {
            DatabaseTaskEntities context = new DatabaseTaskEntities();
            var result = context.Sales.GroupBy(i => i.Date)
                .Select(grp => new StructDays() { Date = grp.Key, Price = grp.Sum(p => p.Price), CountPrice = grp.Count() })
                .Where(p=> p.Date >= StartDate && p.Date <= EndDate);
            return result;
        }

        // GET: api/SalesPerDays/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SalesPerDays
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SalesPerDays/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SalesPerDays/5
        public void Delete(int id)
        {
        }
    }
}
