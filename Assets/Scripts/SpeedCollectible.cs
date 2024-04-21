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
        // Declaring a variable used to get information from RubyController script
        RubyController controller = other.GetComponent<RubyController>();

        // If Ruby is active, do this
        if (controller != null)
        {
            // Increases speed from 3 to 4.5, destroys speed pick up object, and plays boost sound effect
            controller.speed = 4.5f;
            Destroy(gameObject);
            controller.PlaySound(speedUp);
        }
    }
}
