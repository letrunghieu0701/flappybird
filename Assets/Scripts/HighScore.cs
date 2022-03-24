using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    TextMeshProUGUI highScoreText = null;
    // Start is called before the first frame update
    void Start()
    {
        highScoreText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", -99).ToString();
    }
}
