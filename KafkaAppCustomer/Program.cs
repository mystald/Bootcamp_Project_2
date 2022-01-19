using System;
using System.Collections.Generic;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using KafkaAppCustomer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KafkaAppCustomer
{
    class Program
    {
        static int Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.json", true, true);

            var config = builder.Build();


            var Serverconfig = new ConsumerConfig
            {
                BootstrapServers = config["Settings:KafkaServer"],
                GroupId = "tester",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true; // prevent the process from terminating.
                cts.Cancel();
            };
            Console.WriteLine("--------------.NET Application--------------");
            using (var consumer = new ConsumerBuilder<string, string>(Serverconfig).Build())
            {
                Console.WriteLine("Connected");
                var topics = new string[] { "CreateOrderCustomer"};
                consumer.Subscribe(topics);

                Console.WriteLine("Waiting messages....");
                try
                {
                    while (true)
                    {
                        var cr = consumer.Consume(cts.Token);

                        Console.WriteLine($"Terdapat orderan baru dari customer {cr.Message.Value}");

                        // using (var dbcontext = new ApplicationDbContext())
                        // {
                        //     if (cr.Topic == "CreateOrderCustomer")
                        //     {
                        //         Customer customer = JsonConvert.DeserializeObject<Customer>(cr.Message.Value);
                        //         dbcontext.Customers.Add(customer);
                        //     }
                        //     await dbcontext.SaveChangesAsync();
                        //     Console.WriteLine("Data was saved into database");
                        // }


                    }
                }
                catch (OperationCanceledException)
                {
                    // Ctrl-C was pressed.
                }
                finally
                {
                    consumer.Close();
                }

            }
            return 1;
        }
    }
}

// using System;

// namespace KafkaAppCustomer
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             Console.WriteLine("Hello World!");
//         }
//     }
// }
