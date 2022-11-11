using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float interactTime = 2f;
    [SerializeField] float interactRadius = 1f;
    [SerializeField] LayerMask interactLayerMask;
    [SerializeField] Sprite[] interactSprites;
    [SerializeField] SpriteRenderer interactGUI;
    float timer;
    bool isPicking = false;
    PlayerInput playerInput;
    
    private void Awake()
    {
        
        playerInput = GetComponent<PlayerInput>();
    }
    private void OnEnable()
    {
        playerInput.interactAction += InteractKeyPressed;
    }
    private void OnDisable()
    {
        playerInput.interactAction -= InteractKeyPressed;
    }
    private void Update() {
        if(!isPicking) return;
        timer += Time.deltaTime;
        ShowInteractGUI();
        if(timer > interactTime){
            Interact();
            isPicking = false;
            playerInput.DisableMovement(false); 
            interactGUI.transform.gameObject.SetActive(false);
        }

    }

    private void ShowInteractGUI()
    {
        float resolution = interactTime / (interactSprites.Length - 1);
        int imageIndex = (int)(timer / resolution); 
        imageIndex = Mathf.Clamp(imageIndex, 0, interactSprites.Length - 1);
        interactGUI.sprite = interactSprites[imageIndex];
    }

    private void Interact()
    {
        //key was pressed, time was pass
        var col = Physics2D.CircleCast(transform.position, interactRadius, Vector2.zero, 0, interactLayerMask);
        if(col)
        {
            if(col.transform.TryGetComponent<ShelfLogic>(out ShelfLogic shelf)){
                shelf.Interact();
            }            
        }
    }

    void InteractKeyPressed(bool interactActive)
    {
        
        timer = 0;
        if(!interactActive){            
            interactGUI.transform.gameObject.SetActive(interactActive);
            playerInput.DisableMovement(interactActive);  
            isPicking = false;        
        }
        else{
            if(Physics2D.CircleCast(transform.position, interactRadius, Vector2.zero, 0, interactLayerMask)){
                isPicking = true;
                interactGUI.transform.gameObject.SetActive(interactActive);
                playerInput.DisableMovement(interactActive);
            }
            
        }
        
        
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }

}
