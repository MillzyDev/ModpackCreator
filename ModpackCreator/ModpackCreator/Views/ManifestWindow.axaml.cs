using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ModpackCreator.Modpack;
using System;

namespace ModpackCreator.Views
{
    public partial class ManifestWindow : Window
    {

        public ManifestWindow()
        {
            InitializeComponent();
            CanResize = false;

            var minecraftVersion = this.FindControl<TextBox>("mc_minecraftVersion");
            var modloader = this.FindControl<ComboBox>("mc_modloader");
            var modloaderVersion = this.FindControl<TextBox>("mc_modloaderVersion");
            // Modpack Settings
            var modpackName = this.FindControl<TextBox>("mp_modpackName");
            var modpackVersion = this.FindControl<TextBox>("mp_modpackVersion");
            var modpackAuthor = this.FindControl<TextBox>("mp_modpackAuthor");

            minecraftVersion.Text = Manifest.Instance.minecraft.version;
            string[] loaders = { "fabric", "forge" };
            if (Manifest.Instance.minecraft.modLoaders[0] != null)
            {
                string[] loader = Manifest.Instance.minecraft.modLoaders[0].id.Split("-");
                modloader.SelectedIndex = Array.IndexOf(loaders, loader[0]);
                modloaderVersion.Text = loader[1];
            }

            modpackName.Text = Manifest.Instance.name;
            modpackVersion.Text = Manifest.Instance.version;
            modpackAuthor.Text = Manifest.Instance.author;

#if DEBUG
            this.AttachDevTools();
#endif  
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void SaveManifest(object sender, RoutedEventArgs e)
        {
            // Minecraft Settings
            var minecraftVersion = this.FindControl<TextBox>("mc_minecraftVersion");
            var modloader = this.FindControl<ComboBox>("mc_modloader");
            var modloaderVersion = this.FindControl<TextBox>("mc_modloaderVersion");
            // Modpack Settings
            var modpackName = this.FindControl<TextBox>("mp_modpackName");
            var modpackVersion = this.FindControl<TextBox>("mp_modpackVersion");
            var modpackAuthor = this.FindControl<TextBox>("mp_modpackAuthor");

            string[] modloaders = { "fabric", "forge" };

            Manifest.Instance.minecraft.version = minecraftVersion.Text;
            Manifest.Instance.minecraft.modLoaders[0] = new Modloader
            {
                id = $"{modloaders[modloader.SelectedIndex]}-{modloaderVersion.Text}"
            };

            Manifest.Instance.name = modpackName.Text;
            Manifest.Instance.version = modpackVersion.Text;
            Manifest.Instance.author = modpackAuthor.Text;

            Close();
        }
    }
}
