using System;

namespace SyncStory
{
    class Program
    {
        static void Main(string[] args)
        {
            var queryProxy = DispatchProxy.Create<ISql, SqlProxy>();

            Console.WriteLine("Hello World!");
        }

        static void SyncBook()
        {

        }

    }
}
