using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script de detection des bonus
public class BonusScript : MonoBehaviour
{
    public bool invicibilityBonus = false;

    public GameObject invicibilitySprite;

    UpdateScore updateScore;

    void Start()
    {
        updateScore = Camera.main.GetComponent<UpdateScore>();
    }

    void Update()
    {
        if(invicibilityBonus)
        {
            // activer des effets visuels pour indiquer l'invincibilite
            invicibilitySprite.SetActive(true);
        }
        else
        {
            invicibilitySprite.SetActive(false);
        }
    }

    // quand je rentre en collision avec un bonus
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bonus")
        {
            GameObject potion = collision.gameObject;
            Destroy(potion);

            invicibilityBonus = true;
        }

        if (collision.gameObject.tag == "Coin")
        {
            GameObject coin = collision.gameObject;
            Destroy(coin);

            updateScore.bonusScore += 5;
        }
    }
}
