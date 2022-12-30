using AnimalShelter.Entities;
using AnimalShelter.Repositories;
using AnimalShelter.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;
using AnimalShelter.Repositories.Extensions;
using System.Runtime.CompilerServices;

var dogRepository = new SqlRepository<Dog>(new AnimalShelterDbContext());
var employeeRepository = new SqlRepository<Employee>(new AnimalShelterDbContext());
{
    dogRepository.ItemAdded += DogRepositoryOnItemAdded;
    employeeRepository.ItemAdded += EmplyeeRepositoryOnItemAdded;
}

static void DogRepositoryOnItemAdded(object? sender, Dog d)
{
    Console.WriteLine($"Dog added => {d.Name} from {sender?.GetType().Name}");
}

static void DogRepositoryOnItemRemoved(object? sender, Dog d)
{
    Console.WriteLine($"Dog removed => {d.Name} from {sender?.GetType().Name}");
}

static void DogRepositoryMale(IRepository<Dog> dogRepository)
{
    var items = dogRepository.GetAll();
    foreach (var item in items)
    {
        if (item.Gender=="M")
            Console.WriteLine($"Dog Male => {item.Id}, {item.Name}");
    }
}

static void DogRepositoryFemale(IRepository<Dog> dogRepository)
{
    var items = dogRepository.GetAll();
    foreach (var item in items)
    {
        if (item.Gender=="F")
            Console.WriteLine($"Dog Female => {item.Id}, {item.Name}");
    }
}

AddDogs(dogRepository);

static void AddDogs(IRepository<Dog> dogRepository)
{
    var dogs = new[]
    {
        new Dog { Name = "Rex", Gender = "M" },
        new Dog { Name = "Suzi", Gender = "F" },
        new Dog { Name = "Perła", Gender = "F" },
    };
    dogRepository.AddBatch(dogs);
}

static void EmplyeeRepositoryOnItemAdded(object? sender, Employee e)
{
    Console.WriteLine($"Employee added => {e.Name} from {sender?.GetType().Name}");
}

AddEmployees(employeeRepository);

static void AddEmployees(IRepository<Employee> employeeRepository)
{
    var employees = new[]
    {
        new Employee { Name = "Jan", SurName = "Kowalski", Education = "Technik weterynarii" },
        new Employee { Name = "Anna", SurName = "Wolińska", Education = "Podstawowe" },
    };
    employeeRepository.AddBatch(employees);
}

var volunteerRepository = new SqlRepository<Volunteer>(new AnimalShelterDbContext());

AddVolunteers(volunteerRepository);
//WriteAllToConsole(volunteerRepository);
static void AddVolunteers(IRepository<Volunteer> volunteerRepository)
{
    var volunteers = new[]
    {
    new Volunteer {Name = "Urszula", SurName = "Wielicka", Education = "uczeń" },
    new Volunteer { Name = "Joanna", SurName = "Wielicka", Education = "wyższe" },
    };

    volunteerRepository.AddBatch(volunteers);
}

var foodRepository = new SqlRepository<Food>(new AnimalShelterDbContext());

AddFoods(foodRepository);
//WriteAllToConsole(foodRepository);
static void AddFoods(IRepository<Food> foodRepository)
{
    var foods = new[]
    {
    new Food { Name = "Brit Premium By Nature Light", Weight = 3, Quantity = 5, Destination = "Dla psów z nadwagą" },
    new Food { Name = "JosiDog Economy", Weight = 15, Quantity = 2, Destination = "Dla psów o normalnej aktywności" },
    new Food { Name = "ROYAL CANIN Mini", Weight = 4, Quantity = 10, Destination = "Dla szczeniąt do 10 miesiąca życia" },
    new Food { Name = "PERRO Karma sucha Kaczka z batatami", Weight = 3, Quantity = 10, Destination = "Dla małych psów z alergią na zboża" },
    };

    foodRepository.AddBatch(foods);
}

var medicamentRepository = new SqlRepository<Medicament>(new AnimalShelterDbContext());

