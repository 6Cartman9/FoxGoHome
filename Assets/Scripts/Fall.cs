using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fall : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameUIController.gc.ResetGem();

            GameUIController.gc.health--;


            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
