using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFormsDemo.Models
{
    public class AdministrativeRegion
    {
        public string AreaName { get; set; }

        public double Lng { get; set; }

        public double Lat { get; set; }

        public List<AdministrativeRegion> Children { get; set; }
    }
}
