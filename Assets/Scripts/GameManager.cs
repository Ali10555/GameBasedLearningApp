using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }


    public void QuitApp()
    {
        Application.Quit();
    }

}
