using System;
using System.IO;

namespace FileAndDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = "../../../NewFolder";
            string text = "Hello World! I'm a dummy text.";

            WriteFileWithDirectory(folderPath, text);

            ReadAllTextByFile("../../../NewFolder/23-04-2022.txt");
        }

        /*
            Create And Write File With Different Way
         */
        private static void WriteFileWithDirectory(string folderPath, string text)
        {
            DirectoryInfo directoryPath = new(folderPath);
            if (!directoryPath.Exists)
            {
                directoryPath.Create();
            }

            string fileName = $"{directoryPath.FullName}/{DateTime.Now:dd-MM-yyyy}.txt";

            FileInfo filePath = new(fileName);

            if (!filePath.Exists)
            {
                using FileStream stream = filePath.Create();
            }

            FileStream fileStream =  File.Open(fileName, FileMode.Append);
            fileStream.Write(System.Text.Encoding.UTF8.GetBytes(text + Environment.NewLine));
            fileStream.Flush();
            fileStream.Close();
        }

        private static void CreateAllTextByFile(string filePath, string text)
        {
            if (!File.Exists(filePath))
            {
                using FileStream file = File.Create(filePath);
            }

            using FileStream stream = File.Open(filePath, FileMode.Append);
            stream.Write(System.Text.Encoding.UTF8.GetBytes(text + Environment.NewLine));
            stream.Flush();
            stream.Close();
        }

        private static void CreateAllTextByFileInfo(string filePath, string text)
        {
            FileInfo fileInfo = new(filePath);

            if (fileInfo.Exists)
            {
                using FileStream stream = fileInfo.Create();
            }

            using FileStream fileStream = File.Open(filePath, FileMode.Append);
            fileStream.Write(System.Text.Encoding.UTF8.GetBytes(text + Environment.NewLine));
            fileStream.Flush();
            fileStream.Close();
        }

        /*
            Read from File With Different Way
         */
        private static void ReadAllTextByFile(string path)
        {
            /*
                 //Way 1
                 string text = File.ReadAllText(path);
                 Console.WriteLine(text);
            */

            using FileStream stream = File.Open(path, FileMode.Open);
            long length = stream.Length;

            /*
                //Way 2
                byte[] buffer = new byte[length];
                stream.Read(buffer);
                string text = System.Text.Encoding.UTF8.GetString(buffer);
                Console.WriteLine(text);
            */

            for (int i = 0; i < length; i++)
            {
                byte[] buffer = new byte[1];
                stream.Read(buffer);
                string s = System.Text.Encoding.UTF8.GetString(buffer);
                Console.Write(s);
            }
        }

        private static void ReadAllTextByFileInfo(string file)
        {
            FileInfo fileInfo = new(file);

            if (fileInfo.Exists)
            {
                long length = fileInfo.Length;
                string fullName = fileInfo.FullName;
                string shortName = fileInfo.Name;
                DateTime time = fileInfo.LastWriteTime;

                using FileStream fileStream = File.Open(file, FileMode.Open);

                byte[] buffer = new byte[length];
                fileStream.Read(buffer);

                string text = System.Text.Encoding.UTF8.GetString(buffer);
                Console.WriteLine(text);
            }
        }
    }
}
