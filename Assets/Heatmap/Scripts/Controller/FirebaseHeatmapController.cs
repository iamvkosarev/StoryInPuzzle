using System.Diagnostics;
using Heatmap.Readers;
using Heatmap.Visualisation;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Heatmap.Controller
{
    public class FirebaseHeatmapController : BaseHeatmapController
    {
        private HeatmapVisualisation heatmapVisualisation;
        private IEventReader eventReader;


        public override async void LoadEvents()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            eventReader = new FirebaseStorageEventReader(SavePath.FilePath);
            AddEvents(await eventReader.ReadEvents());
            
            stopwatch.Stop();
            Debug.Log("Загрузка событий");
        }
    }
}