using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
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
    public Button settingBtn;
    public Button exitBtn;

    [Header("Settings")]
    public Button closeBtn;
    public GameObject[] musicToggle = new GameObject[2];
    public GameObject[] effectsToggle = new GameObject[2];
    public Slider volSlider;
    public Button helpBtn, policyBtn, creditsBtn;

    [Header("Help")]
    public Button hCloseBtn;

    Panel currentPanel = null;
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
        if (CurrentPanel != null && CurrentPanel.panelObject != null)
            CurrentPanel.panelObject.SetActive(false);

        CurrentPanel = uiPanels.Find((x) => x.key == name);
        if (CurrentPanel != null && CurrentPanel.panelObject != null)
            CurrentPanel.panelObject.SetActive(true);
    }

    void WindowUpdated(Panel panel)
    {
        switch (panel.key)
        {
            case LocalizationKeys.Loading:
                loadingSlider.value = 0f;
                loadingSlider.DOValue(1f, 1f).OnComplete(() =>
                {
                    GameManager.Instance.ShowFade(LocalizationKeys.Loading.ToString(), () => { ChangeWindow(LocalizationKeys.Home); });
                });
                break;
            case LocalizationKeys.Home:
                SetupHomeBtn(); break;
            case LocalizationKeys.Setting:
                SetupSettingPanel(); break;
            case LocalizationKeys.Help:
                SetupHelpPanel(); break;
            default:
                Debug.LogError("No window is setup"); break;
        }
    }

    #region Home
    void SetupHomeBtn()
    {
        playBtn.OnClick(delegate { OnClickStartBtn(); });
        settingBtn.OnClick(delegate { ChangeWindow(LocalizationKeys.Setting); });
        exitBtn.OnClick(delegate { OnClickQuitBtn(); });
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

    #region Settings
    void SetupSettingPanel()
    {
        DoOpenPanel();

        closeBtn.OnClick(delegate { DoClosePanel(LocalizationKeys.Home); });
        helpBtn.OnClick(delegate { OpenHelp(); });

        void OpenHelp()
        {
            ChangeWindow(LocalizationKeys.Help);
        }
    }
    #endregion

    #region Help
    void SetupHelpPanel()
    {
        DoOpenPanel();
        hCloseBtn.OnClick(delegate { DoClosePanel(LocalizationKeys.Setting); });
    }
    #endregion

    void DoOpenPanel()
    {
        Vector3 extraScale = new Vector3(0.3f, 0.3f, 0.3f);
        CurrentPanel.panelObject.transform.localScale = Vector3.zero;
        CurrentPanel.panelObject.transform.DOBlendableScaleBy(Vector3.one + extraScale, 0.3f).OnComplete(() =>
        {
            CurrentPanel.panelObject.transform.DOBlendableScaleBy(-extraScale, 0.3f);
        });
    }
    void DoClosePanel(LocalizationKeys keys)
    {
        CurrentPanel.panelObject.transform.DOBlendableScaleBy(-Vector3.one, 0.3f).OnComplete(() =>
        {
            CurrentPanel.panelObject.transform.localScale = Vector3.zero;
            ChangeWindow(keys);
        });
    }

    [ContextMenu("SetName")]
    void SetName()
    {
        uiPanels.ForEach((x) => x.name = x.key.ToString().ToLower());
    }
}