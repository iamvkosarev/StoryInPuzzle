using UnityEngine;

namespace Heatmap.Recorder
{
    using Events;
    public class ObjectPositionRecorder : AbstractJSONRecorder
    {
        [SerializeField] private Transform @object;
        [SerializeField] private Vector3 offset;

        protected override BaseEvent PrepareData()
        {
            return new BaseEvent(EventName, @object.position + offset);
        }
    }
}