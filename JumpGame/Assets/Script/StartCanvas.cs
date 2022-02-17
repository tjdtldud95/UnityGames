using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class StartCanvas : MonoBehaviour
{
    public PlayerData data;
    private void Start()
    {
        string score;
        var ob = transform.Find("Score").GetComponent<TextMeshProUGUI>();
        
        score = data.GetScore().ToString();

        ob.text = score;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            SystemDown();
    }
    public void GoInScene()
    {
        SceneManager.LoadScene("InGame");
    }


    public void SystemDown()
    {
        Application.Quit();
    }
}
