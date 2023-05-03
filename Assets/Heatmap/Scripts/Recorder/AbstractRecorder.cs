using System;
using System.Collections;
using Heatmap.Events;
using Heatmap.Writers;
using UnityEngine;

namespace Heatmap.Scripts.Recorder
{
    public class RecordeSettingContainer
    {
        public float RecordInterval { get; }
        public string EventName { get; }
        public Func<Vector3> GetData { get; }


        public RecordeSettingContainer(string eventName, float recordInterval, Func<Vector3> getData)
        {
            RecordInterval = recordInterval;
            EventName = eventName;
            GetData = getData;
        }
    }

    public abstract class AbstractRecorder : IRecorder
    {
        protected abstract IEventWriter EventWriter { get; }

        private Coroutine _recording;
        private bool _isRecording;
        private ICoroutineRunner _coroutineRunner;
        private readonly RecordeSettingContainer _recordeSettingContainer;


        protected AbstractRecorder(RecordeSettingContainer recordeSettingContainer,
            ICoroutineRunner coroutineRunner)
        {
            _recordeSettingContainer = recordeSettingContainer;
            _coroutineRunner = coroutineRunner;
        }

        public void StartRecorde()
        {
            _recording = _coroutineRunner.StartCoroutine(Recorde());
            _isRecording = true;
        }

        public virtual void StopRecorde()
        {
            if (_isRecording)
            {
                _coroutineRunner.StopCoroutine(_recording);
            }

            _isRecording = false;
        }

        private IEnumerator Recorde()
        {
            while (true)
            {
                RecordAndSaveEvent();
                yield return new WaitForSeconds(_recordeSettingContainer.RecordInterval);
            }
        }

        private void RecordAndSaveEvent()
        {
            var baseEvent = PrepareData(_recordeSettingContainer.GetData());

            if (baseEvent == null) return;

            EventWriter.SaveEvent(baseEvent);
        }

        private BaseEvent PrepareData(Vector3 getData)
        {
            return new BaseEvent(_recordeSettingContainer.EventName, getData);
        }
    }
}