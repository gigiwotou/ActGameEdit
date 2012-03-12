using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace seesharp
{
    public class MyImage
    {
        public struct ImageInfo
        {
           string name;
           string filename;
           int row;
           int col;
        }
        private List<string> objectTypes = new List<string>();
        public List<string> ObjectTypes { get { return objectTypes; } }
        
        public MyImage()
        {
        }
        public void add(string n)
        {
            objectTypes.Add(n);
        }
        public void del(string n)
        {
            objectTypes.Remove(n);
        }
    }
}
