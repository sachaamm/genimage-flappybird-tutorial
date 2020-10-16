using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script qui gere la musique de notre jeu
public class MusicScript : MonoBehaviour
{
    // un tableau (=liste) contenant les sources sonores de notre jeu
    public AudioClip[] clips;
    Game game;

    int musicIndex = 0; 

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        game = GetComponent<Game>();
        PlayMusic();
    }

    void PlayMusic()
    {
        audioSource.clip = clips[musicIndex];
        audioSource.Play();
        musicIndex++;
        musicIndex %= clips.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(game.gameState != Game.GameState.IN_GAME)
        {
            audioSource.enabled = false;
            return; // me permet d'arreter la fonction a cet endroit
        }
        else
        {
            audioSource.enabled = true;
        }

        if (!audioSource.isPlaying) // quand la musique s'arrete on peut passer a la suivante
        {
            PlayMusic();
        }
    }
}
