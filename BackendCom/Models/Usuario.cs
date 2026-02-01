using System;
using System.Collections.Generic;

namespace BackendCom.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Cedula { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PassHash { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public int RolId { get; set; }

    public string? Estado { get; set; }

    public bool? AceptaNotificaciones { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<HistorialUso> HistorialUsos { get; set; } = new List<HistorialUso>();

    public virtual ICollection<Noticia> Noticia { get; set; } = new List<Noticia>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual Role Rol { get; set; } = null!;

}
