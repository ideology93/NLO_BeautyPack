using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject confetti;

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Instantiate(confetti, spawnPos.position, Quaternion.identity);
          
        }
    }
}
