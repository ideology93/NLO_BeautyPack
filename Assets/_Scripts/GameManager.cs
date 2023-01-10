using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private GameFlow flow;
    public GameObject settingsUI;
    public Material[] materials;
    public GameObject confetiPrefab;
    public GameObject confettiCylinder;
    public static GameManager Instance


    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is null");
            return _instance;
        }
    }
    private void Awake()
    {

        _instance = this;


    }
    private void Start()
    {
        flow = GetComponent<GameFlow>();
        
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void StartMenu(GameObject ui)
    {
        settingsUI.SetActive(!settingsUI.activeSelf);

    }
    public void Resume()
    {

        settingsUI.SetActive(false);
    }
 



}
