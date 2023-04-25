using System;
using System.Drawing;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Heatmap.Controller
{
    [Serializable, CreateAssetMenu(menuName = "Heatmap/Settings/JSON", fileName = "JSON Settings")]
    public class JSONSettings : Settings
    {
        [SerializeField] private string filePath;

        public string FilePath => filePath;
    }
}