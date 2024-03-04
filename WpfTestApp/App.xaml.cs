using System.Windows;

namespace WpfTestApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private static ILoggerFactory _loggerFactory;
        //public static ILogger<App> Logger { get; private set; }

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    // 创建日志工厂和服务集合
        //    _loggerFactory = LoggerFactory.Create(builder =>
        //    {
        //        builder.AddConsole();
        //    });

        //    // 创建 ILogger 实例
        //    Logger = _loggerFactory.CreateLogger<App>();

        //    // 注册服务
        //    var services = new ServiceCollection();
        //    services.AddSingleton<MainWindow>();
        //    services.AddSingleton<ILoggerFactory>(_loggerFactory);
        //    services.AddSingleton<ILogger<App>>(Logger);

        //    // 构建服务提供程序
        //    var serviceProvider = services.BuildServiceProvider();

        //    // 获取 MainWindow 实例并显示
        //    var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
        //    mainWindow.Show();
        //}
    }
}
