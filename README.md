# QueryTalk - for a better coding experience

QueryTalk is a lightweight DAL for .NET and SQL Server extremely easy to learn and use. 

##Motivation

The Entity Framework has never convinced me. It's complicated. Why to do double work - first to make data model in the database and then to create entities? Is it really necessary to have entities inside the code? It's quite a lot of additional code, isn't it?   

QueryTalk also creates "entities" but behind the scene, in the separate DLL file. Supported by semantic querying and handy testing environment the developer can be now more focused on querying, "on real job". 

##Summary

###Semantic data access layer

QueryTalk is a <strong>semantic data access layer</strong> introducing human-readable instructions:

    s.City.WhichHas(d.Most, s.Company)

QueryTalk is also an <strong>ORM framework</strong> using the mapping data to facilitate data manipulation in your programming code. 

###Code less, query with ease

<strong>QueryTalk is simple.</strong> There is no XML settings, no modelling, no contexts. 
<strong>QueryTalk is less code.</strong> In most cases, single instructions or even a single-line of code will do the job. 
<strong>QueryTalk is powerful.</strong> It offers plenty of options: semantic querying, SQL querying, a mix of both, and CRUD instructions.

###Test immediately

Test your queries on the spot, exactly where they are created:

    s.Person.Whose(s.Person.Age, 40)
        .Test()
        .Go();

###Map smarter

Use the mapper application (QueryTalker) to map your databases – fast and efficiently. 
<strong>No interference</strong> between the mapping code and the programming code. 
The mapping files are stored as compiled .dll files in your QueryTalk repository.

###Comparison Example

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

Apache





