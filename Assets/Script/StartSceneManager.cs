using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public bool isLoaded = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Button_Start()
    {
        isLoaded = false;
        SceneManager.LoadScene(2);
    }

    public void Button_Load()
    {
        isLoaded = true;
        SceneManager.LoadScene(2);
    }

    public void Button_Quit()
    {
        Application.Quit();
    }
}
