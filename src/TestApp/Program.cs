using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MyJetWallet.Unlimint.Settings.NoSql;
using MyNoSqlServer.DataWriter;

namespace TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await GenerateAssets();
        }

        private static async Task GenerateAssets()
        {
            var broker = "jetwallet";

            //await clientAsset.CleanAndKeepMaxPartitions(0);
            //await clientAsset.BulkInsertOrReplaceAsync(list);
        }
    }
}