using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] List<Sprite> sideSprites;
    [SerializeField] List<Sprite> frontSprites;
    [SerializeField] List<Sprite> backSprites;
    [SerializeField] float changeTime = 0.1f;
    Mover mover;
    List<Sprite> actualList;
    float timer;
    SpriteRenderer spriteRenderer;
    int actualSpriteIndex;
    bool mirrowed = false;
    bool isMoving = false;
    private void Awake()
    {
        mover = GetComponent<Mover>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        mover.onMovementTypeChange += ChangeMovementAnimation;
    }
    private void OnDisable()
    {
        mover.onMovementTypeChange -= ChangeMovementAnimation;
    }
    void Start()
    {
        actualList = sideSprites;
    }
    void ChangeMovementAnimation(Direction direction, bool isMoving)
    {
        actualSpriteIndex = 0;
        this.isMoving = isMoving;
        switch (direction)
        {
            case Direction.right:
                actualList = sideSprites;
                mirrowed = false;
                break;
            case Direction.top:
                actualList = backSprites;
                mirrowed = false;
                break;
            case Direction.left:
                actualList = sideSprites;
                mirrowed = true;
                break;
            case Direction.bot:
                actualList = frontSprites;
                mirrowed = false;
                break;
            default:
                break;
        }
    }
    
    void Update()
    {
        spriteRenderer.flipX = mirrowed;
        if (!isMoving)
        {
            spriteRenderer.sprite = actualList[actualSpriteIndex];
            return;
        }
        timer += Time.deltaTime;
        if(timer > changeTime)
        {
            timer = 0;
            //change sprite
            if(++actualSpriteIndex == actualList.Count)
            {
                actualSpriteIndex = 0;
            }
            spriteRenderer.sprite = actualList[actualSpriteIndex];
        }
        
    }
}
