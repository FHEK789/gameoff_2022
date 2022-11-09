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
    [SerializeField] float brakingSpeed = 0f;
    [HideInInspector] public bool canMove = true;

    public Action<Direction, bool> onMovementTypeChange;

    Vector2 moveDir = Vector2.zero;
    Vector2 lastMoveDir = Vector2.zero;
    bool isMoving;
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
        rb2d.drag = brakingSpeed;
    }

    void Move(Vector2 moveVector)
    {
        moveDir = moveVector;
        Vector2 compareVectors = new Vector2(
                MathF.Ceiling(moveDir.x), MathF.Ceiling(moveDir.y));
        Debug.Log(compareVectors);
        if(lastMoveDir != compareVectors)
        {
            
            //is moving?
            if (compareVectors == Vector2.zero)
            {
                isMoving = false;
                onMovementTypeChange?.Invoke(ChooseDirection(), false);
                lastMoveDir = moveDir;
            }
            else
            {
                isMoving = false;
                lastMoveDir = moveDir;            
                onMovementTypeChange?.Invoke(ChooseDirection(), true);
            }

            
        }

    }

    private Direction ChooseDirection()
    {
        if (lastMoveDir.y > 0.9f)
        {
            return Direction.top;
        }
        else if (lastMoveDir.y < -0.9f)
        {
            return Direction.bot;
        }
        else if (lastMoveDir.x > 0.9f)
        {
            return Direction.right;
        }
        else if (lastMoveDir.x < -0.9f)
        {
            return Direction.left;
        }
        else return Direction.bot;
        
    }

    /*
void Update()
{        
   if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
       moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
       lastMoveDir = moveDir;            
       isMoving = true;
   }
   else{
       moveDir = Vector2.zero;
       isMoving = false;
   }
   if(!canMove) {
       moveDir = Vector2.zero;
       isMoving = false;
   }
   animator.SetBool("isMoving", isMoving);
   animator.SetFloat("xValue", lastMoveDir.x);
   animator.SetFloat("yValue", lastMoveDir.y);
}
*/
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
