using MAD = Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SharedLibrary.Services
{
    public static class DeviceService
    {
        public static readonly Random rnd = new Random();

        public static async Task SendMessageAsync(DeviceClient deviceClient)
        {
            string _json = JsonConvert.SerializeObject(new TemperatureModel(rnd.Next(20, 40), rnd.Next(0, 20)));

            Message payload = new Message(Encoding.UTF8.GetBytes(_json));

            await deviceClient.SendEventAsync(payload);

            Console.WriteLine($"Message sent: {_json}");
        }

        public static async Task ReceiveMessageAsync(DeviceClient deviceClient, ObservableCollection<BodyMessageModel> bodyMessageModels)
        {
            while(true)
            {
                var payload = await deviceClient.ReceiveAsync();

                if (payload == null)
                {
                    continue;
                }

                string message = Encoding.UTF8.GetString(payload.GetBytes());
                Console.WriteLine($"Message received: {message}");

                await deviceClient.CompleteAsync(payload);

                bodyMessageModels.Add(new BodyMessageModel { Message = message });
            }
        }
    }
}
