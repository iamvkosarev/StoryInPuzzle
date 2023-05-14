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
        private HeatmapVisualisation heatmapVisualisation;
        private IEventReader eventReader;


        public override async void LoadEvents()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            eventReader = new JSONEventReader(SavePath.FilePath);
            AddEvents(await eventReader.ReadEvents());
            
            stopwatch.Stop();
            Debug.Log("Загрузка событий - скорость работы " + stopwatch.ElapsedMilliseconds + " мс");
        }
    }
}