using FuelPriceNamespace;

class Program
{
    private static List<FuelPrice> oils = [];
    private static List<GasStation> stations = [];
    private static Graph graph = new Graph();
    static void Main()
    {
        RunMenuLoop();
    } 

    private static void RunMenuLoop()
    {
        while (true)
        {
            PrintMenu();
            string choice = Console.ReadLine();
            if (choice == "0")
            {
                Console.WriteLine("Выход из программы.");   
                break; 
            }
            ExecuteChoice(choice);
        }
    }

    private static void ExecuteChoice(string choice)
    {
        if (choice == "1")
        {
            AddFuel();
        }
        else if (choice == "2")
        {
            AddGasStation();
        }
        else if (choice == "3")
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
        foreach (string line in File.ReadAllLines("input.txt"))
        {
            oils.Add(FuelPrice.Parse(line));
        }
        ShowFuel();
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
            stations.Add(GasStation.Parse(line));
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
        Console.WriteLine("3. Прочитать graph.txt (граф) и показать список и матрицу");
        Console.WriteLine("0. Выход");
        Console.Write("Выбор: ");
    }
}