using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public int health = 3;
    public int maxHealth = 3;

    public int gemAmount = 0;
    public Text gemText;
    public Text healthText;

    public static GameUIController gc;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (!gc)
        {
            gc = this;
        }
        else
            Destroy(gameObject);
    }

    public void ResetGem()
    {
        gemAmount = 0;
        gemText.text = gemAmount.ToString();
    }

    public void ResetHp()
    {
        health = 3;
        healthText.text = gemAmount.ToString();
    }
}
