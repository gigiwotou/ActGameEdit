using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameData
{
    public class MyImage
    {
        public class ImageInfo
        {
           public string name;
           public string filename;
           public int row;
           public int col;
        }
        private List<ImageInfo> objectTypes = new List<ImageInfo>();
        public List<ImageInfo> ObjectTypes { get { return objectTypes; } }
        
        public MyImage()
        {
        }
        public void add(ImageInfo n)
        {
            objectTypes.Add(n);
        }
        
        public void del(ImageInfo n)
        {
            objectTypes.Remove(n);
        }

        public void del (int index)
        {
            objectTypes.RemoveAt(index);
        }

        public ImageInfo find (int index)
        {
            if (objectTypes.Count < (index + 1))
                return null;
            return objectTypes[index];
        }
    }
}
