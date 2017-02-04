namespace Klockmann.Parsing.Csv
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class CsvUtils
    {
        public static bool IsValidCsv(string filepath, char separator = ';')
        {
            var lines = File.ReadAllLines(filepath);
            return IsValidCsv(lines, separator);
        }

        public static bool IsValidCsv(string[] lines, char separator = ';')
        {
            if (lines.Length < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(lines), "There are no lines given!");
            }

            var headers = lines[0].Split(separator);

            for (var i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(separator);

                if (headers.Length == parts.Length)
                {
                    continue;
                }

                Trace.TraceError($"Line {i - 1} contains {parts.Length} separators, expected {headers.Length}");
                return false;
            }

            return true;
        }

        public static IEnumerable<TResult> Import<TResult>(string filepath, char separator = ';')
        {
            var lines = File.ReadAllLines(filepath);
            return Import<TResult>(lines, separator);
        }

        public static IEnumerable<TResult> Import<TResult>(string[] lines, char separator = ';')
        {
            if (!IsValidCsv(lines, separator))
            {
                throw new ArgumentOutOfRangeException(nameof(lines), "No valid csv given!");
            }

            var type = typeof(TResult);
            var result = new List<TResult>();
            var headers = lines[0].Split(separator);

            for (var i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(separator);
                var obj = (TResult) type.GetConstructor(new Type[0])?.Invoke(new object[0]);

                if (obj == null)
                {
                    throw new ArgumentException($"The given Type \"{type.Name}\" has no public constructor that takes no argument!");
                }

                for (var j = 0; j < parts.Length; j++)
                {
                    PropertyInfo property = null;
                    bool ignore = false;

                    foreach (var prop in type.GetProperties())
                    {
                        var csvHeader = prop.GetCustomAttribute<CsvHeaderAttribute>();

                        if (csvHeader != null && csvHeader.HeaderExport != string.Empty && headers[j] == csvHeader.HeaderExport)
                        {
                            property = prop;
                        }
                        else if (headers[j] == prop.Name)
                        {
                            property = prop;
                        }
                        else
                        {
                            continue;
                        }

                        var csvIgnore = prop.GetCustomAttribute<CsvIgnoreAttribute>();
                        ignore = csvIgnore != null && csvIgnore.IgnoreImport;
                    }

                    if (ignore)
                    {
                        continue;
                    }

                    if (property == null)
                    {
                        if (i == 1)
                        {
                            Trace.TraceWarning($"There is no matching property in type \"{type.Name}\" to header \"{headers[j]}\"!");
                        }
                        continue;
                    }

                    // var prop = type.GetProperty(headers[j]);

                    if (property.PropertyType == typeof(string))
                    {
                        property.SetValue(obj, parts[j]);
                    }
                    else if (property.PropertyType == typeof(int))
                    {
                        int value;
                        int.TryParse(parts[j], out value);
                        property.SetValue(obj, value);
                    }
                    else if (property.PropertyType == typeof(float))
                    {
                        float value;
                        float.TryParse(parts[j], out value);
                        property.SetValue(obj, value);
                    }
                }

                result.Add(obj);
            }

            return result;
        }

        public static void Export<TValue>(IEnumerable<TValue> values, string filepath, char separator = ';')
        {
            File.WriteAllLines(filepath, Export(values, separator));
        }

        public static string[] Export<TValue>(IEnumerable<TValue> values, char separator = ';')
        {
            string[] lines;
            Export(values, out lines, separator);
            return lines;
        }

        public static void Export<TValue>(IEnumerable<TValue> values, out string[] lines, char separator = ';')
        {
            var props = typeof(TValue).GetProperties().Where(
                prop =>
                {
                    var csvIgnore = prop.GetCustomAttribute<CsvIgnoreAttribute>();
                    return csvIgnore == null || !csvIgnore.IgnoreExport;
                });

            lines = new string[values.Count() + 1];

            foreach (var prop in props)
            {
                var csvHeader = prop.GetCustomAttribute<CsvHeaderAttribute>();

                if (csvHeader != null && csvHeader.HeaderExport != string.Empty)
                {
                    lines[0] += csvHeader.HeaderExport.Replace(separator, ',') + separator;
                }
                else
                {
                    lines[0] += prop.Name + separator;
                }

                for (var i = 0; i < values.Count(); i++)
                {
                    lines[i + 1] += prop.GetValue(values.ElementAt(i)).ToString() + separator;
                }
            }

            for (var i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Substring(0, lines[i].Length - 1);
            }
        }
    }
}