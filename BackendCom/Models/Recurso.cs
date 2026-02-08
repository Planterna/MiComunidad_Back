using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BackendCom.Models;

public partial class Recurso
{
    public int Id { get; set; }

    public int TipoRecursoId { get; set; }

    public string Nombre { get; set; } = null!;

    public int? Capacidad { get; set; }

    public int? Stock { get; set; }

    public string? Ubicacion { get; set; }

    public string? ImagenUrl { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    [JsonIgnore]
    public virtual ICollection<HistorialUso> HistorialUsos { get; set; } = new List<HistorialUso>();

    [JsonIgnore]
    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    [JsonIgnore]
    public virtual TipoRecurso? TipoRecurso { get; set; }
}
