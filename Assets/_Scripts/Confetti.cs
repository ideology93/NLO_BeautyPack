using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    public Transform spawnPosition;
    public GameObject confettiPrefab;
    public int confettiSpawned = 0;
    public GameFlow flow;

    public void SpawnConfetti()
    {


        if (confettiSpawned < 200)
        {
            Instantiate(confettiPrefab, spawnPosition.position + new Vector3(Random.Range(-0.35f, 0.35f), 0, Random.Range(-0.20f, 0.20f)), Quaternion.identity);
            confettiSpawned++;
        }
        if (confettiSpawned >= 200)
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
