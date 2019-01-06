using UnityEngine;
using UnityEngine.UI;

public class StandardConfigurations : MonoBehaviour {
    bool hasExtra;
    AndroidJavaClass UnityPlayer;
    AndroidJavaObject extras, currentActivity, intent;
    public static int projectID = 1, pngCount;
    // Use this for initialization
    void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Debug.Log("We are in an android environment, getting extras");
            UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            intent = currentActivity.Call<AndroidJavaObject>("getIntent");
            hasExtra = intent.Call<bool>("hasExtra", "pngCount");
            if (hasExtra)
            {
                Debug.Log("There are extras in the intent, from unity");
                extras = intent.Call<AndroidJavaObject>("getExtras");
                pngCount = extras.Call<int>("getInt", "pngCount");
                projectID = extras.Call<int>("getInt", "Projectid");
            }
        }
    }

    private void FixedUpdate()
    {
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            currentActivity.Call("closeActivity");
        }
#endif

    }
}
