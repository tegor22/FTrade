using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task.Models
{
    public class Sale
    {
        public DateTime? Date { get; set; }
        public decimal? Price { get; set; }
        public Guid? Id { get; set; }
    }
}