using Heatmap.Controller;
using UnityEditor;
using UnityEngine;

namespace Heatmap
{
    public static class Menu
    {
        public const string MENU_PATH = @"Tools/Heatmap/";
        
        [MenuItem(MENU_PATH + "Controllers/JSON Controller", false, 100)]
        private static void AddController()
        {
            var controllerGO = new GameObject("JSON Heatmap Controller");
            controllerGO.AddComponent<JsonHeatmapController>();
        }
    }
}