using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PwSaveController
{
    public class FileHelper: IFileHelper
    {

        public List<string> GetRowsFromFile(string filePath)
        {
            var lines = new List<string>();

            if(File.Exists(filePath))
            {
                using(StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    StringBuilder content = new StringBuilder();
                    while(!sr.EndOfStream)
                    {
                        lines.Add(sr.ReadLine());
                    }
                }
            }

            return lines;
        }

        public bool WriteToFile(string filePath, List<string> lines)
        {
            using(var sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach(var line in lines)
                {
                    sw.WriteLine(line);
                }
            }

            return true;
        }


        public void Dispose()
        { 
        }

    }
}
