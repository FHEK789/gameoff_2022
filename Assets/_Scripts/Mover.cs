using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Direction
{
    right, top, left, bot
}
public class Mover : MonoBehaviour
{
    [Header("Using physics")]
    [SerializeField] float moveSpeed = 2f;    
    [HideInInspector] public bool canMove = true;

    public Action<Direction, bool> onMovementTypeChange;

    Vector2 moveDir = Vector2.zero;
    Vector2 lastMoveDir = Vector2.zero;    
    Animator animator;
    Rigidbody2D rb2d;
    PlayerInput playerInput;
    private void OnEnable()
    {
        playerInput.moveAction += Move;
    }
    private void OnDisable()
    {
        playerInput.moveAction -= Move;
    }
    private void Awake() {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }
    void Start()
    {
        
    }

    void Move(Vector2 moveVector)
    {
        moveDir = moveVector;
            if (moveVector == Vector2.zero){
                onMovementTypeChange?.Invoke(ChooseDirection(), false);
                lastMoveDir = moveDir;
            }
            else{                
                lastMoveDir = moveDir;
                onMovementTypeChange?.Invoke(ChooseDirection(), true);
            }
    }

    private Direction ChooseDirection()
    {
        if (lastMoveDir.y > 0.2f)
        {
            return Direction.top;
        }
        else if (lastMoveDir.y < -0.2f)
        {
            return Direction.bot;
        }
        else if (lastMoveDir.x > 0.2f)
        {
            return Direction.right;
        }
        else if (lastMoveDir.x < -0.2f)
        {
            return Direction.left;
        }
        else return Direction.bot;
        
    }
    private void FixedUpdate() {
        if(canMove)        
        rb2d.velocity = moveDir * moveSpeed;
        
    }    
    public void DisableControl(float time){
        if(!canMove) return;
        canMove = false;
        Invoke("EnableControl", time);
    }
    void EnableControl(){
        canMove = true;
    }
}
