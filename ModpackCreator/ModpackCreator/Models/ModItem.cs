using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModpackCreator.Models
{
    public class ModItem
    {
        public uint FileHash { get; set; }
        public string ModName { get; set; } = "";
        public string ModID { get; set; } = "";
     }
}
