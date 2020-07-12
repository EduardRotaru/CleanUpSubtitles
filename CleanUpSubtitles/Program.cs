namespace CleanUpSubtitles
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePathNames = FileProcessing.AllFilesNames();
            FileProcessing.WriteFormattedStringsInFiles(filePathNames);
        }

       
    }
}
