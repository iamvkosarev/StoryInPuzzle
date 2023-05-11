using System.Threading.Tasks;
using Core.AssetLoading;
using Core.AssetLoading.Concret;
using Heatmap.Controller;
using Heatmap.Scripts.Controller.SavePath;
using Heatmap.Scripts.Recorder;
using UnityEngine;

namespace Core.HeatmapTest
{
    public class RecorderSwitcher : MonoBehaviour
    {
        private IRecorder recorder;
        [SerializeField] private Transform _player;
        [SerializeField] private Vector3 _offcet;
        [SerializeField] private KeyCode _switchKey = KeyCode.R;
        [SerializeField] private BasicSavePath jsonBasicSavePath;
        [SerializeField] private BasicSavePath firebaseBasicSavePath;
        [SerializeField] private bool isRecording;

        private IAssetLoader<RecorderScreen> _recorderScreenAssetLoader;
        private void Awake()
        {
            _recorderScreenAssetLoader = new RecorderScreenLoader();
            recorder = RecorderFactory.Instance.GetFirebaseRecorder(
                new RecordeSettingContainer("playerMove", 0.2f, GetPlayerPos), jsonBasicSavePath.FilePath, firebaseBasicSavePath.FilePath);
        }

        private Vector3 GetPlayerPos() => _player.transform.position + _offcet;

        private void Update()
        {
            if (Input.GetKeyDown(_switchKey))
            {
                SwitchRecorder();
            }
        }

        private async Task SwitchRecorder()
        {
            isRecording = !isRecording;
            if (isRecording)
            {
                await _recorderScreenAssetLoader.LoadAssetAsync();
                recorder.StartRecorde();
            }
            else
            {
                _recorderScreenAssetLoader.ReleaseAsset();
                recorder.StopRecorde();
            }
        }
    }
}