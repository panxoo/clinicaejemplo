using Microsoft.AspNetCore.Mvc;

namespace FRANLES_DENT_3.Models.Sistema
{
    public class DataInputDato<T>
    {
        public T Input { get; set; }
        public T Datos { get; set; }

        public string Metodo { get; set; }
        public string Action { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
    }
}