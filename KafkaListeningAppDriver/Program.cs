using System;
using System.Threading;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace KafkaListeningAppDriver
{
    class Program
    {
        static void Main(string[] args)
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

            var topic = "CreateOrderCustomer";

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            using (var consumer = new ConsumerBuilder<string, string>(Serverconfig).Build())
            {
                consumer.Subscribe(topic);
                Console.WriteLine("Waiting messages....");

                try
                {
                    while (true)
                    {
                        var cr = consumer.Consume(cts.Token);
                        Console.WriteLine($"Consumed record with key: {cr.Message.Key} and value: {cr.Message.Value}");
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
        }
    }
}
