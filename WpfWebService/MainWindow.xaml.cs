using EmbedIO.Routing;
using EmbedIO.WebApi;
using EmbedIO;
using System;
using System.Windows;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static WpfWebService.ServerController;

namespace WpfWebService
{
    public class ServerController : WebApiController
    {
        public class MessageController : WebApiController
        {
            // POST api/<controller>
            [Route(HttpVerbs.Post, "/")]
            public async Task<string> Post()
            {
                using (StreamReader reader = new StreamReader(HttpContext.Request.InputStream, Encoding.UTF8))
                {
                    var message = await reader.ReadToEndAsync();
                    System.Diagnostics.Trace.WriteLine($"Received: {message}");
                    return $"Message received: {message}";
                }
            }
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WebServer server;

        public MainWindow()
        {
            InitializeComponent();

            StartServer();
        }

        private void StartServer()
        {
            Task.Run(async () =>
            {
                server = CreateWebServer();
                await server.RunAsync();
            });
        }

        private WebServer CreateWebServer()
        {
            var server = new WebServer(o => o
                    .WithUrlPrefix("http://localhost:5000")
                    .WithMode(HttpListenerMode.EmbedIO))
                .WithWebApi("/message", m => m.WithController<MessageController>());

            return server;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            server.Dispose();
        }
    }
}
