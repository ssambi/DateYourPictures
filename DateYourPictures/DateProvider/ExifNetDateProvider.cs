using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateYourPictures.DateProvider
{
    public class ExifNetDateProvider : IDateProvider
    {
        public DateTime GetDate(string picturePath)
        {
            ExifNET.Exif exif = new ExifNET.Exif(picturePath);
            if (exif.DateTimeOriginal.HasValue)
            {
                return exif.DateTimeOriginal.Value;
            }

            // if the EXIF date is not found, return the file date
            FileInfo fi = new FileInfo(picturePath);
            return fi.LastWriteTime;
        }
    }
}
