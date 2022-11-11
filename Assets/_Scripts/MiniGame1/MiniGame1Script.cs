using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniGame1Script : MonoBehaviour
{
    public static MiniGame1Script Instance;
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
    [SerializeField] GameObject itemObject;
    Item item;
    Action action;
    bool gameEnded = false;
    float timer;
    private void Awake() {
        if(Instance == null){
            Instance = this;
            return;
        }
        Destroy(this);
    }
    public void StartFight(Item item, Action action)
    {
        this.action = action;
        this.item = item;        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
        ResetGame();
        player.GetComponent<Mover>().canMove = false;        
        gameObject.SetActive(true);
    }

    private void ResetGame()
    {
        timer = waitTime;
        //state
        gameEnded = false;
        //positions
        itemObject.transform.position = new Vector3(transform.position.x, itemObject.transform.position.y, itemObject.transform.position.z);


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
        gameObject.SetActive(false);
    }
    void Update()
    {        
        if(gameEnded){            
            if(timer > 0){
                timer -= Time.deltaTime;
            }
            else{
                textMeshPro.gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Mover>().canMove = true;
                gameObject.SetActive(false);
            }
        }
        else{
            if(itemObject.transform.position.x > enemyWinTransform.position.x){
                Debug.Log("enemy win");
                gameEnded = true;

            }
            else if(itemObject.transform.position.x < playerWinTransform.position.x){
                Debug.Log("player win");
                gameEnded = true;
                action();
                return;
            }
            DuelLogic();
        }
        
    }
    private void DuelLogic(){
        //move to the player        
        if(Physics2D.IsTouching(sliderCollider, pointCollider)){
            
            itemObject.transform.position = new Vector3(
                    itemObject.transform.position.x - itemSpeed * Time.deltaTime, 
                    itemObject.transform.position.y, 
                    itemObject.transform.position.z);
        }
        //move away
        else{
            itemObject.transform.position = new Vector3(
                    itemObject.transform.position.x + itemSpeed * Time.deltaTime, 
                    itemObject.transform.position.y, 
                    itemObject.transform.position.z);
        }
    }
    
}
