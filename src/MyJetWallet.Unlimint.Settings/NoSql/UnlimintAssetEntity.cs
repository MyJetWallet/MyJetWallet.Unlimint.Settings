using MyNoSqlServer.Abstractions;

namespace MyJetWallet.Unlimint.Settings.NoSql
{
    public class UnlimintAssetEntity : MyNoSqlDbEntity
    {
        public const string TableName = "myjetwallet-unlimint-asset";

        public static string GeneratePartitionKey(string brokerId) => $"broker:{brokerId}";
        public static string GenerateRowKey(string asset) => asset;

        public string BrokerId { get; set; }
        public string PaymentAsset { get; set; }
        public string SettlementAsset { get; set; }

        public static UnlimintAssetEntity Create(UnlimintAssetEntity circleAssetEntity)
        {
            var entity = new UnlimintAssetEntity()
            {
                PartitionKey = GeneratePartitionKey(circleAssetEntity.BrokerId),
                RowKey = GenerateRowKey(circleAssetEntity.PaymentAsset),
                BrokerId = circleAssetEntity.BrokerId,
                PaymentAsset = circleAssetEntity.PaymentAsset,
                SettlementAsset = circleAssetEntity.SettlementAsset,
            };

            return entity;
        }
    }
}