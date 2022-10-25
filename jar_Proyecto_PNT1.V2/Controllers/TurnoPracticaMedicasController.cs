using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using jar_Proyecto_PNT1.V2.Context;
using jar_Proyecto_PNT1.V2.Models;

namespace jar_Proyecto_PNT1.V2.Controllers
{
    public class TurnoPracticaMedicasController : Controller
    {
        private readonly ClinicaDatabaseV2Context _context;

        public TurnoPracticaMedicasController(ClinicaDatabaseV2Context context)
        {
            _context = context;
        }

        // GET: TurnoPracticaMedicas
        public async Task<IActionResult> Index(string searching = "", int pg = 1)
        {

            var data2 = _context.TurnoPracticaMedica.ToList();
            if (!string.IsNullOrEmpty(searching))
            {
                data2 = (await _context.TurnoPracticaMedica.Where(x => x.DocumentoPaciente.ToString().Contains(searching) || searching == null).ToListAsync());

            }
            const int pageSize = 3;
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = data2.Count();
            var paginado = new Paginado(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            data2 = data2.Skip(recSkip).Take(paginado.PageSize).ToList();
            ViewBag.Paginado = paginado;
            ViewBag.CurrentSearching = searching;
            return View(data2);
           
            //return View(await _context.TurnoPracticaMedica.ToListAsync());
        }

        // GET: TurnoPracticaMedicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnoPracticaMedica = await _context.TurnoPracticaMedica
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turnoPracticaMedica == null)
            {
                return NotFound();
            }

            return View(turnoPracticaMedica);
        }

        // GET: TurnoPracticaMedicas/Create
        public IActionResult Create(bool esValido = false)
        {
            ViewBag.EsValido = esValido;
            return View();
        }

