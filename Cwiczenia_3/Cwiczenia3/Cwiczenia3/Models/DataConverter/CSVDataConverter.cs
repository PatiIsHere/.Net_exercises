using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia3.Models.DataConverter
{
    class CSVDataConverter : IStudentDataConverter
    {
        public List<string[]> ConvertDataSetForStudentExtensionRecreation(string FilePath)
        {
            var outputList = new List<string[]>();

            if (!File.Exists(FilePath) && new FileInfo(FilePath).Length != 0)
            {
                return outputList; //return empty list if file is not found or is empty
            }
            
            using (StreamReader sr = new StreamReader(FilePath))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');
                    outputList.Add(fields);

                }
            }

            return outputList;
        }
    }
}
