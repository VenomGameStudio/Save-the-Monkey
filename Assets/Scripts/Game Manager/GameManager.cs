using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Fader")]
    GameObject currentFader;
    public GameObject fader;

    private void Awake() => instance = this;

    public void ShowFade()
    {
        if (currentFader == null)
            currentFader = Instantiate(fader, GameObject.FindGameObjectWithTag("Game Canvas").transform);

        Destroy(currentFader, currentFader.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1f);
    }
}