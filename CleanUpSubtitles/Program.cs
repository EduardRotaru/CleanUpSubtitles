using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CleanUpSubtitles
{
    public class FileSpecifications
    {
        public string PathName { get; set; }
        public string FileName { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var filePathNames = AllFilesNames();
            WriteFormattedStringsInFiles(filePathNames);
        }

        static FileSpecifications[] AllFilesNames()
        {
            var folderOrigin = @"F:\SubtitleFiles";

            // cut end of name
            return new DirectoryInfo(folderOrigin).GetFiles().Select(f => new FileSpecifications
            {
                PathName = f.FullName,
                FileName = f.Name.Replace("-en", "")
            }).ToArray();
        }

        static void WriteFormattedStringsInFiles(FileSpecifications[] fileDetails)
        {
            for (int i = 0; i < fileDetails.Length; i++)
            {
                var finalPath = @"F:\SubtitleFiles\Formatted";
                var newFile = File.Create(Path.Combine(finalPath, $"{fileDetails[i].FileName}"));

                using (StreamWriter str = new StreamWriter(newFile))
                {
                    var fileLines = CleanedSubtitle(fileDetails[i].PathName);

                    foreach (char line in fileLines)
                    {
                        str.Write(line);
                    }
                }
            }
        }

        static string CleanedSubtitle(string path)
        {
            var sr = new StringBuilder();
            var listLines = new List<string[]>();

            var text = File.ReadAllLines(path);

            foreach (var t in text)
            {
                if (t.Any(c => char.IsLetter(c)))
                {
                    sr.AppendLine(string.Join("\n", 
                        t.Split('\n')
                            .Where(c => !string.IsNullOrWhiteSpace(c))));
                }
            }

            return sr.ToString();
        }
    }
}
