using UnityEngine;
using System.Collections.Generic;

public class ResponsiveUI : MonoBehaviour
{
    [System.Serializable]
    public class AspectRatio
    {
        public float width;
        public float height;

        public float Value
        {
            get { return width / height; }
        }
    }

    [System.Serializable]
    public class AspectRatioGroup
    {
        public AspectRatio aspectRatio; // Target aspect ratio
        public List<GameObject> canvases; // Canvases for this aspect ratio
        public float threshold = 0.05f; // Threshold for this aspect ratio
    }

    public List<AspectRatioGroup> aspectRatioGroups; // List of aspect ratio groups

    void Update()
    {
        float currentAspectRatio = (float)Screen.width / Screen.height;
        AspectRatioGroup closestGroup = null;
        float minDifference = float.MaxValue;

        // Find the aspect ratio group closest to the current aspect ratio within the threshold
        foreach (var group in aspectRatioGroups)
        {
            float targetAspectRatio = group.aspectRatio.Value;
            float difference = Mathf.Abs(currentAspectRatio - targetAspectRatio);

            if (difference <= group.threshold && difference < minDifference)
            {
                minDifference = difference;
                closestGroup = group;
            }
        }

        // Enable the canvases in the closest group and disable all others
        foreach (var group in aspectRatioGroups)
        {
            bool enable = group == closestGroup;
            foreach (var canvas in group.canvases)
            {
                if (canvas != null) canvas.SetActive(enable);
            }
        }
    }
}
