using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;

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

        public ProgressBar() {}

        public ProgressBar(PixelPoint pos)
        {
            CanResize = false;
            HasSystemDecorations = false;

            

            Position = pos.WithX(pos.X + 100).WithY(pos.Y + 50);

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
