using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.IO;
using DateYourPictures.DateProvider;

namespace DateYourPictures.Tests
{
    [TestClass]
    public class ExifNetDateProviderTest
    {
        [TestMethod]
        public void TestGetDate_Picture()
        {
            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);


            ExifNetDateProvider dateProvider = new ExifNetDateProvider();
            DateTime date = dateProvider.GetDate(Path.Combine(basePath, "resources/picture.jpg"));
            Assert.AreEqual(new DateTime(2016, 7, 10), date.Date);
        }

        [TestMethod]
        public void TestGetDate_NoPicture()
        {
            string basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);


            ExifNetDateProvider dateProvider = new ExifNetDateProvider();
            DateTime date = dateProvider.GetDate(Path.Combine(basePath, "resources/nopicture.jpg"));
            Assert.AreEqual(new DateTime(2016, 10, 15), date.Date);
        }
    }
}
