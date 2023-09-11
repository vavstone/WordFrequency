using System.Reflection;

namespace WordFrequency
{
    public static class Utils
    {
        public static int ReadNumber(string welcomeStr, string errStr, string resStr, int? minValue = null, int? maxValue = null)
        {
            int resNumber;
            Console.WriteLine(welcomeStr);
            while (!int.TryParse(Console.ReadLine(), null, out resNumber) || !IsNumberInRange(resNumber, minValue, maxValue))
            {
                Console.WriteLine(errStr);
                Console.WriteLine(welcomeStr);
            }
            Console.WriteLine(resStr + " " + resNumber);
            return resNumber;
        }

        static bool IsNumberInRange(int value, int? minValue, int? maxValue)
        {
            if (minValue == null && maxValue == null)
                return true;
            if (minValue != null && maxValue != null)
                return value >= minValue.Value && value <= maxValue.Value;
            if (minValue != null)
                return value >= minValue.Value;
            return value <= maxValue.Value;
        }

        static string ExecFileFolder
        {
            get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }
        }

        public static string FolderFilesIn
        {
            get { return Path.Combine(ExecFileFolder, "in"); }
        }
        static string FolderFilesOut
        {
            get { return Path.Combine(ExecFileFolder, "out"); }
        }

        public static string GetFirstFilePath()
        {
            if (Directory.Exists(FolderFilesIn))
                return Directory.GetFiles(FolderFilesIn, "*.txt").FirstOrDefault();
            return null;
        }

        public static string GetOutFilePath()
        {
            return Path.Combine(FolderFilesOut, "out.txt");
        }

        public static bool IsWordInLenghtRange(string word, int minLength, int maxLength)
        {
            if (minLength == 0 && maxLength == 0)
                return true;
            if (minLength > 0 && maxLength > 0)
                return word.Length >= minLength && word.Length <= maxLength;
            if (minLength > 0)
                return word.Length >= minLength;
            return word.Length <= maxLength;
        }

        public static void WriteSplitter()
        {
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
        }

    }
}