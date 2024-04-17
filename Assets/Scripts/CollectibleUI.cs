using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectibleUI : MonoBehaviour
{
    public TextMeshProUGUI collectibleStatusText;
    public RubyController rubyController;

    //All code written by Colby Sparkman
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    if (rubyController != null)
    {
        collectibleStatusText.text = rubyController.hasTwenty ? "Sketch's Collectible Collected" : "Sketch's Collectible Not Collected";
    }
    else
    {
        collectibleStatusText.text = "RubyController not assigned!";
    }
    }
}
