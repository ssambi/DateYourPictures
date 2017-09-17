using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatePicture.DateWriter
{
    public class SimpleDateWriter : IDateWriter
    {
        public void WriteDate(Image img, string formattedDate)
        {
            using (Graphics graphics = Graphics.FromImage(img))
            {
                using (Font font = new Font("Arial", 20, FontStyle.Regular))
                {
                    // calculate the date offset (bottom right)
                    const double BottomPerc = 0.1;
                    const double RightPerc = 0.3;
                    int x = (int)(img.Width * (1.0 - RightPerc));
                    int y = (int)(img.Height * (1.0 - BottomPerc));

                    // color
                    Brush brush = new SolidBrush(Color.FromArgb(253, 161, 0));

                    // write the date
                    graphics.DrawString(formattedDate, font, brush, x, y);


                }
            }
        }
    }
}
