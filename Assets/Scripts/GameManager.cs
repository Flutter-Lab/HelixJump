using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{

    public int best;
    public int score;

    public int currentStage = 0;

    public static GameManager singleton;




    void Awake()
    {
        Advertisement.Initialize("4140741");
        
        

        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        best = PlayerPrefs.GetInt("HighScore");
    }


    
    public void NextLevel()
	{
        currentStage++;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
        Debug.Log("Next Level called");
	}

    public void RestartLevel()
	{
        Debug.Log("Game over");
        //Show Ads
        // 4140741
        Advertisement.Show();




        singleton.score = 0;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
        // Reload Stage
	}

    public void AddScore(int scoreToAdd)
	{
        score += scoreToAdd;

		if (score > best)
		{
            best = score;

            PlayerPrefs.SetInt("HighScore", score);
		}
	}



}
