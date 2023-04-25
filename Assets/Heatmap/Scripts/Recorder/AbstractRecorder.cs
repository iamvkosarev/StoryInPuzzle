using System.Collections;
using UnityEngine;

namespace Heatmap.Recorder
{
    using Events;
    using Writers;

    public abstract class AbstractRecorder : MonoBehaviour
    {
        [SerializeField] private float _recordInterval = 0.2f;
        [SerializeField] private bool _isRecording;
        [SerializeField] private string _eventName;

        protected string EventName => _eventName;

        public virtual void StartRecorde()
        {
            recording = StartCoroutine(Recorde());
            _isRecording = true;
        }

        public virtual void StopRecorde()
        {
            if (_isRecording)
            {
                StopCoroutine(recording);
            }
            _isRecording = false;
        }

        private Coroutine recording;

        private IEnumerator Recorde()
        {
            while (true)
            {
                RecordAndSaveEvent();
                yield return new WaitForSeconds(_recordInterval);
            }
        }

        private void RecordAndSaveEvent()
        {
            var baseEvent = PrepareData();

            if (baseEvent == null) return;

            EventWriter.SaveEvent(baseEvent);
        }

        protected abstract IEventWriter EventWriter { get; }
        protected abstract BaseEvent PrepareData();
    }
}