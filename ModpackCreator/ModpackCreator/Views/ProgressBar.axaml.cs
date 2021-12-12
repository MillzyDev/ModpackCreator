using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ModpackCreator.Views
{
    public partial class ProgressBar : Window
    {
        public string BarTitle
        {
            get =>  this.FindControl<TextBlock>("title").Text;
            set =>  this.FindControl<TextBlock>("title").Text = value;
        }

        public string BarMessage
        {
            get => this.FindControl<TextBlock>("message").Text;
            set => this.FindControl<TextBlock>("message").Text = value;
        }

        public ProgressBar()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