AddMedicaments(medicamentRepository);
//WriteAllToConsole(medicamentRepository);
static void AddMedicaments(IRepository<Medicament> medicamentRepository)
{
    var medicaments = new[]
    {
        new Medicament { Name = "AntiFlexi+", Dose = 5, Quantity = 500 },
        new Medicament { Name = "Dolvit Stoper", Dose = 0.3, Quantity = 90 },
    };

    medicamentRepository.AddBatch(medicaments);
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}

static void AddNewDog(IRepository<Dog> dogRepository)
{
    while (true)
    {
        Console.WriteLine("Dog name:");
        var dogname = Console.ReadLine().ToUpper();
        while (String.IsNullOrEmpty(dogname))
        {
            Console.WriteLine("This input can not be empty.");
            dogname = Console.ReadLine().ToUpper();
        }

        Console.WriteLine("Dog gender: F or M");
        var doggender = Console.ReadLine().ToUpper();
        if (doggender!="M" && doggender!="F")
        {
            Console.WriteLine("Incorect choise.");
            doggender = Console.ReadLine().ToUpper();
        }
        var newDog = new Dog { Name = dogname, Gender = doggender };
        dogRepository.Add(newDog);
        break;
    }
    dogRepository.Save();
}

static T? FindEntityById<T>(IRepository<T> repository) where T : class, IEntity
{
    while (true)
    {
        Console.WriteLine($"Enter the Id number of the item you want to display in {typeof(T).Name}");
        var idInput = Console.ReadLine();
        int idValue = 0;
        var idEntityYouWant = int.TryParse(idInput, out idValue);
        if (!idEntityYouWant)
        {
            Console.WriteLine("Podaj wartość liczbową");
        }
        else
        {
            var result = repository.GetById(idValue);
            if (result == null)
            {
                Console.WriteLine($"There is no element in the {idValue} position in {typeof(T).Name}. ");
            }
            else
            {
                Console.WriteLine(repository.GetById(idValue));
            }
            return result;
        }
    }
}

Console.WriteLine("Welcome to the database Animal Shelter");
Console.WriteLine(
    "---Menu--- \n" +
    "You can do: \n" +
    "1 - View list of entity \n" +
    "2 - Find entity by Id \n" +
    "3 - Change data \n" +
    "Q - Close app \n");

while (true)
{
    Console.WriteLine("1,2, 3, Q choose what you want to do");
    var input = Console.ReadLine().ToUpper();

    switch (input)
    {
        case "1":
            Console.WriteLine("D - View all dogs\n" +
                "E - View all employees\n" +
                "Any Other key - leave and go to MENU");

            var viewEntity = Console.ReadLine().ToUpper();

            if (viewEntity == "D")
            {
                WriteAllToConsole(dogRepository);
            }
            if (viewEntity == "E")
            {
                WriteAllToConsole(employeeRepository);
            }
            break;

        case "2":

            Console.WriteLine("D - View dog by Id\n" +
                "E - View employee by Id\n" +
                "Any Other key - leave and go to MENU");

            var viewIdEntity = Console.ReadLine().ToUpper();

            if (viewIdEntity == "D")
            {
                FindEntityById(dogRepository);
            }
            if (viewIdEntity == "E")
            {
                FindEntityById(employeeRepository);
            }
            break;

        case "3":
            Console.WriteLine("A - add \nR - remove \n" +
                "Any Other key - leave and go to MENU");

            var changeData = Console.ReadLine().ToUpper();

            if (changeData == "A")
            {
                AddNewDog(dogRepository);
            }
            break;

        case "Q":
            System.Environment.Exit(0);
            break;

        default:
            Console.WriteLine("Choose the correct key from the menu");
            break;
    }
}


//DateTime saveUtcNow = DateTime.UtcNow;

//using (var writer = File.AppendText("report.txt"))
//{
//    writer.WriteLine($"....{saveUtcNow}");

//...................................................................
//Console.WriteLine("......t.txt");