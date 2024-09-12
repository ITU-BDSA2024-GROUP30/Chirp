using System.Globalization;
using CsvHelper;
namespace SimpleDB;
 
sealed class CSVDatabase<T> : IDatabaseRepository<T> {
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
}