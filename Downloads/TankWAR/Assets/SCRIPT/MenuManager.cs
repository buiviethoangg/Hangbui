using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void onclickSignalGame()
    {
        SceneManager.LoadScene("Lv2");
        Time.timeScale = 1;
    }
    public void onclickSignalGame2()
    {
        SceneManager.LoadScene("Scene2");
        Time.timeScale = 1;
    }
}