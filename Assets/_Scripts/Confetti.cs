using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    public Transform spawnPosition;
    public GameObject confettiPrefab;
    public int confettiSpawned = 0;
    public GameObject confettiCylinder;
    public GameFlow flow;
    private GameManager gm;
    public Material[] materials;
    [SerializeField]private GameObject prefabToSpawn;
    void Start()
    {
        RevertConfettiColor();

    }
    public void SpawnConfetti()
    {


        if (confettiSpawned < 200)
        {

            Instantiate(prefabToSpawn, spawnPosition.position + new Vector3(Random.Range(-0.125f, 0.125f), 0, Random.Range(-0.09f, 0.09f)), Quaternion.Euler(Random.Range(0,180), Random.Range(0,180), Quaternion.identity.z));
            if(prefabToSpawn.name != "Cockroach")
            confettiSpawned++;
            else
            confettiSpawned+=5;
        }
        if (confettiSpawned >= 200  )
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            print("WHY THE FUCK NOT");
            if (flow.confettiPhase && !flow.confettiPhase2)
            {

                flow.EndConfettiPhase();
                confettiSpawned = 0;
            }
            else if (!flow.confettiPhase && flow.confettiPhase2 && !flow.cardPhase)
            {
                print("phase2");
                confettiSpawned = 0;
                transform.parent.gameObject.SetActive(false);
                flow.EndConfettiPhaseTwo();

            }

        }

    }
    public void ConfettiColor(int a)
    {
        confettiPrefab.GetComponentInChildren<Renderer>().material = materials[a];
        confettiCylinder.GetComponent<Renderer>().material = materials[a];

    }
    public void RevertConfettiColor()
    {
        confettiPrefab.GetComponentInChildren<Renderer>().material = materials[0];
        confettiCylinder.GetComponent<Renderer>().material = materials[0];
    }
    public void PrefabToSpawn(GameObject prefab)
    {

            prefabToSpawn = prefab;

    }
}
