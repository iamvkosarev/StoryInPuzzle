using Heatmap.Events;
using UnityEngine;

namespace Heatmap.Recorder
{
    public abstract class AbstractGoogleStorageRecorder : AbstractJSONRecorder
    {
        public override void StopRecorde()
        {
            base.StopRecorde();
            SendDataToStorage();
        }

        private void SendDataToStorage()
        {
            Debug.Log($"Send date: {Path}");

        }
    }
}