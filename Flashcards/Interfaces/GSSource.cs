using Flashcards.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Interfaces
{
    class GSSource : ISource
    {

        // string _sheetLink = "https://docs.google.com/spreadsheets/d/1dQIMyiHV1SAq4js61c6ZCgX-34oZfwyApXES1oqgipU/edit#gid=0";
        string _sheetId = "1dQIMyiHV1SAq4js61c6ZCgX-34oZfwyApXES1oqgipU";
        string AppName = "Flashcards App";
        private List<string> _sheetNames = new List<string>();
        private SheetsService _service;

        public GSSource()
        {
            ServiceAccountCredential credential;
            string[] Scopes = { SheetsService.Scope.Spreadsheets, SheetsService.Scope.SpreadsheetsReadonly };
            string serviceAccountEmail = "upwork-francis@socrata-project-335309.iam.gserviceaccount.com";
            string jsonfile = "socrata-project-335309-3b827526f39f.json";
            using (Stream stream = new FileStream(@jsonfile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                credential = (ServiceAccountCredential)
                    GoogleCredential.FromStream(stream).UnderlyingCredential;

                var initializer = new ServiceAccountCredential.Initializer(credential.Id)
                {
                    User = serviceAccountEmail,
                    Key = credential.Key,
                    Scopes = Scopes
                };
                credential = new ServiceAccountCredential(initializer);

                // Create Google Sheets API service.
                _service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = AppName,
                });

                var sheetsReq = _service.Spreadsheets.Get(_sheetId);
                var sheet = sheetsReq.Execute();

                foreach (var item in sheet.Sheets)
                {
                    _sheetNames.Add(item.Properties.Title);
                }
            }
        }

        public FlashItem GetWord()
        {
            var random = new Random();
            var idxSheet = random.Next(_sheetNames.Count);
            var sheetName = _sheetNames[idxSheet];

            var range = $"{sheetName}!A2:B";

            var request = _service.Spreadsheets.Values.Get(_sheetId, range);
            var response = request.Execute();
            var values = response.Values;

            if (values != null && values.Count > 0)
            {
                var idxWord = random.Next(values.Count);
                var word = values[idxWord][0];
                var meaning = values[idxWord][1];
                return new FlashItem
                {
                    Meaning = meaning.ToString(),
                    Word = word.ToString(),
                    SheetName = sheetName
                };
            }

            return null;
        }
    }
}
