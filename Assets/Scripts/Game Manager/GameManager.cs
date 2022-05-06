using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Managers")]
    public AudioManagerScript audioManager;
    public UnityAdManagerScript adManager;

    [Header("Fader")]
    public GameObject fader;
    GameObject currentFader;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void ShowFade()
    {
        if (currentFader == null)
            currentFader = Instantiate(fader, GameObject.FindGameObjectWithTag("Game Canvas").transform);

        Destroy(currentFader, currentFader.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1f);
    }
}