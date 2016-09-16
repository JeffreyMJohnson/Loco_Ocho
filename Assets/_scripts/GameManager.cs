using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject cardPrefab = null;
    public RectTransform stockTransform = null;
    public RectTransform pileTransform = null;
    public RectTransform player1HandTransform = null;
    public Text message;

    private List<Card> _deck = new List<Card>();
    private List<Card> _stock = new List<Card>();
    private List<Card> _pile = new List<Card>();
    private Player _player1 = new Player();
    private Player _player2 = new Player();
    private Player _currentPlayer;


    #region initialization 
    private void CreateDeck()
    {
        if (cardPrefab == null)
        {
            Debug.LogError("No prefab, did you forget to set in editor?");
            return;
        }
        Sprite[] fronts = Resources.LoadAll<Sprite>("cards_classic");
        if (fronts == null)
        {
            Debug.LogError("Could not find card front sprites. Are they in the Resources folder?");
            return;
        }
        foreach (Card.Card_Suit suit in Enum.GetValues(typeof(Card.Card_Suit)))
        {
            foreach (Card.Card_Rank rank in Enum.GetValues(typeof(Card.Card_Rank)))
            {
                Card newCard = Instantiate<GameObject>(cardPrefab).GetComponent<Card>();
                newCard.transform.position = Vector3.zero;
                newCard.Init(rank, suit, GetCardFront(fronts, rank, suit));
                _deck.Add(newCard);
            }
        }
    }

    private Sprite GetCardFront(Sprite[] fronts, Card.Card_Rank rank, Card.Card_Suit suit)
    {
        string rankString = Enum.GetName(typeof(Card.Card_Rank), rank).ToLower();
        string suitString = Enum.GetName(typeof(Card.Card_Suit), suit).ToLower();

        string file = string.Format("{0}_{1}",
            rankString, suitString);
        foreach (Sprite front in fronts)
        {
            if (front.name == file)
            {
                return front;
            }
        }
        Debug.LogError("Did not find sprite: " + Enum.GetName(typeof(Card.Card_Rank), rank) + " of " + Enum.GetName(typeof(Card.Card_Suit), suit));
        return null;
    }
    #endregion

    private void ShuffleDeck(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            for (int j = 0; j < _deck.Count; ++j)
            {
                int randIndex = UnityEngine.Random.Range(0, _deck.Count);
                Card temp = _deck[j];
                _deck[j] = _deck[randIndex];
                _deck[randIndex] = temp;
            }
        }
    }

    private void Deal()
    {
        //Deal 5 cards one at a time, face down, beginning with the player to the left.
        for (int i = 0; i < 5; ++i)
        {
            Card topCard = _deck[0];
            _player1.Hand.Add(topCard);
            _deck.RemoveAt(0);
            topCard.transform.SetParent(player1HandTransform, false);
            _player2.Hand.Add(_deck[0]);
            _deck.RemoveAt(0);
        }

        /*The balance of the pack is placed face down in the center of the table and forms 
         * the stock. The dealer turns up the top card and places it in a separate pile; this 
         * card is the “starter.” If an eight is turned, it is buried in the middle of the 
         * pack and the next card is turned.*/
         //deal one to the pile
        Card potential = _deck[0];
        _deck.RemoveAt(0);
        //check if wild
        while (potential.Rank == Card.Card_Rank.EIGHT)
        {
            _deck.Insert(_deck.Count/2, potential);
            potential = _deck[0];
            _deck.RemoveAt(0);
        }
         _pile = new List<Card>() {potential};
        _pile[0].transform.SetParent(pileTransform, false);

        _stock = new List<Card>(_deck);
        _deck = null;
    }


    #region unity lifecycle methods
    void Awake()
    {
        if (message == null)
        {
            Debug.LogError("message is null. Did you remember to set in the editor?");
            return;
        }
        CreateDeck();
        ShuffleDeck(50);
        Deal();
        _currentPlayer = _player1;
    }

    void Update()
    {
        //is there winner?
        if (_player1.Hand.Count == 0 && _player2.Hand.Count == 0)
        {
            message.text = (_player1.Hand.Count == 0 ? "Player 1" : "Player 2") + "Won !!";
            Debug.LogError("winning state not implemented yet!");
        }
        else
        {
            //current player take turn
            message.text = "Your turn " + GetPlayerAsString(_currentPlayer);
        }
    }
    #endregion

    private string GetPlayerAsString(Player player)
    {
        return player == _player1 ? "Player 1" : "Player 2";
    }
}
