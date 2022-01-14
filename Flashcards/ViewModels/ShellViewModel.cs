using Flashcards.Interfaces;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Flashcards.ViewModels
{
    class ShellViewModel
    {
        private readonly ISource _source;
        private readonly IDialogService _dialogService;
        private DispatcherTimer _timer = new DispatcherTimer();
        private Random _random = new Random();
        const int FLASH_INTERVAL_SECONDS = 60;

        public ShellViewModel(ISource source, IDialogService dialogService)
        {
            var randomSeconds = _random.Next(FLASH_INTERVAL_SECONDS);
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
                var randomSeconds = _random.Next(FLASH_INTERVAL_SECONDS);
                _timer.Interval = TimeSpan.FromSeconds(randomSeconds);
                _timer.Start();
            });
        }
    }
}
