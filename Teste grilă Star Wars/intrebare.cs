using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste_grilă_Star_Wars
{
    class intrebare
    {
        private int id;
        private string enunt, a, b, c, d, corect, imagine, raspuns;

        public int ID {  set { id = value;  } get { return id; } }
        public string Enunt { set { enunt = value; } get { return enunt; } }
        public string A { set { a = value; } get { return a; } }
        public string B { set { b = value; } get { return b; } }
        public string C { set { c = value; } get { return c; } }
        public string D { set { d = value; } get { return d; } }
        public string Corect { set { corect = value; } get { return corect; } }
        public string Imagine { set { imagine = value; } get { return imagine; } }
        public string Raspuns { set { raspuns = value; } get { return raspuns; } }

        public intrebare() { }
        public intrebare (int id, string enunt, string a, string b, string c, string d, string corect, string imagine)
        {
            ID = id;
            Enunt = enunt;
            A = a; B = b; C = c; D = d;
            Corect = corect;
            Imagine = imagine;
        }
    }
}
