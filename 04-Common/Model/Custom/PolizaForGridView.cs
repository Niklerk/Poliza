using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Custom
{
    public class PolizaForGridView
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string TipoCubrimiento { get; set; }
        public string TipoRiesgo { get; set; }
        public int PeriodoMeses { get; set; }
        public decimal PrecioPoliza { get; set; }
        public string CurrentStatus { get; set; }
        public bool Deleted { get; set; }
    }
}
