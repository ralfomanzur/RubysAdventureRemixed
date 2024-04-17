using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Ruby;
    public GameObject GameOverPanel;
    public GameObject WinPanel;
    public GameObject Bot;

    // Start is called before the first frame update
    void Start()
    {
        WinPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    if (Ruby.GetComponent<RubyController>().hasTwenty && Bot.GetComponent<EnemyController>().botsFixed >= 1)
    {
        WinPanel.SetActive(true);
        Ruby.SetActive(false);
    }

    if (Ruby.GetComponent<RubyController>().currentHealth == 0)
    {
        GameOverPanel.SetActive(true);
        Ruby.SetActive(false);
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
}
