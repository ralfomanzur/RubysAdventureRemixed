using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Simple script by Ralfo M that controls the game's win/lose conditions & some UI

    public GameObject Ruby;
    public GameObject GameOverPanel;
    public GameObject WinPanel;
    public Text botText;
    public int botsFixed = 0;
    public Image sketchText;
    public GameObject collectibleText;

    // Start is called before the first frame update
    void Start()
    {
        WinPanel.SetActive(false);
        GameOverPanel.SetActive(false);

        botText.text = "Robots fixed: " + botsFixed;
    }

    void Update()
    {
        // Checks for bots fixed and player possession of collectible, collectible component added by Colby

        if (Ruby.GetComponent<RubyController>().hasTwenty && botsFixed == 2)
        {
            if (sketchText.isActiveAndEnabled)
            {
                WinPanel.SetActive(true);
                Ruby.SetActive(false);
                botText.enabled = false;
                collectibleText.SetActive(false);
            }
        }
        
        // If Ruby has no health, game over, press R to restart, reloads scene

        if (Ruby.GetComponent<RubyController>().currentHealth == 0)
        {
            GameOverPanel.SetActive(true);
            Ruby.SetActive(false);
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("MainScene");
            }
        }

        botText.text = "Robots Fixed: " + botsFixed;

        if (botsFixed > 2)
        {
            botsFixed = 2;
        }
    }
}