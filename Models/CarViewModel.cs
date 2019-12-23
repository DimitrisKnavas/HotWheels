using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotWheels.Models
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int cc { get; set; }
        public decimal Price { get; set; }
        public bool IsNegotiable { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public long Views { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string ImageUrl { get; set; }
    }
}