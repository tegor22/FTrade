using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity.SqlServer;
using Task.Models;

namespace Task.Controllers
{
    public class SalesController : ApiController
    {
        // GET: api/Sales
        public IEnumerable<Sales>  Get()
        {
            DatabaseTaskEntities context = new DatabaseTaskEntities();
            var result = context.Sales.OrderBy(i => i.Date);
             return result;
        }
        
        // GET: api/Sales/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sales
        public Sales Post([FromBody]Sales obj)
        {
            DatabaseTaskEntities context = new DatabaseTaskEntities();
            context.Sales.Add(new Sales {Id = Guid.NewGuid(), Date=obj.Date, Price = obj.Price});
            context.SaveChanges();
            return obj;
        }

        // PUT: api/Sales/5
        public void Put(Guid? id, [FromBody]Sales obj)
        {
            DatabaseTaskEntities context = new DatabaseTaskEntities();
            var result = context.Sales.Where(i => i.Id == id).FirstOrDefault();
            if (obj.Price != null)
                result.Price = obj.Price;
            if (obj.Date != null)
               result.Date = obj.Date;
            context.SaveChanges();
        }

        // DELETE: api/Sales/5
        public void Delete(Guid? id)
        {
            DatabaseTaskEntities context = new DatabaseTaskEntities();
            var result = context.Sales.Where(i => i.Id == id).FirstOrDefault();
            context.Sales.Remove(result);
            context.SaveChanges();
        }
    }
}
