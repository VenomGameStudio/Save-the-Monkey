using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[Serializable]
public class Panel
{
    public string name;
    public LocalizationKeys key;
    public GameObject panelObject;
}

public class MenuUiManager : MonoBehaviour
{
    public LocalizationKeys startPanel;
    public List<Panel> uiPanels = new List<Panel>();

    [Header("Loading")]
    public Slider loadingSlider;

    [Header("Home")]
    public Button playBtn;
    public Button exitBtn;

    [SerializeField] Panel currentPanel = null;
    private Panel CurrentPanel { get => currentPanel; set { currentPanel = value; WindowUpdated(currentPanel); } }

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        uiPanels.ForEach((x) => x.panelObject.SetActive(false));
        ChangeWindow(startPanel);
    }

    void ChangeWindow(LocalizationKeys name)
    {
        if (CurrentPanel.panelObject != null)
            CurrentPanel.panelObject.SetActive(false);

        CurrentPanel = uiPanels.Find((x) => x.key == name);
        if (CurrentPanel.panelObject != null)
            CurrentPanel.panelObject.SetActive(true);
    }

    void WindowUpdated(Panel panel)
    {
        switch (panel.key)
        {
            case LocalizationKeys.Loading:
                loadingSlider.value = 0f;
                StartCoroutine(StartLoading());
                break;
            case LocalizationKeys.Home:
                SetupHomeBtn(); break;
            default:
                Debug.LogError("No window is setup"); break;
        }
    }

    #region Loading
    IEnumerator StartLoading()
    {
        while (loadingSlider.value < 1)
        {
            yield return new WaitForEndOfFrame();
            loadingSlider.value += 0.01f;
        }

        GameManager.instance.ShowFade();

        yield return new WaitForSeconds(1f);
        ChangeWindow(LocalizationKeys.Home);
    }
    #endregion

    #region Home
    void SetupHomeBtn()
    {
        playBtn.OnClick(() => OnClickStartBtn());
        exitBtn.OnClick(() => OnClickQuitBtn());
    }
    void OnClickStartBtn()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void OnClickQuitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion

    [ContextMenu("SetName")]
    void SetName()
    {
        uiPanels.ForEach((x) => x.name = x.key.ToString().ToLower());
    }
}