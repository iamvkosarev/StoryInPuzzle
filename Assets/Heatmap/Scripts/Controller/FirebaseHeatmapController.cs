using System.Diagnostics;
using Heatmap.Readers;
using Heatmap.Visualisation;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Heatmap.Controller
{
    public class FirebaseHeatmapController : BaseHeatmapController
    {
        [SerializeField] private JSONSettings settings;

        protected override Settings Settings => settings;

        private HeatmapVisualisation heatmapVisualisation;
        private IEventReader eventReader;


        public override async void LoadEvents()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            eventReader = new FirebaseStorageEventReader(settings.FilePath);
            SetEvents(await eventReader.ReadEvents());
            
            stopwatch.Stop();
            Debug.Log("Загрузка событий");
        }
    }
}