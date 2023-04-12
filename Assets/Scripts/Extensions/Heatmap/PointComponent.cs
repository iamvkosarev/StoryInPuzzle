using UnityEngine;

namespace Extentions.Heatmap
{
    public class PointComponent : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;

        public Material SharedMaterial => meshRenderer.material;
    }
}