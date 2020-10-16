using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

// Script general du jeu : gerer les differentes etapes du jeu
public class Game : MonoBehaviour
{
    public enum GameState
    {
        INTRO, // on est sur lecran d accueil
        IN_GAME // on est en partie
    }

    public GameState gameState = GameState.INTRO;

    // definir une reference vers bird
    public GameObject bird;

    // definir une reference vers le texte d'intro
    public GameObject introText;

    int limitY = 5; // la camera va de +5 a -5

    // Les references vers nos autres scripts
    MoveBird moveBird;
    CreatePipes createPipes;
    UpdateScore updateScore;

    public GameObject highscoreTextGo;
    Text highscoreText;
    int highscore = 0;


    // Start is called before the first frame update
    void Start()
    {
        // print(this.gameObject.name);
        moveBird = bird.GetComponent<MoveBird>();
        createPipes = bird.GetComponent<CreatePipes>();
        updateScore = Camera.main.GetComponent<UpdateScore>();

        highscoreText = highscoreTextGo.GetComponent<Text>();

        IntroScreen();
        GetHighScore();
    }

    // Recuperation du highscore
    void GetHighScore()
    {
        Score score = new Score(0);

        if(File.Exists(Application.dataPath + "/Highscore.json"))
        {
            // je recupere le highscore
            File.ReadAllText(Application.dataPath + "/Highscore.json");

            score = JsonUtility.FromJson<Score>(
                File.ReadAllText(Application.dataPath + "/Highscore.json")); 
        }
        else
        {
            // si le fichier nexiste je le cree
            score = new Score(0);
            string json = JsonUtility.ToJson(score);

            File.WriteAllText(Application.dataPath + "/Highscore.json", json);
        }

        highscore = score.score;
    }

    // ecrire le highscore dans un fichier
    void WriteHighscore(int newHighscore)
    {
        Score score = new Score(newHighscore);
        string json = JsonUtility.ToJson(score);
        File.WriteAllText(Application.dataPath + "/Highscore.json", json);
    }

    // Mettre a jour le texte du highscore
    void UpdateHighscoreText()
    {
        highscoreText.text = "HIGHSCORE : " + highscore;
    }

    // Quand on passe en mode intro
    void IntroScreen()
    {
        gameState = GameState.INTRO;

        // bird soit inactif
        bird.SetActive(false);

        // le texte d'intro actif
        introText.SetActive(true);
    }

    public void GameOver()
    {
        if(updateScore.totalScore > highscore)
        {
            highscore = updateScore.totalScore;
            UpdateHighscoreText();
            WriteHighscore(updateScore.totalScore);
        }

        // je reinitialise la vitesse de deplacement
        moveBird.ResetMoveSpeed();

        // je detruis tous les pipes
        foreach(GameObject pipe in GameObject.FindGameObjectsWithTag("PipeRoot"))
        {
            Destroy(pipe);
        }

        // je detruis tous les bonus
        foreach (GameObject bonus in GameObject.FindGameObjectsWithTag("Bonus"))
        {
            Destroy(bonus);
        }

        // je detruis toutes les pieces
        foreach (GameObject coin in GameObject.FindGameObjectsWithTag("Bonus"))
        {
            Destroy(coin);
        }

        createPipes.ResetNextStepToCreateNewPipe();

        updateScore.bonusScore = 0;

        // je mets l'ecran d'intro
        IntroScreen();
    }

    void StartGame()
    {

        gameState = GameState.IN_GAME;

        // le texte d'intro inactif
        introText.SetActive(false);

        // bird soit actif
        bird.SetActive(true);

        // on positionne Bird a 0,0,0
        bird.transform.position = new Vector3(-5, 0, 0);

        // on va annuler les forces appliquees sur bird
        Rigidbody2D rg = bird.GetComponent<Rigidbody2D>();
        rg.velocity = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        // si on est en mode intro et qu'on clique
        if(gameState == GameState.INTRO &&
            Input.GetMouseButtonDown(0))
        {
            StartGame();
        }

        // QUAND ON SORT DES LIMITES
        if(gameState == GameState.IN_GAME && 
            !BirdInBounds())
        {
            GameOver();
        }

        UpdateHighscoreText();

        // on lance la partie
    }

    // true : false
    // nous dire si l'oiseau est dans les limites ( limitY : -limitY )
    bool BirdInBounds()
    {
        // si loiseau nest pas compris entre -limitY et limitY
        if (-limitY < bird.transform.position.y && 
            bird.transform.position.y < limitY) return true;
        return false;
    }
}
