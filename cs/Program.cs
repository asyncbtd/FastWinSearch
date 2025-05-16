namespace FastWinSearch;

public class Program
{
    public static List<string> Сoincidences = new List<string>();
    public static string? Pattern;
    
    public static void Main()
    {
        Console.Write("Hello, tell me what I can find: ");
        Pattern = Console.ReadLine();
        Console.Write("Now tell me where to look: ");
        StartSearch(Console.ReadLine());
        
        foreach (string item in Сoincidences)
        {
            if (item.Contains("Найдена директория:"))
            {
                Console.WriteLine(item);
            }
        }
            
        Console.WriteLine("=========================================================================================");
            
        foreach (string item in Сoincidences)
        {
            if (item.Contains("Найден файл:"))
            {
                Console.WriteLine(item);
            }
        }
    }

    public static void StartSearch(string startSearchDir)
    {
        try
        {
            if (!Directory.Exists(startSearchDir))
            {
                Console.WriteLine("Directory not found: " + startSearchDir);
            }
            IterativelySearch(startSearchDir);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static void IterativelySearch(string directoryPath)
    {
        Stack<string> directories = new Stack<string>();
        directories.Push(directoryPath);

        while (directories.Count > 0)
        {
            string currentDir = directories.Pop();

            try
            {
                // Обработка файлов в текущей директории
                foreach (string file in Directory.GetFiles(currentDir))
                {
                    string fileName = Path.GetFileName(file);
                
                    if (fileName.Contains(Pattern, StringComparison.OrdinalIgnoreCase))
                    {
                        Сoincidences.Add("Найден файл: " + file);
                    }
                }

                // Обработка поддиректорий
                foreach (string dir in Directory.GetDirectories(currentDir))
                {
                    string dirName = Path.GetFileName(dir);
                    if (dirName.Contains(Pattern, StringComparison.OrdinalIgnoreCase))
                    {
                        Сoincidences.Add("Найдена директория: " + dir);
                    }

                    // Добавляем поддиректорию в стек для последующей обработки
                    directories.Push(dir);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Доступ запрещён: " + currentDir);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при доступе к директории " + currentDir + ": " + ex.Message);
            }
        }
    }
}