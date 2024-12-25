using Cysharp.Threading.Tasks;
using Models.Datas;
using System.Threading;
using Utils.Texts;

namespace Models.Services
{
    public class LoadSaveService : ILoadSaveService
    {
        public async UniTask<SaveData> LoadData(CancellationToken cancellationToken)
        {
            return TextUtils.Load<SaveData>();
        }

        public void SaveData(SaveData saveData)
        {
            TextUtils.Save(saveData);
        }
    }
}
