using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine.EventSystems;
using System;
using System.Linq;


namespace Old_Stuff
{
    public class GameManager : MonoBehaviour
    {
        [Obsolete]
        private class Hand
        {
            public List<Card> cardsList = new List<Card>();
            public RectTransform location;
        };

        public Card[] Deck { get { return _deck.Cards; } }

        private const int CARD_DEAL_COUNT = 5;
        private Deck _deck;
        private Player _playerOne = new Player();
        private Player _playerTwo = new Player();
        private RectTransform _playerOneHandTransform;
        private RectTransform _playerTwoHandTransform;
        private GameObject _mainCanvas;
        private GameObject _cardPrefab;
        private StarterPile _starterPile;

        void Awake()
        {
            _deck = new Deck(Resources.LoadAll<Sprite>("cards_classic"));
            _deck.Shuffle();

            _mainCanvas = FindObjectsOfType<GameObject>().Where(go => go.name == "Main Canvas").First();
            Debug.Assert(_mainCanvas != null);

            _playerOneHandTransform = _mainCanvas.GetComponentsInChildren<RectTransform>()
                .Where(transform => transform.name == "Player One Hand")
                .First();
            Debug.Assert(_playerOneHandTransform != null);

            _playerTwoHandTransform = _mainCanvas.GetComponentsInChildren<RectTransform>()
                    .Where(transform => transform.name == "Player Two Hand")
                    .First();
            Debug.Assert(_playerTwoHandTransform != null);

            _cardPrefab = Resources.Load<GameObject>("prefabs/Card");
            Debug.Assert(_cardPrefab != null);

            _starterPile = _mainCanvas.GetComponentInChildren<StarterPile>();
            Debug.Assert(_starterPile != null);
        }
        void Start()
        {
            DealPlayer(_playerOne, _playerOneHandTransform);
            DealPlayer(_playerTwo, _playerTwoHandTransform);
            DealStarterCard();
        }

        private void DealStarterCard()
        {
            Card card = _deck.DealCard();
            do
            {
                _deck.ReturnCardBackToDeck(card);
                card = _deck.DealCard();
            } while (card.Rank == Old_Stuff.Deck.Rank.EIGHT);

            _starterPile.AddCard(card);
        }

        private void DealPlayer(Player player, RectTransform tablePosition)
        {
            //deal cards face up to player one
            for (int i = 0; i < CARD_DEAL_COUNT; ++i)
            {
                Card newCard = _deck.DealCard();
                player.AddCardToHand(newCard);

                GameObject go = Instantiate<GameObject>(_cardPrefab);
                go.transform.SetParent(tablePosition);
                go.name = newCard.ToString();
                go.GetComponent<Image>().sprite = newCard.Front;
                go.transform.localScale = Vector3.one;
            }
        }
    }
}