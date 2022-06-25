using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    float length;
    public SpriteRenderer L1;
    public GameObject pic1;
    public GameObject pic2;
    public GameObject pic3;

    // Start is called before the first frame update
    void Start()
    {
        length = L1.bounds.size.x;
        SetPositionLayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPositionLayer()
    {
        pic1.transform.position = new Vector3(0,0,0);
        pic2.transform.position = new Vector3(length, 0, 0);
        pic3.transform.position = new Vector3(-length, 0, 0);
    }
}
