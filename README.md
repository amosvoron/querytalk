# QueryTalk - for a better coding experience

QueryTalk is a lightweight DAL for .NET and SQL Server easy to learn and use. 

## Idea

QueryTalk was developed with a purpose to be simple and efficient DAL for .NET. The developer should write as few lines of code as possible and should not repeat actions that have already been done when the data model in the database was implemented. For that reason, QueryTalk does not require any entity modelling. Furthermore, the storage (entity) classes are created behind the scene and stored in the separate DLL file. So the application code is clean and the developer can focus on real job - on *querying*.

## Summary

### Semantic data access layer

QueryTalk is a <strong>semantic data access layer</strong> introducing human-readable instructions:

    s.City.WhichHas(d.Most, s.Company)

QueryTalk is also an <strong>ORM framework</strong> using the mapping data to facilitate data manipulation in your programming code. 

### Code less, query with ease

<strong>QueryTalk is simple.</strong> There is no XML settings, no modelling, no contexts. 
<strong>QueryTalk is less code.</strong> In most cases, single instructions or even a single-line of code will do the job. 
<strong>QueryTalk is powerful.</strong> It offers plenty of options: semantic querying, SQL querying, a mix of both, and CRUD instructions.

### Test immediately

Test your queries on the spot, exactly where they are created:

    s.Person.Whose(s.Person.Age, 40)
        .Test()
        .Go();

### Map smarter

Use the mapper application (QueryTalker) to map your databases – fast and efficiently. 
<strong>No interference</strong> between the mapping code and the programming code. 
The mapping files are stored as compiled .dll files in your QueryTalk repository.

### Comparison Example

Entity Framework:

    using (var contect = new QueryTalkEntities()) 
    {
        var query = context.Person
            .Where(a => a.PersonJob
                .Where(b => b.Job != null).Any());
    }

QueryTalk:

    var query = s.Person.WhichHas(s.Job);

## License

MIT





