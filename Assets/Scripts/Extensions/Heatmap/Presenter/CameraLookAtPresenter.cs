using System;
using System.Collections.Generic;
using Extentions.Heatmap;
using Extentions.Heatmap.Data;
using Extentions.Heatmap.Pools;
using Extentions.Heatmap.Recorder;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Extensions.Heatmap.Presenter
{
    public class CameraLookAtPresenter : MonoBehaviour
    {
        [SerializeField] private string eventName = "cameraLookAt";
        [SerializeField] private PointPool pointPool;
        [SerializeField] private float updateDuration = 1f;
        [SerializeField] private float gridPaddings = 0.2f;
        [SerializeField] private bool update;
        [SerializeField] private Color minColor;
        [SerializeField] private Color middleColor;
        [SerializeField] private Color maxColor;
        [SerializeField, Range(0f, 1f)] private float partOfMaxPointsCount = 0.5f;


        private Dictionary<float, Dictionary<float, Dictionary<float, int>>> roundedTakenPositions = new();
        private List<PointComponent> points = new();
        private float updateTime;
        private int maxPointCount;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                update = !update;
                updateTime = 0f;
            }

            if (!update) return;
            if (updateTime <= 0)
            {
                UpdateHeatMap();
                updateTime = updateDuration;
            }
            else
            {
                updateTime -= Time.deltaTime;
            }
        }

        private void UpdateHeatMap()
        {
            var records = HeatmapEventsContainer.Instance.GerRecords(eventName);
            UpdateHeatMap(records);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void UpdateHeatMap(List<string> records)
        {
            RemovePoints();
            foreach (var record in records)
            {
                var recordData = JsonUtility.FromJson<CameraLookAtData>(record);
                var pos = GetPos(recordData);
                if (pos.HasValue)
                {
                    var point = pointPool.Pop(pos.Value);
                    point.transform.SetParent(transform);
                    points.Add(point);
                }
            }

            UpdateColors();
        }

        private void UpdateColors()
        {
            foreach (var pointComponent in points)
            {
                var pos = pointComponent.transform.position;
                pointComponent.SharedMaterial.color = GetColor(pos);
            }
        }

        private Color GetColor(Vector3 pos)
        {
            var pointsCount = 0;
            var totalCount = 0;
            for (int x_i = -3; x_i <= 3; x_i++)
            {
                for (int y_i = -3; y_i <= 3; y_i++)
                {
                    for (int z_i = -3; z_i <= 3; z_i++)
                    {
                        var x = FormatCoordinate(pos.x + x_i * gridPaddings);
                        var y = FormatCoordinate(pos.y + y_i * gridPaddings);
                        var z = FormatCoordinate(pos.z + z_i * gridPaddings);
                        if (HasPoint(FormatCoordinate(pos.x + x_i * gridPaddings),
                                FormatCoordinate(pos.y + y_i * gridPaddings),
                                FormatCoordinate(pos.z + z_i * gridPaddings)))
                        {
                            pointsCount += roundedTakenPositions[x][y][z];
                            totalCount++;
                        }
                    }
                }
            }

            if (pointsCount <= 27)
            {
                return Color.Lerp(minColor, middleColor, (float)pointsCount / 27);
            }

            return Color.Lerp(middleColor, maxColor, pointsCount / (maxPointCount * partOfMaxPointsCount));
        }

        private bool HasPoint(float x, float y, float z)
        {
            return roundedTakenPositions.ContainsKey(x) && roundedTakenPositions[x].ContainsKey(y) &&
                   roundedTakenPositions[x][y].ContainsKey(z);
        }

        private Vector3? GetPos(CameraLookAtData recordData)
        {
            var pos = new Vector3(GetCoordinateFromString(recordData.X), GetCoordinateFromString(recordData.Y),
                GetCoordinateFromString(recordData.Z));
            if (roundedTakenPositions.ContainsKey(pos.x))
            {
                if (roundedTakenPositions[pos.x].ContainsKey(pos.y))
                {
                    if (roundedTakenPositions[pos.x][pos.y].ContainsKey(pos.z))
                    {
                        var count = roundedTakenPositions[pos.x][pos.y][pos.z]++;
                        if (count > maxPointCount)
                        {
                            maxPointCount = count;
                        }

                        return null;
                    }

                    roundedTakenPositions[pos.x][pos.y].Add(pos.z, 1);
                    return pos;
                }

                roundedTakenPositions[pos.x]
                    .Add(pos.y, new Dictionary<float, int>() { { pos.z, 1 } });
                return pos;
            }

            roundedTakenPositions.Add(pos.x, new Dictionary<float, Dictionary<float, int>>()
            {
                { pos.y, new Dictionary<float, int>() { { pos.z, 1 } } }
            });
            return pos;
        }

        private float GetCoordinateFromString(string coordinateInString)
        {
            var parsedCoordinate = float.Parse(coordinateInString);
            return FormatCoordinate(parsedCoordinate);
        }

        private float FormatCoordinate(float coordinate)
        {
            return Mathf.Round(coordinate / gridPaddings) * gridPaddings;
        }

        private void RemovePoints()
        {
            foreach (var pointComponent in points)
            {
                pointPool.Push(pointComponent);
            }

            points.Clear();
            roundedTakenPositions.Clear();
            roundedTakenPositions = new Dictionary<float, Dictionary<float, Dictionary<float, int>>>();
        }
    }
}