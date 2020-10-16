using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script qui met a jour le score en fonction de la position x de l'oiseau et des points bonus
public class UpdateScore : MonoBehaviour
{
    public GameObject scoreTextGo;
    Text scoreText;

    public GameObject bird;
    Game gameScript;

    public int totalScore = 0;
    public int bonusScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = scoreTextGo.GetComponent<Text>();
        gameScript = GetComponent<Game>();      
    }

    // Update is called once per frame
    void Update()
    {
       if(gameScript.gameState == Game.GameState.IN_GAME)
        {
            int bx = (int)bird.transform.position.x;
            int ix = (int)CreatePipes.intervalleXEntreDeuxTuyaux;

            int score = (bx - (bx % ix)) / ix;

            totalScore = score + bonusScore;
            scoreText.text = "SCORE " + (totalScore);
        }

    }
}
