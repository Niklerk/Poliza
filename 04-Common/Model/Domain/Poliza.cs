using Common;
using Common.CustomFilters;
using Model.Helper;
using System;
using System.Collections.Generic;

namespace Model.Domain
{
    public class Poliza : AuditEntity, ISoftDeleted
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Enums.TipoCubrimiento IdTipoCubrimiento { get; set; }
        public float PorcentajeCobertura { get; set; }
        public DateTime FechaInicio { get; set; }
        public int PeriodoMeses { get; set; }
        public decimal PrecioPoliza { get; set; }
        public Enums.TipoRiesgo idTipoRiesgo { get; set; }
        public Enums.Status CurrentStatus { get; set; }
        public bool Deleted { get; set; }
    }
}
