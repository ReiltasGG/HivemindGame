using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Day 1");
    }

    public void QuitGame()
    {
        Debug.Log("Game has been quit");

        Application.Quit();
    }
}
