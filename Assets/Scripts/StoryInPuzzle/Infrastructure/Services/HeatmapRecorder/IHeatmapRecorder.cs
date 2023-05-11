using System.Threading.Tasks;
using Heatmap.Scripts.Recorder;

namespace StoryInPuzzle.Infrastructure.Services.HeatmapRecorder
{
    public interface IHeatmapRecorder : IService
    {
        void SetRecorder(IRecorder recorder);
        Task SwitchRecording(bool mode);
    }
}