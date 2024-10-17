using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;

namespace AccoutingApplication
{
    public class FormatCSV
    {

        public void FormatCSVAsPerCapium(string inputFilePath, string outputFilePath)
        {
            // Read the input CSV
            using (var reader = new StreamReader(inputFilePath))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                // Create the new CSV file for output
                using (var writer = new StreamWriter(outputFilePath))
                using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    // Read all records from the input CSV
                    var records = csvReader.GetRecords<InputRecord>().ToList();

                    // Write the headers for the output CSV
                    csvWriter.WriteHeader<OutputRecord>();
                    csvWriter.NextRecord();

                    int counter = 0;
                    // Process each record from the input CSV and write the output CSV
                    foreach (var record in records)
                    {
                        if (counter < records.Count - 1)
                        {
                            // Determine the type based on SubCategory
                            string type = string.Empty;
                            if (record.Subcategory == "Counter Credit" || record.Subcategory == "Cash Deposit" || record.Subcategory == "Credit Payment" || record.Subcategory == "Credit" || record.Subcategory == "Contactless Card Refund")
                            {
                                type = "CR";
                            }
                            else if (record.Subcategory == "Debit" || record.Subcategory == "Funds Transfer" || record.Subcategory == "Contactless Card Purchase" || record.Subcategory == "Direct Debit")
                            {
                                type = "DD";
                            }
                            else if (record.Subcategory == "Cash Withdrawal")
                            {
                                type = "ATM";
                            }


                            // Combine SubCategory and Memo for Description
                            string description = $"{record.Subcategory} {record.Memo}";

                            // Determine Paid Out or Paid In based on Amount
                            string paidOut = Convert.ToDecimal(record.Amount) < 0 ? Math.Abs(Convert.ToDecimal(record.Amount)).ToString() : string.Empty;
                            string paidIn = Convert.ToDecimal(record.Amount) > 0 ? record.Amount.ToString() : string.Empty;

                            // Create the output record
                            var outputRecord = new OutputRecord
                            {
                                Date = record.Date,
                                Type = type,
                                Description = description,
                                PaidOut = paidOut,
                                PaidIn = paidIn
                            };

                            // Write the output record to the new CSV
                            csvWriter.WriteRecord(outputRecord);
                            csvWriter.NextRecord();
                            counter++;
                        }

                    }
                }
            }
            Console.WriteLine("CSV file generated successfully!");
        }

    }

    // Class to map input CSV columns
    public class InputRecord
    {
        public string Number { get; set; }
        public string Date { get; set; }
        public string Account { get; set; }
        public string Amount { get; set; }
        public string Subcategory { get; set; }
        public string Memo { get; set; }
    }

    // Class to map output CSV columns
    public class OutputRecord
    {
        public string Date { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string PaidOut { get; set; }
        public string PaidIn { get; set; }
    }
}