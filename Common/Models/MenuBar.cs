using Prism.Mvvm;

namespace WpfApp.Common.Models
{
    public class MenuBar : BindableBase
    {
        public string? Icon { get; set; }
        public string? Title { get; set; }

        public string? NameSpace { get; set; }
    }
}
