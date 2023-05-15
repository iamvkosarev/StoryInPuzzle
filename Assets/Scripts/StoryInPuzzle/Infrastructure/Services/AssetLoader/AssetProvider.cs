using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Debug = UnityEngine.Debug;

namespace StoryInPuzzle.Infrastructure.Services.AssetLoader
{
    public abstract class AssetProvider<T>
    {
        private GameObject _loadedAsset;
        private T _assetComponent;
        protected abstract string AssetKey { get; }

        public async Task<T> Load()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            var handle = Addressables.InstantiateAsync(AssetKey);
            await handle.Task;
            if (handle.Status != AsyncOperationStatus.Succeeded)
                throw new Exception(
                    $"Asset '{AssetKey} couldn't be loaded. Maybe wrong key or asset not added to addressable.");
            _loadedAsset = handle.Result;
            _assetComponent = _loadedAsset.GetComponent<T>();
            stopwatch.Stop();
            //Debug.Log($"Загрузка {typeof(T)} - скорость работы " + stopwatch.ElapsedMilliseconds + " мс");
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