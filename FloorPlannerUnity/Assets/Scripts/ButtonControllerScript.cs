using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControllerScript : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonContainer;

    void Start()
    {
        for(int i = 0; i < StandardConfigurations.pngCount; i++)
        {
            GameObject button = Instantiate(buttonPrefab) as GameObject;
            button.GetComponentInChildren<Text>().text = StandardConfigurations.pdfNames[i];
            button.transform.SetParent(buttonContainer.transform);
        }
    }
}
