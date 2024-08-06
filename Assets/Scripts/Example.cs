using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{

    public static GAMESTATE gameState = GAMESTATE.Game;
    void Awake()
     {
         Debug.Log("Awake");
     }
     void OnEnable()
     {
         Debug.Log("OnEnabled");
     }
     void Start()
     {
         Debug.Log("Start");
     }
    void Update()
    {
        Debug.Log("Update: ");// +Time.deltaTime);
    }
    void LateUpdate()
    {
        Debug.Log("LateUpdate: ");// + Time.deltaTime);
    }
    void FixedUpdate()
    {
        Debug.Log("FixedUpdate: ");// + Time.deltaTime);
    }
    void OnDisable()
    {
        Debug.Log("OnDisable");
    }
    void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }
}
