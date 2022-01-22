using Flashcards.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.ViewModels
{
    class FlashDialogViewModel : BindableBase, IDialogAware
    {
        private readonly ISource _wordSource;
        private string word;
        private string meaning;
        private string sheetName;
        private DelegateCommand _skipCommand;
        private DelegateCommand _revealCommand;
        private bool _showMeaning;

        public string Title => "Flashcard";
        public string Word { get => word; set => SetProperty(ref word, value); }
        public string Meaning { get => meaning; set => SetProperty(ref meaning, value); }
        public string SheetName { get => sheetName; set => SetProperty(ref sheetName, value); }

        public DelegateCommand SkipCommand => _skipCommand = _skipCommand ?? new DelegateCommand(DoSkip);

        public DelegateCommand RevealCommand => _revealCommand = _revealCommand ?? new DelegateCommand(DoReveal);
        private DelegateCommand _closeCommand;

        public DelegateCommand CloseCommand => _closeCommand = _closeCommand ?? new DelegateCommand(DoClose);

        private void DoClose()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        public bool ShowMeaning { get => _showMeaning; set => SetProperty(ref _showMeaning, value); }

        private void DoReveal()
        {
            ShowMeaning = true;
        }

        private async void DoSkip()
        {
            ShowMeaning = false;
            var flashItem = await _wordSource.GetWordAsync();
            Word = flashItem?.Word ?? "NO WORD AVAILABLE";
            Meaning = flashItem?.Meaning ?? "NO WORD AVAILABLE"; ;
            SheetName = flashItem?.SheetName ?? "NO WORD AVAILABLE"; ;
        }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public FlashDialogViewModel(ISource wordSource)
        {
            _wordSource = wordSource;
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            ShowMeaning = false;
            var flashItem = _wordSource.GetWord();


            Word = flashItem?.Word ?? "NO WORD AVAILABLE";
            Meaning = flashItem?.Meaning ?? "NO WORD AVAILABLE"; ;
            SheetName = flashItem?.SheetName ?? "NO WORD AVAILABLE"; ;
        }
    }
}
