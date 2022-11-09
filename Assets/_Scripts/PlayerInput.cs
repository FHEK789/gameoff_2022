using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Action<Vector2> moveAction;
    public Action interactAction;
    void Update()
    {
        
        
        moveAction?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            interactAction?.Invoke();
        }
    }
}
