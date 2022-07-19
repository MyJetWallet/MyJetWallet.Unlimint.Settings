using System;
using Autofac;
using MyJetWallet.Unlimint.Settings.NoSql;
using MyJetWallet.Unlimint.Settings.Services;
using MyJetWallet.Sdk.NoSql;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataReader;
using MyNoSqlServer.DataWriter;

namespace MyJetWallet.Unlimint.Settings.Ioc
{
    public static class AutofacHelper
    {
        public static void RegisterCircleSettingsReader(this ContainerBuilder builder, IMyNoSqlSubscriber myNoSqlClient)
        {
            builder
                .RegisterMyNoSqlReader<UnlimintAssetEntity>(myNoSqlClient, UnlimintAssetEntity.TableName);

            builder
                .RegisterType<UnlimintAssetMapper>()
                .As<IUnlimintAssetMapper>()
                .SingleInstance();
        }

        public static void RegisterCircleSettingsWriter(this ContainerBuilder builder, Func<string> myNoSqlWriterUrl)
        {
            builder
                .RegisterMyNoSqlWriter<UnlimintAssetEntity>(myNoSqlWriterUrl, UnlimintAssetEntity.TableName);
            

            builder.RegisterType<UnlimintAssetSettingsService>()
                .As<IUnlimintAssetSettingsService>()
                .SingleInstance();
        }
    }
}