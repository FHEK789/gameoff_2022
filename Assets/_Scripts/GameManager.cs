using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    
    
    [Header("Max size is Items list size in FakeDatabase")]
    [SerializeField] int numberOfItemsToFind = 3;
    FakeDatabase fakeDatabase;
    [SerializeField] MiniGame1Script minigame;
    void Start()
    {
        fakeDatabase = GetComponent<FakeDatabase>();
        //find all shelfs
        ShelfLogic[] shelfs = FindObjectsOfType<ShelfLogic>();
        //list to take random items
        List<Item> list = new List<Item>(fakeDatabase.items);
        //goal list
        List<Item> goalList = fakeDatabase.goalList;
        //random items            
        for (int i = 0; i < numberOfItemsToFind; i++)
        {
            int rndIndex = Random.Range(0, list.Count);
            for (int j = 0; j < list.Count; j++)
            {
                //add to find list 
                if(j == rndIndex) {
                    fakeDatabase.AddToGoalList(list[j]);
                    list.RemoveAt(j);
                    break;
                }

            }
        }
        //random shelfs + init item        
        List<ShelfLogic> shelfList = new List<ShelfLogic>(FindObjectsOfType<ShelfLogic>());
        for (int i = 0; i < numberOfItemsToFind; i++)
        {
            int rndIndex = Random.Range(0, shelfList.Count);
            for (int j = 0; j < shelfList.Count; j++)
            {
                //add to find list 
                if(j == rndIndex) {
                    //init actual shelf
                    shelfList[j].Initialization(goalList[i]);
                    shelfList.RemoveAt(j);
                    break;
                }

            }
        }
        //random other items for all remaining shelfs
        foreach (ShelfLogic shelf in shelfList)
        {
            int rnd = Random.Range(0, list.Count);
            shelf.Initialization(list[rnd]);
        }
        //create minigame
        Instantiate(minigame);
    }
}
