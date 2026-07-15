using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal.Models;

public class Curso
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El código del curso es obligatorio.")]
    [StringLength(
        20,
        ErrorMessage = "El código no puede superar los 20 caracteres."
    )]
    [Display(Name = "Código")]
    public string Codigo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre del curso es obligatorio.")]
    [StringLength(
        100,
        ErrorMessage = "El nombre no puede superar los 100 caracteres."
    )]
    [Display(Name = "Nombre del curso")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La cantidad de créditos es obligatoria.")]
    [Range(
        1,
        10,
        ErrorMessage = "Los créditos deben estar entre 1 y 10."
    )]
    [Display(Name = "Créditos")]
    public int Creditos { get; set; }

    [Range(
        1,
        100,
        ErrorMessage = "El cupo debe estar entre 1 y 100 estudiantes."
    )]
    [Display(Name = "Cupo máximo")]
    public int CupoMaximo { get; set; } = 30;

    [Display(Name = "Curso activo")]
    public bool Activo { get; set; } = true;

    [Required(ErrorMessage = "Debe seleccionar una carrera.")]
    [Display(Name = "Carrera")]
    public int CarreraId { get; set; }

    [ForeignKey(nameof(CarreraId))]
    public Carrera? Carrera { get; set; }
}