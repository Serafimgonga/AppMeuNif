using AppConsultaNif.View;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;

namespace AppConsultaNif
{
    public partial class App : Application
    {
        public static IConfiguration? Configuration { get; private set; }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            var appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine($"App Directory: {appDirectory}");

            var builder = new ConfigurationBuilder()
                .SetBasePath(appDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }
    }
}
