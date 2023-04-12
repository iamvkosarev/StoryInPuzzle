using UnityEngine;

namespace Heatmap.Recorder
{
    public abstract class AbstractFileRecorder : AbstractRecorder
    {
        [SerializeField] private string path;
        [SerializeField] private bool createFileIfNull = true;

        protected string Path => path;

        protected bool CreateFileIfNull => createFileIfNull;
    }
}