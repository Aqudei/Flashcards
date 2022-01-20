using Flashcards.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Interfaces
{
    class ExcelSource : ISource
    {

        public ExcelSource()
        {

        }
        public FlashItem GetWord()
        {
            var rand = new Random();
            var source = "Vocab.xlsx";
            if (File.Exists(Properties.Settings.Default.Source))
            {
                source = Properties.Settings.Default.Source;
            }

            using (var ep = new ExcelPackage(source))
            {
                var word = "";
                var meaning = "";
                var randSheet = 0;
                var randRow = 0;

                while (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(meaning))
                {
                    randSheet = rand.Next(ep.Workbook.Worksheets.Count - 1);
                    randRow = rand.Next(1, ep.Workbook.Worksheets[randSheet].Dimension.Rows);
                    word = ep.Workbook.Worksheets[randSheet].Cells[randRow, 1].Value?.ToString();
                    meaning = ep.Workbook.Worksheets[randSheet].Cells[randRow, 2].Value?.ToString();
                }

                return new FlashItem
                {
                    Meaning = meaning ?? "",
                    Word = word ?? "",
                    SheetName = ep.Workbook.Worksheets[randSheet].Name
                };
            }
        }
    }
}
