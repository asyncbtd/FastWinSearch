namespace FastWinSearch
{
    public class Program
    {
        private static string patern = "world";
        private static List<string> sovpadeniya;
        
        static void Main()
        {
            sovpadeniya = new List<string>();
            Console.WriteLine("Ку еблан, напиши путь к стартовой директории: ");
            
            Console.WriteLine("=========================================================================================");
            
            //StartSearch(Console.ReadLine().Replace("\\", "/"));
            StartSearch("C:\\Users\\Timof\\AppData");
            
            Console.WriteLine("=========================================================================================");
            
            foreach (string item in sovpadeniya)
            {
                if (item.Contains("Найдена директория:"))
                {
                    Console.WriteLine(item);
                }
            }
            
            Console.WriteLine("=========================================================================================");
            
            foreach (string item in sovpadeniya)
            {
                if (item.Contains("Найден файл:"))
                {
                    Console.WriteLine(item);
                }
            }
        }

        static void StartSearch(string startSearchDir)
        {
            try
            {
                if (!Directory.Exists(startSearchDir))
                {
                    Console.WriteLine("Директория не существует: " + startSearchDir);
                }
                RecursivelySearch(startSearchDir);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        static void RecursivelySearch(string directoryPath)
        {
            try
            {
                foreach (string file in Directory.GetFiles(directoryPath))
                {
                    string fileName = Path.GetFileName(file);
                    
                    if (fileName.Contains(patern, StringComparison.OrdinalIgnoreCase))
                    {
                        sovpadeniya.Add("Найден файл: " + file);
                        //Console.WriteLine("Найден файл: " + file);
                    } 
                }
                
                foreach (string dir in Directory.GetDirectories(directoryPath))
                {
                    string dirName = Path.GetFileName(dir);
                    if (dirName.Contains(patern, StringComparison.OrdinalIgnoreCase))
                    {
                        sovpadeniya.Add("Найдена директория: " + dir);
                        //Console.WriteLine("Найдена директория: " + dir);
                    }

                    // Рекурсивно обходим все поддиректории
                    RecursivelySearch(dir);
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Игнорируем директории, доступ к которым запрещён
                Console.WriteLine("Доступ запрещён: " + directoryPath);
            }
            catch (Exception ex)
            {
                // Обрабатываем другие исключения
                Console.WriteLine("Ошибка при доступе к директории " + directoryPath + ": " + ex.Message);
            }
        } 
    }
}