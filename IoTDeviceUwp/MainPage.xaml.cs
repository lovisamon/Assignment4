using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Windows.UI.ViewManagement;
using Microsoft.Azure.Devices.Client;
using SharedLibrary.Models;
using SharedLibrary.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IoTDeviceUwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static string _conn = "HostName=ec-win20-ass3-iothub.azure-devices.net;DeviceId=IoTDevice;SharedAccessKey=2vh23SxhEp2EC+oXs5gQ8NEZRjybcPHYXc0jKwO53y0=";
        private static DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(_conn, TransportType.Mqtt);

        ObservableCollection<BodyMessageModel> bodyMessageModels = new ObservableCollection<BodyMessageModel>();
        public ObservableCollection<BodyMessageModel> BodyMessageModels => bodyMessageModels;

        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(400, 800);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            DeviceService.ReceiveMessageAsync(deviceClient, bodyMessageModels).GetAwaiter();
            Input.ItemsSource = bodyMessageModels;
        }

        private void btnSendTemp_Click(object sender, RoutedEventArgs e)
        {
            DeviceService.SendMessageAsync(deviceClient).GetAwaiter();
        }
    }
}
