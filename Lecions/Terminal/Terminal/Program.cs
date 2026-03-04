using Terminal.Gui;

Application.Init();

var top = Application.Top;

var window = new Window("Sample App")
{
    X = 1,
    Y = 1,
    Width = Dim.Fill(1),
    Height = Dim.Fill(1),
};

var helpBar = new Label("Esc: Exit")
{
    X = 1,
    Y = Pos.AnchorEnd(1),
    Width = Dim.Fill(1),
};

var button = new Button("Ok")
{
    X = Pos.Center(),
    Y = Pos.Center(),
};

var text = new TextField("") //Поле ввода
{
    X = 1,
    Y = 1,
    Width = Dim.Fill(1),
};

var view = new TextView() //много полей ввода (указывается в виде высоты как я понял)
{
    Text = "",
    X = 1,
    Y = Pos.Bottom(text) + 1,
    Width = Dim.Fill(1),
    Height = 10,
    WordWrap = true,
};

var listView = new ListView()
{
    X = 1,
    Y = Pos.Bottom(button) + 1, //Bottim(что-то) - под чем-то
    Width = Dim.Fill(1),
    Height = Dim.Fill(1),
};

List<string> data = ["Матигоров", "Терентьев", "Власов", "Горбунов", "Панов", "Ватутин"];

listView.SetSource(data); //как .Add(), но для списка
listView.OpenSelectedItem += (args) =>
{
    var result = MessageBox.Query("Привет", args.Value.ToString(), "Ok", "Cancel");
};

button.Clicked += () =>
{
    var result = MessageBox.Query("Привет", text.Text, "Ok", "Cancel");
};

window.Add(text);
window.Add(view);
window.Add(button);
window.Add(listView);

top.Add(window);
top.Add(helpBar);

top.KeyPress += (args) =>
{
    switch (args.KeyEvent.Key)
    {
        case Key.Esc:
            Application.RequestStop(); //закрытие
            Console.Clear();
            break;
    }
};

Application.Run();