
using AccoutingApplication;

string inputFilePath = @"/Users/haseebahmed/Desktop/Haseeb Data/Engineering System Concern Data/VAT July-Sept 2024/data.csv";
string outputFilePath = @"/Users/haseebahmed/Desktop/Haseeb Data/Engineering System Concern Data/VAT July-Sept 2024/formatted.csv";

FormatCSV obj = new FormatCSV();
obj.FormatCSVAsPerCapium(inputFilePath, outputFilePath);