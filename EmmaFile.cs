using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ExcelDataReader;

namespace MikeyFriedChicken.EmmaExportToBankImport
{
    public  class EmmaFile
    {
        public Dictionary<string, List<List<string>>> GroupedByAccount { get; private set; }
        public Dictionary<string, int>  Mapping { get; private set; }

        private readonly string _filePath;


        public EmmaFile(string filePath)
        {
            _filePath = filePath;
        }

        public void Process()
        {
            var dataSet = GetDataSet(_filePath);
            var data = GetAsListofLists(dataSet.Tables[0]);
            Mapping = GetColumnMapping(data);
            GroupedByAccount = GroupByAccount(data, Mapping);
        }

        private static Dictionary<string, List<List<string>>> GroupByAccount(List<List<string>> data, Dictionary<string, int> mapping)
        {
            Dictionary<string, List<List<string>>> groupedByAccount = new Dictionary<string, List<List<string>>>();
            foreach (var row in data.Skip(1))
            {
                var accountName = row[mapping["Account"]].ToUpper().Trim();
                if (!groupedByAccount.TryGetValue(accountName, out var grouped))
                {
                    grouped = new List<List<string>>();
                    groupedByAccount.Add(accountName, grouped);
                }

                grouped.Add(row);
            }

            return groupedByAccount;
        }

        private static Dictionary<string, int> GetColumnMapping(List<List<string>> data)
        {
            var headerRow = data.First();
            var i = 0;
            var mapping = headerRow.ToDictionary(fieldName => fieldName.ToString(), fieldName => i++);
            return mapping;
        }

        private static List<List<string>> GetAsListofLists(DataTable firstTable)
        {
            List<List<string>> data = firstTable
                .Rows
                .Cast<DataRow>()
                .Select(row => row.ItemArray
                    .Select(fieldData => fieldData.ToString())
                    .ToList())
                .ToList();
            return data;
        }

        private static DataSet GetDataSet(string filePath)
        {
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);
            DataSet result = reader.AsDataSet();
            return result;
        }
    }
}