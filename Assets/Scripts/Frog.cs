using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpHeigth = 10f;
    [SerializeField] private float jumpLength = 15f;

    [SerializeField] private LayerMask ground;

    private bool lookleft = true;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
       
        if (anim.GetBool("jump"))
        {
            if (rg.velocity.y < 0.1)
            {
                anim.SetBool("fall", true);
                anim.SetBool("jump", false);
            }
        }

        if (coll.IsTouchingLayers(ground) && anim.GetBool("fall"))
        {
            anim.SetBool("fall", false);
        }
    }

    private void FrogMove()
    {
        if (lookleft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }

                if (coll.IsTouchingLayers(ground))
                {
                    rg.velocity = new Vector2(-jumpLength, jumpHeigth);
                    anim.SetBool("jump", true);
                }
            }
            else
            {
                lookleft = false;
            }
        }
        else
        {
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }

                if (coll.IsTouchingLayers(ground))
                {
                    rg.velocity = new Vector2(jumpLength, jumpHeigth);
                    anim.SetBool("jump", true);
                }
            }
            else
            {
                lookleft = true;
            }
        }

    }

   
}
