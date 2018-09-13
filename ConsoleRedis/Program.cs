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
            var redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();



            db.HashSet("P1", "1+1", "2");
            db.HashSet("P1", "1+2", "3");
            db.HashSet("P1", "2+2", "4");
            db.HashSet("P1", "4+1", "5");



            var sub = redis.GetSubscriber();
            sub.Subscribe("Oraculo_eh_nois", (ch, msg) => 
                {
                    
                    


                    var q = db.HashGet("P1", msg.ToString());
                    Console.WriteLine(q);
                }
            );

            Console.Read();
        }
    }
}
