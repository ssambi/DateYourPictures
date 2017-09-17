using DateYourPictures.DateProvider;
using System;
using System.IO;
using Autofac;
using DatePicture;
using DatePicture.DateWriter;
using DateYourPictures.ImageAdjuster;

namespace DateYourPictures.Console
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            Initialize();

            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IDateWriter>();

                System.Console.WriteLine("Write the EXIF date to pictures");

                if (args.Length >= 2)
                {
                    string inputFolder = args[0];
                    string destinationFolder = args[1];

                    System.Console.WriteLine("Input Folder: " + inputFolder);
                    System.Console.WriteLine("Destination Folder: " + destinationFolder);

                    int writtenPictureCount = 0;
                    string[] allFiles = Directory.GetFiles(inputFolder);
                    foreach (var file in allFiles)
                    {
                        try
                        {
                            DateYourPictures datePicture = scope.Resolve<DateYourPictures>();
                            datePicture.AddDate(file, destinationFolder);

                            writtenPictureCount++;
                            System.Console.WriteLine("\tWritten date in picture {0}", file);
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine("Error adding date in picture {0}.\r\nException:{1}", file, ex.ToString());
                        }
                    }

                    System.Console.WriteLine("Date written in {0} images", writtenPictureCount);
                }
                else
                {
                    System.Console.WriteLine("Arguments: [input pictures folder ] [destination folder]");
                }

                System.Console.WriteLine("Press ENTER to continue");
                System.Console.ReadLine();
            }
        }

        private static void Initialize()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<ExifNetDateProvider>().As<IDateProvider>();
            builder.RegisterType<OutlineDateWriter>().As<IDateWriter>();
            builder.RegisterType<OrientationImageAdjuster>().As<IImageAdjuster>();
            builder.RegisterType<DateYourPictures>().As<DateYourPictures>();
            Container = builder.Build();
        }
    }
}
