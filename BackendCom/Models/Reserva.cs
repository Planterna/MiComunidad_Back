using System;
using System.Collections.Generic;

namespace BackendCom.Models;

public partial class Reserva
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int RecursoId { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFin { get; set; }

    public string? Motivo { get; set; }

    public string? Estado { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual Recurso? Recurso { get; set; } = null!;

    public virtual Usuario? Usuario { get; set; } = null!;
}
