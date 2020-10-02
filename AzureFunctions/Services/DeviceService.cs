using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using MAD = Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using AzureFunctions.Models;
using System.Collections.ObjectModel;

namespace AzureFunctions.Services
{
    public static class DeviceService
    {
        public static async Task SendMessageToDeviceAsync(MAD.ServiceClient serviceClient, string targetDeviceID, string message)
        {
            var payload = new MAD.Message(Encoding.UTF8.GetBytes(message));
            await serviceClient.SendAsync(targetDeviceID, payload);
        }
    }
}
