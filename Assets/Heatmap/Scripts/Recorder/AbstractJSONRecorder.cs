using UnityEngine;

namespace Heatmap.Recorder
{
    using Writers;

    public abstract class AbstractJSONRecorder : AbstractFileRecorder
    {
        private IEventWriter eventWriter;

        protected override IEventWriter EventWriter
        {
            get { return eventWriter ??= new JSONEventWriter(CreateFileIfNull, Path); }
        }
    }
}