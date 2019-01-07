using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{

    private void Update()
    {
        
    }
    public void TogglePdf()
    {
        Debug.Log("In togglepdf start");
        foreach (PlanPrefabScript obj in CreatePNGPrefabs.plans)
        {
            Debug.Log("Cycling through plans: " + obj.GetName());
            Debug.Log("comparing plan " + obj.GetName() + " with button " + gameObject.GetComponentInChildren<Text>().text);
            if(obj.GetName() == gameObject.GetComponentInChildren<Text>().text)
            {

                Debug.Log("There was a match in pdfname and button text");
                if (obj.gameObject.activeSelf)
                {
                    Debug.Log("Setting plan as false: " + obj.GetName());
                    obj.gameObject.SetActive(false);
                    //var colors = gameObject.GetComponent<Button>().colors;
                    //colors.normalColor = Color.grey;
                    //gameObject.GetComponent<Button>().colors = colors;
                }
                else if (!obj.gameObject.activeSelf)
                {
                    Debug.Log("Setting plan as true: " + obj.GetName());
                    obj.gameObject.SetActive(true);
                    //var colors = gameObject.GetComponent<Button>().colors;
                    //colors.normalColor = Color.white;
                    //gameObject.GetComponent<Button>().colors = colors;
                }
                
            }
        }
    }
}
