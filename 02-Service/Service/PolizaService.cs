using Common;
using Model.Auth;
using Model.Custom;
using Model.Domain;
using NLog;
using Persistence.DbContextScope;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public interface IPolizaService
    {
        IEnumerable<PolizaForGridView> GetAllByUser();
        IEnumerable<PolizaForGridView> GetAllById(int id);
        Poliza Get(int id);
        ResponseHelper InsertOrUpdate(Poliza model);
        ResponseHelper Delete(int id);
    }

    public class PolizaService : IPolizaService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Poliza> _polizaRepository;
        private readonly IRepository<ApplicationUser> _applicationUser;
        private readonly IRepository<ApplicationRole> _applicationRole;

        public PolizaService(
            IDbContextScopeFactory dbContextScopeFactory,
            IRepository<Poliza> PolizaRepository,
            IRepository<ApplicationUser> applicationUser,
            IRepository<ApplicationRole> applicationRole
        )
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _polizaRepository = PolizaRepository;
            _applicationUser = applicationUser;
            _applicationRole = applicationRole;
        }

        public IEnumerable<PolizaForGridView> GetAllByUser()
        {
            var result = new List<PolizaForGridView>();
            try
            {
                var userId = CurrentUserHelper.Get != null ? CurrentUserHelper.Get.UserId : null;

                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    var Poliza = _polizaRepository.Find(
                        x => x.CreatedBy == userId
                    ).AsQueryable();
                    result = _polizaRepository.GetAll(x => x.CreatedUser)
                        .Select(x => new PolizaForGridView
                        {
                            Id = x.Id,
                            Nombre = x.Nombre,
                            TipoCubrimiento = Enum.GetName(typeof(Enums.TipoCubrimiento), x.IdTipoCubrimiento),
                            TipoRiesgo = Enum.GetName(typeof(Enums.TipoRiesgo), x.idTipoRiesgo),
                            PeriodoMeses = x.PeriodoMeses,
                            CurrentStatus = x.CurrentStatus == Enums.Status.Enable ? "Active" : "Disabled",
                            PrecioPoliza = x.PrecioPoliza,
                            Usuario = x.CreatedUser.UserName
                        }).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public IEnumerable<PolizaForGridView> GetAllById(int id)
        {
            var result = new List<PolizaForGridView>();
            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {
                    var Poliza = _polizaRepository.Find(
                        x => x.Id == id
                    ).AsQueryable();
                    result = _polizaRepository.GetAll(x => x.CreatedUser)
                        .Select(x => new PolizaForGridView
                        {
                            Id = x.Id,
                            Nombre = x.Nombre,
                            TipoCubrimiento = Enum.GetName(typeof(Enums.TipoCubrimiento), x.IdTipoCubrimiento),
                            TipoRiesgo = Enum.GetName(typeof(Enums.TipoRiesgo), x.idTipoRiesgo),
                            PeriodoMeses = x.PeriodoMeses,
                            CurrentStatus = x.CurrentStatus == Enums.Status.Enable ? "Active" : "Disabled",
                            PrecioPoliza = x.PrecioPoliza,
                            Usuario = x.CreatedUser.UserName
                        }).ToList();
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public Poliza Get(int id)
        {
            var result = new Poliza();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _polizaRepository.SingleOrDefault(x => x.Id == id);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return result;
        }

        public ResponseHelper InsertOrUpdate(Poliza model)
        {
            var rh = new ResponseHelper();
            try
            {
                if ((Enums.TipoRiesgo)model.idTipoRiesgo == Enums.TipoRiesgo.Alto && model.PorcentajeCobertura > 50)
                {
                    rh.SetResponse(false, "el porcentaje de cobertura no puede ser mayor al 50%");
                }
                else
                {
                    using (var ctx = _dbContextScopeFactory.Create())
                    {
                        if (model.Id == 0)
                        {
                            _polizaRepository.Insert(model);
                        }
                        else
                        {
                            _polizaRepository.Update(model);
                        }

                        ctx.SaveChanges();
                        rh.SetResponse(true);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return rh;
        }

        public ResponseHelper Delete(int id)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var model = _polizaRepository.SingleOrDefault(x => x.Id == id);
                    _polizaRepository.Delete(model);

                    ctx.SaveChanges();
                    rh.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return rh;
        }
    }
}
