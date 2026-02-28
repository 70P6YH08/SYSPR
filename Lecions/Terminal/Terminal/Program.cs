using Terminal.Gui;

Application.Init();

using Window window = new Window() { Title = "Hello World (Esc to quit)" };
Label label = new()
{
    Text = "Hello, Terminal.Gui v2!",
    X = Pos.Center(),
    Y = Pos.Center()
};
window.Add(label);

Application.Run(window);