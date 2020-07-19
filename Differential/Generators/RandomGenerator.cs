using System;
using System.Collections.Generic;
using Differential.Accessors;
using Differential.Dto;
using System.Linq;

namespace Differential.Generators
{
    public static class RandomGenerator
    {
        private static string SettingPath = "./Generators/RamdomGeneratorSetting.csv";

        private static IList<double> TiltDistribution;

        private static ValueDto Before = new ValueDto() { CreateOn = DateTime.Now, Value = new Random().NextDouble() * 100 };


        public static void Initialize()
        {
            var fileAccessor = new FileAccessor(SettingPath);
            TiltDistribution = (from e in fileAccessor.Read()
                                select double.Parse(e[1])).ToList();
        }

        public static ValueDto CreateValue()
        {
            var next = new ValueDto() { CreateOn = DateTime.Now, Value = GenerateValue() };
            UpdateBefore(next);
            return next;
        }

        private static double GenerateValue()
        {
            return Before.Value * TiltDistribution[new Random().Next(0, 100)];
        }

        private static void UpdateBefore(ValueDto after)
        {
            Before.CreateOn = after.CreateOn;
            Before.Value = after.Value;
        }

    }
}
