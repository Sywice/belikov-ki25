namespace FuelPriceNamespace
{
    public class Graph
    {
        private readonly Dictionary<string, LinkInfo> links = new Dictionary<string, LinkInfo>();
        private readonly List<string> fromList = new List<string>();
        private readonly List<string> toList = new List<string>();

        private void ResetGraph()
        {
            links.Clear();
            fromList.Clear();
            toList.Clear();
        }

        public void ReadFile(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentException("Имя файла не может быть пустым.", nameof(filename));
            }

            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("Файл с данными графа не найден.", filename);
            }

            ResetGraph();
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
            if (string.IsNullOrWhiteSpace(line) || !line.Contains("->"))
            {
                return;
            }

            string[] edgeLinks = GetLinks(line);
            AddEdge(edgeLinks[0], edgeLinks[1]);
        }

        private string[] GetLinks(string line)
        {
            string[] parts = line.Split("->", StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                throw new FormatException($"Неверный формат ребра графа в строке: '{line}'. Ожидается формат 'A -> B'.");
            }

            string from = parts[0].Trim();
            string to = parts[1].Trim();

            if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
            {
                throw new FormatException($"Имена вершин не могут быть пустыми в строке: '{line}'.");
            }

            return new[] { from, to };
        }

        private void AddEdge(string from, string to)
        {
            fromList.Add(from);
            toList.Add(to);
            AddLink(from);
            AddLink(to);
            UpdateLinks(from, to);
        }

        private void AddLink(string name)
        {
            if (!links.ContainsKey(name))
            {
                links.Add(name, new LinkInfo());
            }
        }

        private void UpdateLinks(string from, string to)
        {
            links[from].targets.Add(to);
            links[from].linkOut++;
            links[to].linkIn++;
        }

        private int GetIndex(string v, int j)
        {
            if (fromList[j] == v && toList[j] == v)
            {
                return 2; 
            }
            if (fromList[j] == v)
            {
                return 1;
            }
            if (toList[j] == v)
            {
                return -1;
            }
            return 0;
        }

        private void FillMatrix(int[,] matrix, List<string> vertList, int rowCount, int colCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                string v = vertList[i];
                for (int j = 0; j < colCount; j++)
                {
                    matrix[i, j] = GetIndex(v, j);
                }
            }
        }

        public int[,] BuildAndReturnMatrix()
        {
            int rowCount = links.Count;
            int colCount = fromList.Count;

            int[,] matrix = new int[rowCount, colCount];

            List<string> vertList = new List<string>(links.Keys);

            FillMatrix(matrix, vertList, rowCount, colCount);
            return matrix;
        }
    }
}