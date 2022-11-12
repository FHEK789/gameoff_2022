using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShelfState
{
    idle,
    interact,    
    restock,
    stop
}
public class ShelfLogic : MonoBehaviour
{   
    Action<bool> callbackAction;
    [SerializeField] float timeToRestock = 5f;
    [Header("Battle is checked vs random float 0-1")]
    [Range(0,1)]
    [SerializeField] float fightChance = 0.25f;
    [Header("HANDS AWAY!")]
    [SerializeField] Sprite[] timerSprites;
    [SerializeField] SpriteRenderer child;    
    bool hasItem = true;
    ShelfState state = ShelfState.idle;
    Item item;
    float timer;
    private void OnEnable() {
        callbackAction += MinigameEnd;
    }
    private void OnDisable() {
        callbackAction -= MinigameEnd;
    }
    void Start()
    {
        child.sprite = item.sprite;        
    }

    
    void Update()
    {
        switch (state)
        {
            case(ShelfState.idle): IdleBehaviour(); break;
            case(ShelfState.interact): InteractBehaviour(); break;
            case(ShelfState.restock): RestockBehaviour(); break;
            default:break;
        }
    }

    private void RestockBehaviour()
    {
        timer += Time.deltaTime;
        ShowRightCooldownSprite();
        if(timer > timeToRestock){            
            child.sprite = item.sprite;
            hasItem = true;
            state = ShelfState.idle;
            timer = 0;
        }
    }

    private void ShowRightCooldownSprite()
    {
        
        float resolution = timeToRestock / (timerSprites.Length - 1);
        int imageIndex = (int)(timer / resolution); 
        imageIndex = Mathf.Clamp(imageIndex, 0, timerSprites.Length - 1);
        child.sprite = timerSprites[imageIndex];
    }

    private void InteractBehaviour()
    {
        //if you can pick an item    
        if (!FakeDatabase.Instance.CanPick(item))
        {
            timer = 0;
            state = ShelfState.idle;
            return;
        }
        float rnd = UnityEngine.Random.Range(0f,1f);
        Debug.Log("random number is: "+rnd+" chcecket vs: "+fightChance);
        if(rnd < fightChance){
            //fight
            MiniGame1Script.Instance.StartFight(item, callbackAction);
            state = ShelfState.stop;
            return;
        }
        else{
            //just take
            MinigameEnd(true);
        }
                
        
    }

    private void MinigameEnd(bool isWinner)
    {
        if(isWinner){
            FakeDatabase.Instance.AddToPlayerList(item);
        }        
        state = ShelfState.restock;
        hasItem = false;
        timer = 0;
        child.sprite = timerSprites[0];
    }

    private void IdleBehaviour()
    {
        if(hasItem) return;
    }
    public void Interact(){
        if(hasItem) state = ShelfState.interact;
    }
    public void Initialization(Item item){
        this.item = item;
    }
}
