using System.Globalization;
using CsvHelper;
namespace Chirp.Cli.SimpleDB;
 
sealed class CSVDatabase<T> : IDatabaseRepository<T> {
    //public string Author { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    //public string Message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    //public long Timestamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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