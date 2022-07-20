using System;
using System.Linq;
using System.Threading.Tasks;
using MyJetWallet.Unlimint.Settings.NoSql;
using MyNoSqlServer.Abstractions;

namespace MyJetWallet.Unlimint.Settings.Services
{
    public class UnlimintAssetSettingsService : IUnlimintAssetSettingsService
    {
        private readonly IMyNoSqlServerDataWriter<UnlimintAssetEntity> _writer;

        public UnlimintAssetSettingsService(IMyNoSqlServerDataWriter<UnlimintAssetEntity> unlimintAssets)
        {
            _writer = unlimintAssets;
        }

        public async ValueTask<bool> CreateAssetMapEntityAsync(UnlimintAssetEntity entity)
        {
            if (string.IsNullOrEmpty(entity.BrokerId))
                throw new Exception("Cannot create unlimint asset. BrokerId cannot be empty");
            if (string.IsNullOrEmpty(entity.SettlementAsset))
                throw new Exception("Cannot create unlimint asset. SettlementAsset cannot be empty");
            if (string.IsNullOrEmpty(entity.PaymentAsset))
                throw new Exception("Cannot create unlimint asset. PaymentAsset cannot be empty");

            var newEntity = UnlimintAssetEntity.Create(entity);

            var existingItem = await _writer.GetAsync(newEntity.PartitionKey, newEntity.RowKey);
            if (existingItem != null) throw new Exception("Cannot create unlimint asset. Already exist");

            await _writer.InsertAsync(newEntity);

            return true;
        }

        public async ValueTask<bool> UpdateAssetMapEntityAsync(UnlimintAssetEntity entity)
        {
            if (string.IsNullOrEmpty(entity.BrokerId))
                throw new Exception("Cannot update unlimint asset. BrokerId cannot be empty");
            if (string.IsNullOrEmpty(entity.PaymentAsset))
                throw new Exception("Cannot update unlimint asset. PaymentAsset cannot be empty");
            if (string.IsNullOrEmpty(entity.SettlementAsset))
                throw new Exception("Cannot create unlimint asset. SettlementAsset cannot be empty");

            var newEntity = UnlimintAssetEntity.Create(entity);

            var existingEntity = await _writer.GetAsync(newEntity.PartitionKey, newEntity.RowKey);
            if (existingEntity == null) throw new Exception("Cannot update unlimint asset. unlimint asset not found");

            await _writer.InsertOrReplaceAsync(newEntity);

            return true;
        }

        public async ValueTask<bool> DeleteAssetMapEntityAsync(UnlimintAssetEntity entity)
        {
            if (string.IsNullOrEmpty(entity.BrokerId))
                throw new Exception("Cannot delete unlimint asset. BrokerId cannot be empty");
            if (string.IsNullOrEmpty(entity.PaymentAsset))
                throw new Exception("Cannot delete unlimint asset. PaymentAsset cannot be empty");

            var existingEntity = await _writer.GetAsync(UnlimintAssetEntity.GeneratePartitionKey(entity.BrokerId),
                UnlimintAssetEntity.GenerateRowKey(entity.PaymentAsset));

            if (existingEntity != null)
            {
                await _writer.DeleteAsync(existingEntity.PartitionKey, existingEntity.RowKey);
            }

            return true;
        }

        public async ValueTask<UnlimintAssetEntity[]> GetAllAssetMapsAsync()
        {
            var entities = await _writer.GetAsync();
            return entities.ToArray();
        }
    }
}