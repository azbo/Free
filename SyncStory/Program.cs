﻿using Fas;
using Fas.Sql;
using Fas.Util;
using System;
using System.Reflection;

namespace SyncStory
{
    class Program
    {
        static void Main(string[] args)
        {
            new YouTuService().SyncBook();

            var queryProxy = DispatchProxy.Create<ISql, SqlProxy>();

            Console.WriteLine("Hello World!");
        }

        static void SyncBook()
        {

        }

    }
}
