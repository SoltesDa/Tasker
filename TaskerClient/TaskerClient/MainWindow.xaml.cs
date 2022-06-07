
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskerClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HubClient hub = new HubClient();
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
         
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
           await hub.Send();
        }
    }
    public class HubClient
    {
        private HubConnection hubConnection;
        public HubClient()
        {
            Init();
        }
        public async void Init()
        {
            hubConnection = new HubConnectionBuilder()
               .WithUrl("https://localhost:7081/myhub")
               .Build();

            hubConnection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await hubConnection.StartAsync();
            };
            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var encodedMsg = $"{user}: {message}";
                MessageBox.Show(encodedMsg);
            });
            await hubConnection.StartAsync();
        }
        public async Task Send()
        {
            if (hubConnection is not null)
            {
                await hubConnection.SendAsync("SendMessage", "david", "prdel");
            }
        }
    }
}
