using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    public static int score;
    public Text scoreText;
    public AudioSource soundPlayer;
    public List<AudioClip> splashSounds = new List<AudioClip>();
    
    public void HitsTheToilet()
    {
        soundPlayer.clip = splashSounds[(Random.Range(0, splashSounds.Count))];
        soundPlayer.Play();
        score++;

        scoreText.text = "Score: " + score.ToString();

        if (score >= 10 && score < 20)
        {
            if(scoreText.color != Color.yellow)
                scoreText.color = Color.yellow;

        }

        if (score >= 20)
        {
            if (scoreText.color != Color.green)
                scoreText.color = Color.green;
        }

    } 

}
