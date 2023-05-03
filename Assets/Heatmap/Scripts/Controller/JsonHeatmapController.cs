using System.Collections.Generic;
using System.Diagnostics;
using Heatmap.Events;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Heatmap.Controller
{
    using Visualisation;
    using Readers;

    public sealed class JsonHeatmapController : BaseHeatmapController
    {
        [SerializeField] private JSONSettings settings;


        protected override Settings Settings => settings;

        private HeatmapVisualisation heatmapVisualisation;
        private IEventReader eventReader;


        public override async void LoadEvents()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            eventReader = new JSONEventReader(settings.SavePath.FilePath);
            SetEvents(await eventReader.ReadEvents());
            
            stopwatch.Stop();
            Debug.Log("Загрузка событий - скорость работы " + stopwatch.ElapsedMilliseconds + " мс");
        }
    }
}