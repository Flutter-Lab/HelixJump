using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText, bestScoreText;





    void Update()
    {
        bestScoreText.text = "Best: " + GameManager.singleton.best;
        scoreText.text = "Score: " + GameManager.singleton.score;
    }
}
