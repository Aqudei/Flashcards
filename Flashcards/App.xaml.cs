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
            containerRegistry.Register<ISource, ExcelSource>();
            containerRegistry.RegisterDialog<FlashDialog, FlashDialogViewModel>();
            containerRegistry.RegisterDialogWindow<MetroDialogWindow>();
        }
    }
}
