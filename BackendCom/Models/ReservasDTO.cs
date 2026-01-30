using System;
using System.Collections.Generic;
using System.Text;

namespace BackendCom.Models
{
    public partial class ReservasDTO
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

        public string? NombreVecino { get; set; }

        public string? Cedula { get; set; }

        public string? Email { get; set; }

        public string? NombreRecurso { get; set; }
        public string? ImagenUrl { get; set; }
        public int? TipoRecursoId {  get; set; }

    }
}
