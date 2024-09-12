using System.Collections;
interface IDatabaseRepository<T> {
    public void Store(T record, string file);
    public IEnumerable<T> Read(int limit, string file){
        //can we replace int limit with int? limit = null
        //and not have to return a list??
        var errorList = new ArrayList();
        errorList.Add("Hov, der gik noget helt galt!");
        return (IEnumerable<T>) errorList;
    } 
}
