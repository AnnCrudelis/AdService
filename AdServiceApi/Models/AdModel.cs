using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdServiceApi.Models
{
    public class AdModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public int Price { get; set; }
        public string Photo { get; set; }
    }
}
