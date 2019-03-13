using Model.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Persistence.DatabaseContext.Mapping
{
    public class PolizaMapping : EntityTypeConfiguration<Poliza>
    {
        public PolizaMapping()
        {
            Property(m => m.Nombre).IsRequired().HasMaxLength(100);
            Property(m => m.Descripcion).IsRequired().HasMaxLength(2000);
            Property(m => m.PorcentajeCobertura).IsRequired();
            Property(m => m.PeriodoMeses).IsRequired();
            Property(m => m.PrecioPoliza).IsRequired();
        }
    }
}
