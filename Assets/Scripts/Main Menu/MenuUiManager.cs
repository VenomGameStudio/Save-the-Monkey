using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuUiManager : MonoBehaviour
{
    [Header("Loading")]
    public GameObject loadingPanel;
    public Slider loadingSlider;

    [Header("Menu Ui")]
    public GameObject menuUiPanel;
    public Button startBtn;
    public Button quitBtn;

    private void Awake()
    {
        loadingPanel.SetActive(true);
        menuUiPanel.SetActive(false);

        loadingSlider.value = 0f;
        startBtn.onClick.AddListener(() => OnClickStartBtn());
        quitBtn.onClick.AddListener(() => OnClickQuitBtn());

        StartCoroutine(StartLoading());
    }

    void OnClickStartBtn()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void OnClickQuitBtn()
    {
        Application.Quit();
    }

    IEnumerator StartLoading()
    {
        while (loadingSlider.value < 1)
        {
            yield return new WaitForEndOfFrame();
            loadingSlider.value += 0.01f;
        }

        GameManager.instance.ShowFade();

        yield return new WaitForSeconds(1f);
        ShowMenu();
    }
    void ShowMenu()
    {
        loadingPanel.SetActive(false);
        menuUiPanel.SetActive(true);
    }
}