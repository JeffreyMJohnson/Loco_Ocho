using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{

    #region public fields/properties/events
    public RectTransform player1HandTransform = null;
    public RectTransform player2HandTransform = null;
    public Text message;
    public Player CurrentPlayer { get { return _currentPlayer; } }
    public class PlayerTurnChangedEvent : UnityEvent<Player> { }
    public PlayerTurnChangedEvent PlayerTurnChanged = new PlayerTurnChangedEvent();
    #endregion
    #region public methods

    public bool IsValidPlay(Card testCard)
    {
        if (testCard.IsWild) return true;

        if (testCard.Rank == _discardPile.CurrentRank || testCard.Suit == _discardPile.CurrentSuit)
        {
            return true;
        }
        return false;
    }

    public void PlayCard(Card card, Card.Card_Suit wildCardSuit = Card.Card_Suit.CLUBS)
    {
        //is it valid play
        if (IsValidPlay(card))
        {
            _discardPile.AddCard(card, wildCardSuit);

            //check if winner
            if (_player1.IsHandEmpty)
            {
                Debug.Log("Player 1 winner!.");
            }
            else if (_player2.IsHandEmpty)
            {
                Debug.Log("Player 2 winner!.");
            }
            //swap current player
            _currentPlayer = _currentPlayer == _player1 ? _player2 : _player1;
            PlayerTurnChanged.Invoke(_currentPlayer);
        }
    }

    public Card DrawCard()
    {
        if (_drawPile.IsEmpty)
        {
            return null;
        }

        return _drawPile.DrawCard();
    }
    #endregion
    #region interface implementations

    #endregion

    #region private fields
    private DrawPile _drawPile;
    private DiscardPile _discardPile;
    private Player _player1;
    private Player _player2;
    private Player _currentPlayer;
    #endregion
    #region private methods
    private void Deal()
    {
        Deck deck = new Deck();
        deck.Shuffle(50);
        //Deal 5 cards one at a time, face down, beginning with the player to the left.
        for (int i = 0; i < 5; ++i)
        {
            Card topCard = deck.DealCard();
            _player1.AddCardToHand(topCard);
            topCard.transform.SetParent(player1HandTransform, false);


            topCard = deck.DealCard();
            _player2.AddCardToHand(topCard);
            topCard.transform.SetParent(player2HandTransform, false);

        }

        /*The balance of the pack is placed face down in the center of the table and forms 
         * the stock. The dealer turns up the top card and places it in a separate pile; this 
         * card is the “starter.” If an eight is turned, it is buried in the middle of the 
         * pack and the next card is turned.*/
        //deal one to the pile
        Card potential = deck.DealCard();
        //check if wild
        while (potential.Rank == Card.Card_Rank.EIGHT)
        {
            deck.AddCardToMiddle(potential);
            potential = deck.DealCard();
        }

        _discardPile.AddCard(potential);

        _drawPile.CreateNewPile(deck.ToArray());
    }

    private string GetPlayerAsString(Player player)
    {
        return player == _player1 ? "Player 1" : "Player 2";
    }
    #endregion
    #region unity lifecycle methods
    void Awake()
    {
        //if (message == null)
        //{
        //    Debug.LogError("message is null. Did you remember to set in the editor?");
        //    return;
        //}

        //init players
        if (_player1 == null)
        {
            _player1 = new Player(this, Player.PlayerID.ONE);
        }
        if (_player2 == null)
        {
            _player2 = new Player(this, Player.PlayerID.TWO);
        }

        //get main canvas
        GameObject mainCanvas =
            FindObjectsOfType<GameObject>().Where(gameObject => gameObject.name == "Main Canvas").First();
        if (mainCanvas == null)
        {
            Debug.LogError("Did not find main canvas.");
        }

        //init discard pile
        GameObject discardPilePrefab = Resources.Load<GameObject>("prefabs/Discard Pile");
        GameObject discardPileInstance = Instantiate<GameObject>(discardPilePrefab);
        discardPileInstance.name = "Discard Pile";
        discardPileInstance.transform.SetParent(mainCanvas.transform, false);
        _discardPile = discardPileInstance.GetComponent<DiscardPile>();

        //init draw pile
        GameObject drawPilePrefab = Resources.Load<GameObject>("prefabs/Draw Pile");
        GameObject drawPileInstance = Instantiate<GameObject>(drawPilePrefab);
        drawPileInstance.name = "Draw Pile";
        drawPileInstance.transform.SetParent(mainCanvas.transform, false);
        _drawPile = drawPileInstance.GetComponent<DrawPile>();
        _drawPile.DrawPileClicked.AddListener(DrawPileClickEventHandler);

        Deal();
        _currentPlayer = _player1;

        PlayerTurnChanged.Invoke(_currentPlayer);

    }

    void Update()
    {
        //is there winner?
        if (_player1.IsHandEmpty && _player2.IsHandEmpty)
        {
            message.text = (_player1.IsHandEmpty ? "Player 1" : "Player 2") + "Won !!";
            Debug.LogError("winning state not implemented yet!");
        }
        else
        {
            //current player take turn
            //message.text = "Your turn " + GetPlayerAsString(_currentPlayer);
        }
    }
    #endregion

    public void HandleDebugButtonClickEvent()
    {
        //swap current player
        _currentPlayer = _currentPlayer == _player1 ? _player2 : _player1;
        PlayerTurnChanged.Invoke(_currentPlayer);
    }

    private void DrawPileClickEventHandler()
    {
        //is empty?
        if (!_drawPile.IsEmpty)
        {
            _currentPlayer.AddCardToHand(_drawPile.DrawCard());

            if(_drawPile.IsEmpty)
            {
                _drawPile.gameObject.SetActive(false);
            }

        }
        else
        {
            //click is pass then
        }

    }

}
