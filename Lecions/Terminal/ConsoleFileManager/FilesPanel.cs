using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

namespace ConsoleFileManager
{
    public class FilesPanel : FrameView
    {
        public string CurrectPath { get; private set; } = @"\";
        public bool IsSelected => listView.HasFocus;

        public string SelectedFullName =>
            Path.Combine(CurrectPath, listView.SelectedItem >= 0
                ? listView.Source.ToList()[listView.Value.Value] as string
                : "");

        private ListView listView;

        public FilesPanel()
        {
            listView = new ListView()
            {
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };

            listView.Accepted += ListView_Accepted;

            listView.FocusedChanged += ListView_FocusedChanged;

            Add(listView);
            Refresh();
        }

        private void ListView_FocusedChanged(object?  sender, HasFocusEventArgs e)
        {
            if (IsSelected)
            {

            }
        }
        
        private void ListView_Accepted(object? sender, Terminal.Gui.Input.CommandEventArgs e)
        {
            var selected = listView.Source.ToList()[listView.Value.Value] as string;
            Navigate(selected);
        }

        private void Navigate(string target)
        {
            if (target == "..")
            {
                CurrectPath = Directory.GetParent(CurrectPath)?.FullName ?? CurrectPath;
                Refresh();
            }
            else
            {
                string path = Path.Combine(CurrectPath, target.TrimStart('\\'));
                if (Directory.Exists(path))
                {
                    CurrectPath = path;
                    Refresh();
                }
            }
        }

        private void Refresh()
        {
            try
            {
                Title = CurrectPath;

                var entries = Directory.GetFileSystemEntries(CurrectPath)
                    .OrderBy(e => e)
                    .Select(e => (Directory.Exists(e) ? "\\" : "") + Path.GetFileName(e))
                    .Prepend("..")
                    .ToList();
                listView.SetSource([..entries]);
            }
            catch (Exception ex)
            {
                listView.SetSource(["..", "<Error>"]);
            }
        }
    }
}
