using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Using physics, so breaking and moving speed must be set right and tuned!")]
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float brakingSpeed = 5f;
    [HideInInspector] public bool canMove = true;
    Vector2 moveDir = Vector2.zero;
    Vector2 lastMoveDir = Vector2.zero;
    bool isMoving;
    Animator animator;
    Rigidbody2D rb2d;
    private void Awake() {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb2d.drag = brakingSpeed;
    }

    
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
