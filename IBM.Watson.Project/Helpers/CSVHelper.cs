using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBM.Watson.Project.Helpers
{
    public static class CSVHelper
    {
        public static void ExporToCSV(string filepath, string filename, List<List<String>> output)
        {
            if (!filename.EndsWith(".csv"))
            {
                filename = filename + ".csv";
            }
            string filePath = filepath + "\\"+filename;

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            string delimter = ",";

            int length = output.Count;

            using (System.IO.TextWriter writer = File.CreateText(filePath))
            {
                for (int index = 0; index < length; index++)
                {
                    writer.WriteLine(string.Join(delimter, output[index]));
                }
            }
        }
    }
}
