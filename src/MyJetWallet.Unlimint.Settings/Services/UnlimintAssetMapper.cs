using System;
using System.Linq;
using MyJetWallet.Unlimint.Settings.NoSql;
using MyNoSqlServer.Abstractions;
using Newtonsoft.Json;

// ReSharper disable UnusedMember.Global

namespace MyJetWallet.Unlimint.Settings.Services
{
    public class UnlimintAssetMapper : IUnlimintAssetMapper
    {
        private readonly IMyNoSqlServerDataReader<UnlimintAssetEntity> _unlimintCoins;

        public UnlimintAssetMapper(IMyNoSqlServerDataReader<UnlimintAssetEntity> unlimintCoins)
        {
            _unlimintCoins = unlimintCoins;
        }

        public UnlimintAssetEntity GetUnlimintBySettlement(string brokerId, string settlementAsset)
        {
            var assetEntities = _unlimintCoins.Get(UnlimintAssetEntity.GeneratePartitionKey(brokerId))
                .Where(e => e.PaymentAsset == settlementAsset).ToList();

            if (!assetEntities.Any())
            {
                return null;
            }

            if (assetEntities.Count > 1)
            {
                throw new Exception(
                    $"Cannot map unlimint asset {assetEntities} to Asset. Table: {UnlimintAssetEntity.TableName}. Found many assets: {JsonConvert.SerializeObject(settlementAsset)}");
            }

            var entity = assetEntities.First();

            return entity;
        }

        public UnlimintAssetEntity GetUnlimintByPaymentAsset(string brokerId, string paymentSymbol)
        {
            return _unlimintCoins.Get(UnlimintAssetEntity.GeneratePartitionKey(brokerId), UnlimintAssetEntity.GenerateRowKey(paymentSymbol));
        }
    }
}