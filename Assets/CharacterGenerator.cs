using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    [SerializeField] Texture2D[] baseSpriteSheets;
    [SerializeField] Texture2D[] clothesSpriteSheets;
    [SerializeField] Texture2D[] pantsSpriteSheets;
    [SerializeField] Texture2D[] dressSpriteSheets;
    [SerializeField] Texture2D[] hairSpriteSheets;    
    [SerializeField] SpriteRenderer test;
    Texture2D newCharTexture;
    int spriteWidth = 32;
    int spriteHeight = 32;
    public static CharacterGenerator Instance;
    private void Awake() {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
        return;
    }
    void Update()
    {
        
    }
    
    public Sprite[] CreateRandomCharacter(Direction direction)
    {
        int lineIndex = 0;
        switch (direction)
        {
            case Direction.top: lineIndex = 2;
                break;
            case Direction.bot: lineIndex = 3;
                break;
            default: lineIndex = 1; break;
        }
        newCharTexture = new Texture2D(256,128);
        Texture2D textureToMerge = new Texture2D(256,128,TextureFormat.RGBA32,1,true);
        Graphics.CopyTexture(baseSpriteSheets[GetRandomIndex(baseSpriteSheets)],textureToMerge);
        

        
        MergeTextures(textureToMerge,true);
        
        Graphics.CopyTexture(clothesSpriteSheets[GetRandomIndex(clothesSpriteSheets)],textureToMerge);
        MergeTextures(textureToMerge,true);
        
        Graphics.CopyTexture(pantsSpriteSheets[GetRandomIndex(pantsSpriteSheets)],textureToMerge);
        MergeTextures(textureToMerge);
        
        Graphics.CopyTexture(hairSpriteSheets[GetRandomIndex(hairSpriteSheets)],textureToMerge);
        MergeTextures(textureToMerge);

        newCharTexture.Apply();
        Sprite[] spr = new Sprite[8];
        for (int i = 0; i < 8; i++)
        {
            spr[i] = Sprite.Create(newCharTexture, new Rect(32*i, 32*lineIndex, 32, 32), new Vector2(0.5f, 0), 16, 1, SpriteMeshType.FullRect);            
        }
        return spr;
    }

    private int GetRandomIndex(Texture2D[] array)
    {
        return UnityEngine.Random.Range(0, array.Length);
    }

    private void MergeTextures(Texture2D addedTexture, bool isFirstImage = false)
    {
        Color color;
        for (int x = 0; x < 256; x++)
        {
            for (int y = 0; y < 127; y++)
            {
                color = addedTexture.GetPixel(x, y);
                if (!isFirstImage && color.a == 0) continue;
                newCharTexture.SetPixel(x, y, color);
            }
        }
    }
}
