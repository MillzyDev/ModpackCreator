using Avalonia;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using ModpackCreator.Modpack;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ForgedCurse;
using ForgedCurse.Json;
using ModpackCreator.Models;
using System.Collections.ObjectModel;
using ModpackCreator.ViewModels;
using System.Reflection;
using MessageBox.Avalonia;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using Avalonia.Media.Imaging;
using static ForgedCurse.Json.HashSearchResult;
using System.Text.Json;
using System.IO.Compression;

namespace ModpackCreator.Views
{
    public class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();

            CanResize = false;
#if DEBUG
            this.AttachDevTools();
#endif
        }

        ~MainWindow()
        {
            Environment.Exit(0);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void OpenSourcePage(object sender, RoutedEventArgs e)
        {
            const string url = "https://github.com/MillzyDev/ModpackCreator";
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                } 
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        public async void AddMods(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                AllowMultiple = true,
                Title = "Uploading Mods...",
                Filters = { new FileDialogFilter
                    {
                        Extensions = { "jar" },
                        Name = "Executable JAR files"
                    }
                },
            };

            string[] filenames = await fileDialog.ShowAsync(this);
            if (filenames == null || filenames == Array.Empty<string>()) 
                return;
            AddMods(filenames);
        }
    
        public async void AddMods(IEnumerable<string?> filenames)
        {
            var progressBar = new ProgressBar(Position)
            {
                Height = 155,
                Width = 600,
                Title = "Uploading Mods",
                BarTitle = "Uploading Mods",
                BarMessage = "Copying files..."
            };
            progressBar.Show();

            MainWindowViewModel dataContext = (MainWindowViewModel)DataContext;

            string tempDir = Path.GetFullPath(Path.Combine(Path.GetTempPath(), @"ModpackCreator"));
            try
            {
                Directory.Delete(tempDir);
            } catch {}
            Directory.CreateDirectory(tempDir);

            if (filenames == null) return;

            List<string> coppiedFiles = new List<string>();
            foreach (string filename in filenames) 
            {
                if (filename == null || !filename.EndsWith(".jar")) continue;
                progressBar.BarMessage = $"Copying {Path.GetFileName(filename)} to a temporary location";
                File.Copy(filename, Path.Combine(tempDir, Path.GetFileName(filename)), true);
                coppiedFiles.Add(Path.Combine(tempDir, Path.GetFileName(filename)));
            }

            ForgeClient client = new ForgeClient();

            foreach (string filename in coppiedFiles)
            {
                progressBar.BarMessage = $"Fingerprinting {Path.GetFileName(filename)}: Reading bytes";
                byte[] data = File.ReadAllBytes(filename);
                progressBar.BarMessage = $"Fingerprinting {Path.GetFileName(filename)}: Computing hash";
                uint hash = ForgeHash.ComputeHash(data);
                progressBar.BarMessage = $"Fingerprinting {Path.GetFileName(filename)}: Searching for hash";
                HashSearchResult result = await client.Files.SearchHashes(hash);
                progressBar.BarMessage = $"Fingerprinting {Path.GetFileName(filename)}: Found hash!";

                dataContext.Items.Add(new ModItem
                {
                    FileHash = hash,
                    ModID = result.Hits[0].File.Identifier.ToString(),
                    ModName = client.Addons.RetriveAddon(result.Hits[0].AddonId).Result.Name
                });

            }
            progressBar.Close();
        }


        public void OpenManifestSettings(object sender, RoutedEventArgs e)
        {
            new ManifestWindow
            {
                Width = 800,
                MaxWidth = 800,
                MinWidth = 800,
                Height = 450,
                MaxHeight = 450,
                MinHeight = 450,
            }.Show();
        }

        public async void ExportModpackAsync(object sender, RoutedEventArgs e)
        {
            Manifest manifest = Manifest.Instance;
            manifest.files = Array.Empty<Mod>();

            Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), @"ModpackCreator", @"modpack"));
            
            if (AnyValuesNullOrEmpty(manifest))
            {
                var messageBox = MessageBoxManager
                    .GetMessageBoxStandardWindow(new MessageBoxStandardParams
                    {
                        ButtonDefinitions = ButtonEnum.Ok,
                        ContentTitle = "Empty manifest properties",
                        ContentMessage = "Some values in the manifest are empty or null. All manifest values must have a value",
                        Icon = MessageBox.Avalonia.Enums.Icon.Forbidden,
                    });
                messageBox.Show();
                return;
            }

            var saveDialog = new SaveFileDialog();
            saveDialog.Title = $"Save {Manifest.Instance.name} modpack...";
            saveDialog.InitialFileName = $"{Manifest.Instance.name}-{Manifest.Instance.version}";
            var filter = new FileDialogFilter();
            filter.Name = "Zip Archives";
            filter.Extensions.Add("zip");
            saveDialog.Filters.Add(filter);
            saveDialog.DefaultExtension = "zip";

            string savePath = await saveDialog.ShowAsync(this);
            if (savePath == null || savePath == String.Empty)
                return;


            var progressBar = new ProgressBar(Position)
            {
                Height = 155,
                Width = 600,

                Title = "Creating Modpack...",
                BarTitle = "Creating Modpack...",
                BarMessage = "Fetching Mod IDs"
            };
            progressBar.Show();

            var dataContext = (MainWindowViewModel)DataContext;
            IEnumerable<ModItem> items = dataContext.Items;

            var client = new ForgeClient();

            foreach (ModItem item in items)
            {
                progressBar.BarMessage = $"Adding mods to manifest: {item.ModName}";
                HashSearchResult result = await client.Files.SearchHashes(item.FileHash);
                Mod mod = new Mod
                {
                    fileID = result.Hits[0].File.Identifier,
                    projectID = result.Hits[0].AddonId
                };
                List<Mod> files = new List<Mod>(Manifest.Instance.files);
                files.Add(mod);
                Manifest.Instance.files = files.ToArray();
            }

            progressBar.BarMessage = "Serialising manfiest...";
            string manifestJson = JsonSerializer.Serialize(Manifest.Instance, new JsonSerializerOptions
            {
                WriteIndented = true,
            });

            progressBar.BarMessage = "Writing to manifest.json";
            File.WriteAllText(Path.Combine(Path.GetTempPath(), @"ModpackCreator", @"modpack", @"manifest.json"), manifestJson);

            progressBar.BarMessage = "Writing modlist.html";

            string fullHtml = "";
            string fullList = "";
            foreach (ModItem item in items)
            {
                HashSearchResult result = await client.Files.SearchHashes(item.FileHash);
                Addon addon = await client.Addons.RetriveAddon(result.Hits[0].AddonId);
                List<string> authors = new List<string>();
                foreach (Author author in addon.Authors)
                {
                    authors.Add(author.Name);
                }
                fullList += $"<li><a href=\"{addon.Website}\">{addon.Name} (by {string.Join(", ", authors)})</a></li>";
            }
            fullHtml = $"<html><head></head><body><ul>{fullList}</ul></body></html>";
            File.WriteAllText(Path.Combine(Path.GetTempPath(), @"ModpackCreator", @"modpack", @"modlist.html"), fullHtml);

            progressBar.BarMessage = "Creating Zip Archive";
            try { File.Delete(savePath); } catch {}
            ZipFile.CreateFromDirectory(Path.Combine(Path.GetTempPath(), @"ModpackCreator", @"modpack"), savePath);
            progressBar.BarMessage = "Done!";
            progressBar.Close();
        }

        private bool AnyValuesNullOrEmpty(Manifest obj)
        {
            return string.IsNullOrEmpty(obj.name) ||
                 string.IsNullOrEmpty(obj.version) ||
                 string.IsNullOrEmpty(obj.author) ||
                 string.IsNullOrEmpty(obj.minecraft.version) ||
                 string.IsNullOrEmpty(obj.minecraft.modLoaders[0].id);
        }

        public void RemoveMod(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.Source;
            MainWindowViewModel viewModel = (MainWindowViewModel)DataContext;
            foreach (ModItem mi in viewModel.Items.ToArray())
            {
                if (mi.ModID == button.Name)
                {
                    viewModel.Items.Remove(mi);
                }
            }
        }

        public void OpenOverrides(object sender, RoutedEventArgs e)
        {
            string overridesPath = Path.Combine(Path.GetTempPath(), @"ModpackCreator", @"modpack", @"overrides");
            Directory.CreateDirectory(overridesPath);
            Process.Start(new ProcessStartInfo
            {
                FileName = overridesPath,
                UseShellExecute = true,
                Verb = "open"
            });
        }
    }
}
