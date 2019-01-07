    using GoogleARCore;
    using UnityEngine;

    public class ImageRectVisualizer : MonoBehaviour
    {

        public AugmentedImage Image;

        public GameObject FrameLowerLeft, FrameUpperRight, FrameUpperLeft, FrameLowerRight;


        public void Update()
        {
            if (Image == null || Image.TrackingState != TrackingState.Tracking)
            {
                FrameLowerLeft.SetActive(false);
                FrameLowerRight.SetActive(false);
                FrameUpperLeft.SetActive(false);
                FrameUpperRight.SetActive(false);
                return;
            }
            

            float halfWidth = Image.ExtentX / 2;
            float halfHeight = Image.ExtentZ / 2;
            FrameLowerLeft.transform.localPosition = (halfWidth * Vector3.left) + (halfHeight * Vector3.back);
            FrameLowerRight.transform.localPosition = (halfWidth * Vector3.right) + (halfHeight * Vector3.back);
            FrameUpperLeft.transform.localPosition = (halfWidth * Vector3.left) + (halfHeight * Vector3.forward);
            FrameUpperRight.transform.localPosition = (halfWidth * Vector3.right) + (halfHeight * Vector3.forward);

            FrameLowerLeft.SetActive(false);
            FrameLowerRight.SetActive(false);
            FrameUpperLeft.SetActive(false);
            FrameUpperRight.SetActive(false);
        }
    }

