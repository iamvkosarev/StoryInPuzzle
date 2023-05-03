using System;
using Firebase.Storage;
using UnityEngine;

namespace Heatmap.Scripts.Recorder
{
    public class FirebaseStorageRecorder : JSONRecorder
    {
        private readonly string _storageFilePath;

        public override void StopRecorde()
        {
            base.StopRecorde();
            SendDataToStorage();
        }

        private void SendDataToStorage()
        {
            Debug.Log($"Send date: {SavePath}");
            var storage = FirebaseStorage.DefaultInstance;
            var storageRef = storage.RootReference;
            var riversRef = storageRef.Child(_storageFilePath);
            riversRef.PutFileAsync(SavePath)
                .ContinueWith(task =>
                {
                    if (task.IsFaulted || task.IsCanceled)
                    {
                        Debug.Log(task.Exception.ToString());
                    }
                    else
                    {
                        var metadata = task.Result;
                        var md5Hash = metadata.Md5Hash;
                        Debug.Log("Finished uploading...");
                        Debug.Log("md5 hash = " + md5Hash);
                    }
                });
        }

        public FirebaseStorageRecorder(RecordeSettingContainer recordeSettingContainer, string savePath,
            string storageFilePath, ICoroutineRunner coroutineRunner) : base(recordeSettingContainer, savePath,
            coroutineRunner)
        {
            _storageFilePath = storageFilePath;
        }
    }
}