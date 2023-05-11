using System;
using System.Drawing;
using Heatmap.Scripts.Controller.SavePath;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Heatmap.Controller
{
    [Serializable, CreateAssetMenu(menuName = "Heatmap/Settings/JSON", fileName = "JSON Settings")]
    public class JSONSettings : Settings
    {
        [SerializeField] private BaseSavePath basicSavePath;
        public BaseSavePath BasicSavePath => basicSavePath;
    }
}