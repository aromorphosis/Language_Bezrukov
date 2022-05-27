using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Language_Bezrukov.DB;


namespace Language_Bezrukov.Classes
{
    class ClassHelper
    {
        public static Entities context { get; set; } = new Entities();
    }
}
