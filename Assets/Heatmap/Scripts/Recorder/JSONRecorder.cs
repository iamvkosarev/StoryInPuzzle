using Heatmap.Writers;

namespace Heatmap.Scripts.Recorder
{
    public abstract class JSONRecorder : AbstractFileRecorder
    {
        private IEventWriter eventWriter;

        protected override IEventWriter EventWriter
        {
            get { return eventWriter ??= new JSONEventWriter(Path); }
        }
    }
}