using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Differential.Models;

namespace Differential.Context
{
    public class DifferentialContext
    {
        private readonly static string Path = "./Storages/";
        private readonly static string Extension = ".csv";
        private readonly static string Delimiter = ",";

        public CsvSet<Sequence> Sequences = new CsvSet<Sequence>(Path, Extension, Delimiter);

    }
}
