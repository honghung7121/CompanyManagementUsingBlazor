using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dto
{
    public class CompanyDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
    }
}
