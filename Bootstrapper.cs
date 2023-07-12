using Prism.DryIoc;
using Prism.Ioc;
using System.Windows;
using WpfApp.ViewModel;
using WpfApp.Views;

namespace WpfApp
{
    internal class Bootstrapper : PrismBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindow, MainViewModel>();

            containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>();
            containerRegistry.RegisterForNavigation<AboutView, AboutViewModel>();
        }
    }
}
