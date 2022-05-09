using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    public AudioManagerScript audioManager;
    public UnityAdManagerScript adManager;

    [Header("Fader")]
    public CanvasGroup fader;
    public Text faedText;

    private void Awake()
    {
        fader.alpha = 0f;
        fader.gameObject.SetActive(false);
    }

    public void ShowFade(string text = "", System.Action onComplete = null)
    {
        fader.gameObject.SetActive(true);
        faedText.text = text;
        fader.DOFade(1f, 1f).OnComplete(() =>
        {
            fader.DOFade(0f, 0.5f).OnComplete(() =>
            {
                onComplete?.Invoke();
                fader.gameObject.SetActive(false);
            });
        });
    }

    static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Instance = FindObjectOfType<GameManager>();
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
}