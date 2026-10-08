namespace FuelPriceNamespace
{
    public class ClassAdjesency
    {
        private readonly Dictionary<string, List<string>> inheritance = new();
        private readonly Dictionary<string, List<string>> aggregation = new();
        private readonly List<string> classNames = new();
        public void ReadFile(string filename)
        {
            inheritance.Clear();
            aggregation.Clear();
            classNames.Clear();
            string[] lines = File.ReadAllLines(filename);
            ParseLines(lines);
        }

        public void ParseLines(string[] lines)
        {
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
            string[] parts;
            if (line.Contains("-->"))
            {
                parts = line.Split("-->");

                string from = parts[0].Trim();
                string to = parts[1].Trim();

                AddClass(from);
                AddClass(to);
                inheritance[from].Add(to);
            }
            else if (line.Contains("0->"))
            {
                parts = line.Split("0->");

                string from = parts[0].Trim();
                string to = parts[1].Trim();

                AddClass(from);
                AddClass(to);
                aggregation[from].Add(to);
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