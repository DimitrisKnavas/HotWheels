using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HotWheels.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Color { get; set; }

        public int cc { get; set; }
        public decimal Price { get; set; }

        public bool IsNegotiable { get; set; }
        public string Description { get; set; }

        
        public DateTime Created { get; set; }

        public long Views { get; set; }
        public string ImageUrl { get; set; }

        public int ModelId { get; set; }
        public virtual Model Model { get; set; }
        
    }
}