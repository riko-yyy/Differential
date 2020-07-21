using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Differential.Context
{
    public class CsvSet<TEntity> : IEnumerable<TEntity>, IEnumerable where TEntity : new()
    {
        private readonly string Path;
        private readonly string Extension;
        private readonly string Delimiter;
        private readonly string FileName = SingularToPlural.ConvertToPlural(typeof(TEntity).Name);

        private IEnumerable<Tuple<int, string>> Maps;

        public CsvSet(string path, string extension, string delimiter)
        {
            this.Path = path;
            this.Extension = extension;
            this.Delimiter = delimiter;
            this.SetMaps();
        }

        private StreamReader GetConnectionForRead()
        {
            return new StreamReader($"{Path}{FileName}{Extension}");
        }

        private StreamWriter GetConnectionForWrite()
        {
            return new StreamWriter($"{Path}{FileName}{Extension}", true);
        }

        private void SetMaps()
        {
            Maps = this.GetFieldMap();
        }

        private IEnumerable<Tuple<int, string>> GetFieldMap()
        {
            int counter = 0;
            foreach (var field in this.GetHeader())
            {
                yield return new Tuple<int, string>(counter, field);
                counter++;
            }
        }

        private string[] GetHeader()
        {
            string[] header;
            using (StreamReader sr = GetConnectionForRead())
            {
                header = sr.ReadLine().Split(Delimiter);
            }
            return header;
        }

        public IEnumerable<TEntity> ReadAll()
        {
            using (StreamReader sr = GetConnectionForRead())
            {
                var res = new TEntity();

                //ヘッダを捨てる
                sr.ReadLine();

                while (sr.EndOfStream == false)
                {
                    string[] fields = sr.ReadLine().Split(Delimiter);
                    foreach (var map in Maps)
                    {
                        var fieldValue = typeof(TEntity).GetProperty(map.Item2).GetValue(res);
                        var converter = TypeDescriptor.GetConverter(fieldValue.GetType());
                        typeof(TEntity).GetProperty(map.Item2).SetValue(res, converter.ConvertFromInvariantString(fields[map.Item1]));
                    }
                    yield return res;
                }
            }
        }

        public TEntity Add(TEntity item)
        {
            using (StreamWriter sw = GetConnectionForWrite())
            {
                var holder = new List<string>();
                foreach (var map in Maps.OrderBy(v => v.Item1))
                {
                    holder.Add(typeof(TEntity).GetProperty(map.Item2).GetValue(item).ToString());
                }

                sw.WriteLine($"{string.Join(Delimiter, holder)}");
            }
            return item;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return this.ReadAll().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
