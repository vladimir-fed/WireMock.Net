﻿using System;
using Newtonsoft.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;


namespace WireMock.Net.ConsoleApplication
{
    static class Program
    {
        static void Main(string[] args)
        {
            int port;
            if (args.Length == 0 || !int.TryParse(args[0], out port))
                port = 8080;

            var server = FluentMockServer.Start(port);
            Console.WriteLine("FluentMockServer running at {0}", server.Port);

            server
              .Given(
                RequestBuilder
                    .WithUrl("/*")
                    .UsingGet()
              )
              .RespondWith(
                ResponseBuilder
                  .WithStatusCode(200)
                  .WithHeader("Content-Type", "application/json")
                  .WithBody(@"{ ""msg"": ""Hello world!""}")
              );

            Console.WriteLine("Press any key to stop the server");
            Console.ReadKey();


            Console.WriteLine("Displaying all requests");
            var allRequests = server.RequestLogs;
            Console.WriteLine(JsonConvert.SerializeObject(allRequests, Formatting.Indented));

            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }
    }
}