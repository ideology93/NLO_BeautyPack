using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    public Transform spawnPosition;
    public GameObject confettiPrefab;
    public int confettiSpawned = 0;
    public GameFlow flow;
    void Start()
    {

    }
    void FixedUpdate()
    {

        // if (flow.confettiPhase)
        // {
        //     if (Input.GetMouseButton(0))
        //     {
        //         if (confettiSpawned < 100)
        //         {
        //             Instantiate(confettiPrefab, spawnPosition.position + new Vector3(Random.Range(-0.35f, 0.35f), 0, Random.Range(-0.20f, 0.20f)), Quaternion.identity);
        //             confettiSpawned++;
        //         }
        //         if (confettiSpawned >= 100)
        //         {
        //             gameObject.SetActive(false);
        //             flow.EndConfettiPhase();

        //         }
        //     }

        // }
        // if (Input.touchCount == 1)
        //     {
        //         Touch screenTouch = Input.GetTouch(0);

        //         if (screenTouch.phase == TouchPhase.Moved)
        //         {
        //             transform.parent.Rotate(0f, screenTouch.deltaPosition.x, 0f);
        //         }

        //     }
    }
    public void SpawnConfetti()
    {


        if (confettiSpawned < 150)
        {
            Instantiate(confettiPrefab, spawnPosition.position + new Vector3(Random.Range(-0.35f, 0.35f), 0, Random.Range(-0.20f, 0.20f)), Quaternion.identity);
            confettiSpawned++;
        }
        if (confettiSpawned >= 150)
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            if (flow.confettiPhase && !flow.confettiPhase2)
            {
                print("phase1");
                flow.EndConfettiPhase();
                confettiSpawned = 0;
            }
            else if (!flow.confettiPhase && flow.confettiPhase2)
            {
                print("phase2");
                confettiSpawned = 0;
                flow.EndConfettiPhaseTwo();
                
            }



        }

    }
}
