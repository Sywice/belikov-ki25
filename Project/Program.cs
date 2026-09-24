using System.Globalization;
using FuelPriceNamespace;

class Program
{
    static void Main()
    {
        List<FuelPrice> oils = [];
        List<GasStation> stations = [];
        Graph graph = new Graph();


        if (File.Exists("input.txt"))
        {
            foreach (string line in File.ReadLines("input.txt"))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] data = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (data[0] == "OIL")
                {
                    oils.Add(FuelPrice.Parse(line));
                }
                else if (data[0] == "GASSTATION")
                {
                    stations.Add(GasStation.Parse(line));
                }
            }
        }

        while (true) 
        {
            Console.WriteLine("\n--- МЕНЮ ---");
            Console.WriteLine("1. Создать объект топливо и отобразить список");
            Console.WriteLine("2. Создать объект азс и отобразить список");
            Console.WriteLine("3. Прочитать graph.txt (граф) и показать список и матрицу");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");

            string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("\n[Добавление топлива]");
                    Console.Write("Введите тип топлива (например, АИ-95): ");
                    string type = Console.ReadLine();

                    Console.Write("Введите дату (в формате гггг.мм.дд, например 2026.05.12): ");
                    string dateStr = Console.ReadLine();
                    DateTime date;
                    if (!DateTime.TryParseExact(dateStr, "yyyy.MM.dd", null, DateTimeStyles.None, out date))
                    {
                        date = DateTime.Now;
                    }

                    Console.Write("Введите цену: ");
                    double price;
                    double.TryParse(Console.ReadLine(), NumberStyles.Any, null, out price);

                    FuelPrice newOil = new FuelPrice(price, date, type);
                    oils.Add(newOil);

                    Console.WriteLine("\n--- Список топлива ---");
                    for (int i = 0; i < oils.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {oils[i].FuelType} | {oils[i].Date:yyyy.MM.dd} | {oils[i].Price}");
                    }
                }
                else if (choice == "2")
                {
                    Console.WriteLine("\n[Добавление АЗС]");
                    Console.Write("Введите название АЗС (например, Лукойл): ");
                    string name = Console.ReadLine();

                    Console.Write("Введите координату X: ");
                    double x;
                    double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out x);

                    Console.Write("Введите координату Y: ");
                    double y;
                    double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out y);

                    GasStation newStation = new GasStation(x, y, name);
                    stations.Add(newStation);

                    Console.WriteLine("\n--- Список АЗС ---");
                    for (int i = 0; i < stations.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {stations[i].Name} | X: {stations[i].LocationX}, Y: {stations[i].LocationY}");
                    }
                }
                if (choice == "3")
                {
                    if (!File.Exists("graph.txt"))
                    {
                        Console.WriteLine("Файл graph.txt не найден!");
                    }
                    graph.ReadFile("graph.txt");
                    graph.BuildAndPrintMatrix();
                }
                else if (choice == "0")
                {
                    Console.WriteLine("Выход из программы.");
                    break;
                }
                else
                {
                    Console.WriteLine("Неверный ввод!");
                }
        }
    } 
}