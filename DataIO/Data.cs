using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DataIO
{
    public class Data : List<List<double>>
    {
        public List<string> Labels { get; set; } = new List<string>(); 

        public void ExportCsv(string filePath)
        {
            var csv = new StringBuilder();
            var line = string.Join(";", Labels);
            csv.AppendLine(line); 

            foreach(var datum in this)
            {
                line = string.Join(";", datum.Select(d => d.ToString(CultureInfo.InvariantCulture)));
                csv.AppendLine(line); 
            }

            File.WriteAllText(filePath, csv.ToString());
        }
    }
}
