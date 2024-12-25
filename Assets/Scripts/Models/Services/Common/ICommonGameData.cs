using Models.Datas;
using System.Collections.Generic;
using UniRx;

namespace Models.Services.Common
{
    public interface ICommonGameData
    {
        bool IsLoaded { get; }
        ReactiveCommand OnLoadGameData { get; }
        IReadOnlyDictionary <string, BlockModel> BlockModels { get; }
        SaveData SaveData { get; }
    }
}