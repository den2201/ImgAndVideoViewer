using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoViewer
{
    public class ImgItem
    {
        public string Name { get; private set; } = string.Empty;
        public string Extension { get; private set; } = string.Empty;

        public string Location { get; private set; } = string.Empty;
        public ImageType ImageType { get; private set; }
        public bool IsShown { get; set; } = false;

        public ImgItem(string path)
        {
            Location = path;
            Name = GetNameFromPath(path);
           ImageType = GetExtension(path);
        }

        private string GetNameFromPath(string path)
        {
            string name = path.Split('\\').Last();
            return name;
        }
        private ImageType GetExtension(string path)
        {
            string ext = Path.GetExtension(path);
            if (ext == ".jpg")
                return ImageType.Photo;
            else if ((ext == ".mpeg")||(ext == ".mp4"))
            {
                return ImageType.Video;
            }

            return ImageType.Unknown;
        }
    }
}
