using Microsoft.AspNetCore.Mvc;

namespace FRANLES_DENT_3.Models.Sistema
{
    public class DataInputDato<T> : DataModulo
    {
        public T Input { get; set; }
        public T Datos { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
    }
}