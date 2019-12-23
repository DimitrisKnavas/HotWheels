﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotWheels.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Model> Models { get; set; }
    }
}