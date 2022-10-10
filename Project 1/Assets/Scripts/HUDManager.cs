using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public float score = 0;
    public float playerHealth = 3;
    public float power = 1;

    [SerializeField]
    TextMeshProUGUI scoreLabel;

    [SerializeField]
    TextMeshProUGUI powerLabel;

    [SerializeField]
    Slider healthBar;

    [SerializeField]
    RectTransform hudDimensions;

    public float hudWidth
    {        
        get { return hudDimensions.anchorMax.x - hudDimensions.anchorMin.x; }
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = "Score: " + score.ToString("000000000");
        powerLabel.text = "Power: " + power.ToString("0.00");
    }
}
