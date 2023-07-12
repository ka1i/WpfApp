using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using WpfApp.Common.Models;
using WpfApp.Extensions;

namespace WpfApp.ViewModel
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel(IRegionManager regionManager)
        {
            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();

            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            GoBackCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack)
                    journal.GoBack();
            });

            GoNextCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoForward)
                    journal.GoForward();
            });
            this.regionManager = regionManager;
        }

        public DelegateCommand<MenuBar> NavigateCommand { get; set; }
        public DelegateCommand GoBackCommand { get; set; }
        public DelegateCommand GoNextCommand { get; set; }

        public void Navigate(MenuBar obj)
        {
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
            {
                return;
            }
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(obj.NameSpace, back =>
            {
                journal = back.Context.NavigationService.Journal;
            });
        }

        private ObservableCollection<MenuBar>? menuBars = null;
        private readonly IRegionManager regionManager;
        private IRegionNavigationJournal? journal = null;

        public ObservableCollection<MenuBar>? MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

        void CreateMenuBar()
        {
            if (MenuBars is not null)
            {
                MenuBars.Add(new MenuBar() { Icon = "Home", Title = "首页", NameSpace = "HomeView" });
                MenuBars.Add(new MenuBar() { Icon = "InformationOutline", Title = "关于", NameSpace = "AboutView" });
            }
        }
    }
}
