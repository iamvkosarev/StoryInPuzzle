using Heatmap.Controller;
using Heatmap.Scripts.Recorder;
using UnityEngine;

namespace Core.HeatmapTest
{
    public class RecorderSwitcher : MonoBehaviour
    {
        private IRecorder recorder;
        [SerializeField] private Transform _player;
        [SerializeField] private Vector3 _offcet;
        [SerializeField] private KeyCode switchKey;
        [SerializeField] private bool isRecording;
        [SerializeField] private SavePath _savePath;


        private void Awake()
        {
            var recordeSettingsContainer = new RecordeSettingContainer("playerMove", 0.2f, GetPlayerPos);
            recorder = RecorderFactory.Instance.GetJSONRecorder(recordeSettingsContainer, _savePath.FilePath);
        }

        private Vector3 GetPlayerPos() => _player.transform.position + _offcet;

        private void Update()
        {
            if (Input.GetKeyDown(switchKey))
            {
                isRecording = !isRecording;
                if (isRecording)
                    recorder.StartRecorde();
                else
                    recorder.StopRecorde();
            }
        }
    }
}