using Microsoft.AspNetCore.SignalR.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntClient
{
    public class Client
    {
        private Client()
        {

        }

        private static Client instance;

        public static Client Instance()
        {
            if (instance == null)
                instance = new Client();
            return instance;
        }

        public RestClient httpClient;
        public HubConnection hubConnection;
    }
}
