using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Core.AssetLoading
{
    public abstract class AssetLoader<T> : IAssetLoader<T>
    {
        protected abstract string Key { get; }
        private GameObject _createdAsset;

        public async Task<T> LoadAssetAsync()
        {
            var handle = Addressables.InstantiateAsync(Key);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _createdAsset = handle.Result;
                if (!_createdAsset.TryGetComponent<T>(out var asset))
                {
                    throw new NullReferenceException($"Object of type {typeof(T)} is null in loaded addressable");
                }

                return asset;
            }

            Debug.LogError(handle.GetDownloadStatus());
            return default;
        }

        public void ReleaseAsset()
        {
            if (_createdAsset == null) return;
            _createdAsset.gameObject.SetActive(false);
            Addressables.Release(_createdAsset);
            _createdAsset = null;
        }
    }
}