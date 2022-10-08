using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public float score = 0;
    public float playerHealth = 3;

    [SerializeField]
    TextMeshProUGUI scoreLabel;

    [SerializeField]
    Slider healthBar;

    [SerializeField]
    RectTransform hudDimensions;

    public float hudWidth
    {        
        get { return hudDimensions.anchorMax.x - hudDimensions.anchorMin.x; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = "Score: " + score.ToString("000000000");
    }
}
