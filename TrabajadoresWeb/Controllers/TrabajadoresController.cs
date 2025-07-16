using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrabajadoresWeb.Data;
using TrabajadoresWeb.Models;

namespace TrabajadoresWeb.Controllers
{
    public class TrabajadoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrabajadoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trabajadores
        public async Task<IActionResult> Index(string sexo, int? departamentoId)
        {
            var trabajadoresQuery = _context.Trabajadores
                .Include(t => t.Departamento)
                .Include(t => t.Provincia)
                .Include(t => t.Distrito)
                .AsQueryable();

            if (!string.IsNullOrEmpty(sexo))
            {
                trabajadoresQuery = trabajadoresQuery.Where(t => t.Sexo == sexo);
            }

            if (departamentoId.HasValue)
            {
                trabajadoresQuery = trabajadoresQuery.Where(t => t.IdDepartamento == departamentoId.Value);
            }

            ViewBag.Departamentos = _context.Departamentos.ToList();

            return View(await trabajadoresQuery.ToListAsync());
        }


        // GET: Trabajadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabajador = await _context.Trabajadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajador == null)
            {
                return NotFound();
            }

            return View(trabajador);
        }

        // GET: Trabajadores/Create
        public IActionResult Create()
        {
            ViewBag.TipoDocumentos = new List<SelectListItem>
            {
                new SelectListItem { Value = "DNI", Text = "DNI" },
                new SelectListItem { Value = "CE", Text = "Carnet Extranjeria" },
                new SelectListItem { Value = "PAS", Text = "Pasaporte" }
            };

            ViewBag.Sexos = new List<SelectListItem>
            {
                new SelectListItem { Value = "M", Text = "Masculino" },
                new SelectListItem { Value = "F", Text = "Femenino" }
            };

            ViewBag.Departamentos = new SelectList(_context.Departamentos, "Id", "NombreDepartamento");
            return View();
        }

        // POST: Trabajadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajador trabajador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trabajador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trabajador);
        }

        // GET: Trabajadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var trabajador = await _context.Trabajadores
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trabajador == null) return NotFound();

            // Combo de tipo documento
            ViewBag.TipoDocumentos = new List<SelectListItem>
            {
                new SelectListItem { Value = "DNI", Text = "DNI" },
                new SelectListItem { Value = "CE", Text = "Carnet Extranjeria" },
                new SelectListItem { Value = "PAS", Text = "Pasaporte" }
            };

             // Combo de sexo
            ViewBag.Sexos = new List<SelectListItem>
            {
                new SelectListItem { Value = "M", Text = "Masculino" },
                new SelectListItem { Value = "F", Text = "Femenino" }
            };
    
            // Combo cascada
            ViewBag.Departamentos = new SelectList(_context.Departamentos, "Id", "NombreDepartamento", trabajador.IdDepartamento);
            ViewBag.Provincias = new SelectList(_context.Provincias.Where(p => p.IdDepartamento == trabajador.IdDepartamento), "Id", "NombreProvincia", trabajador.IdProvincia);
            ViewBag.Distritos = new SelectList(_context.Distritos.Where(d => d.IdProvincia == trabajador.IdProvincia), "Id", "NombreDistrito", trabajador.IdDistrito);

            return View(trabajador);
        }

        // POST: Trabajadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajador trabajador)
        {
            if (id != trabajador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trabajador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrabajadorExists(trabajador.Id))
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
            return View(trabajador);
        }

        // GET: Trabajadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabajador = await _context.Trabajadores
                .Include(t => t.Departamento)
                .Include(t => t.Provincia)
                .Include(t => t.Distrito)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (trabajador == null)
            {
                return NotFound();
            }

            return View(trabajador);
        }


        // POST: Trabajadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador != null)
            {
                _context.Trabajadores.Remove(trabajador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public JsonResult GetProvincias(int departamentoId)
        {
            var provincias = _context.Provincias
                .Where(p => p.IdDepartamento == departamentoId)
                .Select(p => new { p.Id, p.NombreProvincia })
                .ToList();

            return Json(provincias);
        }

        [HttpGet]
        public JsonResult GetDistritos(int provinciaId)
        {
            var distritos = _context.Distritos
                .Where(d => d.IdProvincia == provinciaId)
                .Select(d => new { d.Id, d.NombreDistrito })
                .ToList();

            return Json(distritos);
        }


        private bool TrabajadorExists(int id)
        {
            return _context.Trabajadores.Any(e => e.Id == id);
        }
    }
}
