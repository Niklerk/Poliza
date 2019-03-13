namespace Persistence.DatabaseContext.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class poliza : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Polizas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Descripcion = c.String(),
                        IdTipoCubrimiento = c.Int(nullable: false),
                        PorcentajeCobertura = c.Single(nullable: false),
                        FechaInicio = c.DateTime(nullable: false),
                        PeriodoMeses = c.Int(nullable: false),
                        PrecioPoliza = c.Decimal(nullable: false, precision: 18, scale: 2),
                        idTipoRiesgo = c.Int(nullable: false),
                        CurrentStatus = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 128),
                        DeletedAt = c.DateTime(),
                        DeletedBy = c.String(maxLength: 128),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Poliza_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy)
                .Index(t => t.DeletedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Polizas", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Polizas", "DeletedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Polizas", "CreatedBy", "dbo.AspNetUsers");
            DropIndex("dbo.Polizas", new[] { "DeletedBy" });
            DropIndex("dbo.Polizas", new[] { "UpdatedBy" });
            DropIndex("dbo.Polizas", new[] { "CreatedBy" });
            DropTable("dbo.Polizas",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Poliza_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
