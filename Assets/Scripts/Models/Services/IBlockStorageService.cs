using System.Collections.Generic;
using UniRx;

namespace Models.Services
{
    public interface IBlockStorageService
    {
        IReactiveCollection<List<GameBlock>> BlocksChanges { get; }
    }
}