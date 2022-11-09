using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] List<Sprite> sideSprites;
    [SerializeField] List<Sprite> frontSprites;
    [SerializeField] List<Sprite> backSprites;
    [SerializeField] float changeTime = 0.1f;
    List<Sprite> actualList;
    float timer;
    SpriteRenderer spriteRenderer;
    int actualSprite;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        actualList = sideSprites;
    }

    
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > changeTime)
        {
            timer = 0;
            //change sprite
            if(++actualSprite == actualList.Count)
            {
                actualSprite = 0;
            }
            spriteRenderer.sprite = actualList[actualSprite];
        }
        
    }
}
