using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    public int health { get { return currentHealth; } }
    public int currentHealth;
    bool isInvincible;
    float invincibleTimer;
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    public GameObject projectilePrefab;
    public AudioClip throwSound;
    public AudioClip hitSound;
    public AudioClip speechSound;
    AudioSource audioSource;
    public GameObject HurtParticle;
    public GameObject HealParticle;
    public bool hasTwenty;
    public bool hasSpreadShot;
    //public ParticleSystem Hurtfab;
    //public ParticleSystem Healfab;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (hasSpreadShot)
            {
                LaunchSpreadShot();
            }
            else
            {
                Launch();
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                    PlaySound(speechSound);
                }
                SketchController sketch = hit.collider.GetComponent<SketchController>();
                if (sketch != null)
                {
                    sketch.DisplayDialog();
                    PlaySound(speechSound);
                }
            }
        }
    }
    public void SetSpreadShot(bool value)
        {
        hasSpreadShot = value;
        }
    public void SetTwenty(bool value)
        {
        hasTwenty = value;
        }
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;    
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
            //ParticleSystem particleObject = Instantiate(HurtEffect, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            PlaySound(hitSound);
        }

        if (amount > 0)
        {
            //ParticleSystem particleObject = Instantiate(Healfab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        PlaySound(throwSound);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Hurt();
        }

        /*if (other.gameObject.CompareTag("health"))
        {
            Heal();
        }*/
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("health") && currentHealth < 5)
        {
            Heal();
        }

        if (other.gameObject.CompareTag("spikes"))
        {
            Hurt();
        }
    }

    public void Hurt()
    {
        GameObject HurtEffect = Instantiate(HurtParticle, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        HurtEffect.GetComponent<ParticleSystem>().Play();
    }

    public void Heal()
    {
        GameObject HealEffect = Instantiate(HealParticle, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        HealEffect.GetComponent<ParticleSystem>().Play();
    }

     void LaunchSpreadShot()
    {
        for (int i = -1; i <= 1; i++)
        {
            Vector2 spreadDirection = new Vector2(lookDirection.x, lookDirection.y + i * 0.1f);
            GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(spreadDirection, 300);
        }

        animator.SetTrigger("Launch");
        PlaySound(throwSound);
    }
}
