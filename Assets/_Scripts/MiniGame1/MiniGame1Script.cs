using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGame1Script : MonoBehaviour
{
    
    [Header("Customizable Variables")]
    [Space(15)]
    [Header("Minigame")]
    [Header("Big slider")]
    [Range(0,20)]
    [SerializeField] float upPushForce = 2f;
    [Header("Point to hit(red dash)")]
    [SerializeField] float changeSpeed = 0.05f;
    [SerializeField] float minChangeTime = 0.5f;
    [SerializeField] float maxChangeTime = 2f;
    [Header("Contest item speed")]
    [SerializeField] float itemSpeed = 1f;
    [Header("Text after duel")]
    [SerializeField] float waitTime = 2f;
    [Space(30)]
    [Header("DONT TOUCH")]
    [SerializeField] Transform playerWinTransform;
    [SerializeField] Transform enemyWinTransform;
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] MiniGame1Slider slider;
    BoxCollider2D sliderCollider;
    [SerializeField] MiniGame1PointToHit point;
    BoxCollider2D pointCollider;
    //TODO RIGHT INPUT
    [Header("TEST")]
    [SerializeField] GameObject item;
    
    //string winner = "not set";
    bool gameEnded = false;
    public void StartFight(GameObject player)
    {
        transform.position = player.transform.position;
        player.GetComponent<Mover>().canMove = false;
        gameObject.SetActive(true);
    }
    private void Start() {
        sliderCollider = slider.GetComponent<BoxCollider2D>();
        pointCollider = point.GetComponent<BoxCollider2D>();

        Initialization();
    }
    private void Initialization()
    {
        //slider
        slider.Init(upPushForce);
        //pointhit
        point.Init(changeSpeed, minChangeTime, maxChangeTime);
    }
    void Update()
    {        
        if(gameEnded){
            if(waitTime > 0){
                waitTime -= Time.deltaTime;
            }
            else{
                textMeshPro.gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Mover>().canMove = true;
                transform.parent.gameObject.SetActive(false);
            }
        }
        else{
            if(item.transform.position.x > enemyWinTransform.position.x){
                Debug.Log("enemy win");
            }
            else if(item.transform.position.x < playerWinTransform.position.x){
                Debug.Log("player win");
            }
            DuelLogic();
        }
        
    }
    private void DuelLogic(){
        //move to the player
        if(Physics2D.IsTouching(sliderCollider, pointCollider)){
            
            item.transform.position = new Vector3(
                    item.transform.position.x - itemSpeed * Time.deltaTime, 
                    item.transform.position.y, 
                    item.transform.position.z);
        }
        //move away
        else{
            item.transform.position = new Vector3(
                    item.transform.position.x + itemSpeed * Time.deltaTime, 
                    item.transform.position.y, 
                    item.transform.position.z);
        }
    }
    
}
