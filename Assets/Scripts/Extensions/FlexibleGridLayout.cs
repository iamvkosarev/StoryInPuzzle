using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI_Extentions
{
    public class FlexibleGridLayout : LayoutGroup
    {

        enum FitType
        {
            Uniform,
            Width,
            Hight,
            FixedRows,
            FixedColumns
        }

        enum ScaleType
        {
            Base,
            WidthControlsHight,
            HightControlsWidth
        }

        [SerializeField] private FitType fitType;
        [SerializeField] private int rows,colums;
        [SerializeField] private Vector2 spacing;
        [SerializeField] private bool notFitX;
        [ShowIf("notFitX")]
        [SerializeField] private float cellWidth;
        [SerializeField] private bool notFitY;
        [ShowIf("notFitY")]
        [SerializeField] private float cellHight;
        [SerializeField] private ScaleType scaleType;
        [ShowIf("useAspectRation")]
        [SerializeField] private float aspectRation;

        private bool useAspectRation => scaleType == ScaleType.WidthControlsHight ||
                                        scaleType == ScaleType.HightControlsWidth;

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            int childCount = CountChild();
            if(fitType == FitType.Width || fitType == FitType.Hight || fitType == FitType.Uniform)
            {
                float sqrRt = Mathf.Sqrt(childCount);
                rows = Mathf.CeilToInt(sqrRt);
                colums = Mathf.CeilToInt(sqrRt);
            }

            if (fitType == FitType.Width || fitType == FitType.FixedColumns)
                rows = Mathf.CeilToInt(childCount / (float)colums);
            if (fitType == FitType.Hight || fitType == FitType.FixedRows)
                colums = Mathf.CeilToInt(childCount / (float)rows);



            float parentWidth = rectTransform.rect.width;
            float parentHeigth = rectTransform.rect.height;

            //spacing = new Vector2(Mathf.Clamp(spacing.x, 0f, parentWidth / (float)(colums-1)), Mathf.Clamp(spacing.y, 0f, parentHeigth / (float)(rows-1)));


            float scaledCellWidth = parentWidth / colums - (spacing.x * (colums-1) / colums) - (padding.left / (float)colums) - (padding.right / (float)colums);
            float scaledCellHight = parentHeigth/ rows - (spacing.y * (rows - 1) / rows) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

            cellWidth = !notFitX ? scaledCellWidth : cellWidth;
            cellHight = !notFitY ? scaledCellHight : cellHight;

            if (scaleType == ScaleType.WidthControlsHight)
                cellHight = cellWidth * aspectRation;
            if (scaleType == ScaleType.HightControlsWidth)
                cellWidth = cellHight * aspectRation;

            var columnCount = 0;
            var rowCount = 0;

            for (int i = 0; i < childCount; i++)
            {
                if(!transform.GetChild(i).gameObject.activeSelf)continue;
                rowCount = i / colums;
                columnCount = i % colums;
                RectTransform item = null;
                try
                {

                    item = rectChildren[i];
                }
                catch (Exception e)
                {
                    Debug.Log(i);
                    throw;
                }

                var xPos = (cellWidth * columnCount) + (spacing.x * columnCount) + padding.left;
                var yPos = (cellHight * rowCount) + (spacing.y * rowCount) + padding.top;

                SetChildAlongAxis(item, 0, xPos, cellWidth);
                SetChildAlongAxis(item, 1, yPos, cellHight);
                if(transform.GetChild(i).TryGetComponent<LayoutGroup>(out var layoutGrid))
                {
                    layoutGrid.CalculateLayoutInputHorizontal();
                }
            }

            if (notFitY)
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, cellHight * rows + spacing.y * (rows -1) + padding.top + padding.bottom);
            if(notFitX)
                rectTransform.sizeDelta = new Vector2(cellWidth * colums + spacing.x * (colums - 1) + padding.left + padding.right, rectTransform.sizeDelta.y);
        }

        private int CountChild()
        {
            var count = 0;
            for (int i = 0; i < transform.childCount; i++)
            {
                if(!transform.GetChild(i).gameObject.activeSelf) continue;
                count++;
            }

            return count;
        }

        public override void CalculateLayoutInputVertical()
        {
        }

        public override void SetLayoutHorizontal()
        {
        }

        public override void SetLayoutVertical()
        {
        }
    }
}
