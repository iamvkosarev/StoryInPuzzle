using UnityEngine;

namespace Heatmap.Scripts.Recorder
{
    public abstract class AbstractFileRecorder : AbstractRecorder
    {
        [SerializeField] private string _path;

        protected string Path => _path;
    }
}