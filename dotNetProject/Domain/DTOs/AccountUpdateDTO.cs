using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class AccountUpdateDTO
    {
        public string name { get; set; }
        public string mainCurrency { get; set; }
        public double euro { get; set; }
        public double krone { get; set; }
        public double pound { get; set; }
    }
}
