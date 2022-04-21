using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

public class Player:EncapsulatedMonoBehaviorComponent
{
    [SerializeField]private List<GameObject> cards;
    [SerializeField] private bool isAi = false;
    private void OnEnable()
    {
        AddSubscription((MessageBroker.Default.Receive<GameStartEventArgs>().ObserveOnMainThread().Subscribe(Init)));
    }
    [Button]
    private void Init(GameStartEventArgs obj)
    {
        AddSubscription(MessageBroker.Default.Receive<CardsDealtEventArgs>().Subscribe(OnReceiveCards));
    }
    //receive the cards from the dealer and distribute them to the player
    private void OnReceiveCards(CardsDealtEventArgs obj)
    {
        cards = isAi ? obj.aiCardList : obj.playerCardList;
    }
}