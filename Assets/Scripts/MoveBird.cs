using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script qui permet de deplacer l'oiseau
public class MoveBird : MonoBehaviour
{
    // Declaration des variables 
    // On va declarer les objets ( variables ) qu'on va utiliser dans notre script
    Rigidbody2D rigidbody2d;
    float jumpPower = 300;

    static float initMoveSpeed = 5000f;
    float moveSpeed = initMoveSpeed;
    float gravitySpeed = 1f;

    float acceleration = 0.01f;

    AudioSource audioSource;

    // Start est appellee au lancement du jeu
    void Start()
    {
        // On doit definir les objets qu'on a declare 
        rigidbody2d = GetComponent<Rigidbody2D>();

        audioSource = GetComponentInChildren<AudioSource>();
    }
    public void ResetMoveSpeed()
    {
        moveSpeed = initMoveSpeed;
    }

    // Update est appellee sur toutes les frames du jeu
    void Update()
    {
        // si la touche espace est appuyee 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // on applique une force au rigidbody
            rigidbody2d.AddForce(new Vector2(0,1) * jumpPower);

            audioSource.Play();
        }

        // on deplace l'oiseau avec le rigidbody vers la droite
        rigidbody2d.AddForce(new Vector2(1,0) * (moveSpeed * Time.deltaTime));

        rigidbody2d.AddForce(new Vector2(0, -1) * gravitySpeed);

        moveSpeed += acceleration;

        // Vector2 nextPos = 
        //    new Vector2(transform.position.x, transform.position.y)
        //    + new Vector2(1, 0) * moveSpeed;

        rigidbody2d.velocity =
            new Vector2(rigidbody2d.velocity.x / 2,
            rigidbody2d.velocity.y);

        // rigidbody.MovePosition(nextPos);
    }
}
