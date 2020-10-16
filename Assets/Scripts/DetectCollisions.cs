using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script pour detecter les collisions avec les tuyaux
public class DetectCollisions : MonoBehaviour
{
    // je defini mon script Game que j'ai code 
    // precedemment 
    Game game;
    BonusScript bonusScript;

    void Start()
    {
        GameObject mainCamera = Camera.main.gameObject;
        game = mainCamera.GetComponent<Game>();

        bonusScript = GetComponent<BonusScript>();
    }

    void Update()
    {
       
    }

    // Evenement qui est appelle quand
    // On rentre dans un trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si je suis en collision avec un objet de tag Pipe
        if(collision.gameObject.tag == "Pipe")
        {
            if(!bonusScript.invicibilityBonus)
            {
                // GAME OVER
                Debug.Log("GAME OVER");
                game.GameOver();
            }
            else
            {
                bonusScript.invicibilityBonus = false;
            }
            
        }

        
    }
}
