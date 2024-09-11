using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper.Configuration.Attributes;
using System;

namespace SimpleDB;
 
sealed class CSVDatabase<T> : IDatabaseRepository<T>
{
    //skal have de to metoder med.. skal laves om og bruges her mener jeg..
    
    public string Author { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string Message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public long Timestamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IEnumerable<T> Read(int limit, string file) {
        IEnumerable<Cheep> cheepList; 
        using (var reader = new StreamReader(file))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
            var records = csv.GetRecords<Cheep>();
            cheepList = records.ToList();
        }
        return (IEnumerable<T>) cheepList; 
    }
    public void Store(T record, string file){

        using (var writer = new StreamWriter(file, true))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) {
            writer.Write("\n");
            csv.WriteRecord(record);
        }
    }
    /*public record Cheep{
       [Name("Author")][Index(0)]
        public required string Author { get; set; }
        [Name("Message")][Index(1)]
        public required string Message { get; set; }
        [Name("Timestamp")][Index(2)]
        public required long Timestamp { get; set; }

        //public static void cheep(string[] args){ // rename "cheep" to ex. formattedMessage (so it becomes more readable)
        //    storeToFile(args, "chirp_cli_db.csv");
        //    readFromFile("chirp_cli_db.csv");
        //}
    }*/
}
    

    //Store(T record);
    

//1. alt hvad der er i Program.cs nu skal refaktoriseres herind(e)
//2. går udfra program.cs skal bare have en main run methode så?
//3. vi skal lave Read()
//4. vi skal lave Store()
//5. når det hele spiller skal vi se om vi kan få referencer til at virke på main 
//så vi kan reade og store til vores database fra main, så det bliver et "slim" projekt
