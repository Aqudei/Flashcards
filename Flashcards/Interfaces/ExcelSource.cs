using Flashcards.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
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
            using (var ep = new ExcelPackage("Vocab.xlsx"))
            {
                var rand = new Random();
                var randSheet = rand.Next(ep.Workbook.Worksheets.Count - 1);

                var randRow = rand.Next(1, ep.Workbook.Worksheets[randSheet].Dimension.Rows);
                var word = ep.Workbook.Worksheets[randSheet].Cells[randRow, 1].Value?.ToString();
                var meaning = ep.Workbook.Worksheets[randSheet].Cells[randRow, 2].Value?.ToString();
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
