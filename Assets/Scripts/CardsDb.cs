using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
//unity scriptable object that holds data for card including name, description, and image and value
[CreateAssetMenu(fileName = "CardsDb", menuName = "CardDeck")]

public class CardsDb : ScriptableObject
{   
   [BoxGroup("CardsData")] [SerializeField] private Sprite card;

     //list of all the card names in a Standard 52-card deck
     [BoxGroup("CardsData")] [SerializeField] private  List<string> cardNames = new List<string>()
     {
        "Ace ","Two ","Three ","Four ","Five ","Six ","Seven ","Eight ","Nine ","Ten ","Jack ","Queen ","King "
     };
     [BoxGroup("CardsData")] [SerializeField] private  List<string> cardSuits = new List<string>()
     {
        "Spades"," Hearts","Diamonds","Clubs"
     };

     [BoxGroup("CardsData")] [SerializeField]
     private List<Sprite> cardsSprites = new List<Sprite>();
     
     
     [BoxGroup("CardsItems")][SerializeField]private List<CardData> cards = new List<CardData>();

     
     //function to generate 52 cards and add them to the list of cards
     [Button]
     public void GenerateCards()
     {
         cards.Clear();
         for(var i = 0; i < cardNames.Count; i++)
         {
             foreach (var t in cardSuits)
             {
                 //select all the sprites from the list of sprites which have the same name as the card name and suit
                 //sprite => sprite.name.ToLower().Replace("800px-playing_card_", "").Replace(".svg", "")
                 var cardSpriteNames = cardsSprites.Where(x => x.name.ToLower().Contains(t.ToLower())).ToDictionary(sprite => sprite.name.ToLower().
                     Replace("800px-playing_card_", "").Replace(".svg", ""), sprite => sprite);
                 var cardImage = cardSpriteNames.FirstOrDefault(x => GetValue(x.Key) == i + 1).Value;
                 var card = new CardData
                 {
                     name = cardNames[i],
                     description = "This is a " + cardNames[i] +" of "+ t + " card",
                     image = cardImage,
                     backImage = this.card,
                     value = i + 1
                 };
                 cards.Add(card);
             }
         }
     }

     private int GetValue(string spriteName)
     {
         var value = spriteName[spriteName.Length-1];
         switch (value)
         {
             case 'a':
                 return 1;
             case '2':
                 return 2;
             case '3':
                 return 3;
             case '4':
                 return 4;
             case '5':
                 return 5;
             case '6':
                 return 6;
             case '7':
                 return 7;
             case '8':
                 return 8;
             case '9':
                 return 9;
             case '0':
                 return 10;
             case 'j':
                 return 11;
             case 'q':
                 return 12;
             case 'k':
                 return 13;
             default:
                 return int.Parse(value.ToString());
         }

         }
     public List<CardData> GETCards()
     {
         return cards;
     }
}