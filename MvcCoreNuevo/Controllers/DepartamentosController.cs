using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MvcCoreNuevo.Data;
using MvcCoreNuevo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Controllers
{
    public class DepartamentosController : Controller
    {
        IRepositoryHospital repo;
        public DepartamentosController(IRepositoryHospital repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            return View(this.repo.BuscarDepartamento(id));
        }
        public IActionResult PaginarVistaDeptRegistro(int? posicion)
        {
            if(posicion == null)
            {
                posicion = 1;
            }
            int ultimo = this.repo.GetNumRegVistaDepartamentos();
            int siguiente = posicion.Value + 1;
            if(siguiente > ultimo)
            {
                siguiente = ultimo;
            }
            int anterior = posicion.Value - 1;
            if(anterior < 1)
            {
                anterior = 1;
            }
            VistaDept dept = this.repo.GetRegistroDepartamento(posicion.Value);
            ViewBag.Ultimo = ultimo;
            ViewBag.Siguiente = siguiente;
            ViewBag.Anterior = anterior;
            return View(dept);
        }
        public IActionResult PaginarVistaDeptGrupo(int? posicion)
        {
            //COMPROBAMOS SI HEMOS RECIBIDO POSICION
            if(posicion == null)
            {
                posicion = 1;
            }
            int numeropagina = 1;
            int numregistros = this.repo.GetNumRegVistaDepartamentos();
            string html = "<div>";
            for (int i = 1; i<= numregistros; i += 2)
            {
                html += "<a href='PaginarVistaDeptGrupo?posicion=" + i + "'>" + numeropagina + "</a> | ";
                numeropagina++;
            }
            html += "</div>";
            ViewBag.Paginas = html;
            List<VistaDept> departamentos = this.repo.GetGrupoDepartamentos(posicion.Value);
            return View(departamentos);
        }

        public IActionResult PaginarRegistroDepartamentoSQL (int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int siguiente = posicion.Value + 1;
            int ultimo = this.repo.GetNumRegVistaDepartamentos();
            Departamento departamento = this.repo.GetDepartamentoPosicion(posicion.Value, ref ultimo);
            int anterior = posicion.Value - 1;
            if (siguiente > ultimo)
            {
                siguiente = ultimo;
            }
            if (anterior < 1)
            {
                anterior = 1;
            }
            ViewBag.Ultimo = ultimo;
            ViewBag.Siguiente = siguiente;
            ViewBag.Anterior = anterior;
            ViewBag.Posicion = posicion.Value;
            return View(departamento);
        }

        public IActionResult PaginarGrupoDepartamentos(int? posicion)
        {
            //COMPROBAMOS SI HEMOS RECIBIDO POSICION
            if (posicion == null)
            {
                posicion = 1;
            }
            int numeropagina = 1;
            int numregistros = 0;
            
            List<Departamento> departamentos = this.repo.GetGrupoDepartamentosSQL(posicion.Value, ref numregistros);
            ViewBag.NumeroRegistros = numregistros;
            
            return View(departamentos);
        }
    }
}
