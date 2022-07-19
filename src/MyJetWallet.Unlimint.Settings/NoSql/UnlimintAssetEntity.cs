using MyNoSqlServer.Abstractions;

namespace MyJetWallet.Unlimint.Settings.NoSql
{
    public class UnlimintAssetEntity : MyNoSqlDbEntity
    {
        public const string TableName = "myjetwallet-unlimint-asset";

        public static string GeneratePartitionKey(string brokerId) => $"broker:{brokerId}";
        public static string GenerateRowKey(string asset) => asset;

        public string BrokerId { get; set; }
        public string UnlimintAsset { get; set; }
        public string AssetSymbol { get; set; }
        public string AssetTokenSymbol { get; set; }

        public static UnlimintAssetEntity Create(UnlimintAssetEntity circleAssetEntity)
        {
            var entity = new UnlimintAssetEntity()
            {
                PartitionKey = GeneratePartitionKey(circleAssetEntity.BrokerId),
                RowKey = GenerateRowKey(circleAssetEntity.UnlimintAsset),
                BrokerId = circleAssetEntity.BrokerId,
                UnlimintAsset = circleAssetEntity.UnlimintAsset,
                AssetSymbol = circleAssetEntity.AssetSymbol,
                AssetTokenSymbol = circleAssetEntity.AssetTokenSymbol,
            };

            return entity;
        }
    }
}