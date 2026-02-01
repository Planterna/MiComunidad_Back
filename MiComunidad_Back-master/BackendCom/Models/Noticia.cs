using System;
using System.Collections.Generic;

namespace BackendCom.Models;

public partial class Noticia
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Contenido { get; set; } = null!;

    public string? ImagenUrl { get; set; }

    public DateTime FechaPublicacion { get; set; }

    public bool Activo { get; set; }

    public int UsuarioId { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
