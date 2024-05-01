using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ONLINEFOODMOLL.Models
{
    public class Cart
    {
        public int p_id { get; set; }

        public int standard { get; set; }
        public string p_name { get; set; }

        public int p_qty { get; set; }

        public int p_price { get; set; }

        public int p_bill { get; set; }
        public string EmployeeOrganizationName { get; set; }
    }
}