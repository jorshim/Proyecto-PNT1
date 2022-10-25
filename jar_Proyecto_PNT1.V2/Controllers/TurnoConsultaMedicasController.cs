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
    public class TurnoConsultaMedicasController : Controller
    {
        private readonly ClinicaDatabaseV2Context _context;

        public TurnoConsultaMedicasController(ClinicaDatabaseV2Context context)
        {
            _context = context;
        }

        // GET: TurnoConsultaMedicas
        public async Task<IActionResult> Index(string searching = "", int pg = 1)
        {

            var data2 = _context.TurnoConsultaMedica.ToList();
            if (!string.IsNullOrEmpty(searching))
            {
                data2 = (await _context.TurnoConsultaMedica.Where(x => x.DocumentoPaciente.ToString().Contains(searching) || searching == null).ToListAsync());

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

            //return View(await _context.TurnoConsultaMedica.ToListAsync());
        }

        // GET: TurnoConsultaMedicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnoConsultaMedica = await _context.TurnoConsultaMedica
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turnoConsultaMedica == null)
            {
                return NotFound();
            }

            return View(turnoConsultaMedica);
        }

        // GET: TurnoConsultaMedicas/Create
        public IActionResult Create(bool esValido = false)
        {
            List<SelectListItem> MedicosItems = new List<SelectListItem>();
            foreach (Medico m in _context.Medicos)
            {
                MedicosItems.Add(new SelectListItem()
                {
                    Text = m.Nombre.ToString() +" "+m.Apellido.ToString()+ " -    Especialidad: " + m.Especialidad.ToString(),

                    Value = m.Id.ToString(),
                    Selected = false
                });
            }
            ViewBag.MedicosItems = MedicosItems;
            ViewBag.EsValido = esValido;

            return View();
        }

        // POST: TurnoConsultaMedicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMedico,FechaConsultaMedica,DocumentoPaciente,Id,IdPaciente,DiasDisponibles,HorasDisponibles")] TurnoConsultaMedica turnoConsultaMedica)
        {

            if (ModelState.IsValid)
            {
                
                DateTime date = DateTime.Today;

                switch (turnoConsultaMedica.DiasDisponibles)
                {
                    case DiasDisponibles.Lunes:
                        turnoConsultaMedica.FechaConsultaMedica = date.AddDays(7).ToString("dd/MM/yyyy") + " " + (int)turnoConsultaMedica.HorasDisponibles + ":00";
                        break;
                    case DiasDisponibles.Martes:
                        turnoConsultaMedica.FechaConsultaMedica = date.AddDays(8).ToString("dd/MM/yyyy") + " " + (int)turnoConsultaMedica.HorasDisponibles + ":00";
                        break;
                    case DiasDisponibles.Miercoles:
                        turnoConsultaMedica.FechaConsultaMedica = date.AddDays(9).ToString("dd/MM/yyyy") + " " + (int)turnoConsultaMedica.HorasDisponibles + ":00";
                        break;
                    case DiasDisponibles.Jueves:
                        turnoConsultaMedica.FechaConsultaMedica = date.AddDays(10).ToString("dd/MM/yyyy") + " " + (int)turnoConsultaMedica.HorasDisponibles + ":00";
                        break;
                    case DiasDisponibles.Viernes:
                        turnoConsultaMedica.FechaConsultaMedica = date.AddDays(11).ToString("dd/MM/yyyy") + " " + (int)turnoConsultaMedica.HorasDisponibles + ":00";
                        break;
                }
                foreach (TurnoConsultaMedica tcm in _context.TurnoConsultaMedica.Where(s => s.FechaConsultaMedica.Equals(turnoConsultaMedica.FechaConsultaMedica)))
                {

                    if (tcm.IdMedico == turnoConsultaMedica.IdMedico)
                    {
                        TempData["AlertMessage"] = "El médico ya tiene un turno asignado en esa fecha y hora.";

                        return RedirectToAction("Create", new { esValido = true });
                        //return Content("EL MEDICO  YA TIENE UN TURNO ASIGNADO EN ESA FECHA Y HORA");
                    }
                    else if (tcm.DocumentoPaciente == turnoConsultaMedica.DocumentoPaciente)
                    {
                        TempData["AlertMessage"] = "El paciente ya tiene un turno asignado en esa fecha y hora.";
                        //return Content("EL PACIENTE YA TIENE UN TURNO ASIGNADO EN ESA FECHA Y HORA");
                        return RedirectToAction("Create", new { esValido = true });

                    }
                    
                }
                foreach (TurnoPracticaMedica tpm in _context.TurnoPracticaMedica.Where(s => s.FechaConsultaMedica.Equals(turnoConsultaMedica.FechaConsultaMedica)))
                {
                    if (tpm.DocumentoPaciente == turnoConsultaMedica.DocumentoPaciente)
                    {
                        TempData["AlertMessage"] = "El paciente ya tiene un turno para una práctica médica asignado en esa fecha y hora.";
                        //return Content("EL PACIENTE YA TIENE UN TURNO ASIGNADO EN ESA FECHA Y HORA");
                        return RedirectToAction("Create", new { esValido = true });

                    }
                }
                foreach (Paciente p in _context.Pacientes.Where(s=> s.Documento == turnoConsultaMedica.DocumentoPaciente)){
                    turnoConsultaMedica.IdPaciente = p.Id;
                }



                
                _context.Add(turnoConsultaMedica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            List<SelectListItem> MedicosItems = new List<SelectListItem>();
            foreach (Medico m in _context.Medicos)
            {
                MedicosItems.Add(new SelectListItem()
                {
                    Text = m.Nombre.ToString() + " " + m.Apellido.ToString() + " -    Especialidad: " + m.Especialidad.ToString(),

                    Value = m.Id.ToString(),
                    Selected = false
                });
            }
            ViewBag.MedicosItems = MedicosItems;
            return View(turnoConsultaMedica);
        }

        // GET: TurnoConsultaMedicas/Edit/5
        public async Task<IActionResult> Edit(int? id, bool esValido = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnoConsultaMedica = await _context.TurnoConsultaMedica.FindAsync(id);
            if (turnoConsultaMedica == null)
            {
                return NotFound();
            }
            List<SelectListItem> MedicosItems = new List<SelectListItem>();
            foreach (Medico m in _context.Medicos)
            {
                MedicosItems.Add(new SelectListItem()
                {
                    Text = m.Nombre.ToString() + " " + m.Apellido.ToString() + " -    Especialidad: " + m.Especialidad.ToString(),

                    Value = m.Id.ToString(),
                    Selected = false
                });
            }
            ViewBag.MedicosItems = MedicosItems;
            ViewBag.EsValido = esValido;
            return View(turnoConsultaMedica);
        }

        // POST: TurnoConsultaMedicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMedico,FechaConsultaMedica,DocumentoPaciente,Id,IdPaciente,DiasDisponibles,HorasDisponibles")] TurnoConsultaMedica turnoConsultaMedica)
        {
            if (id != turnoConsultaMedica.Id)
            {
                return NotFound();
            }
            List<SelectListItem> MedicosItems = new List<SelectListItem>();
            foreach (Medico m in _context.Medicos)
            {
                MedicosItems.Add(new SelectListItem()
                {
                    Text = m.Nombre.ToString() + " " + m.Apellido.ToString() + " -    Especialidad: " + m.Especialidad.ToString(),

                    Value = m.Id.ToString(),
                    Selected = false
                });
            }
            ViewBag.MedicosItems = MedicosItems;

            if (ModelState.IsValid)
            {
                try
                {
                    DateTime date = DateTime.Today;

                    switch (turnoConsultaMedica.DiasDisponibles)
                    {
                        case DiasDisponibles.Lunes:
                            turnoConsultaMedica.FechaConsultaMedica = date.AddDays(7).ToString("dd/MM/yyyy") + " " + (int)turnoConsultaMedica.HorasDisponibles + ":00";
                            break;
                        case DiasDisponibles.Martes:
                            turnoConsultaMedica.FechaConsultaMedica = date.AddDays(8).ToString("dd/MM/yyyy") + " " + (int)turnoConsultaMedica.HorasDisponibles + ":00";
                            break;
                        case DiasDisponibles.Miercoles:
                            turnoConsultaMedica.FechaConsultaMedica = date.AddDays(9).ToString("dd/MM/yyyy") + " " + (int)turnoConsultaMedica.HorasDisponibles + ":00";
                            break;
                        case DiasDisponibles.Jueves:
                            turnoConsultaMedica.FechaConsultaMedica = date.AddDays(10).ToString("dd/MM/yyyy") + " " + (int)turnoConsultaMedica.HorasDisponibles + ":00";
                            break;
                        case DiasDisponibles.Viernes:
                            turnoConsultaMedica.FechaConsultaMedica = date.AddDays(11).ToString("dd/MM/yyyy") + " " + (int)turnoConsultaMedica.HorasDisponibles + ":00";
                            break;
                    }
                    foreach (TurnoConsultaMedica tcm in _context.TurnoConsultaMedica.Where(s => s.FechaConsultaMedica.Equals(turnoConsultaMedica.FechaConsultaMedica)))
                    {

                        if (tcm.IdMedico == turnoConsultaMedica.IdMedico)
                        {
                            TempData["AlertMessage"] = "El médico ya tiene un turno asignado en esa fecha y hora.";

                            return RedirectToAction("Create", new { esValido = true });
                            //return Content("EL MEDICO  YA TIENE UN TURNO ASIGNADO EN ESA FECHA Y HORA");
                        }
                        else if (tcm.DocumentoPaciente == turnoConsultaMedica.DocumentoPaciente)
                        {
                            TempData["AlertMessage"] = "El paciente ya tiene un turno asignado en esa fecha y hora.";
                            //return Content("EL PACIENTE YA TIENE UN TURNO ASIGNADO EN ESA FECHA Y HORA");
                            return RedirectToAction("Create", new { esValido = true });

                        }

                    }
                    foreach (TurnoPracticaMedica tpm in _context.TurnoPracticaMedica.Where(s => s.FechaConsultaMedica.Equals(turnoConsultaMedica.FechaConsultaMedica)))
                    {
                        if (tpm.DocumentoPaciente == turnoConsultaMedica.DocumentoPaciente)
                        {
                            TempData["AlertMessage"] = "El paciente ya tiene un turno para una práctica médica asignado en esa fecha y hora.";
                            //return Content("EL PACIENTE YA TIENE UN TURNO ASIGNADO EN ESA FECHA Y HORA");
                            return RedirectToAction("Create", new { esValido = true });

                        }
                    }
                    foreach (Paciente p in _context.Pacientes.Where(s => s.Documento == turnoConsultaMedica.DocumentoPaciente))
                    {
                        turnoConsultaMedica.IdPaciente = p.Id;
                    }
                    
                    _context.Update(turnoConsultaMedica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoConsultaMedicaExists(turnoConsultaMedica.Id))
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
           

            return View(turnoConsultaMedica);
        }

        // GET: TurnoConsultaMedicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnoConsultaMedica = await _context.TurnoConsultaMedica
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turnoConsultaMedica == null)
            {
                return NotFound();
            }

            return View(turnoConsultaMedica);
        }

        // POST: TurnoConsultaMedicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turnoConsultaMedica = await _context.TurnoConsultaMedica.FindAsync(id);
            _context.TurnoConsultaMedica.Remove(turnoConsultaMedica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoConsultaMedicaExists(int id)
        {
            return _context.TurnoConsultaMedica.Any(e => e.Id == id);
        }
    }
}
