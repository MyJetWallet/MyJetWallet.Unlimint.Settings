using System.Threading.Tasks;
using MyJetWallet.Unlimint.Settings.NoSql;

namespace MyJetWallet.Unlimint.Settings.Services
{
    public interface IUnlimintAssetSettingsService
    {
        ValueTask<bool> CreateAssetMapEntityAsync(UnlimintAssetEntity entity);

        ValueTask<bool> UpdateAssetMapEntityAsync(UnlimintAssetEntity entity);

        ValueTask<bool> DeleteAssetMapEntityAsync(UnlimintAssetEntity entity);

        ValueTask<UnlimintAssetEntity[]> GetAllAssetMapsAsync();
    }
}