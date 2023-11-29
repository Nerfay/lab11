using System.Text;

namespace lab10;

public class TaskManager
{
    public async Task Generate(Person[] persons)
    {
        Task[] tasks = new Task[4];
        var countItems = persons.Length / 4;
        for (var i = 0; i < tasks.Length; i++)
        {
            var startIndex = i * countItems;
            var endIndex = (i == 3) ? persons.Length : (i + 1) * countItems;

            tasks[i] = GeneratePersons(persons, startIndex, endIndex);
        }

        await Task.WhenAll(tasks);
    }

    private Task GeneratePersons(Person[] persons, int startIndex, int endIndex)
    {
        for (var i = startIndex; i < endIndex; i++)
        {
            persons[i] = new Person()
            {
                Id = i,
                Name = GenerateName(),
                Age = Random.Shared.Next(1, 100),
                Gender = Random.Shared.Next(1, 3)
            };
        }
        
        return Task.CompletedTask;
    }

    private string GenerateName()
    {
        int length = Random.Shared.Next(3, 10);
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < length; i++)
        {
            builder.Append((char)Random.Shared.Next(97, 123));
        }

        builder[0] = char.ToUpper(builder[0]);
        return builder.ToString();
    }

    public async Task ParallelSort(Person[] persons, int countTasks)
    {
        var comparer =
            new PersonComparer("Id");
        Task[] tasks = new Task[countTasks];
        var countItems = persons.Length / countTasks;
        for (var i = 0; i < tasks.Length; i++)
        {
            var startIndex = i * countItems;
            var endIndex = (i == 3) ? persons.Length : (i + 1) * countItems;

            tasks[i] = Sort(persons, startIndex, endIndex, comparer);
        }
        
        await Task.WhenAll(tasks);

        MergeArray(persons, comparer);
    }

    private void MergeArray(Person[] persons, PersonComparer comparer)
    {
        Array.Sort(persons, comparer);
    }

    private Task Sort(Person[] persons, int startIndex, int endIndex, PersonComparer comparer)
    {
        Span<Person> segment = persons.AsSpan(startIndex, endIndex - startIndex);
        segment.Sort(comparer);

        return Task.CompletedTask;
    }
}