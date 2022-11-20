using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationObject : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    
    [SerializeField] float frameDuration;
    SpriteRenderer spriteRenderer;
    float timer;
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        int _number = (int)((timer / frameDuration) % sprites.Length);
        Debug.Log(_number + " of " + sprites.Length);
        spriteRenderer.sprite = sprites[_number];
    }
}
