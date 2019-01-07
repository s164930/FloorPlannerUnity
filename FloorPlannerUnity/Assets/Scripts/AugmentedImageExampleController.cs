//-----------------------------------------------------------------------
// <copyright file="AugmentedImageExampleController.cs" company="Google">
//
// Copyright 2018 Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------


using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

/// <summary>
/// Controller for AugmentedImage example.
/// </summary>
public class AugmentedImageExampleController : MonoBehaviour
{
    /// <summary>
    /// A prefab for visualizing an AugmentedImage.
    /// </summary>
    public AugmentedImageVisualizer AugmentedImageVisualizerPrefab;

    public GameObject groundPlanePrefab;
    public static Anchor anchor;

    /// <summary>
    /// The overlay containing the fit to scan user guide.
    /// </summary>

    private Dictionary<int, AugmentedImageVisualizer> m_Visualizers
        = new Dictionary<int, AugmentedImageVisualizer>();

    private List<AugmentedImage> m_TempAugmentedImages = new List<AugmentedImage>();

    /// <summary>
    /// The Unity Update method.
    /// </summary>
    public void Update()
    {

        // Check that motion tracking is tracking.
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        // Get updated augmented images for this frame.
        Session.GetTrackables<AugmentedImage>(m_TempAugmentedImages, TrackableQueryFilter.Updated);

        // Create visualizers and anchors for updated augmented images that are tracking and do not previously
        // have a visualizer. Remove visualizers for stopped images.
        foreach (var image in m_TempAugmentedImages)
        {
            AugmentedImageVisualizer visualizer = null;
            m_Visualizers.TryGetValue(image.DatabaseIndex, out visualizer);
            if (image.TrackingState == TrackingState.Tracking && visualizer == null)
            {
                // Create an anchor to ensure that ARCore keeps tracking this augmented image.
                anchor = image.CreateAnchor(image.CenterPose);
                //Instantiate(groundPlanePrefab, anchor.transform);
                foreach (PlanPrefabScript obj in CreatePNGPrefabs.plans)
                {
                    obj.gameObject.SetActive(true);
                    obj.transform.SetParent(anchor.transform);
                    obj.transform.SetPositionAndRotation(anchor.transform.position, anchor.transform.rotation);
                    obj.transform.Rotate(90, 0, 0);
                }
                visualizer = (AugmentedImageVisualizer)Instantiate(AugmentedImageVisualizerPrefab, anchor.transform);
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

