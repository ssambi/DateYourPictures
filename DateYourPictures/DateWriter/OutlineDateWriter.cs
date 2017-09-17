using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatePicture.DateWriter
{
    public class OutlineDateWriter : IDateWriter
    {
        public void WriteDate(Image img, string formattedDate)
        {
            using (Graphics g = Graphics.FromImage(img))
            using (StringFormat stringFormat = new StringFormat())
            using (GraphicsPath gp = new GraphicsPath())
            {
                // the font size is proportional to the image size
                double fontSizePx = Math.Max(img.Height, img.Width) * 0.02;

                // date in the bottom right
                stringFormat.Alignment = StringAlignment.Far;
                stringFormat.LineAlignment = StringAlignment.Far;

                // inner color
                Brush innerBrush = new SolidBrush(Color.FromArgb(253, 161, 0));
                // outline color
                Brush outlineBrush = Brushes.Black;

                Pen outlinePen = new Pen(outlineBrush, (int)(fontSizePx / 10));
                outlinePen.LineJoin = LineJoin.Round;

                // padding
                int padding = (int)fontSizePx * 3;
                Rectangle rectangle = new Rectangle(0, 0, img.Width - padding, img.Height - padding);

                gp.AddString(formattedDate, 
                    FontFamily.GenericSansSerif, 
                    (int)FontStyle.Regular, 
                    (float)fontSizePx, 
                    rectangle, 
                    stringFormat);

                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.DrawPath(outlinePen, gp);
                g.FillPath(innerBrush, gp);
            }
        }
    }
}
