using System.Collections.Generic;
using System.Diagnostics;
using Heatmap.Scripts.Controller.SavePath;
using Sirenix.OdinInspector;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Heatmap.Controller
{
    using Visualisation;
    using Events;

    public abstract class BaseHeatmapController : MonoBehaviour
    {
        [SerializeField] private BoxCollider particleSystemBox;
        [SerializeField] private List<EventsContainer> eventsContainersList = new();
        [SerializeField] private Settings settings;
        [SerializeField] private BaseSavePath _savePath;

        protected BaseSavePath SavePath => _savePath;

        private HeatmapVisualisation heatmapVisualisation;
        private bool particleSystemIsInitialized;

        private HeatmapVisualisation HeatmapVisualisation =>
            heatmapVisualisation ??= new HeatmapVisualisation(settings);

        private List<EventsContainer> EventsContainersList
        {
            get => eventsContainersList;
            set => eventsContainersList = value;
        }

        [Button]
        public void ClearEvents()
        {
            EventsContainersList.Clear();
        }

        [Button]
        public abstract void LoadEvents();

        protected void AddEvents(IEnumerable<EventsContainer> readEvents)
        {
            EventsContainersList.AddRange(readEvents);;
        }

        [Button, DisableIf("IsParticlesInitialize"), HorizontalGroup("Initialize")]
        public void InitializeParticlesSystem()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            HeatmapVisualisation.InitializeParticleSystem(particleSystemBox);
            HeatmapVisualisation.InitializeParticleArray();
            particleSystemIsInitialized = true;
            settings.IsParticlesInitialize = particleSystemIsInitialized;
            stopwatch.Stop();
            Debug.Log("Создание сетки из частиц - скорость работы " + stopwatch.ElapsedMilliseconds + " мс");
        }

        [Button, DisableIf("IsNotParticlesInitialize"), HorizontalGroup("Initialize")]
        public void DestroyParticlesSystem()
        {
            HeatmapVisualisation.DestroyParticleSystem();
            particleSystemIsInitialized = false;
            settings.IsParticlesInitialize = particleSystemIsInitialized;
        }

        private bool IsNotParticlesInitialize => !particleSystemIsInitialized;
        private bool IsParticlesInitialize => particleSystemIsInitialized;

        [Button, DisableIf("IsNotParticlesInitialize")]
        public void ShowSelectedEvents()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            HeatmapVisualisation.ResetParticlesColor();

            foreach (var eventsContainer in EventsContainersList)
            {
                if (eventsContainer.IsCurrentlyDisplayedOnHeatmap)
                {
                    HeatmapVisualisation.AddEventToHeatMap(eventsContainer);
                }
            }

            HeatmapVisualisation.UpdateParticlesInParticleSystem();
            stopwatch.Stop();
            Debug.Log("Отображение тепловой карты - скорость работы " + stopwatch.ElapsedMilliseconds + " мс");
        }

        [Button, DisableIf("IsNotParticlesInitialize")]
        public void ResetHeatmap()
        {
            HeatmapVisualisation.ResetParticlesColor();
            HeatmapVisualisation.UpdateParticlesInParticleSystem();
        }
    }
}