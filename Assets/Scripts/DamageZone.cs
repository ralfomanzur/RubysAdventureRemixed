using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public GameObject HurtParticle;
    public GameObject Rubyobj;
    Rigidbody2D rb;
    bool activated;
    public float downTime = 0;

    void Start()
    {
        rb = Rubyobj.GetComponent<Rigidbody2D>();
        activated = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null && activated == false)
        {
            controller.ChangeHealth(-1);
            Hurt();
        }
    }

    void Update()
    {
        if (downTime >= 2.0f && activated == true)
        {
            //yield WaitForSeconds(2);
            activated = false;
        }

        /*if (downTime <= 0.0f && activated == true)
        {
            activated = false;
        }*/
    }

    void Hurt()
    {
        GameObject HurtEffect = Instantiate(HurtParticle, rb.position + Vector2.up * 0.5f, Quaternion.identity);
        HurtEffect.GetComponent<ParticleSystem>().Play();
        activated = true;
        downTime = 2.0f;
    }

    /*IEnumerator Coroutine()
    {

    }*/
}