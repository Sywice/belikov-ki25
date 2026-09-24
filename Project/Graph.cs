namespace FuelPriceNamespace
{
    public class Graph
    {
        public Dictionary<string, Info> links = new Dictionary<string, Info>();
        public List<string> fromList = new List<string>();
        public List<string> toList = new List<string>();

        public void ReadFile(string filename)
        {
            links.Clear();
            fromList.Clear();
            toList.Clear();

            foreach (string line in File.ReadAllLines(filename))
            {
                if (line == "") continue;

                for (int i = 0; i < line.Length - 1; i++)
                {
                    if (line[i] == '-' && line[i + 1] == '>')
                    {
                        string a = "";
                        string b = "";

                        for (int j = 0; j < i; j++)
                        {
                            if (line[j] != ' ') a = a + line[j];
                        }

                        for (int j = i + 2; j < line.Length; j++)
                        {
                            if (line[j] != ' ') b = b + line[j];
                        }

                        fromList.Add(a);
                        toList.Add(b);

                        if (!links.ContainsKey(a))
                        {
                            links.Add(a, new Info());
                        }
                        if (!links.ContainsKey(b))
                        {
                            links.Add(b, new Info());
                        }
                        links[a].Targets.Add(b);
                        links[a].Out++;
                        links[b].In++;

                        break;
                    }
                }
            }
        }

        public void BuildAndPrintMatrix()
        {
            int rowCount = links.Count;
            int colCount = fromList.Count;

            int[,] matrix = new int[rowCount, colCount];

            List<string> vertList = new List<string>(links.Keys);

            for (int i = 0; i < rowCount; i++)
            {
                string v = vertList[i];
                for (int j = 0; j < colCount; i++)
                {
                    if (fromList[j] == v)
                    {
                        matrix[i, j] = 1;   
                    }
                    else if (toList[j] == v)
                    {
                        matrix[i, j] = -1;  
                    }
                    else
                    {
                        matrix[i, j] = 0;  
                    }
                }
            }
        }
    }
}