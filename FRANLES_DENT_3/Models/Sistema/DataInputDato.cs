using FRANLES_DENT_3.Variables;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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