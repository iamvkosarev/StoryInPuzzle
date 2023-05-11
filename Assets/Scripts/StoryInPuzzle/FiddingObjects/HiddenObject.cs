using UnityEngine;

namespace StoryInPuzzle.FiddingObjects
{
    public class HiddenObject : MonoBehaviour
    {
        [SerializeField] private Color _color;
        [SerializeField] private string _objectName;

        public Color Color => _color;
        public string ObjectName => _objectName;
    }
}