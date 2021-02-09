using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MvcCoreNuevo.Data;
using MvcCoreNuevo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Controllers
{
    public class EmpleadosController : Controller
    {
        private IRepositoryHospital repo;
        private IMemoryCache cache;
        public EmpleadosController(IRepositoryHospital repo, IMemoryCache cache)
        {
            this.repo = repo;
            this.cache = cache;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PaginarGrupoEmpleados(int? posicion, int? regpag)
        {
            if(regpag != null) { 
                this.cache.Set("NUMEMPPAG", regpag  
                , new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration
                (TimeSpan.FromMinutes(5)));
                            ViewData["NUMEMPPAG"] =
                this.cache.Get("NUMEMPPAG");
            }
            else
            {
                if(this.cache.Get("NUMEMPPAG") == null)
                {
                    regpag = 4;
                } else
                {
                    regpag = int.Parse(this.cache.Get("NUMEMPPAG").ToString());
                }
            }

            //COMPROBAMOS SI HEMOS RECIBIDO POSICION
            if (posicion == null)
            {
                posicion = 1;
            }
            int numeropagina = 1;
            int numregistros = 0;

            List<Empleado> departamentos = this.repo.GetGrupoEmpleadosSQL(posicion.Value, regpag.Value,  ref numregistros);
            ViewBag.NumeroRegistros = numregistros;
            ViewBag.RegistrosPorPagina = regpag;
            return View(departamentos);
        }
    }
}
