using Firebase.Storage;
using UnityEngine;

namespace Heatmap.Scripts.Recorder
{
    public abstract class FirebaseStorageRecorder : JSONRecorder
    {
        [SerializeField] private string _fileNameInStorage;
        
        public override void StopRecorde()
        {
            base.StopRecorde();
            SendDataToStorage();
        }

        private void SendDataToStorage()
        {
            Debug.Log($"Send date: {Path}");
            var storage = FirebaseStorage.DefaultInstance;
            var storageRef = storage.RootReference;
            var riversRef = storageRef.Child(_fileNameInStorage);
            riversRef.PutFileAsync(Path)
                .ContinueWith(task => {
                    if (task.IsFaulted || task.IsCanceled) {
                        Debug.Log(task.Exception.ToString());
                    }
                    else {
                        var metadata = task.Result;
                        var md5Hash = metadata.Md5Hash;
                        Debug.Log("Finished uploading...");
                        Debug.Log("md5 hash = " + md5Hash);
                    }
                });
        }
    }
}