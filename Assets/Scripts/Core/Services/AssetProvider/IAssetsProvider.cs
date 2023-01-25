using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.Services.AssetProvider
{
    public interface IAssetsProvider : IService
    {
        Task<T> LoadAsset<T>(string key) where T : Object;
        Task<IList<T>> LoadAssets<T>(string key);
    }
}