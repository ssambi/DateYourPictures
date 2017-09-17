using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateYourPictures.ImageAdjuster
{
    public class OrientationImageAdjuster : IImageAdjuster
    {
        public void Adjust(string imgPath, Image img)
        {
            ExifNET.Exif exif = new ExifNET.Exif(imgPath);
            switch (exif.Orientation)
            {
                case ExifNET.Models.Types.Orientation.Rotate90:
                    Rotate(img, RotateFlipType.Rotate90FlipNone);
                    break;
                case ExifNET.Models.Types.Orientation.Rotate180:
                    Rotate(img, RotateFlipType.Rotate180FlipNone);
                    break;
                case ExifNET.Models.Types.Orientation.Rotate270:
                    Rotate(img, RotateFlipType.Rotate270FlipNone);
                    break;

                case ExifNET.Models.Types.Orientation.Normal:
                default:
                    break;
            }
        }

        private void Rotate(Image img, RotateFlipType rotateFlip)
        {
            img.RotateFlip(rotateFlip);
        }
    }
}
