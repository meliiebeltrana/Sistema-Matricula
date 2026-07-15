using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers;

[Authorize]
public class CarrerasController : Controller
{
    private readonly ApplicationDbContext _context;

    public CarrerasController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Todos los usuarios autenticados pueden listar carreras.
    // GET: Carreras
    public async Task<IActionResult> Index()
    {
        var carreras = await _context.Carreras
            .OrderBy(c => c.Nombre)
            .ToListAsync();

        return View(carreras);
    }

    // Todos los usuarios autenticados pueden consultar detalles.
    // GET: Carreras/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var carrera = await _context.Carreras
            .FirstOrDefaultAsync(c => c.Id == id);

        if (carrera == null)
        {
            return NotFound();
        }

        return View(carrera);
    }

    // Solo el administrador puede agregar carreras.
    // GET: Carreras/Create
    [Authorize(Roles = "Administrador")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: Carreras/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Create(
        [Bind("Nombre,Codigo,Descripcion,Activa")]
        Carrera carrera)
    {
        if (ModelState.IsValid)
        {
            _context.Carreras.Add(carrera);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] =
                "La carrera se registró correctamente.";

            return RedirectToAction(nameof(Index));
        }

        return View(carrera);
    }

    // Solo el administrador puede editar carreras.
    // GET: Carreras/Edit/5
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var carrera = await _context.Carreras.FindAsync(id);

        if (carrera == null)
        {
            return NotFound();
        }

        return View(carrera);
    }

    // POST: Carreras/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id,Nombre,Codigo,Descripcion,Activa")]
        Carrera carrera)
    {
        if (id != carrera.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Carreras.Update(carrera);
                await _context.SaveChangesAsync();

                TempData["Mensaje"] =
                    "La carrera se actualizó correctamente.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarreraExists(carrera.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(carrera);
    }

    // Solo el administrador puede eliminar carreras.
    // GET: Carreras/Delete/5
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var carrera = await _context.Carreras
            .FirstOrDefaultAsync(c => c.Id == id);

        if (carrera == null)
        {
            return NotFound();
        }

        return View(carrera);
    }

    // POST: Carreras/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var carrera = await _context.Carreras.FindAsync(id);

        if (carrera != null)
        {
            _context.Carreras.Remove(carrera);
            await _context.SaveChangesAsync();

            TempData["Mensaje"] =
                "La carrera se eliminó correctamente.";
        }

        return RedirectToAction(nameof(Index));
    }

    private bool CarreraExists(int id)
    {
        return _context.Carreras.Any(c => c.Id == id);
    }
}