using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateYourPictures.ImageAdjuster
{
    public interface IImageAdjuster
    {
        void Adjust(string imgPath, Image img);
    }
}
