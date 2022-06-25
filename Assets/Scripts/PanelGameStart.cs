using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGameStart : MonoBehaviour
{
    #region variable
    public GameObject GamePanelPrefab;
    private GameObject GamePanel;

    
    #endregion

    public static PanelGameStart sharedInstance;
    // Start is called before the first frame update
    void Start()
    {
        sharedInstance = this;
        //StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //JoystickPanel.sharedInstance.gameObject.SetActive(true);
        GamePanel = Instantiate(GamePanelPrefab,this.gameObject.transform);
    }

    public void RemoveGamePanel()
    {
        Destroy(GamePanel);
        GamePanel = null;
    }

    
}
