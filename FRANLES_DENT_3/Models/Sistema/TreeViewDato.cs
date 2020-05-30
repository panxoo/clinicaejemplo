using System.Collections.Generic;

namespace FRANLES_DENT_3.Models.Sistema
{
    public class TreeViewDato
    {
        public string id { get; set; }
        public string text { get; set; }
        public bool @checked { get; set; }
        public bool hasChildren { get; set; }
        public virtual List<TreeViewDato> children { get; set; }
        public string cssIcon { get; set; }
    }
}