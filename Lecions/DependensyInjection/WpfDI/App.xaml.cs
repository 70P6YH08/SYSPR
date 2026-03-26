using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace WpfDI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider Services { get; set; }

        public App()
        {
            Services = new ServiceCollection()
                .AddSingleton<MainWindow>()
                .AddTransient<FilesServices>()
                .BuildServiceProvider();

            this.Run(Services.GetRequiredService<MainWindow>());
        }
    }

}
