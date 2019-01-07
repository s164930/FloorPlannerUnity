using UnityEngine;

public class TouchInputHandler : MonoBehaviour
{

    private Vector3 originalScale;
    static private float scaleFactor = 1.0f;
    public bool handleScaleInput = false;
    public bool adjustScale = true;
    private float speed = 1f;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (handleScaleInput)
        {
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
                Vector2 touchZeroPrevPos =
                    touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos =
                    touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag =
                    (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag =
                    (touchZero.position - touchOne.position).magnitude;

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                float v = scaleFactor * 100f;

                v -= deltaMagnitudeDiff * speed;

                scaleFactor = Mathf.Clamp(v, 1f, 1000f) / 100f;
            }
            
        }

        if (adjustScale)
        {
            transform.localScale = originalScale * scaleFactor;

            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                Debug.Log("Delta position is: " + touch.deltaPosition);

                if (touch.deltaPosition.y > 0)
                {
                    Debug.Log("Direction is larger than zero, it should go up");
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition + Vector3.up, touch.deltaPosition.magnitude *Time.deltaTime / 50);
                }
                else if (touch.deltaPosition.y < 0)
                {
                    Debug.Log("Direction is smaller than zero, it should go down");
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition + Vector3.down, touch.deltaPosition.magnitude *Time.deltaTime/ 50);
                }
            }
        }
    }
}
