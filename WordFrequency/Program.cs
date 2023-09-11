using WordFrequency;

int minWordLenght;
int maxWordLenght;
int sortOrder;
List<WordStatItem> wordStat = new  List<WordStatItem>();

var ranges = new CharsReangeSet();
ranges.AddRange(new CharsRange('А', 'Я'));
ranges.AddRange(new CharsRange('а', 'я'));
ranges.AddRange(new CharsRange('A', 'Z'));
ranges.AddRange(new CharsRange('a', 'z'));

Utils.WriteSplitter();
Console.WriteLine($"Поместите текстовый файл для обработки в следующую папку (название не имеет значения, важно наличие расширения .txt):");
Console.WriteLine(Utils.FolderFilesIn);
Utils.WriteSplitter();

minWordLenght = Utils.ReadNumber(
    "Введите минимальный размер слова (0 - без ограничений по минимальной длине):", 
    "Допустимы только целые числа!",
    "Минимальный размер слова:");
Utils.WriteSplitter();

maxWordLenght = Utils.ReadNumber(
    "Введите максимальный размер слова (0 - без ограничений по максимальной длине):",
    "Допустимы только целые числа!",
    "Максимальный размер слова:");
Utils.WriteSplitter();

sortOrder = Utils.ReadNumber(
    "Выберите порядок сортировки найденных слов (1 - по алфавиту, 2 - по частоте использования)",
    "Введите только 1 или 2!",
    "Выбранный вариант сортировки:", 1, 2);
Utils.WriteSplitter();



string txtInFilePath = Utils.GetFirstFilePath();
if (string.IsNullOrEmpty(txtInFilePath))
    Console.WriteLine("Не найден исходный файл для обработки!");
else
{
    Console.WriteLine($"Обрабатываем файл {txtInFilePath}");

    List<string> words = new List<string>();
    string curWord = "";
    char curSymbol;

    //читаем исходный файл
    using (var reader = new StreamReader(txtInFilePath))
    {
        do
        {
            curSymbol = (char) reader.Read();
            if (ranges.CharInRanges(curSymbol))
            {
                curWord += curSymbol;
            }
            else
            {
                if (curWord != "")
                {
                    if (Utils.IsWordInLenghtRange(curWord,minWordLenght,maxWordLenght))
                        words.Add(curWord.ToLower());
                    curWord = "";
                }
            }
        } while (!reader.EndOfStream);
        //дописываем последнее слово из буфера (если оно там есть)
        if (curWord != "")
        {
            if (Utils.IsWordInLenghtRange(curWord, minWordLenght, maxWordLenght))
                words.Add(curWord.ToLower());
        }
    }

    //Сортируем по алфавиту и считаем количество использований для каждого слова
    words.Sort();
    curWord = "";
    int wordsCount = 1;
    foreach (var word in words)
    {
        if (curWord == word)
            wordsCount++;
        else
        {
            if (curWord != "")
                wordStat.Add(new WordStatItem { Word = curWord, Count = wordsCount });
            wordsCount = 1;
            curWord = word;
        }
    }
    //выводим последнее слово
    wordStat.Add(new WordStatItem { Word = curWord, Count = wordsCount });

    //пишем результат в файл
    using (FileStream strm = File.Create(Utils.GetOutFilePath()))
    {
        using (StreamWriter sw = new StreamWriter(strm))
        {
            //если пользователь выбрал сортировку по алфавиту
            if (sortOrder == 1)
            {
                foreach (var word in wordStat)
                    sw.WriteLine(word.Word + ": " + word.Count);
            }
            //если пользователь выбрал сортировку по частоте использования
            else
            {
                var sortedStat = wordStat.OrderByDescending(c => c.Count);
                foreach (var word in sortedStat)
                    sw.WriteLine(word.Word + ": " + word.Count);
            }
        }
    }
    Utils.WriteSplitter();
    Console.WriteLine("Готово! Результат в файле:");
    Utils.WriteSplitter();
    Console.WriteLine(Utils.GetOutFilePath());
}

Console.Read();
