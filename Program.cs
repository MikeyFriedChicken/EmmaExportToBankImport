using System;
using System.IO;
using System.Text;

namespace MikeyFriedChicken.EmmaExportToBankImport
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting...");

                // Arguments
                var inputFilePath = "D:\\Users\\Michael\\Downloads\\emma-export-11678-2020-09-22T10_24_18+00_00-19584.xlsx";
                var outputPath = "D:\\Users\\Michael\\Downloads\\";
                var from = new DateTime(2019, 12, 20);
                var to = DateTime.MaxValue;

                // Process the email input file
                var filePrefix = Path.GetFileNameWithoutExtension(inputFilePath);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var emmaFile = new EmmaFile(inputFilePath);
                emmaFile.Process();

                // Write an OFX file for each account
                foreach (var accountData in emmaFile.GroupedByAccount)
                {
                    OfxCreator.CreateOfxFile(accountData.Key, accountData.Value, emmaFile.Mapping, outputPath, filePrefix,
                        from, to);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }
}
