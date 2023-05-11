using System.Collections.Generic;
using System.Linq;
using Heatmap.Controller;
using Heatmap.Events;
using Heatmap.Readers;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Core.HeatmapTest
{
    public class NearbyEventPopularityController : MonoBehaviour
    {
        [SerializeField] private List<EventsContainer> eventsContainersList;
        [SerializeField] private JSONSettings settings;
        [SerializeField] private Transform popularityParent;
        [SerializeField] private NearbyEventPopularityView nearbyEventPopularityPrefab;
        [SerializeField] private float radiusToCheck = 0.3f;
        
        
        
        private IEventReader eventReader;
        private List<EventsContainer> EventsContainersList
        {
            get => eventsContainersList;
            set => eventsContainersList = value;
        }

        
        [Button]
        public async void LoadEvents()
        {
            eventReader = new JSONEventReader(settings.BasicSavePath.FilePath);
            SetEvents(await eventReader.ReadEvents());
        }

        [Button]
        private void LoadPopularity()
        {
            var list = FindObjectsOfType<CheckingPopularityPlaceComponent>();
            foreach (var checkingPopularityPlaceComponent in list)
            {
                checkingPopularityPlaceComponent.Popularity = 0;
            }
            foreach (var eventsContainer in EventsContainersList)
            {
                if (eventsContainer.IsCurrentlyDisplayedOnHeatmap)
                {
                    foreach (var checkingPopularityPlaceComponent in list)
                    {
                        AddPopularityValues(checkingPopularityPlaceComponent, eventsContainer);
                    }
                }
            }

            long totalPopularity = 0;
            foreach (var checkingPopularityPlaceComponent in list)
            {
                totalPopularity += checkingPopularityPlaceComponent.Popularity;
            }
            Debug.Log(totalPopularity);

            list.Sort();
            for (int i = popularityParent.childCount - 1; i >= 0; i--)
            {
                var child = popularityParent.GetChild(i);
                DestroyImmediate(child.gameObject);
            }

            for (var i = list.Length - 1; i >= 0; i--)
            {
                var checkingPopularityPlaceComponent = list[i];
                var newView = Instantiate(nearbyEventPopularityPrefab, popularityParent);
                newView.PlaceText.text = checkingPopularityPlaceComponent.name;
                newView.PopularityNumberText.text = $"#{list.Length - i}";
                if(totalPopularity!= 0)
                    newView.PopularityPercentText.text = $"{((float)checkingPopularityPlaceComponent.Popularity / totalPopularity):P}";
                else
                {
                    newView.PopularityPercentText.text = "...%";
                }
                newView.PopularityValue.text = checkingPopularityPlaceComponent.Popularity.ToString();
            }
        }

        private void AddPopularityValues(CheckingPopularityPlaceComponent checkingPopularityPlaceComponent, EventsContainer eventsContainer)
        {
            foreach (var eventsContainerPosition in eventsContainer.Positions)
            {
                if (Vector3.Distance(checkingPopularityPlaceComponent.transform.position,
                        eventsContainerPosition.Position) <= radiusToCheck)
                {
                    checkingPopularityPlaceComponent.Popularity++;
                }
            }
        }


        private void SetEvents(List<EventsContainer> readEvents)
        {
            EventsContainersList = readEvents;
        }
    }
}