using Heatmap.Events;
using UnityEngine;

namespace Heatmap.Scripts.Recorder.Concrete
{
    public class PositionFirebaseRecorder : FirebaseStorageRecorder
    {
        [SerializeField] private Transform _object;
        [SerializeField] private Vector3 _offset;

        protected override BaseEvent PrepareData()
        {
            return new BaseEvent(EventName, @_object.position + _offset);
        }
    }
}