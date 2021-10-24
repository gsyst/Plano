using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Plano.Model
{
    public class CurrencyProps
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CPID { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public string CurrencyCode { get; set; }

        public short DecimalPlace { get; set; }
    }
}
