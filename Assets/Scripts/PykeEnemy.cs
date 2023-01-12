using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PykeEnemy : MonoBehaviour
{
    [SerializeField]
    private int health = 100;
    [SerializeField]
    public int speed = 300;

    private float directionX;
    private Rigidbody2D Rigidbody2D;
    private bool justTurned;

    private bool headingLeft = true;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("Walk", true);
        directionX = -1f;

        int dificulty = PlayerPrefs.GetInt("Difficulty");
        if (dificulty == 1) health = health * 2;
        if (dificulty == 2) health = health * 3;
        if (dificulty == 3) health = health * 5;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!justTurned && collision.gameObject.CompareTag("Wall"))
        {
            directionX *= -1f;
            StartCoroutine(JustTurned());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Sprite turning
        if (!headingLeft)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (!justTurned)
        {
            //Rigidbody2D.velocity = new Vector2(directionX * speed * Time.deltaTime, Rigidbody2D.velocity.y);
            Rigidbody2D.AddForce(new Vector2(directionX * speed * Time.deltaTime, Rigidbody2D.velocity.y));
        }
    }

    IEnumerator JustTurned()
    {
        headingLeft = !headingLeft;
        justTurned = true;
        animator.SetBool("Idle", true);
        animator.SetBool("Walk", false);
        yield return new WaitForSeconds(0.6f);
        justTurned = false;
        animator.SetBool("Idle", false);
        animator.SetBool("Walk", true);
    }

    public void TakeDamage(int damage)
    {

        health -= damage;
        if (health < 0)
        {
            StartCoroutine(Die());
        }
        else
        {
            StartCoroutine(Hitted());
        }
    }

    IEnumerator Die()
    {
        animator.SetTrigger("Dead");
        Rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;
        Rigidbody2D.gravityScale = 500;
        yield return new WaitForSecondsRealtime(0.6f);
        animator.SetTrigger("Destroy");
        Destroy(gameObject);
    }

    IEnumerator Hitted()
    {
        animator.SetBool("Hit", true);
        yield return new WaitForSecondsRealtime(0.2f);
        animator.SetBool("Hit", false);
    }
}
