using System;
using System.Collections.Generic;
using System.IO;

namespace Differential.Accessors
{
    public class FileAccessor
    {
        private string Path;

        public FileAccessor(string path)
        {
            Path = path;
        }


        public IEnumerable<string[]> Read()
        {
            using (StreamReader sr = new StreamReader(Path))
            {
                while (sr.EndOfStream == false)
                {
                    string line = sr.ReadLine();
                    string[] fields = line.Split(',');
                    yield return fields;
                }
            }
        }

    }
}
