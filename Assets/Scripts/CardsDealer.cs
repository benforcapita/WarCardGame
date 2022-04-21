using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

//This class instantiate a pool of card gameobjects based on the cardsDB shuffled them and distribute them in the game
public class CardsDealer : MonoBehaviour
{
    [SerializeField] private CardsDb db;
    
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardParent;
    
    private List<GameObject> _cards;
    [Button]
    //create the cards
    private void CreateCards()
    {
            _cards = new List<GameObject>();
        foreach (var card in db.GETCards())
        {
            var newCard = Instantiate(cardPrefab, cardParent);
            newCard.GetComponent<Card>().SetCard(card);
            newCard.name = $"{card.description.Replace("This is a ","")}";
            _cards.Add(newCard);
        }
    }
    
    //shuffle the cards
    private void ShuffleCards()
    {
        for (var i = 0; i < _cards.Count; i++)
        {
            var temp = _cards[i];
            var randomIndex = Random.Range(i, _cards.Count);
            _cards[i] = _cards[randomIndex];
            _cards[randomIndex] = temp;
        }
    }
    
    //create two list of cards from the shuffled cards and distribute them in the game 
    [Button]
    private void DistributeCards()
    {
        var playerCards = new List<GameObject>();
        var aiCards = new List<GameObject>();
        for (var i = 0; i < _cards.Count; i++)
        {
            if (i % 2 == 0)
            {
                playerCards.Add(_cards[i]);
            }
            else
            {
                aiCards.Add(_cards[i]);
            }
        }
        MessageBroker.Default.Publish(new CardsDealtEventArgs(){playerCardList = playerCards, aiCardList = aiCards});
    }
    
    //destroy all the cards
    [Button]
    private void DestroyCards()
    {
        foreach (var card in _cards)
        {
            if (Application.isPlaying)
            {
                Destroy(card);
            }
            else
            {
                DestroyImmediate(card);
            }
        }
        _cards.Clear();
    }

}

