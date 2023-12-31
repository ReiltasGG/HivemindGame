using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject buttonConfig;
    public GameObject mainMenuButton;
    void Start()
    {
        buttonConfig.SetActive(false);
    }
    public void PlayGame()
    {
        PlayerPrefs.DeleteKey("SkillPoints");
        PlayerPrefs.DeleteKey("PlayerBulletSpeed");
        PlayerPrefs.DeleteKey("PlayerMaxHealth");
        PlayerPrefs.DeleteKey("PlayerAttackDamage");

        PlayerPrefs.DeleteKey("PreviousScenePlayerBulletSpeed");
        PlayerPrefs.DeleteKey("PreviousScenePlayerMaxHealth");
        PlayerPrefs.DeleteKey("PreviousScenePlayerAttackDamage");

        for(int i = 0; i < 13; i++){
            PlayerPrefs.DeleteKey("hasBeenBought" + i);
            PlayerPrefs.DeleteKey("SkillLogicExecuted" + i);
        }
        
        SceneManager.LoadScene("Day 1");
    }

    public void Credits()
    {
        SceneManager.LoadScene("CreditsScene");
    }
    public void HowToPlay()
    {
        SceneManager.LoadScene("Day 0");
    }

    public void QuitGame()
    {
        Debug.Log("Game has been quit");

        Application.Quit();
    }
    public void ButtonConfiguration()
    {
        buttonConfig.SetActive(true);
        mainMenuButton.SetActive(false);
    }
    public void closeButtonConfig()
    {
         buttonConfig.SetActive(false);
        mainMenuButton.SetActive(true);
    }

}
