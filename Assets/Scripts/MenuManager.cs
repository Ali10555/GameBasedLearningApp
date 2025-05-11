using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
