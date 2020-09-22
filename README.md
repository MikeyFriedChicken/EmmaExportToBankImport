# Emma Export To Bank Import
Takes the exported data from https://emma-app.com/ and produces OFX files suitable for importing into finance software

# Usuage
Change the following lines in Program.cs

 ```csharp
var inputFilePath = "D:\\Users\\Michael\\Downloads\\emma-export-11678-2020-09-22T10_24_18+00_00-19584.xlsx";
var outputPath = "D:\\Users\\Michael\\Downloads\\";
var from = new DateTime(2019, 12, 20);
var to = DateTime.MaxValue;
