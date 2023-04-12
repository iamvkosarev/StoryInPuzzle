using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.HeatmapTest
{
    public class NearbyEventPopularityView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI placeText;
        [SerializeField] private TextMeshProUGUI popularityNumberText;
        [SerializeField] private TextMeshProUGUI popularityPercentText;
        [SerializeField] private TextMeshProUGUI popularityValue;

        public TextMeshProUGUI PlaceText => placeText;

        public TextMeshProUGUI PopularityNumberText => popularityNumberText;
        public TextMeshProUGUI PopularityPercentText => popularityPercentText;

        public TextMeshProUGUI PopularityValue => popularityValue;
    }
}