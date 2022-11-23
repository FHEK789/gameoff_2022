using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationObject : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] bool isAnimated = true;
    [SerializeField] float frameDuration;
    [SerializeField] SpriteRenderer standSprite;
    private float timer;
    
    private void Start() {
        if(!isAnimated && sprites.Length != 0) standSprite.sprite = sprites[0];
    }
    void Update()
    {
        if(!isAnimated) {
            Destroy(this);
            return;
        }
        timer += Time.deltaTime;
        int _number = (int)((timer / frameDuration) % sprites.Length);
        Debug.Log(_number + " of " + sprites.Length);
        standSprite.sprite = sprites[_number];
    }
}
