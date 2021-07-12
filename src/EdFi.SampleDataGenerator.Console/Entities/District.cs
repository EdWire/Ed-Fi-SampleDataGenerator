using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdFi.SampleDataGenerator.Console.Entities
{
    class District
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string AreaCode { get; set; }

        public Demographics DistrictDemographics { get; set; }

        public District()
        {
            this.DistrictDemographics = new Demographics();
        }
    }
}
