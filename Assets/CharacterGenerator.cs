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
    
    public Texture2D CreateRandomCharacter()
    {        
        newCharTexture = new Texture2D(256,128);
        Texture2D textureToMerge = new Texture2D(256,128,TextureFormat.RGBA32,1,true);
        Graphics.CopyTexture(baseSpriteSheets[GetRandomIndex(baseSpriteSheets)],textureToMerge);
        

        
        MergeTextures(textureToMerge,true);
        
        Graphics.CopyTexture(clothesSpriteSheets[GetRandomIndex(clothesSpriteSheets)],textureToMerge);
        MergeTextures(textureToMerge);
        
        Graphics.CopyTexture(pantsSpriteSheets[GetRandomIndex(pantsSpriteSheets)],textureToMerge);
        MergeTextures(textureToMerge);
        
        Graphics.CopyTexture(hairSpriteSheets[GetRandomIndex(hairSpriteSheets)],textureToMerge);
        MergeTextures(textureToMerge);

        newCharTexture.Apply();
        
        return newCharTexture;
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
            for (int y = 0; y < 128; y++)
            {
                color = addedTexture.GetPixel(x, y);
                if (!isFirstImage && color.a == 0) continue;
                newCharTexture.SetPixel(x, y, color);
            }
        }
    }
}
