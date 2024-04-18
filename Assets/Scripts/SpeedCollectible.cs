using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCollectible : MonoBehaviour
{
    //This was made by Ralfo M
    // Public (so I can modify in Inspector) AudioClip variable for the speed up sound effect
    public AudioClip speedUp;

    // Code that speeds Ruby up, plays a sound, and destroys the pickup object
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            controller.speed = 4.5f;
            Destroy(gameObject);
            controller.PlaySound(speedUp);
        }
    }
}
