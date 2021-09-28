using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using System.Collections.Generic;

public class FacebookManager : MonoBehaviour
{
    public static FacebookManager instance;

    public TMP_Text userName;
    public Image profilePic;
    public Button loginBtn, logoutBtn;

    private void Awake()
    {
        instance = this;

        loginBtn.onClick.AddListener(() => OnClickLoginBtn());
    }
    private void Start()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                    FB.ActivateApp();
                else
                    Debug.LogError("Couldn't initialize");
            }, isGameShown =>
            {
                if (!isGameShown)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            });
        }
        else
            FB.ActivateApp();
    }

    void OnClickLoginBtn()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");
        permissions.Add("user_friends");
        FB.LogInWithReadPermissions(permissions, AuthCallBack);
    }
    void AuthCallBack(IResult result)
    {
        if (result.Error != null)
            Debug.Log(result.Error);
        else
        {
            if (FB.IsLoggedIn)
                Debug.Log("Facebook is Login!");
            else
                Debug.Log("Facebook is not Logged in!");
            DealWithFbMenus(FB.IsLoggedIn);
        }
    }
    void DealWithFbMenus(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
        }
    }
    void DisplayUsername(IResult result)
    {
        if (result.Error == null)
        {
            string name = "" + result.ResultDictionary["first_name"];
            userName.text = name;
        }
        else
            Debug.LogError(result.Error);
    }
    void DisplayProfilePic(IGraphResult result)
    {
        if (result.Texture != null)
            profilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
        else
            Debug.LogError(result.Error);
    }

}