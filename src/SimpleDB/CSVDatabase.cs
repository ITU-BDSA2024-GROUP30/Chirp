using System.Globalization;
using CsvHelper;
using Chirp.Cli;
using CsvHelper.Configuration.Attributes;

namespace Chirp.Cli.SimpleDB;
 
public sealed class CSVDatabase<T> : IDatabaseRepository<T> {
    private CSVDatabase() { }
    
    private static CSVDatabase<T> csvInstance = null!;

    public static CSVDatabase<T> getInstance()
    {
        if (csvInstance == null)
        {
            csvInstance = new CSVDatabase<T>();
        }
        return csvInstance;
    }
    public IEnumerable<T> Read(int limit, string file) {
        IEnumerable<T> List; 
        using (var reader = new StreamReader(file))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
            var records = csv.GetRecords<T>().Take(limit);
            List = records.ToList();
        }
        return (IEnumerable<T>) List; 
    }
    public void Store(T record, string file){
        using var writer = new StreamWriter(file, true);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        writer.Write("\n");
        csv.WriteRecord(record);
    }
    
}