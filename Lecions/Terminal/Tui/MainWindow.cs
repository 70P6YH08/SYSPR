using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

namespace Tui
{
    internal class MainWindow : Terminal.Gui.Views.Window
    {
        FrameView panel1;//Панель, содержащая рамку и название
        FrameView panel2;

        ListView files1;
        ListView files2;

        private void InitizlizeComponent()
        {
            panel1 = new FrameView()
            {
                X = 0,
                Y = 0,
                Height = Dim.Fill(),
                Width = Dim.Percent(50),
            };

            panel2 = new FrameView()
            {
                X = Pos.Percent(50),
                Y = 0,
                Height = Dim.Fill(),
                Width = Dim.Percent(50),
            };

            files1 = new ListView()
            {
                Height = Dim.Fill(),
                Width = Dim.Fill(),
            };

            files2 = new ListView()
            {
                Height = Dim.Fill(),
                Width = Dim.Fill(),
            };

            panel1.Add(files1);
            panel2.Add(files2);

            this.Add(panel1);
            this.Add(panel2);
        }


        public MainWindow()
        {
            InitizlizeComponent();

            var files = Directory.GetFiles("C:\\Temp\\").ToList();

            files1.SetSource([..files]); // [.. ...] перевод в коллекцию
            files2.SetSource([..files]);
        }
    }
}
