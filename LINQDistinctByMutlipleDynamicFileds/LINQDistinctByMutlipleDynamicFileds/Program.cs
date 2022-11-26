using LINQDistinctByMutlipleDynamicFields;

Console.WriteLine("LINQDistinctByMutlipleDynamicFields");

var personList = new List<Person>()
{
    new Person() { Name =  "Narendra",Age = 26, DateOfBirth = Convert.ToDateTime("20/09/1987")},
    new Person() {Name =  "Narendra",Age = 26, DateOfBirth = Convert.ToDateTime("20/09/1987")},
    new Person() { Name =  "Kiran",Age = 23, DateOfBirth = Convert.ToDateTime("04/07/1991")},
    new Person() {Name =  "Kiran", Age = 23, DateOfBirth = Convert.ToDateTime("03/07/1991")}
};

var propertNames = new List<string> { "Name", "DateOfBirth", "Age" };   

IEqualityComparer<Person> customComparer =
                   new PropertyComparer<Person>(propertNames);
IEnumerable<Person> distinctPersons = personList.Distinct(customComparer);

Console.WriteLine(distinctPersons.Count());
    
