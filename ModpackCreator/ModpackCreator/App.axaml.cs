using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ModpackCreator.ViewModels;
using ModpackCreator.Views;

namespace ModpackCreator
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    
                    Width = 800,
                    MaxWidth = 800,
                    MinWidth = 800,
                    Height = 450,
                    MaxHeight = 450,
                    MinHeight = 450,
                };
                desktop.MainWindow.DataContext = new MainWindowViewModel();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
