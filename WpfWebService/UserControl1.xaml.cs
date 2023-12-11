using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfWebService
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private HttpListener _listener;

        public UserControl1()
        {
            InitializeComponent();
            StartServer();
        }

        private void StartServer()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:5000/");
            _listener.Start();

            Task.Run(() =>
            {
                while (_listener.IsListening)
                {
                    var context = _listener.GetContext();

                    if (context.Request.HttpMethod == "POST" && context.Request.Url.AbsolutePath == "/message")
                    {
                        using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                        {
                            string content = reader.ReadToEnd();
                            Console.WriteLine($"Received: {content}");

                            var responseString = "Message received at server";
                            var buffer = Encoding.UTF8.GetBytes(responseString);
                            context.Response.ContentLength64 = buffer.Length;
                            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                        }
                    }

                    context.Response.Close();
                }
            });
        }

        private void StopServer()
        {
            _listener?.Close();
            _listener = null;
        }

        //protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        //{
        //    base.OnClosing(e);
        //    StopServer();
        //}
    }
}
