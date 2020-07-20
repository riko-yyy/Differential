using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Differential.Models;

namespace Differential.Context
{
    public class DifferentialContext
    {
        private string Path = "./Storages/";
        private string Extension = ".csv";
        private string Delimiter = ",";

        public IEnumerable<Sequence> Sequences { get; set; }


        public DifferentialContext()
        {
            Sequences = Read<Sequence>(nameof(Sequences));

        }

        private IEnumerable<T> Read<T>(string fileName) where T : new()
        {
            using (StreamReader sr = new StreamReader($"{Path}{fileName}{Extension}"))
            {
                var res = new T();
                var header = sr.ReadLine().Split(Delimiter);
                var maps = GetFieldMap(header);

                while (sr.EndOfStream == false)
                {
                    string[] fields = sr.ReadLine().Split(Delimiter);
                    foreach (var map in maps)
                    {
                        var fieldValue = typeof(T).GetProperty(map.Item2).GetValue(res);
                        var converter = TypeDescriptor.GetConverter(fieldValue.GetType());                      
                        typeof(T).GetProperty(map.Item2).SetValue(res, converter.ConvertFromInvariantString(fields[map.Item1]));
                    }                  
                    yield return res;
                }
            }
        }

        private IEnumerable<Tuple<int, string>> GetFieldMap(string[] header)
        {
            int counter = 0;
            foreach (var field in header)
            {
                yield return new Tuple<int, string>(counter, field);
                counter++;
            }
        }
    }
}
