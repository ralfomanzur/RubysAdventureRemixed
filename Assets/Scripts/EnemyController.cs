using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    Rigidbody2D rb;
    float timer;
    int direction = 1;
    Animator animator;
    bool broken;
    public ParticleSystem smokeEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        broken = true;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        if (!broken)
        {
            return;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rb.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction; ;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction; ;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rb.MovePosition(position);

        if (!broken)
        {
            return;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        rb.simulated = false;
        smokeEffect.Stop();
        animator.SetTrigger("Fixed");
    }
}
