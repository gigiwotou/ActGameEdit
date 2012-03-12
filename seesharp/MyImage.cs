using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace seesharp
{
    public class MyImage
    {
        private List<string> objectTypes = new List<string>();
        public List<string> ObjectTypes { get { return objectTypes; } }
        public int x;
        public MyImage()
        {
            x = 1;
        }
        public void add(string n)
        {
            objectTypes.Add(n);
        }
    }
}
