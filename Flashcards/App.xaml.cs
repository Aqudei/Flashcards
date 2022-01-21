using Flashcards.Interfaces;
using Flashcards.ViewModels;
using Flashcards.Views;
using OfficeOpenXml;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Flashcards
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            var shell = Container.Resolve<Shell>();
            return shell;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (Flashcards.Properties.Settings.Default.UseGoogleSheet)
            {
                containerRegistry.Register<ISource, GSSource>();
            }
            else
            {
                containerRegistry.Register<ISource, ExcelSource>();
            }
            containerRegistry.RegisterDialog<FlashDialog, FlashDialogViewModel>();
            containerRegistry.RegisterDialog<SettingsDialog, SettingsViewModel>();
            containerRegistry.RegisterDialogWindow<MetroDialogWindow>();
        }
    }
}
