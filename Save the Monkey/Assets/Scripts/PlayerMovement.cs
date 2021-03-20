using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 300f;
    public GameObject player;

    private Vector2 targetPos;
    private Vector3 playerScale;
    private float screenHeight;

    void Start()
    {
        screenHeight = Screen.height - (Screen.height * 0.2f);
        playerScale = transform.localScale;
        targetPos = new Vector2(1.45f, -3f);
    }

    private void Update()
    {
#if UNITY_EDITOR_WIN
        MovePlayer(Input.GetAxis("Horizontal"));
#else
        if (Input.touchCount > 0 && !PauseScript.gameIsPause)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if(touch.position.y < screenHeight)
                    if (touch.position.x > Screen.width / 2)
                        MovePlayer(1);
                    if (touch.position.x < Screen.width / 2)
                        MovePlayer(-1);
            }
        }
#endif
    }

    private void MovePlayer(float horizopntalInput)
    {
        if (horizopntalInput > 0)
        {
            targetPos = new Vector2(1.45f, -3f);
            playerScale.y = 1;
        }
        if (horizopntalInput < 0)
        {
            targetPos = new Vector2(-1.45f, -3f);
            playerScale.y = -1;
        }

        //FindObjectOfType<AudioManagerScript>().Play("Jump");
        player.transform.position = Vector2.MoveTowards(player.transform.position, targetPos, moveSpeed * Time.deltaTime);
        player.transform.localScale = playerScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Trunk"))
        {
            FindObjectOfType<PauseScript>().gameOverUI.SetActive(true);
            Time.timeScale = 0f;
            UnityAdManagerScript.ShowBanner();
            Destroy(gameObject);
        }
    }
}
