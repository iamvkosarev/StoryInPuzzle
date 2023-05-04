using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.AssetLoading
{
    public interface IAssetLoader<T>
    {
        Task<T> LoadAssetAsync();
        void ReleaseAsset();
    }
}