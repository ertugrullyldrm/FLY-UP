using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 5;
    [SerializeField] ParticleSystem Death;
    [SerializeField] GameObject[] Dragon;
    
    
    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
        
    }

    void StartCrashSequence()
    {
        foreach (GameObject Dragon in Dragon)
            {
            Dragon.GetComponent<SkinnedMeshRenderer>().enabled = false;  
            }
        Death.Play();
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Controller>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
        
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }
} 

