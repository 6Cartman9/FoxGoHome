using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChage : MonoBehaviour
{

    [SerializeField] private string sceneToLoad;
    [SerializeField] private AudioSource inhome;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inhome.Play();
            GameUIController.gc.ResetHp();
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
