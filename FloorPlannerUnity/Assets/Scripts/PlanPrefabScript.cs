using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanPrefabScript : MonoBehaviour
{
    public string pdfName;
    
    public void SetName(string name)
    {
        this.pdfName = name;
    }
    public string GetName()
    {
        return this.pdfName;
    }

    
}
