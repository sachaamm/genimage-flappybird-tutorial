using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script qui permet a la camera de suivre sur x la position de l'oiseau
public class FollowBird : MonoBehaviour
{
    // definir une reference vers bird
    public GameObject bird;

    void Start()
    {
        
    }

    void Update()
    {
        // la position x de la camera soit 
        // la meme que la position x de bird

        // transform est un mot cle qui correspond
        // au composant transform du GameObject 
        // sur lequel est assigne le script
        
        // on a recupere la position de la camera
        Vector3 camPos = transform.position;

        // on va assigne la valeur x de la position de la camera
        // pour la faire correspondre a la position x de bird
        camPos.x = bird.transform.position.x;

        // mettre a jour la position de la camera
        transform.position = camPos;

    }
}
