using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui.Drivers;
using Terminal.Gui.Input;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

namespace ConsoleFileManager
{
    internal class MainWindow : Window
    {
        FilesPanel leftPanel;
        FilesPanel rightPanel;

        private void InitializeComponent()
        {
            leftPanel = new FilesPanel()
            {
                X = 0,
                Y = 0,
                Height = Dim.Fill(),
                Width = Dim.Percent(50),
            };

            rightPanel = new FilesPanel()
            {
                X = Pos.Percent(50),
                Y = 0,
                Height = Dim.Fill(),
                Width = Dim.Percent(50),
            };

            Add(rightPanel);
            Add(leftPanel);

            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object? sender, Key e)
        {
            switch (e.KeyCode)
            {
                case KeyCode.F5:
                    var panels = GetPanels();
                    var file = panels.source.SelectedFullName;
                    File.Copy(file,
                        Path.Combine(panels.target.CurrectPath,
                        Path.GetFileName(file)));
                    panels.target.Refresh();
                    break;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private (FilesPanel source, FilesPanel target) GetPanels()
        {
            return leftPanel.IsSelected
                ? (leftPanel, rightPanel)
                : (rightPanel, leftPanel);
        }

    }
}
