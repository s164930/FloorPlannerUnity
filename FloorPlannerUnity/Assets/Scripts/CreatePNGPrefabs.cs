using System.Collections.Generic;
using UnityEngine;

public class CreatePNGPrefabs : MonoBehaviour
{

    AndroidJavaClass unityplayer;
    AndroidJavaObject currentActivity;
    List<GameObject> plans;
    public static List<byte[]> pngArray;
    public GameObject pngPrefab;
    // Start is called before the first frame update
    void Start()
    {
        plans = new List<GameObject>();
        pngArray = new List<byte[]>();
        unityplayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = unityplayer.GetStatic<AndroidJavaObject>("currentActivity");
        for (int i = 0; i < StandardConfigurations.pngCount; i++)
        {
            pngArray.Add(currentActivity.Call<byte[]>("getPNG", new object[] { i }));
        }
        Debug.Log("We got " + pngArray.Count + " pngs from java");

        foreach (byte[] png in pngArray)
        {
            Texture2D pngTex = new Texture2D(2, 2);
            bool isLoaded = pngTex.LoadImage(png);
            if (isLoaded)
            {
                Sprite pngImage = Sprite.Create(pngTex, new Rect(0, 0, pngTex.width, pngTex.height), new Vector2(0.5f, 0.5f));
                GameObject currentPlan = Instantiate(pngPrefab, gameObject.transform) as GameObject;
                currentPlan.GetComponent<SpriteRenderer>().sprite = pngImage;
                currentPlan.SetActive(true);
                currentPlan.transform.SetParent(transform);
                plans.Add(currentPlan);
            }
        }
    } 
}
