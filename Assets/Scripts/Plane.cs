using UnityEngine;
using System.Collections;

// PLEASE NOTE! THIS SCRIPT IS FOR DEMO PURPOSES ONLY :) //

public class Plane : MonoBehaviour {
	public GameObject prop;
	public GameObject propBlured;

	public bool engenOn = true;
    public int AngleAdjustmentZ = 0;
    public int AngleAdjustmentY = 0;
    public int AngleAdjustmentX = 0;
    public bool XaxisTurn = true;
    public bool YaxisTurn = false;
    public bool ZaxisTurn = false;
    private void Start()
    {
        engenOn = true;
    }
    void Update () 
	{
		if (engenOn) {
			prop.SetActive (false);
			propBlured.SetActive (true);
            turn();
            //propBlured.transform.Rotate (1000 * Time.deltaTime+ AngleAdjustmentX, AngleAdjustmentY, AngleAdjustmentZ);
		} else {
			prop.SetActive (true);
			propBlured.SetActive (false);
		}
	}
    public void turn()
    {
        if(XaxisTurn)
        {
            propBlured.transform.Rotate(1000 * Time.deltaTime , AngleAdjustmentY, AngleAdjustmentZ);
        }
        else if(YaxisTurn)
        {
            propBlured.transform.Rotate(AngleAdjustmentX,1000 * Time.deltaTime , AngleAdjustmentZ);
        }
        else
        {
            propBlured.transform.Rotate(AngleAdjustmentX, AngleAdjustmentY, 1000 * Time.deltaTime);
        }
    }
}

// PLEASE NOTE! THIS SCRIPT IS FOR DEMO PURPOSES ONLY :) //