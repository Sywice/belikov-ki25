namespace FuelPriceNamespace
{
    public class ClassAdjesency
    {
        private readonly Dictionary<string, List<string>> inheritance = new();
        private readonly Dictionary<string, List<string>> aggregation = new();
        private readonly List<string> classNames = new();
        private void ResetData()
        {
            inheritance.Clear();
            aggregation.Clear();
            classNames.Clear();
        }

        public void ReadFile(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentException("Имя файла не может быть пустым.", nameof(filename));
            }

            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("Файл со связями классов не найден.", filename);
            }

            ResetData();
            string[] lines = File.ReadAllLines(filename);
            ParseLines(lines);
        }

        public void ParseLines(string[] lines)
        {
            if (lines == null)
            {
                throw new ArgumentNullException(nameof(lines), "Массив строк не может быть null.");
            }

            foreach (string line in lines)
            {
                ProcessLine(line);
            }
        }

        public void ProcessLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return;
            }

            if (line.Contains("-->"))
            {
                ParseAndAddRelation(line, "-->", inheritance);
            }
            else if (line.Contains("0->"))
            {
                ParseAndAddRelation(line, "0->", aggregation);
            }
            else
            {
                throw new FormatException($"Неизвестный тип связи в строке: '{line}'. Ожидается '-->' или '0->'.");
            }
        }

        private void ParseAndAddRelation(string line, string separator, Dictionary<string, List<string>> targetDict)
        {
            string[] parts = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                throw new FormatException($"Неверный формат связи в строке: '{line}'. Ожидается формат 'ClassA {separator} ClassB'.");
            }

            string from = parts[0].Trim();
            string to = parts[1].Trim();

            if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
            {
                throw new FormatException($"Имена классов не могут быть пустыми в строке: '{line}'.");
            }

            AddClass(from);
            AddClass(to);

            if (!targetDict[from].Contains(to))
            {
                targetDict[from].Add(to);
            }
        }

        private void AddClass(string className)
        {
            if (!inheritance.ContainsKey(className))
            {
                inheritance.Add(className, new List<string>());
                aggregation.Add(className, new List<string>());
                classNames.Add(className);
            }
        }

        public void PrintAdjacencyLists()
        {
            foreach (string className in classNames)
            {
                string inheritanceList = string.Join(",", inheritance[className]);
                string aggregationList = string.Join(",", aggregation[className]);

                Console.WriteLine(
                    $"({className},[{inheritanceList}],[{aggregationList}])");
            }
        }
    }
}