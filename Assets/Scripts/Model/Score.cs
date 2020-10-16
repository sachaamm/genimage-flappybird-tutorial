using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// classe serializable de score pour enregistrer le highscore dans un fichier
[Serializable]
public class Score
{
    public int score;

    public Score(int score)
    {
        this.score = score;
    }
}
