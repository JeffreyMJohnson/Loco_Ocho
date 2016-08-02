using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private class Hand
    {
        public List<Card> cardsList = new List<Card>();
        public RectTransform location;
    };

    public Card[] Deck { get { return _deck.Cards; } }

    private const int CARD_DEAL_COUNT = 5;
    private Deck _deck;
    private Hand _playerOneHand;
    private Hand _playerTwoHand;
    private GameObject _mainCanvas;
    private GameObject _cardPrefab;

    void Awake()
    {
        _deck = new Deck(Resources.LoadAll<Sprite>("cards_classic"));
        _deck.Shuffle();

        _mainCanvas = FindObjectsOfType<GameObject>().Where(go => go.name == "Main Canvas").First();
        Debug.Assert(_mainCanvas != null);

        _playerOneHand = new Hand()
        {
            location = _mainCanvas.GetComponentsInChildren<RectTransform>()
                .Where(transform => transform.name == "Player One Hand")
                .First()
        };

        _playerTwoHand = new Hand()
        {
            location = _mainCanvas.GetComponentsInChildren<RectTransform>()
                .Where(transform => transform.name == "Player Two Hand")
                .First()
        };

        _cardPrefab = Resources.Load<GameObject>("prefabs/Card");
        Debug.Assert(_cardPrefab != null);
    }
    void Start()
    {
        DealPlayer(_playerOneHand);
        DealPlayer(_playerTwoHand);
    }

    private void DealPlayer(Hand playerHand)
    {
        //deal cards face up to player one
        for (int i = 0; i < CARD_DEAL_COUNT; ++i)
        {
            Card newCard = _deck.DealCard();
            playerHand.cardsList.Add(newCard);

            GameObject go = Instantiate<GameObject>(_cardPrefab);
            go.transform.SetParent(playerHand.location);
            go.name = newCard.ToString();
            go.GetComponent<Image>().sprite = newCard.Front;
            go.transform.localScale = Vector3.one;
        }
    }


    /*
    private Deck _deck;


    
    
    void OnCardClickedHandler(Card card)
    {
        //Debug.Log(string.Format("{0} was clicked.", card));
        card.Flip();
    }

    public void ShuffleDeck()
    {
        _deck.Shuffle();
    }
    */
}
