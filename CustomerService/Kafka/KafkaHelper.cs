using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CustomerService.Models;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Configuration;

namespace CustomerService.Kafka
{
    public class KafkaHelper
    {
        public static async Task<bool> SendMessage(IConfiguration configuration, string topic, string key, string val)
        {
            var succeed = false;
            var config = new ProducerConfig
            {
                BootstrapServers = configuration["KafkaSettings:Server"],
                ClientId = Dns.GetHostName(),
            };

            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                producer.Produce(topic, new Message<string, string>
                {
                    Key = key,
                    Value = val
                }, (deliveryReport) =>
                {
                    if (deliveryReport.Error.Code != ErrorCode.NoError)
                    {
                        Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                    }
                    else
                    {
                        // TODO delete trace logs
                        Console.WriteLine(deliveryReport.Error.Reason);
                        Console.WriteLine(deliveryReport.Message.Value);
                        Console.WriteLine($"Produced message to: {deliveryReport.TopicPartitionOffset}");
                        succeed = true;
                    }
                });
                producer.Flush(TimeSpan.FromSeconds(10));
            }

            return await Task.FromResult(succeed);
        }

        public static void CreateTopic(IConfiguration configuration, string topic)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = configuration["KafkaSettings:Server"],
                ClientId = Dns.GetHostName(),
            };

            using (var adminClient = new AdminClientBuilder(config).Build())
            {
                try
                {
                    adminClient.CreateTopicsAsync(new List<TopicSpecification> {
                        new TopicSpecification {
                            Name = topic,
                            NumPartitions = Convert.ToInt32(configuration["KafkaSettings:NumPartitions"]),
                            ReplicationFactor = Convert.ToInt16(configuration["KafkaSettings:ReplicationFactor"])
                            }
                        });
                }
                catch (CreateTopicsException e)
                {
                    if (e.Results[0].Error.Code != ErrorCode.TopicAlreadyExists)
                    {
                        Console.WriteLine($"An error occured creating topic {topic}: {e.Results[0].Error.Reason}");
                    }
                    else
                    {
                        Console.WriteLine("Topic already exists");
                    }
                }
            }
        }
    }
}