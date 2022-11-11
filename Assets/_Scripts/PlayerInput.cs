using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] KeyCode interactKeay;
    public Action<Vector2> moveAction;
    public Action<bool> interactAction;
    Vector2 lastDir = Vector2.zero;
    bool canMove = true;
    void Update()
    {
        
        Vector2 newDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if(!canMove) newDir = Vector2.zero;
        if(newDir != lastDir){
            moveAction?.Invoke(newDir);
            lastDir = newDir;
        }
        
        
        if (Input.GetKeyDown(interactKeay))
        {
            interactAction?.Invoke(true);
        }
        if (Input.GetKeyUp(interactKeay))
        {
            interactAction?.Invoke(false);
        }
    }
    public void DisableMovement(bool disable){
        canMove = !disable;
    }
}
