using FRANLES_DENT_3.Variables;
using System.Collections.Generic;

namespace FRANLES_DENT_3.Models.Sistema
{
    public class DataCollect<T> : DataModulo
    {
        public List<T> ListDatos { get; set; }
    }
}