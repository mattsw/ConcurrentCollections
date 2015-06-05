namespace CSharpConcurrentCollections
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class Program
    {
        private static object _lockObj = new object();

        public static void Main(string[] args)
        {
            var orders = new Queue<string>();
            //PlaceOrders(orders, "Matt");
            //PlaceOrders(orders, "Mike");
            Task task1 = Task.Run(() => PlaceOrders(orders, "Matt"));
            Task task2 = Task.Run(() => PlaceOrders(orders, "Mike"));
            Task.WaitAll(task1, task2);
            Parallel.ForEach(orders, ProcessOrder);

            foreach (var order in orders)
            {
                Console.WriteLine("Order: " + order);
            }

            Console.ReadKey();
        }

        public static void ProcessOrder(string order)
        {
            Console.WriteLine("Order processed: " + order);
        }

        public static void PlaceOrders(Queue<string> orders, string customerName)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1);
                string orderName = string.Format("{0} wants t-shirt {1}", customerName, i + 1);
                lock (_lockObj)
                {
                    orders.Enqueue(orderName);
                }
            }
        }
    }
}
