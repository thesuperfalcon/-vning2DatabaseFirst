using Övning2DatabaseFirst.Models;

namespace Övning2DatabaseFirst
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                using (var db = new Parking10Context())
                {
                    var cityList = db.Cities;

                    var parkingHouseList = db.ParkingHouses;

                    foreach (var city in cityList.ToList())
                    {
                        Console.WriteLine($"{city.Id}\t{city.CityName}");

                        var parkingHouse = (from p in db.ParkingHouses
                                            where city.Id == p.CityId
                                            select p).Distinct().ToList();

                        foreach (var p in parkingHouse)
                        {
                            Console.WriteLine($"\t{p.Id}\t{p.HouseName}");
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("1. Change City");
                    Console.WriteLine("2. Add City");
                    Console.WriteLine("3. Change ParkingHouse");
                    Console.WriteLine("4. Add ParkingHouse");

                    char key = Console.ReadKey().KeyChar;

                    int cityId;
                    int parkingHouseId;

                    switch(key)
                    {
                        case '1':
                            cityId = GetIntegerInput("Id:");
                            ChangeCity(cityId);
                            break;
                        case '2':
                            AddCity();
                            break;
                        case '3':
                            parkingHouseId = GetIntegerInput("ParkingHouse-Id: ");
                            ChangeParkingHouse(parkingHouseId);
                            break;
                        case '4':
                            AddParkingHouse();
                            break;

                    }
                }
            }
        }
        private static void ChangeCity(int cityId)
        {
            using (var db = new Parking10Context())
            {
                var cityList = db.Cities;

                var city = (from c in db.Cities
                            where c.Id == cityId
                            select c).SingleOrDefault();
                if(city != null)
                {
                    string cityName = GetInput("Name: ");
                    city.CityName = cityName;
                    db.SaveChanges();
                }
            }
        }
        private static void AddCity()
        {
            using (var db = new Parking10Context())
            {
                var cityList = db.Cities;

                Models.City createCity = new Models.City()
                {
                    CityName = GetInput("Name: "),
                };
                cityList.Add(createCity);
                db.SaveChanges();
            }
        }
        private static void ChangeParkingHouse(int parkingHouseId)
        {
            using (var db = new Parking10Context())
            {
                var parkingHouseList = db.ParkingHouses;

                var parkingHouse = (from p in db.ParkingHouses
                                    where p.Id == parkingHouseId
                                    select p).SingleOrDefault();
                if(parkingHouse != null)
                {
                    string name = GetInput("Name: ");
                    parkingHouse.HouseName = name;
                    db.SaveChanges();
                }
            }
        }
        private static void AddParkingHouse()
        {
            using (var db = new Parking10Context())
            {
                var parkingHouseList = db.ParkingHouses;

                Models.ParkingHouse createParkingHouse = new Models.ParkingHouse()
                {
                    HouseName = GetInput("Name: "),
                    CityId = GetIntegerInput("CityId: "),
                };
                parkingHouseList.Add(createParkingHouse);
                db.SaveChanges();
            }
        }
        private static string GetInput(string prompt)
        {
            string input = "";
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    TryAgain();
                }
            }
            return FormatString(input);
        }
        private static int GetIntegerInput(string prompt)
        {
            int result;
            while (!int.TryParse(GetInput(prompt), out result))
            {
                TryAgain();
            }
            return result;
        }
        private static void TryAgain()
        {
            Console.WriteLine("Try again.");
        }
        private static string FormatString(string input)
        {
            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }
    }
}

            