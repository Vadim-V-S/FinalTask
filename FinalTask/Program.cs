using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    class Program
    {
        static Student[] students;
        static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        static string TargetsFolder = string.Format(@"{0}\Student",desktopPath);

        static void Main(string[] args)
        {
            string sourcePath = string.Format(@"{0}\Students.dat", desktopPath);

            using (var fs = new FileStream(sourcePath, FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                students = (Student[])formatter.Deserialize(fs);

                if (!Directory.Exists(TargetsFolder))
                {
                    Directory.CreateDirectory(TargetsFolder);
                }

                int i = 0;
                foreach (Student st in students)
                {
                    string targetPath = string.Format($"{TargetsFolder}\\{students[i].Group}.txt");
                    if (!File.Exists(targetPath))
                    {
                        using StreamWriter sw = File.CreateText(targetPath);
                    }
                    using (StreamWriter sw = File.AppendText(targetPath))
                    {
                        sw.WriteLine(string.Format($"{students[i].Name}, {students[i].DateOfBirth}"));
                    }
                    i++;
                }
            }
        }
    }
}
