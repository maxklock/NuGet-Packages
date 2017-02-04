namespace Klockmann.Parsing.Csv
{
    using System;

    public class CsvHeaderAttribute : Attribute
    {
        public string HeaderImport { get; set; }

        public string HeaderExport { get; set; }

        public CsvHeaderAttribute()
        {
            HeaderImport = string.Empty;
            HeaderExport = string.Empty;
        }

        public CsvHeaderAttribute(string header) : this()
        {
            HeaderImport = header;
            HeaderExport = header;
        }
    }
}