using System;
using Heatmap.Scripts.Recorder;
using UnityEngine;

namespace Core.HeatmapTest
{
    public class RecorderSwitcher : MonoBehaviour
    {
        [SerializeField] private AbstractRecorder abstractRecorder;
        [SerializeField] private KeyCode switchKey;
        [SerializeField] private bool isRecording;

        private void Update()
        {
            if (Input.GetKeyDown(switchKey))
            {
                isRecording = !isRecording;
                if (isRecording)
                    abstractRecorder.StartRecorde();
                else
                    abstractRecorder.StopRecorde();
            }
        }
    }
}