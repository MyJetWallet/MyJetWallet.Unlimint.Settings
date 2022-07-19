using MyJetWallet.Unlimint.Settings.NoSql;

namespace MyJetWallet.Unlimint.Settings.Services
{
    public interface IUnlimintAssetMapper
    {
        UnlimintAssetEntity AssetToUnlimint(string brokerId, string assetSymbol);
        UnlimintAssetEntity UnlimintToAsset(string brokerId, string unlimintAsset);
        UnlimintAssetEntity AssetToUnlimintToken(string brokerId, string assetTokenSymbol);
    }
}