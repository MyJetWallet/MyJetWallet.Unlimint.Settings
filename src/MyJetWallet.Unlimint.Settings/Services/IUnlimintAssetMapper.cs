using MyJetWallet.Unlimint.Settings.NoSql;

namespace MyJetWallet.Unlimint.Settings.Services
{
    public interface IUnlimintAssetMapper
    {
        UnlimintAssetEntity GetUnlimintByPaymentAsset(string brokerId, string assetSymbol);
        UnlimintAssetEntity GetUnlimintBySettlement(string brokerId, string assetTokenSymbol);
    }
}