using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Models.Sistema
{
    public class DataCollectInput<T>
    {
        public List<T> ListDatos { get; set; }
        public T Input { get; set; }
        public string Metodo { get; set; }
        public string Action { get; set; }

    }
}
