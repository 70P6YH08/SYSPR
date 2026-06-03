using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static FileDirInfo;
using Path = System.IO.Path;

namespace LabWork17.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MainViewModel viewModel = new();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void OpenFolder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = DisksDataGrid.SelectedItem as FileDirInfo ?? FilesDataGrid.SelectedItem as FileDirInfo;

            if (selectedItem == null)
            {
                MessageBox.Show("Такого файла не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (selectedItem?.Type == FileType.HardDrive)
                {
                    var currentPath = viewModel.SearchString;
                    var newSearchPath = Path.Combine(currentPath, selectedItem.Name);
                    viewModel.Search(newSearchPath);
                    DirPathTextBox.Text = newSearchPath;

                    viewModel.IsVisibleDisks = Visibility.Collapsed;
                    viewModel.IsVisibleFiles = Visibility.Visible;
                    viewModel.IsProperiesMenuVisible = Visibility.Visible;
                }
                else if (selectedItem?.Type == FileType.ParentFolder)
                {
                    var currentDirPath = DirPathTextBox.Text;
                    if (currentDirPath == Environment.GetFolderPath(Environment.SpecialFolder.MyComputer))
                    {
                        MessageBox.Show("Вы уже находитесь в 'Мой компьюьтер'",
                            "Предупреждение",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        return;
                    }
                    var parentFolderPath = Directory.GetParent(currentDirPath)?.FullName ??
                        Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);

                    viewModel.Search(parentFolderPath);
                    DirPathTextBox.Text = parentFolderPath;

                    if (parentFolderPath == Environment.GetFolderPath(Environment.SpecialFolder.MyComputer))
                    {
                        viewModel.IsVisibleDisks = Visibility.Visible;
                        viewModel.IsVisibleFiles = Visibility.Collapsed;
                        viewModel.IsProperiesMenuVisible = Visibility.Visible;
                    }
                    else
                    {
                        viewModel.IsVisibleDisks = Visibility.Collapsed;
                        viewModel.IsVisibleFiles = Visibility.Visible;
                        viewModel.IsProperiesMenuVisible = Visibility.Visible;
                    }
                }
                else if (selectedItem?.Type == FileType.Folder)
                {
                    var currentPath = viewModel.SearchString;
                    var newSearchPath = Path.Combine(currentPath, selectedItem.Name);

                    viewModel.Search(newSearchPath);
                    DirPathTextBox.Text = newSearchPath;

                    viewModel.IsVisibleDisks = Visibility.Collapsed;
                    viewModel.IsVisibleFiles = Visibility.Visible;
                    viewModel.IsProperiesMenuVisible = Visibility.Visible;
                }
                else
                {
                    var nameFile = selectedItem.Name;

                    if (nameFile != null)
                    {
                        var currentPath = viewModel.SearchString;
                        var filePath = Path.Combine(currentPath, nameFile);
                        ProcessStartInfo fileWithInfo = new ProcessStartInfo
                        {
                            FileName = filePath,
                            UseShellExecute = true,
                        };
                        Process.Start(fileWithInfo);
                    }
                    viewModel.IsVisibleDisks = Visibility.Collapsed;
                    viewModel.IsVisibleFiles = Visibility.Visible;
                    viewModel.IsProperiesMenuVisible = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DirPathTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var newFolderPath = DirPathTextBox.Text;

                viewModel.Search(newFolderPath);
                DirPathTextBox.Text = newFolderPath;

                if (newFolderPath == Environment.GetFolderPath(Environment.SpecialFolder.MyComputer))
                {
                    viewModel.IsVisibleDisks = Visibility.Visible;
                    viewModel.IsVisibleFiles = Visibility.Collapsed;
                    viewModel.IsProperiesMenuVisible = Visibility.Visible;
                }
                else
                {
                    viewModel.IsVisibleDisks = Visibility.Collapsed;
                    viewModel.IsVisibleFiles = Visibility.Visible;
                    viewModel.IsProperiesMenuVisible = Visibility.Visible;
                }
            }
        }

        private void CopyFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            StringCollection filesList = new();

            var selectedItems = FilesDataGrid.SelectedItems;
            foreach (FileDirInfo item in selectedItems)
            {
                string filePath = Path.Combine(viewModel.SearchString, item.Name);
                filesList.Add(filePath);
            }
            Clipboard.SetFileDropList(filesList);
        }

        private void PasteFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsFileDropList())
            {
                var currentFolderPath = viewModel.SearchString;
                var clipboardList = Clipboard.GetFileDropList();

                foreach (string? clipboardFilePath in clipboardList)
                {
                    var fileName = Path.GetFileName(clipboardFilePath);
                    var newFilePath = Path.Combine(currentFolderPath, fileName);
                    if (!File.Exists(newFilePath))
                    {
                        try
                        {
                            if(clipboardFilePath != null)
                                File.Copy(clipboardFilePath, newFilePath, true);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                        MessageBox.Show($"В папке уже есть файл {fileName}", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                viewModel.Search(currentFolderPath);
            }
            else
                MessageBox.Show("Буфер обмена пуст!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CompressFolderMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var currentFolderPath = viewModel.SearchString;

            var selectedItem = FilesDataGrid.SelectedItem as FileDirInfo;
            var fullFolderPath = Path.Combine(currentFolderPath, selectedItem.Name);

            if (selectedItem?.Type == FileType.Folder)
            {
                var zipFile = fullFolderPath + ".zip";
                if (!File.Exists(zipFile))
                    ZipFile.CreateFromDirectory(fullFolderPath, zipFile);
                else
                    MessageBox.Show("Такой архив уже существует", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            viewModel.Search(currentFolderPath);
        }

        private void ArchiveExtractMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var currentFolderPath = viewModel.SearchString;

            var selectedItem = FilesDataGrid.SelectedItem as FileDirInfo;
            var targetFolderPath = "";


            if (selectedItem?.Type == FileType.Archive)
            {
                var zipFilePath = Path.Combine(currentFolderPath, selectedItem.Name);

                var dirInfo = new DirectoryInfo(currentFolderPath);
                foreach (var dir in dirInfo.GetDirectories())
                {
                    string dirName = dir.Name;
                    if (zipFilePath.Contains(dirName))
                        targetFolderPath = Path.Combine(currentFolderPath, dirName);
                }
                ZipFile.ExtractToDirectory(zipFilePath, targetFolderPath, true);
            }
            viewModel.Search(currentFolderPath);
        }
    }
}