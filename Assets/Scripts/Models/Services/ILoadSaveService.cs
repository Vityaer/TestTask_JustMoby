using Cysharp.Threading.Tasks;
using Models.Datas;
using System.Threading;

namespace Models.Services
{
    public interface ILoadSaveService
    {
        UniTask<SaveData> LoadData(CancellationToken cancellationToken);
        void SaveData(SaveData saveData);
    }
}