using System;
using UnityEngine;

namespace Core.HeatmapTest
{
    public class CheckingPopularityPlaceComponent : MonoBehaviour, IComparable<CheckingPopularityPlaceComponent>
    {
        public int Popularity { set; get; }

        public int CompareTo(CheckingPopularityPlaceComponent other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Popularity.CompareTo(other.Popularity);
        }
    }
}