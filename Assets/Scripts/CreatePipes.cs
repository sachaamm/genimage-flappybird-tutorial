using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script de generation des tuyaux
public class CreatePipes : MonoBehaviour
{
    // on enregistre la position x a atteindre
    // pour creer le prochain tuyau
    float nextStepToCreateNewPipe = -intervalleXEntreDeuxTuyaux;

    // variable qui correspond a lintervalle de creation
    // entre 2 tuyaux
    public static float intervalleXEntreDeuxTuyaux = 5;

    public GameObject pipePrefab, coinPrefab;
 
    int intervalleYEntreDeuxTuyaux = 3; // espacement entre le tuyau du haut et le tuyau du bas
    int screenHeight = 10; // hauteur de l'ecran : de -5 a +5 : 10
    int sizeMin = 2; // hauteur minimale des tuyaux

    // le prefab de la potion 
    public GameObject potionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // on determine la valeur initiale de nextStepToCreateNewPipe
        Vector2 birdPos = transform.position;

        nextStepToCreateNewPipe = birdPos.x +
            intervalleXEntreDeuxTuyaux;       
    }

    // Quand on relance une partie on doit reinitialiser la valeur de nextStepToCreateNewPipe
    public void ResetNextStepToCreateNewPipe()
    {
        nextStepToCreateNewPipe = -intervalleXEntreDeuxTuyaux;
    }

    // Update is called once per frame
    void Update()
    {
        // quand bird atteint une position x > nextStepToCreateNewPipe, on cree le tuyau suivant
        Vector2 birdPos = transform.position;

        if(birdPos.x > nextStepToCreateNewPipe)
        {
            // Creation aleatoire hypothetique d'une potion bonus
            int chanceToCreatePotion = Random.Range(0, 10);

            if(chanceToCreatePotion > 8)
            {
                GameObject newPotion = Instantiate(potionPrefab);
                newPotion.transform.position = new Vector3(
                    nextStepToCreateNewPipe + intervalleXEntreDeuxTuyaux
                    + intervalleXEntreDeuxTuyaux / 2,
                    Random.Range(-4, 4), 0
                    );
            }

            // Creation aleatoire hypothetique d'une piece de monnaie
            int chanceToCreateCoin = Random.Range(0, 10);

            if (chanceToCreateCoin > 8)
            {
                GameObject newCoin = Instantiate(coinPrefab);
                newCoin.transform.position = new Vector3(
                    nextStepToCreateNewPipe + intervalleXEntreDeuxTuyaux
                    + intervalleXEntreDeuxTuyaux / 2,
                    Random.Range(-4, 4), 0
                    );
            }

            // Creation du tuyau du bas 
            GameObject newPipe = Instantiate(pipePrefab);
            newPipe.transform.position =
                new Vector2(
                    nextStepToCreateNewPipe + intervalleXEntreDeuxTuyaux, 0)
                ;

            // On recupere les enfants ( les squares ) du pipe
            GameObject pipeTop = 
                newPipe.transform.GetChild(0).gameObject;

            GameObject pipeBottom =
                newPipe.transform.GetChild(1).gameObject;

            // on definit la taille de PipeBottom en y ( sa hauteur )
            float randomSize = Random.Range(sizeMin, screenHeight - intervalleYEntreDeuxTuyaux);
            float randomSizeFromTop = (screenHeight - intervalleYEntreDeuxTuyaux) - randomSize;

            // on met a jour la taille de pipeBottom
            Vector3 localScalePipeBottom = 
                pipeBottom.transform.localScale;

            // je met a jour la dimension de pipeBottom
            pipeBottom.transform.localScale = new Vector3(
                localScalePipeBottom.x, 
                randomSize, 
                localScalePipeBottom.z);

            // je dois adapter la position de pipeTop
            Vector3 pipeTopPos = pipeTop.transform.position;
            pipeTopPos = new Vector3(pipeTopPos.x,
                randomSize / 2, pipeTopPos.z);
            pipeTop.transform.position = pipeTopPos;

            // on doit adapter la position de pipe
            Vector3 newPipePos = newPipe.transform.position;
            newPipePos = new Vector3(newPipePos.x,
                -((5 - randomSize / 2)), newPipePos.z);
            newPipe.transform.position = newPipePos;


            // Creation du tuyau du bas
            GameObject newPipeFromTop = Instantiate(pipePrefab);
            newPipeFromTop.transform.position =
                new Vector2(
                    nextStepToCreateNewPipe + intervalleXEntreDeuxTuyaux, 0)
                ;

            // On recupere les enfants ( les squares ) du pipe
            GameObject pipeTopFromTop =
                newPipeFromTop.transform.GetChild(0).gameObject;

            GameObject pipeBottomFromTop =
                newPipeFromTop.transform.GetChild(1).gameObject;

            // on met a jour la taille de pipeBottom
            Vector3 localScalePipeBottomFromTop =
                pipeBottomFromTop.transform.localScale;

            // je met a jour la dimension de pipeBottom
            pipeBottomFromTop.transform.localScale = new Vector3(
                localScalePipeBottomFromTop.x,
                randomSizeFromTop,
                localScalePipeBottomFromTop.z);

            // je dois adapter la position de pipeTop
            Vector3 pipeTopPosFromTop = pipeTopFromTop.transform.position;
            pipeTopPosFromTop = new Vector3(pipeTopPosFromTop.x,
                -(randomSizeFromTop / 2), pipeTopPosFromTop.z);
            pipeTopFromTop.transform.position = pipeTopPosFromTop;

            // on doit adapter la position de pipe
            Vector3 newPipePosFromTop = newPipeFromTop.transform.position;
            newPipePosFromTop = new Vector3(newPipePosFromTop.x,
                ((5 - randomSizeFromTop / 2)), newPipePosFromTop.z);
            newPipeFromTop.transform.position = newPipePosFromTop;

            // on augmente la valeur de nextStepToCreateNewPipe
            // pour mettre a jour la prochaine etape de
            // creation d'un nouveau tuyau
            nextStepToCreateNewPipe += intervalleXEntreDeuxTuyaux;
        }

    }
}
