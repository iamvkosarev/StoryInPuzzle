using System;
using UnityEngine;

namespace Extentions.Heatmap.Data
{
    [Serializable]
    public class CameraLookAtData
    {
        [SerializeField] private string x;
        [SerializeField] private string y;
        [SerializeField] private string z;
        [SerializeField] private int colliderId;

        public string X => x;

        public string Y => y;

        public string Z => z;

        public int ColliderId => colliderId;

        public CameraLookAtData(string x, string y, string z, int colliderId)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.colliderId = colliderId;
        }
    }
}