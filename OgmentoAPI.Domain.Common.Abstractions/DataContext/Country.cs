using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OgmentoAPI.Domain.Common.Abstractions.DataContext
{
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string TwoLetterIsoCode { get; set; }
        public int NumericIsoCode { get; set; }

    }
}
