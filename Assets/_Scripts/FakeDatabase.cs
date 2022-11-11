using UnityEngine;

using System;
using System.Collections.Generic;

[Serializable]
public struct Item{
    public Sprite sprite;
    public string name;
}
public class FakeDatabase : MonoBehaviour {
    public static FakeDatabase Instance;
    public List<Item> items;
    public List<Item> goalList = new();
    public List<Item> playerList = new();
    private void Awake() {
        if(Instance == null){
            Instance = this;
            return;
        }
        Destroy(this);
        return;
    }
    private void Start() {
        GenerateGoalList();
    }
    void GenerateGoalList(){
        goalList.Clear();
        playerList.Clear();
    }
    public bool CanPick(Item item){
        return goalList.Contains(item) && !playerList.Contains(item);
    }
    public void AddToPlayerList(Item item){
        playerList.Add(item);
        //check if player have all items
        if(CheckCompleteItemList()){
            Debug.Log("complete!!!!");
        }

    }

    private bool CheckCompleteItemList()
    {
        foreach (var item in goalList)
        {
            if(!playerList.Contains(item)) return false;
        }
        return true;
    }

    public void AddToGoalList(Item item){
        goalList.Add(item);
    }
}