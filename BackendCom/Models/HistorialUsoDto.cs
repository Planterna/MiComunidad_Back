using System;
using System.Collections.Generic;
using System.Text;

namespace BackendCom.Models
{
    public class HistorialUsoDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int RecursoId { get; set; }
        public string FechaUso { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string Estado { get; set; }
        public string? Notas { get; set; }
        public bool Activo { get; set; }
    }

}