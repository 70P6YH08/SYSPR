using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace LabWork17.ViewModels
{
    public partial class DiskViewModel : ViewModelBase
    {
        [ObservableProperty]
        public FileDirInfo selectedDisk;

        public DiskViewModel(FileDirInfo fileDirInfo)
        {
            SelectedDisk = fileDirInfo;
        }

        public DiskViewModel()
        {

        }

    }
}
