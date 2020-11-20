using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DermaHacker.Models.ImagePreprocessing
{
    class ImagePreprocessing
    {
        public Image loadImage()
        {
            string path = "C:\\Users\\Wika\\Desktop\\Kej\\hackaton\\Cases_Thermography\\Case1A.jpg";
            var image = new Image { Source = path };
            return image;
        }
    }
}
