using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using jar_Proyecto_PNT1.V2.Context;
using jar_Proyecto_PNT1.V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jar_Proyecto_PNT1.V2.Controllers
{
    public class Datos : Controller
    {
        private readonly ClinicaDatabaseV2Context _context;

        public Datos (ClinicaDatabaseV2Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["CantidadConsultas"] = _context.TurnoConsultaMedica.Count();
            ViewData["CantidadPrácticas"] = _context.TurnoPracticaMedica.Count();
            ViewData["CantidadPacientes"] = _context.Pacientes.Count();
            ViewData["CantidadMedicos"] = _context.Medicos.Count();



            var data = _context.TurnoConsultaMedica.GroupBy(info => info.IdMedico)
                        .Select(group => new
                        {
                            Metric = group.Key,
                            CantidadTurnos = group.Count()

                        });

            var qs = (from tcm in data
                      join med in _context.Medicos on tcm.Metric equals med.Id into medNombre
                      from medicosNombres in medNombre.DefaultIfEmpty()
                      select new { tcm.CantidadTurnos, medicosNombres.Nombre, medicosNombres.Apellido }).ToList();

            var qsOrdeer = from s in qs
                           orderby s.CantidadTurnos descending
                           select s;


            ViewBag.TurnosCompletos = qsOrdeer;
            ViewData["TurnosCompletos"] = qsOrdeer;


            var data2 = _context.TurnoPracticaMedica.GroupBy(info => info.IdPracticaMedica)
                       .Select(group => new
                       {
                           Metric = group.Key,
                           CantidadTurnos = group.Count()

                       });

            


            return View();
        }
    }
}
