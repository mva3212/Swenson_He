using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Swenson_He.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Sku { get; set; }

        public ProductType ProductType { get; set; }
    }

    public class Attribute
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public AttributeType AttributeType { get; set; }

    }

    public class AttributeType
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class ProductType
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

   
}