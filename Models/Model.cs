using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotWheels.Models
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
    }
}