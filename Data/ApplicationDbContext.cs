using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Carrera> Carreras { get; set; } = null!;

    public DbSet<Curso> Cursos { get; set; } = null!;

    public DbSet<Docente> Docentes { get; set; } = null!;

    public DbSet<Estudiante> Estudiantes { get; set; } = null!;

    public DbSet<Matricula> Matriculas { get; set; } = null!;
}