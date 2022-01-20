using Flashcards.Interfaces;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Flashcards.ViewModels
{
    class ShellViewModel
    {
        private readonly ISource _source;
        private readonly IDialogService _dialogService;
        private DispatcherTimer _timer = new DispatcherTimer();
        private Random _random = new Random();
        private DelegateCommand _exitCommand;

        public DelegateCommand ExitCommand => _exitCommand = _exitCommand ?? new DelegateCommand(DoExit);
        private DelegateCommand _settingsCommand;
        private DelegateCommand _restartAppCommand;

        public DelegateCommand SettingsCommand
        {
            get { return _settingsCommand = _settingsCommand ?? new DelegateCommand(OpenSettings); }
        }

        public DelegateCommand RestartAppCommand => _restartAppCommand = _restartAppCommand ?? new DelegateCommand(DoRestartApp);

        private void DoRestartApp()
        {
            Application.Current.Shutdown();
        }

        private void OpenSettings()
        {
            _dialogService.Show("SettingsDialog");
        }

        private void DoExit()
        {
            Application.Current.Shutdown();
        }

        public ShellViewModel(ISource source, IDialogService dialogService)
        {
            var randomSeconds = _random.Next(Properties.Settings.Default.MiniFlashTimeout,
                Properties.Settings.Default.MaxFlashTimeout + 1);
            _dialogService = dialogService;
            _source = source;
            Debug.WriteLine(source.GetWord());
            _timer.Tick += _timer_Tick;
            _timer.Interval = TimeSpan.FromSeconds(randomSeconds);
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();

            _dialogService.Show("FlashDialog", result =>
            {
                var randomSeconds = _random.Next(Properties.Settings.Default.MiniFlashTimeout,
                    Properties.Settings.Default.MaxFlashTimeout + 1);
                _timer.Interval = TimeSpan.FromSeconds(randomSeconds);
                _timer.Start();
            });
        }
    }
}
