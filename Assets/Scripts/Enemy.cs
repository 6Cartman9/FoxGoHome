using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rg;
    protected Collider2D coll;
    protected AudioSource hit;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rg = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        hit = GetComponent<AudioSource>();

    }

    public void JumpOn()
    {
        anim.SetTrigger("death");
        hit.Play();
        rg.velocity = Vector2.zero;
        rg.bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }

}
