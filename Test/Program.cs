using Fas.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
        }

        /// <summary>
        /// 异步方法
        /// </summary>
        /// <returns></returns>
        static async Task AsyncTestMethod()
        {
            await Task.Run(() => {
                    Config config = Config.Instance;
                    config.Load();
            });
        }
    }
}
