using UnityEngine;

namespace Heatmap.Recorder
{
    using Events;
    public sealed class ObjectPositionJSONRecorder : AbstractJSONRecorder
    {
        [SerializeField] private Transform @object;
        [SerializeField] private Vector3 offset;

        protected override BaseEvent PrepareData()
        {
            return new BaseEvent(EventName, @object.position + offset);
        }
    }
}