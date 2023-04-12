using System;
using System.Drawing;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Heatmap.Controller
{
    [Serializable]
    public class JSONSettings : Settings
    {
        [SerializeField] private string filePath;

        public string FilePath => filePath;
    }
}