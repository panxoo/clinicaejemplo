using FRANLES_DENT_3.Variables;
using System.Collections.Generic;

namespace FRANLES_DENT_3.Models.Sistema
{
    public class DataModulo
    {
        public DataModulo()
        {
            Acciones = VarGnrl.GetActionAll();
        }

        public string Metodo { get; set; }
        public string ModAct { get; set; }
        public string Action { get; set; }

        public Dictionary<string, string> Acciones { get; set; }
    }
}