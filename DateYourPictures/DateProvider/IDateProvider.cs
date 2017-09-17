using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateYourPictures.DateProvider
{
    public interface IDateProvider
    {
        DateTime GetDate(string picturePath);
    }
}
