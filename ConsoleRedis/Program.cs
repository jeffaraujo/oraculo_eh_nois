using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
namespace ConsoleRedis
{
    class Program
    {
        static void Main(string[] args)
        {

            // Connect
            //var redis = ConnectionMultiplexer.Connect("191.232.234.20");
            var redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();          


            var sub = redis.GetSubscriber();
            sub.Subscribe("perguntas", (ch, msg) =>
                {
                    string[] split = msg.ToString().Split(':');
                    string split2 = split[0].Substring(1, split[0].Length - 1);
                    int resposta = Convert.ToInt32(split2);
                    db.HashSet(split[0], "Só os Jeffersons", resposta + resposta);
                    Console.WriteLine(resposta + resposta);
                }
            );

            Console.Read();
        }
    }
}
