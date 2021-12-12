using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using System;
using System.IO;

namespace ModpackCreator
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }
        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();

        static void OnProcessExit(object sender, EventArgs e)
        {
            try
            {
                Directory.Delete(Path.Combine(Path.GetTempPath(), @"ModpackCreator"), true);
            }
            catch {}
        }
    }
}
