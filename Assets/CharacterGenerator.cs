using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    [SerializeField] Texture2D[] baseSpriteSheets;
    [SerializeField] Texture2D[] clothesSpriteSheets;
    [SerializeField] Texture2D[] hairSpriteSheets;    
    [SerializeField] SpriteRenderer test;
    Texture2D newCharTexture;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateRandomCharacter();
        }
    }

    private void CreateRandomCharacter()
    {
        newCharTexture = new Texture2D(32,32);
        Texture2D textureToMerge = baseSpriteSheets[GetRandomIndex(baseSpriteSheets)];

        //copy image
        //offset for other sprites (spritesheet must be sliced)
        int xOffset = 0;
        int yOffset = 0;

        MergeTextures(textureToMerge, xOffset, yOffset, true);
        textureToMerge = clothesSpriteSheets[GetRandomIndex(clothesSpriteSheets)];
        MergeTextures(textureToMerge, xOffset, yOffset);
        textureToMerge = hairSpriteSheets[GetRandomIndex(hairSpriteSheets)];
        MergeTextures(textureToMerge, xOffset, yOffset);

        newCharTexture.Apply();
        Sprite spr = Sprite.Create(newCharTexture, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0), 16, 1, SpriteMeshType.FullRect);
        test.sprite = spr;
    }

    private int GetRandomIndex(Texture2D[] array)
    {
        return UnityEngine.Random.Range(0, array.Length);
    }

    private void MergeTextures(Texture2D addedTexture, int xOffset, int yOffset, bool isFirstImage = false)
    {
        Color color;
        for (int x = 0; x < 32; x++)
        {
            for (int y = 0; y < 32; y++)
            {
                color = addedTexture.GetPixel(x + xOffset, y + yOffset);
                if (!isFirstImage && color.a == 0) continue;
                newCharTexture.SetPixel(x, y, color);
            }
        }
    }
}
