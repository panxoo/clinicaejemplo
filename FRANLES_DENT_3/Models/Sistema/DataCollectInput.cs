using FRANLES_DENT_3.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRANLES_DENT_3.Models.Sistema
{
    public class DataCollectInput<T> : DataModulo
    {    
        public List<T> ListDatos { get; set; }
        public T Input { get; set; }

    }
}
