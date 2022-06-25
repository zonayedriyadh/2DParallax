using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AlertView : MonoBehaviour
{
    [Header("Text Fields")]
    public TextMeshProUGUI txtTitle;
    public TextMeshProUGUI txtDescription;

    [Header("Gameobjects")]
    public GameObject btnContainer;
    public GameObject btnPrefabs;

    public Button btnExitAlertView;

    int btnIndex = 1;

    private void Start()
    {
        btnExitAlertView.onClick.AddListener(() => btnExitAlertViewCallBack());
    }

    private void btnExitAlertViewCallBack()
    {
        DestroyPanel();
    }

    public void InitilizeAlertView(string _texTitle,string _description)
    {
        txtTitle.text = _texTitle;
        txtDescription.text = _description;
    }

    public void AddButtonWithTitle(string _buttonTitle)
    {
        GameObject newBtn = Instantiate(btnPrefabs,btnContainer.transform);
        newBtn.GetComponent<ButtonAlertView>().index = btnIndex++;
        newBtn.GetComponentInChildren<TextMeshProUGUI>().text = _buttonTitle;
    }

    public void DestroyPanel()
    {
        Destroy(this.gameObject);
    }


}
