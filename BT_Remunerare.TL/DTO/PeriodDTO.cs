using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_Remunerare.TL.DTO
{
    public class PeriodDTO
    {
        public int PeriodId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public virtual ICollection<SaleDTO>? Sales { get; set; }

        public virtual ICollection<SalesRemunerationRuleDTO>? SalesRemunerationRules { get; set; }
    }
}
