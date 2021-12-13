using Avalonia.Media.Imaging;
using ModpackCreator.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModpackCreator.ViewModels
{
    
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ModItem> Items { get; set; } = new ObservableCollection<ModItem>(new List<ModItem>());

        public MainWindowViewModel()
        { 

        }
    }
}

