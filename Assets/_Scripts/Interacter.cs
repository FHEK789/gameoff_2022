using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float interactRadius = 1f;
    [SerializeField] LayerMask interactLayerMask;
    PlayerInput playerInput;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    private void OnEnable()
    {
        playerInput.interactAction += Interact;
    }
    private void OnDisable()
    {
        playerInput.interactAction -= Interact;
    }
    void Interact()
    {
        //key was pressed, check collision
        if(Physics2D.CircleCast(transform.position, interactRadius, Vector2.zero, 0, interactLayerMask))
        {
            Debug.Log("interacting!");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }

}
