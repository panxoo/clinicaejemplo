using FRANLES_DENT_3.Variables;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FRANLES_DENT_3.Models.Sistema
{
    public class DataInput<T>
    {
        public DataInput()
        {
            Acciones = VarGnrl.GetActionAll();
        }

        public T Input { get; set; }

        public string Metodo { get; set; }
        public string ModAct { get; set; }
        public string Action { get; set; }

        public Dictionary<string, string> Acciones { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
    }
}