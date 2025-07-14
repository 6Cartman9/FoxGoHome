using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{
    private PlayerController player;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.Jump();
            player.TakeDamage();

           

            //StartCoroutine(HurtTime());


        }
    }

    private IEnumerator HurtTime()
    {
        yield return new WaitForSeconds(1);

        GameUIController.gc.ResetGem();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


        GetComponent<SpriteRenderer>().color = Color.white;
    }

}
