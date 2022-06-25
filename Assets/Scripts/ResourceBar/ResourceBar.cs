using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResourceBar : MonoBehaviour
{
    #region Varibale
    public TextMeshProUGUI txtBuckSValue;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        LoadValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadValues()
    {
        txtBuckSValue.text = ResourcesData.sharedManager().AmountOfResources((int)ResourceIndex.bucksIndex).ToString();
    }
}
