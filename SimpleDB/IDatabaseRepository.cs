using System.Collections;
interface IDatabaseRepository<T> {
    public void Store(T record, string file){
        /*var author = Environment.UserName; // UserName or UserDomainName for device name
        var message = String.Join(" ", Program.args[1..]);
        var currentTimestamp = DateTimeOffset.Now; //only gets the current time.
        long timestamp = currentTimestamp.ToUnixTimeSeconds();

        var hello = new Parsing { Author = author, Message = message, Timestamp = timestamp };

        using (var writer = new StreamWriter(file, true))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) {
            writer.Write("\n");
            csv.WriteRecord(hello);
        }*/
    }
    public IEnumerable<T> Read(int limit, string file){
        /*List<Parsing> cheepList;
        using (var reader = new StreamReader(file))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
                var records = csv.GetRecords<Parsing>();
                cheepList = records.ToList();
        }

        return cheepList;*/
        //return null;
        var errorList = new ArrayList();
        errorList.Add("Hov, der gik noget helt galt!");
        return (IEnumerable<T>) errorList;
    } 
    

    // public IEnumerable<T> Read(int? limit = null) { //write this properly
}
