using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public GameObject mainPanel;  // 메인 패널
    public GameObject subPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMainPanel()
    {
        mainPanel.SetActive(true);
        subPanel.SetActive(false);
    }
}
