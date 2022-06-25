using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class ButtonAlertView : MonoBehaviour
{
    public int index = 0;
    public static UnityAction<string, bool> callback;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => ShowCallBack());
    }


    private void ShowCallBack()
    {
        AlertViewManager.sharedManager().AlertViewCallBack(index);
        GetComponentInParent<AlertView>().DestroyPanel();
    }
}
