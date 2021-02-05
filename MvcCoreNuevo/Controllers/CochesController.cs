using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcCoreNuevo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Controllers
{
    public class CochesController : Controller
    {
        private List<Coche> GetCoches()
        {
            List<Coche> coches = new List<Coche>();
            Coche car = new Coche { IdCoche = 1, Marca = "Ford", Modelo = "Mustang", VelocidadMaxima = 320 };
            Coche car2 = new Coche { IdCoche = 2, Marca = "Seat", Modelo = "Ibiza", VelocidadMaxima = 180 };
            Coche car3 = new Coche { IdCoche = 3, Marca = "Volkswagen", Modelo = "Passat", VelocidadMaxima = 200 };

            coches.Add(car);
            coches.Add(car2);
            coches.Add(car3);

            return coches;
        }

        public IActionResult Index()
        {
            return View(this.GetCoches());
        }

        public IActionResult CochesAsincronos()
        {
            return View();
        }

        public IActionResult GetCochesPartial()
        {
            List<Coche> coches = this.GetCoches();
            return PartialView("_CochesPartial", coches);
        }

        public IActionResult GetDetallesCochePartial(int id)
        {
            Coche car = this.GetCoches().SingleOrDefault(z => z.IdCoche == id);
            return PartialView("_DetallesCochePartial", car);
        }

    }
}
