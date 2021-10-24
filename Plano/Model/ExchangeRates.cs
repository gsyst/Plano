using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Plano.Model
{
    public class ExchangeRates
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ERID { get; set; }

        public string CurrencyCode { get; set; }

        [Column("ExchangeRates")]
        public decimal ExchangeRate { get; set; }

        public bool IsDefault { get; set; }
    }
}
