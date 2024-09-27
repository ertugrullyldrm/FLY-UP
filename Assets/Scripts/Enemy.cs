using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] int scorePerHit = 10;
    [SerializeField] int scorePerKill = 20;
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int hitPoints = 20; 
    Rigidbody _rigidbody;
    GameObject parentGameObject;
    AudioSource _audiosource;
    bool killSequence = false;
    
    
    

    ScoreBoard scoreBoard;

     
    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidbody();
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        KillEnemy();
        AdjustScore();
    }

    void ProcessHit()
    {
        hitPoints = hitPoints - 1; // or hitPoints--;
        GameObject Hvfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        Hvfx.transform.parent = parentGameObject.transform;
        
        
        

    }

    void KillEnemy()
    {
        if (hitPoints < 1)
        {
        killSequence = true;
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;
        Destroy(this.gameObject); 
        }
        else {return;}
    }  
    
      void AdjustScore()
    {
        AdjustScorePerHit();
        if (killSequence == true)
        {
            AdjustScorePerKill();
        }
        else {return;}
    }

    void AdjustScorePerHit()
    {
        scoreBoard.IncreaseScore(scorePerHit);
    }

    void AdjustScorePerKill()
      {
        scoreBoard.IncreaseScore(scorePerKill);
      }
    void AddRigidbody()
    {
        _rigidbody = gameObject.AddComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }
}
