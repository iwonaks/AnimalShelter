using AnimalShelter.Entities;
using AnimalShelter.Repositories;
using AnimalShelter.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;

var dogRepository = new SqlRepository<Dog>(new AnimalShelterDbContext());
AddDogs(dogRepository);
WriteAllToConsole(dogRepository);
static void AddDogs(IRepository<Dog> dogRepository)
{
    dogRepository.Add(new Dog { Name = "Rex", Gender = "M" });
    dogRepository.Add(new Dog { Name = "Suzi", Gender = "F" });
    dogRepository.Add(new Dog { Name = "Perła", Gender = "F" });
    dogRepository.Save();
}

var foodRepository = new SqlRepository<Food>(new AnimalShelterDbContext());
AddFoods(foodRepository);
WriteAllToConsole(foodRepository);  
static void AddFoods(IRepository<Food> foodRepository)
{
    foodRepository.Add(new Food { Name = "Brit Premium By Nature Light", Weight = 3, Quantity = 5, Destination = "Dla psów z nadwagą" });
    foodRepository.Add(new Food { Name = "JosiDog Economy", Weight = 15, Quantity = 2, Destination = "Dla psów o normalnej aktywności" });
    foodRepository.Add(new Food { Name = "ROYAL CANIN Mini", Weight = 4, Quantity = 10, Destination = "Dla szczeniąt do 10 miesiąca życia" });
    foodRepository.Add(new Food { Name = "PERRO Karma sucha Kaczka z batatami", Weight = 3, Quantity = 10, Destination = "Dla małych psów z alergią na zboża" });
    foodRepository.Save();
}

var medicamentRepository = new SqlRepository<Medicament>(new AnimalShelterDbContext());
AddMedicaments(medicamentRepository);
WriteAllToConsole(medicamentRepository);
static void AddMedicaments(IRepository<Medicament> medicamentRepository)
{
    medicamentRepository.Add(new Medicament { Name = "AntiFlexi+", Dose = 5, Quantity = 500});
    medicamentRepository.Add(new Medicament { Name = "Dolvit Stoper", Dose = 0.3, Quantity = 90 });
    medicamentRepository.Save();
}

var employeeRepository = new SqlRepository<Employee>(new AnimalShelterDbContext());
AddEmployees(employeeRepository);
AddVolunteers(employeeRepository);
WriteAllToConsole(employeeRepository);
static void AddEmployees(IRepository<Employee> employeeRepository)
{
    employeeRepository.Add(new Employee { Name = "Jan", SurName = "Kowalski", Education = "Technik weterynarii" });
    employeeRepository.Add(new Employee { Name = "Anna", SurName = "Wolińska", Education = "Podstawowe" });
    employeeRepository.Save();
}

static void AddVolunteers(IWriteRepository<Volunteer> volunteerRepository)
{
    volunteerRepository.Add(new Volunteer { Name = "Urszula", SurName = "Wielicka", Education = "uczeń" });
    volunteerRepository.Add(new Volunteer { Name = "Joanna", SurName = "Wielicka", Education = "wyższe" });
    volunteerRepository.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach(var item in items)
    {
        Console.WriteLine(item);
    }
}