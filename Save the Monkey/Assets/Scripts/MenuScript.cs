using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public bool transmit = false;
    
    private void OnEnable()
    {
        UnityAdManagerScript.ShowBanner();
    }

    void Update()
    {
        if (transmit)
        {
            PlayGame();
        }
    }

    public void PlayGame()
    {
        transmit = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
