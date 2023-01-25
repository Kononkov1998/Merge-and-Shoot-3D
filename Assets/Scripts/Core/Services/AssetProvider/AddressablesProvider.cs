using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.Services.AssetProvider
{
    public class AddressablesProvider : IAssetsProvider
    {
        public async Task<T> LoadAsset<T>(string key) where T : Object
        {
            AsyncOperationHandle<Object> handle = Addressables.LoadAssetAsync<Object>(key);
            await handle.Task;
            Object result = handle.Result;

            if (result is ScriptableObject scriptableObject)
                return scriptableObject as T;

            return result.GetComponent<T>();
        }

        public async Task<IList<T>> LoadAssets<T>(string key)
        {
            AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(key, null);
            await handle.Task;
            return handle.Result;
        }
    }
}