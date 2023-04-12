using System.Collections.Generic;
using System.Diagnostics;
using Heatmap.Events;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Heatmap.Controller
{
    using Visualisation;
    using Readers;

    public class JsonHeatmapController : BaseHeatmapController
    {
        [SerializeField] private JSONSettings settings;


        public override Settings Settings => settings;

        private HeatmapVisualisation heatmapVisualisation;
        private IEventReader eventReader;


        public override void LoadEvents()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            eventReader = new JSONEventReader(settings.FilePath);
            SetEvents(eventReader.ReadEvents());
            
            stopwatch.Stop();
            Debug.Log("Загрузка событий - скорость работы " + stopwatch.ElapsedMilliseconds + " мс");
        }
    }
}