using System;
using System.ComponentModel.DataAnnotations;

namespace FRANLES_DENT_3.Models.PacienteGnrl
{
    public class Paciente
    {
        public string PacienteId { get; set; }

        public int TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Sexo { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "El formato no corresponde a un correo")]
        public string Correo { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "El formato del dato ingresado no corresponde a un Movil")]
        public string Movil { get; set; }

        public DateTime Fecha_Add { get; set; }
    }
}