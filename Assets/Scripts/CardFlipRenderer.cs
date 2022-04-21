using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

//class that changes the sprite of the card based on the cards transform rotation
public class CardFlipRenderer : EncapsulatedMonoBehaviorComponent
{
    [SerializeField] private Sprite front;
    [SerializeField] private Sprite backSprite;
    [SerializeField] private Vector2 cardSize;
    [Button]
    public void Init()
    {
        AddSubscription(this.transform.ObserveEveryValueChanged(transform1 => transform1.localEulerAngles).ObserveOnMainThread().Subscribe(OnCardFlip));
    }

    private void OnCardFlip(Vector3 obj)
    {
        //if the absolute value of x or y is above 90 change the front sprite to the back sprite otherwise change it to the front sprite
        var spriteRenderer = this.GetComponent<SpriteRenderer>();
        Debug.Log(obj);
        if ((obj.x > 90 && obj.x<270 )||(obj.y > 90 && obj.y<270 ))
        {
          
            spriteRenderer.sprite = backSprite;
            spriteRenderer.size = cardSize;

        }
        else
        {
            spriteRenderer.sprite = front;
            spriteRenderer.size = cardSize;
        }
    }
    public void SetCard(CardData card)
    {
        front = card.image;
        backSprite = card.backImage;
    }
}
