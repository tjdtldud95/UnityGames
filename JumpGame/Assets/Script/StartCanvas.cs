using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class StartCanvas : MonoBehaviour
{

    private void Start()
    {
        var ob = transform.Find("Score").GetComponent<TextMeshProUGUI>();
    }
    public void GoInScene()
   {
        SceneManager.LoadScene("InGame");
    }
}
