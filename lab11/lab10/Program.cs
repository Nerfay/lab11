using System.Diagnostics;

namespace lab10;

internal class Program
{
    public static async Task Main(string[] args)
    {
        Person[] persons = new Person[1000000];

        TaskManager taskManager = new TaskManager();
        var taskInfo = taskManager.Generate(persons);

        await taskInfo;
        
        // Lab 10
        // Sort in one thread
        /*Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        var comparer =
            new PersonComparer("Name");
        Array.Sort(persons, comparer);
        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);
        
        stopwatch.Reset();
        stopwatch.Start();*/
        // Sort in some threads
        /*taskInfo = taskManager.ParallelSort(persons, 4);

        await taskInfo;
        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);*/
        
        // Lab 11
        FilterPersons filter = new FilterPersons();

        var result = filter.GetGenderGroupsByName(persons, "a", 15);
        
        
    }
}