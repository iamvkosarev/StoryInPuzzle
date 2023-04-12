using System.Collections;
using UnityEngine;

namespace Heatmap.Recorder
{
    using Events;
    using Writers;
    public abstract class AbstractRecorder : MonoBehaviour
    {
        [SerializeField] private float recordInterval = 0.5f;
        [SerializeField] private bool isRecording;
        [SerializeField] private string eventName;

        protected string EventName => eventName;


        public bool IsRecording
        {
            get => isRecording;
            set
            {
                if (!value && isRecording)
                {
                    StopCoroutine(recording);
                }

                isRecording = value;
                if (value)
                {
                    recording = StartCoroutine(Recorde());
                }
            }
        }

        private Coroutine recording;

        private IEnumerator Recorde()
        {
            while (true)
            {
                RecordAndSaveEvent();
                yield return new WaitForSeconds(recordInterval);
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