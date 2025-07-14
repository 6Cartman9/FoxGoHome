using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private enum State
    {
        Idel,
        Run,
        Jump,
        Fall,
        Hurt,
        Clipmb
    }

    private State state = State.Idel;

    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    

    [SerializeField] private LayerMask ground;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float hurtForce = 10f;

    [SerializeField] private AudioSource gemPickup;
    [SerializeField] private AudioSource footsteps;
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource heal;

    [HideInInspector] public bool canClimb = false;
    [HideInInspector] public bool isTopLadder = false;
    [HideInInspector] public bool isDownLadder = false;
    public Ladder ladder;
    private float normalGravity;
    [SerializeField] private float climbSpeed = 3f;
   
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        normalGravity = rb.gravityScale;
    }

    private void Update()
    {
        if(state == State.Clipmb)
        {
            Climb();
        }
        else if(state != State.Hurt)
        {
            Movement();
        }

        AnimationState();

        anim.SetInteger("state", (int)state);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            gemPickup.Play();
            Destroy(collision.gameObject);
            GameUIController.gc.gemAmount++;
            GameUIController.gc.gemText.text = GameUIController.gc.gemAmount.ToString();
        }
        if(collision.tag == "PowerUp")
        {
            Destroy(collision.gameObject);
            speed += 5f;
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(ResetPowerUp());
        }
        if (collision.tag == "Heal")
        {
            heal.Play();
            Destroy(collision.gameObject);
            HealPlayer(1);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if (state == State.Fall)
            {
                enemy.JumpOn();
                Jump();
            }
            else
            {
                TakeDamage();

                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
    }

    public void TakeDamage()
    {
        state = State.Hurt;

        GameUIController.gc.health--;

        GetComponent<SpriteRenderer>().color = Color.red;

        GameUIController.gc.healthText.text = GameUIController.gc.healthText.ToString();

        StartCoroutine(ResetPowerUp());

        if(GameUIController.gc.health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    public static void HealPlayer(int healAmount)
    {
        GameUIController.gc.health = Mathf.Min(GameUIController.gc.maxHealth, GameUIController.gc.health + healAmount);
    }

    private void Movement()
    {
        float horDir = Input.GetAxis("Horizontal");

        // Влево
        if (horDir < 0)
        {
            if (!IsTouchingWall(Vector2.left))
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                transform.localScale = new Vector2(-1, 1);
            }
        }
        // Вправо
        else if (horDir > 0)
        {
            if (!IsTouchingWall(Vector2.right))
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.localScale = new Vector2(1, 1);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Прыжок
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
        }

        if (canClimb && Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
        {
            state = State.Clipmb;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            rb.gravityScale = 0f;
        }
    }

    private bool IsTouchingWall(Vector2 direction)
    {
        float rayLength = 0.5f; 

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, ground);

        return hit.collider != null;
    }

    private bool IsWallInFront(Vector2 direction)
    {
        float rayLength = 0.5f; 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, ground);

        return hit.collider != null;
    }

    private void Climb()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            canClimb = false;
            rb.gravityScale = normalGravity;
            anim.speed = 1f;
            Jump();
            return;
        }

        float climbDir = Input.GetAxis("Vertical");

        if(climbDir > 0.1f && !isTopLadder)
        {
            rb.velocity = new Vector2(0f, climbDir * climbSpeed);
            anim.speed = 1f;

        }
        else if (climbDir < -0.1f && !isDownLadder)
        {
            rb.velocity = new Vector2(0f, climbDir * climbSpeed);
            anim.speed = 1f;

        }
        else
        {
            anim.speed = 0f;
            rb.velocity = Vector2.zero;

        }
    }

    public void Jump()
    {
        jump.Play();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.Jump;
    }

    private void AnimationState()
    {
        if(state == State.Clipmb)
        {

        }
        else if(state == State.Jump)
        {
            if(rb.velocity.y < .1f)
            {
                state = State.Fall;
            }
        }
        else if(state == State.Fall)
        {
            if(coll.IsTouchingLayers(ground))
            {
                state = State.Idel;
            }
        }
        else if(state == State.Hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.Idel;
            }
        }
        else if(Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.Run;
        }
        else
        {
            state = State.Idel;
        }
    }

    private void PlayFootstepSound()
    {
        footsteps.Play();
    }

    private IEnumerator ResetPowerUp()
    {
        yield return new WaitForSeconds(1);
        speed = 7;
        GetComponent<SpriteRenderer>().color = Color.white;

    }
}
