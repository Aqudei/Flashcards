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
    class SettingsViewModel : BindableBase, IDialogAware
    {
        public string Title => "Setting";
        private string _source;

        public string Source
        {
            get { return _source; }
            set { SetProperty(ref _source, value); }
        }

        private int _minFlashTimeout;

        public int MinFlashTimeout
        {
            get { return _minFlashTimeout; }
            set { SetProperty(ref _minFlashTimeout, value); }
        }


        private int _maxFlashTimeout;

        public int MaxFlashTimeout
        {
            get { return _maxFlashTimeout; }
            set { SetProperty(ref _maxFlashTimeout, value); }
        }


        public event Action<IDialogResult> RequestClose;

        private DelegateCommand _browseSourceCommand;

        public DelegateCommand BrowseSourceCommand => _browseSourceCommand = _browseSourceCommand ?? new DelegateCommand(BrowseSource);

        private void BrowseSource()
        {
            var dlg = new Microsoft.WindowsAPICodePack.Dialogs.CommonOpenFileDialog
            {
                IsFolderPicker = false,
                Multiselect = false
            };

            var reslt = dlg.ShowDialog();
            if (reslt == Microsoft.WindowsAPICodePack.Dialogs.CommonFileDialogResult.Ok)
            {
                Source = dlg.FileName;
            }
        }

        private DelegateCommand _closeCommand;

        public DelegateCommand CloseCommand
        {
            get { return _closeCommand = _closeCommand ?? new DelegateCommand(CloseSettings); }
        }


        private DelegateCommand _applyCommand;

        public DelegateCommand ApplyCommand
        {
            get { return _applyCommand = _applyCommand ?? new DelegateCommand(DoApply); }
        }

        private void DoApply()
        {
            Properties.Settings.Default.Source = Source;
            Properties.Settings.Default.MiniFlashTimeout = MinFlashTimeout;
            Properties.Settings.Default.MaxFlashTimeout = MaxFlashTimeout;
            Properties.Settings.Default.Save();
        }

        private void CloseSettings()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Source = Properties.Settings.Default.Source;
            MinFlashTimeout = Properties.Settings.Default.MiniFlashTimeout;
            MaxFlashTimeout = Properties.Settings.Default.MaxFlashTimeout;
        }
    }
}
