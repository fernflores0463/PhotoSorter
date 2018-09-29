using System;
using System.Globalization;
using System.IO;

namespace PhotoSorter {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("All of your photos will be put in the folder 'organizedPhotos' on your Desktop\n");
            Console.WriteLine("Where is the source of your pictures? Please make sure it's on your Desktop");
            string source = Console.ReadLine();
            source = "/Users/fernando/Desktop/" + source;
            var organizer = new Organizer(source);
            organizer.OrganizePhotos();
        }
    }

    class Organizer {
        private string photoDirectory;

        public Organizer(string _photoDirectory) {
            this.photoDirectory = _photoDirectory;
        }

        public void OrganizePhotos() {
            var directory = new DirectoryInfo(this.photoDirectory);
            if (!directory.Exists) {
                Console.WriteLine("The directory you specified does not exist! Start Program Again!");
                System.Environment.Exit(1);
            }
            var newRootDir = new DirectoryInfo("/Users/fernando/Desktop/organizedPhotos");
            if (!newRootDir.Exists) {
                newRootDir.Create();
            }
            foreach (var photo in directory.GetFiles()) {
                var monthName = photo.CreationTimeUtc.ToString("MMMM", CultureInfo.InvariantCulture);
                var yearString = photo.CreationTimeUtc.Year.ToString();
                var newDir = new DirectoryInfo("/Users/fernando/Desktop/organizedPhotos/" + yearString + "/" + monthName);
                if (!newDir.Exists) {
                    newDir.Create();
                }
                else {
                    photo.MoveTo("/Users/fernando/Desktop/organizedPhotos/" + yearString + "/" + monthName + "/" + photo.Name);
                }
                Console.WriteLine(photo.Name);
//                Console.WriteLine(photo.CreationTimeUtc.ToString("MMMM", CultureInfo.InvariantCulture));
//                Console.WriteLine(photo.CreationTime.ToString("MMMM", CultureInfo.InvariantCulture));
            }
        }
    }
}