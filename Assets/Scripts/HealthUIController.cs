using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    public GameObject heartContainer;

    private float fillValue;

    private void Update()
    {
        fillValue = GameUIController.gc.health;
        fillValue = fillValue / GameUIController.gc.maxHealth;
        heartContainer.GetComponent<Image>().fillAmount = fillValue;
    }
}
