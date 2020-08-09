using System.Collections.Generic;

namespace FRANLES_DENT_3.Models.Sistema
{
    public class DataCollectInput<T> : DataModulo
    {
        public List<T> ListDatos { get; set; }
        public T Input { get; set; }
    }
}