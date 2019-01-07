using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePNGPrefabs : MonoBehaviour
{
    public bool isAndroidLib;
    AndroidJavaClass unityplayer;
    AndroidJavaObject currentActivity;
    public static List<PlanPrefabScript> plans;
    public static List<byte[]> pngArray;
    public PlanPrefabScript pngPrefab;
    public Material planMaterial;
    // Start is called before the first frame update
    void Start()
    {
        plans = new List<PlanPrefabScript>();
        pngArray = new List<byte[]>();
        if (isAndroidLib)
        {
            unityplayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = unityplayer.GetStatic<AndroidJavaObject>("currentActivity");
            for (int i = 0; i < StandardConfigurations.pngCount; i++)
            {
                pngArray.Add(currentActivity.Call<byte[]>("getPNG", new object[] { i }));
            }
            Debug.Log("We got " + pngArray.Count + " pngs from java");

            int j = 0;
            foreach (byte[] png in pngArray)
            {
                Texture2D pngTex = new Texture2D(2, 2);
                bool isLoaded = pngTex.LoadImage(png);
                if (isLoaded)
                {
                    Sprite pngImage = Sprite.Create(pngTex, new Rect(0, 0, pngTex.width, pngTex.height), new Vector2(0.5f, 0.5f));
                    PlanPrefabScript currentPlan = (PlanPrefabScript)Instantiate(pngPrefab, gameObject.transform);
                    currentPlan.GetComponent<SpriteRenderer>().sprite = pngImage;
                    currentPlan.gameObject.SetActive(false);
                    currentPlan.SetName(StandardConfigurations.pdfNames[j]);

                    currentPlan.transform.SetParent(transform);
                    plans.Add(currentPlan);
                    j++;
                }
            }
        }
        else
        {
            Texture2D stueplanTex = Resources.Load<Texture2D>("Images/stueplan") as Texture2D;

            Debug.Log("Went to else");
            Sprite stuePlanImg = Sprite.Create(stueplanTex, new Rect(0, 0, stueplanTex.width, stueplanTex.height), new Vector2(.5f, .5f));
            PlanPrefabScript stuePlan = Instantiate(pngPrefab) as PlanPrefabScript;
            stuePlan.GetComponent<SpriteRenderer>().sprite = stuePlanImg;
            stuePlan.gameObject.SetActive(false);
            plans.Add(stuePlan);

            Texture2D loftplanTex = Resources.Load<Texture2D>("Images/loftplan") as Texture2D;
            Sprite loftPlanImg = Sprite.Create(loftplanTex, new Rect(0, 0, loftplanTex.width, loftplanTex.height), new Vector2(.5f, .5f));
            PlanPrefabScript loftplan = Instantiate(pngPrefab) as PlanPrefabScript;
            loftplan.GetComponent<SpriteRenderer>().sprite = loftPlanImg;
            loftplan.gameObject.SetActive(false);
            plans.Add(loftplan);


        }

    }
}
