using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace StoryInPuzzle.Infrastructure.Services.AssetLoader
{
    public abstract class AssetProvider<T>
    {
        private GameObject _loadedAsset;
        private T _assetComponent;
        protected abstract string AssetKey { get; }

        public async Task<T> Load()
        {
            var handle = Addressables.InstantiateAsync(AssetKey);
            await handle.Task;
            if (handle.Status != AsyncOperationStatus.Succeeded)
                throw new Exception(
                    $"Asset '{AssetKey} couldn't be loaded. Maybe wrong key or asset not added to addressable.");
            _loadedAsset = handle.Result;
            _assetComponent = _loadedAsset.GetComponent<T>();
            return _assetComponent;
        }

        public async Task Unload()
        {
            if (_loadedAsset == null) return;
            _loadedAsset.gameObject.SetActive(false);
            Addressables.Release(_loadedAsset);
            _loadedAsset = null;
        }
    }
}