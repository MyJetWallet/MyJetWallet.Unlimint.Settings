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

        public UnlimintAssetEntity AssetToUnlimint(string brokerId, string assetSymbol)
        {
            var assetEntities = _unlimintCoins.Get(UnlimintAssetEntity.GeneratePartitionKey(brokerId)).Where(e => e.AssetSymbol == assetSymbol).ToList();

            if (!assetEntities.Any())
            {
                return null;
            }

            if (assetEntities.Count > 1)
            {
                throw new Exception(
                    $"Cannot map unlimint asset {assetEntities} to Asset. Table: {UnlimintAssetEntity.TableName}. Found many assets: {JsonConvert.SerializeObject(assetSymbol)}");
            }

            var entity = assetEntities.First();

            return entity;
        }

        public UnlimintAssetEntity UnlimintToAsset(string brokerId, string assetTokenSymbol)
        {
            var assetEntities = _unlimintCoins.Get(UnlimintAssetEntity.GeneratePartitionKey(brokerId)).Where(e => e.AssetTokenSymbol == assetTokenSymbol).ToList();

            if (!assetEntities.Any())
            {
                return null;
            }

            if (assetEntities.Count > 1)
            {
                throw new Exception(
                    $"Cannot map unlimint asset {assetEntities} to Asset. Table: {UnlimintAssetEntity.TableName}. Found many assets: {JsonConvert.SerializeObject(assetTokenSymbol)}");
            }

            var entity = assetEntities.First();

            return entity;
        }

        public UnlimintAssetEntity AssetToUnlimintToken(string brokerId, string unlimintAsset)
        {
            return _unlimintCoins.Get(UnlimintAssetEntity.GeneratePartitionKey(brokerId), UnlimintAssetEntity.GenerateRowKey(unlimintAsset));
        }
    }
}