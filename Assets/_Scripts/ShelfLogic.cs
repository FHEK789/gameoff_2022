using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfLogic : MonoBehaviour
{
    [SerializeField] Sprite item;
    [SerializeField] SpriteRenderer child;
    ParticleSystem myParticleSystem;
    
    void Start()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
        child.sprite = item;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        myParticleSystem.Play();
    }
}
