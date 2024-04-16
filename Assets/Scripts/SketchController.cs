using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SketchController : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public GameObject extraBox;
    float timerDisplay;

    private RubyController rubyController;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        extraBox.SetActive(false);
        timerDisplay = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
                extraBox.SetActive(false);
            }
        }
    }

    public void DisplayDialog()
{
    RubyController rubyController = FindObjectOfType<RubyController>();
    if (rubyController != null && rubyController.hasTwenty)
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(false);
        extraBox.SetActive(true);
    }
    else
    {
        timerDisplay = displayTime;
        dialogBox.SetActive(true);
        extraBox.SetActive(false);
    }
    }
}