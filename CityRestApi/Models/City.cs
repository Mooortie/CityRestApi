using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityRestApi.Models
{
    internal class City
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime Created { get; set; } = DateTime.Now;

        public string CityName { get; set; }

        public bool Visited { get; set; }
        
        public int PostalCode { get; set; }
    }

    internal class CreateCity
    {
        public string CityName { get; set; }
        public int PostalCode { get; set; }
    }

    internal class UpdateCity
    {
        public string CityName{ get; set; }
        
        public bool Visited { get; set; }
    }
}
