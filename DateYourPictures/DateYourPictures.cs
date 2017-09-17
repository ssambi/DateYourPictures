using System;
using System.Drawing;
using System.IO;
using DateYourPictures.DateProvider;
using DatePicture.DateWriter;
using System.Collections.Generic;
using DateYourPictures.ImageAdjuster;
using System.Drawing.Imaging;
using System.Linq;

namespace DateYourPictures
{
    public class DateYourPictures
    {
        public IDateProvider DateProvider { get; private set; }
        public IDateWriter DateWriter { get; private set; }
        public IEnumerable<IImageAdjuster> ImageAdjusters { get; private set;}

        public DateYourPictures(IDateProvider dateProvider, IDateWriter dateWriter, IEnumerable<IImageAdjuster> imageAdjusters)
        {
            DateProvider = dateProvider;
            DateWriter = dateWriter;
            ImageAdjusters = imageAdjusters;
        }

        public void AddDate(string picturePath, string destinationFolder, string dateFormat = "dd/MM/yyyy")
        {
            DateTime date = DateProvider.GetDate(picturePath);

            string formattedDate = date.ToString(dateFormat);

            // use stream to avoid locking the file
            byte[] bytes = File.ReadAllBytes(picturePath);
            using (Stream inputStream = new MemoryStream(bytes))
            using (Bitmap bitmap = new Bitmap(inputStream))
            {
                foreach (var imgAdj in ImageAdjusters)
                {
                    imgAdj.Adjust(picturePath, bitmap);
                }

                DateWriter.WriteDate(bitmap, formattedDate);

                // create destination folder if it doesn't exist
                if (!Directory.Exists(destinationFolder))
                {
                    Directory.CreateDirectory(destinationFolder);
                }

                // save modified image in destination folder
                string destinationFile = Path.Combine(destinationFolder, Path.GetFileName(picturePath));
                EncoderParameters encoderParameters = new EncoderParameters();
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 95L);
                bitmap.Save(destinationFile,
                    ImageCodecInfo.GetImageEncoders().First(ie => ie.FormatID == ImageFormat.Jpeg.Guid),
                    encoderParameters);
            }
        }
    }
}

