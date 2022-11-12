using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

[Serializable]
public struct Item{
    public Sprite sprite;
    public string name;
}
public class FakeDatabase : MonoBehaviour {
    Action onListChange;
    [SerializeField] GameObject listObject;
    [SerializeField] GameObject itemPrefab;
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
    private void OnEnable() {
        onListChange += RedrawList;
    }
    private void OnDisable() {
        onListChange -= RedrawList;
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
        onListChange?.Invoke();
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
        onListChange?.Invoke();
    }
    void RedrawList(){
        //clear
        foreach (Transform transform in listObject.transform)
        {
            Destroy(transform.gameObject);
        }
        //draw
        foreach (var item in goalList)
        {
            GameObject go = Instantiate(itemPrefab, listObject.transform);
            go.GetComponent<Image>().sprite = item.sprite;
            if(playerList.Contains(item)) go.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
            else go.GetComponentInChildren<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
            go.GetComponentInChildren<TextMeshProUGUI>().text = item.name;
        }
    }
}