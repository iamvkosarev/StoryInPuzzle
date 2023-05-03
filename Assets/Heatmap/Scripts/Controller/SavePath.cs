using System;
using UnityEngine;

namespace Heatmap.Controller
{
    [Serializable, CreateAssetMenu(menuName = "Heatmap/Settings/Other/SavePath")]
    public class SavePath : ScriptableObject
    {
        [SerializeField] private string filePath;
        public string FilePath => filePath;
    }
}