using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject subPanel1;
    public GameObject subPanel2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnclickGameStart()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnclickHowTo()
    {
        mainPanel.SetActive(false);
        subPanel1.SetActive(true);
    }

    public void OnclickInfo() 
    {
        mainPanel.SetActive(false);
        subPanel2.SetActive(true);
    }

}
