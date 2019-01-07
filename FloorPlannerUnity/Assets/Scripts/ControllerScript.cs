using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class ControllerScript: MonoBehaviour
{
    public ImageRectVisualizer ImageRectVisualizerPrefab;

    public GameObject groundPlanePrefab;
    public static Anchor anchor;
    private Dictionary<int, ImageRectVisualizer> m_Visualizers
        = new Dictionary<int, ImageRectVisualizer>();

    private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();

    public void Update()
    {

        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        Session.GetTrackables<AugmentedImage>(m_TempAugmentedImages, TrackableQueryFilter.Updated);

        foreach (var image in m_TempAugmentedImages)
        {
            ImageRectVisualizer visualizer = null;
            m_Visualizers.TryGetValue(image.DatabaseIndex, out visualizer);
            if (image.TrackingState == TrackingState.Tracking && visualizer == null)
            {
                anchor = image.CreateAnchor(image.CenterPose);
                foreach (PlanPrefabScript obj in CreatePNGPrefabs.plans)
                {
                    obj.gameObject.SetActive(true);
                    obj.transform.SetParent(anchor.transform);
                    obj.transform.SetPositionAndRotation(anchor.transform.position, anchor.transform.rotation);
                    obj.transform.Rotate(90, 0, 0);
                }
                visualizer = (ImageRectVisualizer)Instantiate(ImageRectVisualizerPrefab, anchor.transform);
                visualizer.gameObject.SetActive(false);
                visualizer.Image = image;
                m_Visualizers.Add(image.DatabaseIndex, visualizer);
            }
            else if (image.TrackingState == TrackingState.Stopped && visualizer != null)
            {
                m_Visualizers.Remove(image.DatabaseIndex);
                GameObject.Destroy(visualizer.gameObject);
            }
        }
    }
}

