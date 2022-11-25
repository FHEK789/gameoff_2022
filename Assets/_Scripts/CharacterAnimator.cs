using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    Sprite[] sideSprites;
    Sprite[] frontSprites;
    Sprite[] backSprites;
    Texture2D charTextureSheet;
    [SerializeField] float changeTime = 0.1f;
    Mover mover;
    Sprite[] actualList;
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
        CreateCharacterSprites();

        actualList = sideSprites;
    }

    private void CreateCharacterSprites()
    {
        charTextureSheet = null;            
        int spritewidth = 32;
        charTextureSheet = CharacterGenerator.Instance.CreateRandomCharacter();
        int numberOfSprites = charTextureSheet.width / spritewidth;
        if(sideSprites == null){
            sideSprites = new Sprite[numberOfSprites];
            frontSprites = new Sprite[numberOfSprites];
            backSprites = new Sprite[numberOfSprites];
        }
        

        for (int i = 0; i < numberOfSprites; i++)
        {
            sideSprites[i] = Sprite.Create(charTextureSheet, new Rect(i * 32, 32, 32, 32), new Vector2(0.5f, 0), 16, 1, SpriteMeshType.FullRect);
            frontSprites[i] = Sprite.Create(charTextureSheet, new Rect(i * 32, 96, 32, 32), new Vector2(0.5f, 0), 16, 1, SpriteMeshType.FullRect);
            backSprites[i] = Sprite.Create(charTextureSheet, new Rect(i * 32, 64, 32, 32), new Vector2(0.5f, 0), 16, 1, SpriteMeshType.FullRect);
        }
        
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
        if(Input.GetKeyDown(KeyCode.J)){
            CreateCharacterSprites();
            spriteRenderer.sprite = actualList[actualSpriteIndex];
        }
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
            if(++actualSpriteIndex == actualList.Length)
            {
                actualSpriteIndex = 0;
            }
            spriteRenderer.sprite = actualList[actualSpriteIndex];
        }
        
    }
}
