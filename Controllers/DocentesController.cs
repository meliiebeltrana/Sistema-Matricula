using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers;

[Authorize(Roles = "Administrador")]
public class DocentesController : Controller
{
    private readonly ApplicationDbContext _context;

    public DocentesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Docentes
    public async Task<IActionResult> Index()
    {
        var docentes = await _context.Docentes
            .OrderBy(d => d.NombreCompleto)
            .ToListAsync();

        return View(docentes);
    }

    // GET: Docentes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var docente = await _context.Docentes
            .FirstOrDefaultAsync(d => d.Id == id);

        if (docente == null)
        {
            return NotFound();
        }

        return View(docente);
    }

    // GET: Docentes/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Docentes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind(
            "Identificacion,NombreCompleto,Correo," +
            "Especialidad,Telefono,Activo"
        )]
        Docente docente)
    {
        if (ModelState.IsValid)
        {
            _context.Docentes.Add(docente);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] =
                "El docente se registró correctamente.";

            return RedirectToAction(nameof(Index));
        }

        return View(docente);
    }

    // GET: Docentes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var docente = await _context.Docentes.FindAsync(id);

        if (docente == null)
        {
            return NotFound();
        }

        return View(docente);
    }

    // POST: Docentes/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind(
            "Id,Identificacion,NombreCompleto,Correo," +
            "Especialidad,Telefono,Activo"
        )]
        Docente docente)
    {
        if (id != docente.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Docentes.Update(docente);
                await _context.SaveChangesAsync();

                TempData["Mensaje"] =
                    "El docente se actualizó correctamente.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocenteExists(docente.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(docente);
    }

    // GET: Docentes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var docente = await _context.Docentes
            .FirstOrDefaultAsync(d => d.Id == id);

        if (docente == null)
        {
            return NotFound();
        }

        return View(docente);
    }

    // POST: Docentes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var docente = await _context.Docentes.FindAsync(id);

        if (docente != null)
        {
            _context.Docentes.Remove(docente);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] =
                "El docente se eliminó correctamente.";
        }

        return RedirectToAction(nameof(Index));
    }

    private bool DocenteExists(int id)
    {
        return _context.Docentes.Any(d => d.Id == id);
    }
}