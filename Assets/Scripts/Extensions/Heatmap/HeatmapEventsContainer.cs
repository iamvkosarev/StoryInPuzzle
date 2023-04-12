using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Extentions.Heatmap
{
    [Serializable]
    public class EventContainer
    {
        [SerializeField] private string eventName;
        [SerializeField] private string eventData;

        public string EventName => eventName;

        public string EventData => eventData;

        public EventContainer(string eventName, string eventData)
        {
            this.eventName = eventName;
            this.eventData = eventData;
        }
    }

    public class HeatmapEventsContainer : Singleton<HeatmapEventsContainer>
    {
        [SerializeField] private List<EventContainer> records = new ();

        public void AddRecord(string eventName, string eventData)
        {
            records.Add(new EventContainer(eventName, eventData));
        }

        public List<string> GerRecords(string eventName)
        {
            return (from eventContainer in records where eventContainer.EventName == eventName select eventContainer.EventData).ToList();
        }
    }
}