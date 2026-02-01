using System;
using System.Collections.Generic;

namespace BackendCom.Models;

public partial class TipoRecurso
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public virtual ICollection<Recurso> Recursos { get; set; } = new List<Recurso>();
}
