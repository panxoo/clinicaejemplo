using FRANLES_DENT_3.Variables;
using System.Collections.Generic;

namespace FRANLES_DENT_3.Models.Sistema
{
    public class DataCollect<T>
    {
        public DataCollect()
        {
            Acciones = VarGnrl.GetActionAll();
        }

        public List<T> ListDatos { get; set; }
        public string Metodo { get; set; }
        public string ModAct { get; set; }

        public Dictionary<string, string> Acciones { get; set; }
    }
}