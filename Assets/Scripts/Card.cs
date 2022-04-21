using Positioning;
using UnityEngine;
using UnityEngine.Serialization;

public class Card:MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CardFlipRenderer cardFlipRenderer;
     [SerializeField] private ScaleToFitScreen scaleToFitScreen;
    
    
    
    public void SetCard(CardData card)
    {
        spriteRenderer.sprite = card.image;
        cardFlipRenderer.SetCard(card);
        cardFlipRenderer.Init();
        scaleToFitScreen.Init();
        var currentScale = transform.localScale;
        transform.localScale = currentScale / 4;
    }
}