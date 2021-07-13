using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdFi.SampleDataGenerator.Console.Entities
{
    class Demographics
    {
        public double AmericanIndianAlaskaNativePercentage { get; set; }
        public double AsianPercentage { get; set; }
        public double BlackPercentage { get; set; }
        public double HispanicPercentage { get; set; }
        public double NativeHawaiianOtherPacificIslander { get; set;} 
        public double WhitePercentage { get; set; }
        public double TwoOrMoreRaces { get; set; }
        public double NoCategoryCodes { get; set; }
        public double NotSpecified { get; set; }
        
        public double MalePercentage { get; set; }
        public double FemalePercentage { get; set; }

        public int TotalStudents { get; set; }

        /*
         *  American Indian or Alaska Native
            Asian
            Black or African American
            Hispanic/Latino
            Native Hawaiian or Other Pacific Islander
            No Category Codes
            Not Specified
            Two or more races
            White */

    }
}
