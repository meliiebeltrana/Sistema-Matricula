using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models;

public class Carrera
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre de la carrera es obligatorio.")]
    [StringLength(
        100,
        ErrorMessage = "El nombre no puede superar los 100 caracteres."
    )]
    [Display(Name = "Nombre de la carrera")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El código de la carrera es obligatorio.")]
    [StringLength(
        20,
        ErrorMessage = "El código no puede superar los 20 caracteres."
    )]
    [Display(Name = "Código")]
    public string Codigo { get; set; } = string.Empty;

    [StringLength(
        500,
        ErrorMessage = "La descripción no puede superar los 500 caracteres."
    )]
    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; }

    [Display(Name = "Carrera activa")]
    public bool Activa { get; set; } = true;

    public ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}