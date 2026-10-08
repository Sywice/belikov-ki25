using FuelPriceNamespace;

class Program
{
    private static List<FuelPrice> oils = [];
    private static List<GasStation> stations = [];
    private static Graph graph = new Graph();
    private static readonly int[] choices = { 0, 1, 2, 3 };
    static void Main()
    {
        RunMenuLoop();
    } 

    private static void RunMenuLoop()
    {
        while (true)
        {
            PrintMenu();
            string? choice = Console.ReadLine();
            try
            {
                if (string.IsNullOrWhiteSpace(choice))
                {
                    throw new ArgumentException("Ввод не может быть пустым");
                }

                int parsedChoice = int.Parse(choice);
                if (parsedChoice == 0)
                {
                    Console.WriteLine("Выход из программы");
                    break;
                }
                if (!choices.Contains(parsedChoice))
                {
                    throw new ArgumentException("Такого пункта меню не существует");
                }
                ExecuteChoice(parsedChoice);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }
        }
    }

    private static void ExecuteChoice(int parsedChoice)
    {
        if (parsedChoice == 1)
        {
            AddFuel();
        }
        else if (parsedChoice == 2)
        {
            AddGasStation();
        }
        else if (parsedChoice == 3)
        {
            ProcessGraph();
        }
        else
        {
            Console.WriteLine("Неверный ввод");
        }
    }

    private static void AddFuel()
    {
        try
        {
            if (File.Exists("input.txt"))
            {
                Console.WriteLine("Файл input.txt не найден");
                return;
            }
            foreach (string line in File.ReadAllLines("input.txt"))
            {
                oils.Add(Parsers.FuelParse(line));
            }
            ShowFuel();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private static void ShowFuel()
    {
        Console.WriteLine("\n--- Список топлива ---");
        for (int i = 0; i < oils.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {oils[i].FuelType} | {oils[i].Date:yyyy.MM.dd} | {oils[i].Price}");
        }
    }

    private static void AddGasStation()
    {
        foreach (string line in File.ReadAllLines("input.txt"))
        {
            stations.Add(Parsers.StationParse(line));
        }
        ShowGasStations();
    }

    private static void ShowGasStations()
    {
        Console.WriteLine("\n--- Список АЗС ---");
        for (int i = 0; i < stations.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {stations[i].Name} | X: {stations[i].LocationX}, Y: {stations[i].LocationY}");
        }
    }

    private static void ProcessGraph()
    {
        graph.ReadFile("input.txt");
        int[,] matrix = graph.BuildAndReturnMatrix();
        PrintMatrix(matrix);
    }

    private static void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write($"{matrix[i, j],3}");
            }
            Console.WriteLine();
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("\n--- МЕНЮ ---");
        Console.WriteLine("1. Создать объект топливо и отобразить список");
        Console.WriteLine("2. Создать объект азс и отобразить список");
        Console.WriteLine("3. Прочитать graph.txt и показать список и матрицу");
        Console.WriteLine("0. Выход");
        Console.Write("Выбор: ");
    }
}