        // POST: TurnoPracticaMedicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPracticaMedica,FechaConsultaMedica,DocumentoPaciente,Id,IdPaciente,DiasDisponibles,HorasDisponibles")] TurnoPracticaMedica turnoPracticaMedica)
        {
            if (ModelState.IsValid)
            {
                DateTime date = DateTime.Today;

                switch (turnoPracticaMedica.DiasDisponibles)
                {
                    case DiasDisponibles.Lunes:
                        turnoPracticaMedica.FechaConsultaMedica = date.AddDays(7).ToString("dd/MM/yyyy") + " " + (int)turnoPracticaMedica.HorasDisponibles + ":00";
                        break;
                    case DiasDisponibles.Martes:
                        turnoPracticaMedica.FechaConsultaMedica = date.AddDays(8).ToString("dd/MM/yyyy") + " " + (int)turnoPracticaMedica.HorasDisponibles + ":00";
                        break;
                    case DiasDisponibles.Miercoles:
                        turnoPracticaMedica.FechaConsultaMedica = date.AddDays(9).ToString("dd/MM/yyyy") + " " + (int)turnoPracticaMedica.HorasDisponibles + ":00";
                        break;
                    case DiasDisponibles.Jueves:
                        turnoPracticaMedica.FechaConsultaMedica = date.AddDays(10).ToString("dd/MM/yyyy") + " " + (int)turnoPracticaMedica.HorasDisponibles + ":00";
                        break;
                    case DiasDisponibles.Viernes:
                        turnoPracticaMedica.FechaConsultaMedica = date.AddDays(11).ToString("dd/MM/yyyy") + " " + (int)turnoPracticaMedica.HorasDisponibles + ":00";
                        break;
                }
                foreach (TurnoPracticaMedica tpm in _context.TurnoPracticaMedica.Where(s => s.FechaConsultaMedica.Equals(turnoPracticaMedica.FechaConsultaMedica)))
                {

                    if (tpm.IdPracticaMedica == turnoPracticaMedica.IdPracticaMedica)
                    {
                        TempData["AlertMessage"] = "El estudio ya tiene un turno asignado en esa fecha y hora.";

                        return RedirectToAction("Create", new { esValido = true });
                    }
                    else if (tpm.DocumentoPaciente == turnoPracticaMedica.DocumentoPaciente)
                    {
                        TempData["AlertMessage"] = "El paciente ya tiene un turno asignado en esa fecha y hora.";
                        //return Content("EL PACIENTE YA TIENE UN TURNO ASIGNADO EN ESA FECHA Y HORA");
                        return RedirectToAction("Create", new { esValido = true });
                    }
                }
                foreach (TurnoConsultaMedica tcm in _context.TurnoConsultaMedica.Where(s => s.FechaConsultaMedica.Equals(turnoPracticaMedica.FechaConsultaMedica)))
                {

                    if (tcm.DocumentoPaciente == turnoPracticaMedica.DocumentoPaciente)
                    {
                        TempData["AlertMessage"] = "El paciente ya tiene un turno para una consulta médica asignado en esa fecha y hora.";

                        return RedirectToAction("Create", new { esValido = true });
                        //return Content("EL MEDICO  YA TIENE UN TURNO ASIGNADO EN ESA FECHA Y HORA");
                    }
                }

                    foreach (Paciente p in _context.Pacientes.Where(s => s.Documento == turnoPracticaMedica.DocumentoPaciente))
                {
                    turnoPracticaMedica.IdPaciente = p.Id;
                }

                _context.Add(turnoPracticaMedica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(turnoPracticaMedica);
        }

        // GET: TurnoPracticaMedicas/Edit/5
        public async Task<IActionResult> Edit(int? id, bool esValido = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnoPracticaMedica = await _context.TurnoPracticaMedica.FindAsync(id);
            if (turnoPracticaMedica == null)
            {
                return NotFound();
            }
            ViewBag.EsValido = esValido;
            return View(turnoPracticaMedica);
        }

        // POST: TurnoPracticaMedicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPracticaMedica,FechaConsultaMedica,DocumentoPaciente,Id,IdPaciente,DiasDisponibles,HorasDisponibles")] TurnoPracticaMedica turnoPracticaMedica)
        {
            if (id != turnoPracticaMedica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DateTime date = DateTime.Today;

                    switch (turnoPracticaMedica.DiasDisponibles)
                    {
                        case DiasDisponibles.Lunes:
                            turnoPracticaMedica.FechaConsultaMedica = date.AddDays(7).ToString("dd/MM/yyyy") + " " + (int)turnoPracticaMedica.HorasDisponibles + ":00";
                            break;
                        case DiasDisponibles.Martes:
                            turnoPracticaMedica.FechaConsultaMedica = date.AddDays(8).ToString("dd/MM/yyyy") + " " + (int)turnoPracticaMedica.HorasDisponibles + ":00";
                            break;
                        case DiasDisponibles.Miercoles:
                            turnoPracticaMedica.FechaConsultaMedica = date.AddDays(9).ToString("dd/MM/yyyy") + " " + (int)turnoPracticaMedica.HorasDisponibles + ":00";
                            break;
                        case DiasDisponibles.Jueves:
                            turnoPracticaMedica.FechaConsultaMedica = date.AddDays(10).ToString("dd/MM/yyyy") + " " + (int)turnoPracticaMedica.HorasDisponibles + ":00";
                            break;
                        case DiasDisponibles.Viernes:
                            turnoPracticaMedica.FechaConsultaMedica = date.AddDays(11).ToString("dd/MM/yyyy") + " " + (int)turnoPracticaMedica.HorasDisponibles + ":00";
                            break;
                    }
                    foreach (TurnoPracticaMedica tpm in _context.TurnoPracticaMedica.Where(s => s.FechaConsultaMedica.Equals(turnoPracticaMedica.FechaConsultaMedica)))
                    {

                        if (tpm.IdPracticaMedica == turnoPracticaMedica.IdPracticaMedica)
                        {
                            TempData["AlertMessage"] = "El estudio ya tiene un turno asignado en esa fecha y hora.";

                            return RedirectToAction("Create", new { esValido = true });
                        }
                        else if (tpm.DocumentoPaciente == turnoPracticaMedica.DocumentoPaciente)
                        {
                            TempData["AlertMessage"] = "El paciente ya tiene un turno asignado en esa fecha y hora.";
                            //return Content("EL PACIENTE YA TIENE UN TURNO ASIGNADO EN ESA FECHA Y HORA");
                            return RedirectToAction("Create", new { esValido = true });
                        }
                    }
                    foreach (TurnoConsultaMedica tcm in _context.TurnoConsultaMedica.Where(s => s.FechaConsultaMedica.Equals(turnoPracticaMedica.FechaConsultaMedica)))
                    {

                        if (tcm.DocumentoPaciente == turnoPracticaMedica.DocumentoPaciente)
                        {
                            TempData["AlertMessage"] = "El paciente ya tiene un turno para una consulta médica asignado en esa fecha y hora.";

                            return RedirectToAction("Create", new { esValido = true });
                            //return Content("EL MEDICO  YA TIENE UN TURNO ASIGNADO EN ESA FECHA Y HORA");
                        }
                    }

                    foreach (Paciente p in _context.Pacientes.Where(s => s.Documento == turnoPracticaMedica.DocumentoPaciente))
                    {
                        turnoPracticaMedica.IdPaciente = p.Id;
                    }
                    _context.Update(turnoPracticaMedica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoPracticaMedicaExists(turnoPracticaMedica.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(turnoPracticaMedica);
        }

        // GET: TurnoPracticaMedicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnoPracticaMedica = await _context.TurnoPracticaMedica
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turnoPracticaMedica == null)
            {
                return NotFound();
            }

            return View(turnoPracticaMedica);
        }

        // POST: TurnoPracticaMedicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turnoPracticaMedica = await _context.TurnoPracticaMedica.FindAsync(id);
            _context.TurnoPracticaMedica.Remove(turnoPracticaMedica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoPracticaMedicaExists(int id)
        {
            return _context.TurnoPracticaMedica.Any(e => e.Id == id);
        }
    }
}
