using System;
using System.Threading.Tasks;
using Heatmap.Scripts.Recorder;
using StoryInPuzzle.Infrastructure.Services.AssetLoader.Concrete.RecorderScreen;

namespace StoryInPuzzle.Infrastructure.Services.HeatmapRecorder
{
    public class HeatmapRecorder : IHeatmapRecorder
    {
        private readonly IRecordeScreenProvider _recordeScreenProvider;
        private IRecorder _recorder;
        private RecordeScreen _screen;

        public HeatmapRecorder(IRecordeScreenProvider recordeScreenProvider)
        {
            _recordeScreenProvider = recordeScreenProvider;
        }

        public void SetRecorder(IRecorder recorder)
        {
            _recorder = recorder;
        }

        public async Task SwitchRecording(bool mode)
        {
            if (_recorder == null) throw new Exception("Recorder wasn't selected");
            if (mode)
            {
                await  _recordeScreenProvider.Load();
                _recorder.StartRecorde();
            }
            else
            {
                _recordeScreenProvider.Unload();
                _recorder.StopRecorde();
            }
        }
    }
}