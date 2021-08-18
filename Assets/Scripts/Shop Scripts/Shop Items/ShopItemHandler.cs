using UnityEngine;

public class ShopItemHandler : MonoBehaviour
{
    public ShopBgItem[] shopBgItems;

    public static ShopItemHandler instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}