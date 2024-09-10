using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper.Configuration.Attributes;
using System;

namespace SimpleDB;
 

interface IDatabaseRepository<T>
    {
    public IEnumerable<T> Read(int? limit = null);
    public void Store(T record);
    }

sealed class CSVDatabase<T> : IDatabaseRepository<T>
{
    //skal have de to metoder med.. skal laves om og bruges her mener jeg..
    IEnumerable<T> Read();

    Store(T record);
    

}

//public record Cheep(string Author, string Message, long Timestamp);
//IDatabaseRepository<Cheep> database = new CSVDatabase<Cheep>();

//var cheepTest1 = new Cheep("Henri", "Hello MAMA!", DateTimeOffset.UtcNow.ToUnixTimeSeconds());
//database.Store(cheepTest1);


//1. alt hvad der er i Program.cs nu skal refaktoriseres herind(e)
//2. går udfra program.cs skal bare have en main run methode så?
//3. vi skal lave Read()
//4. vi skal lave Store()
//5. når det hele spiller skal vi se om vi kan få referencer til at virke på main 
//så vi kan reade og store til vores database fra main, så det bliver et "slim" projekt
