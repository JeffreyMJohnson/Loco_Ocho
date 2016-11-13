using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player
{
    public enum PlayerID { ONE, TWO };
    public bool IsHandEmpty { get { return _hand.Count == 0; } }

    public Player(GameManager gameManager, PlayerID id)
    {
        _id = id;
        _gameManager = gameManager;
        _gameManager.PlayerTurnChanged.AddListener(PlayerTurnChangedEventHandler);
        //set parent to appropriate one
        _handParent = _id == PlayerID.ONE ? _gameManager.player1HandTransform : _gameManager.player2HandTransform;
        MessagePanel.Instance.SuitSelectedEvent.AddListener(WildCardSuitSelectionEventHandler);
    }

    //todo want this decoupled - move rules logic to player then...
    private GameManager _gameManager;
    private PlayerID _id;
    private RectTransform _handParent;
    private List<Card> _hand = new List<Card>();
    private bool _waitingForSuitSelection = false;
    private Card _clickedCard = null;
    private void PlayerTurnChangedEventHandler(Player currentPlayer)
    {

        //todo implementing ai player
        if (currentPlayer != this)
        {
            return;
        }

        //if human ignore?

        //check if have a valid play
        // List<Card> validPlays = GetValidPlayCards();

        //draw card
        // AddCardToHand(_gameManager.DrawCard());
    }

    private List<Card> GetValidPlayCards()
    {
        List<Card> result = new List<Card>();
        foreach (Card card in _hand)
        {
            if (_gameManager.IsValidPlay(card))
            {
                result.Add(card);
            }
        }
        return result;
    }

    public void AddCardToHand(Card newCard)
    {
        _hand.Add(newCard);
        newCard.transform.SetParent(_handParent, false);
        newCard.CardClicked.AddListener(CardClickEventHandler);
    }

    private void CardClickEventHandler(Card clickedCard)
    {
        //ignore click if waiting for suit selection
        if (_waitingForSuitSelection)
        {
            return;
        }
        //Debug.Log("My card " + clickedCard.ToString() + " was clicked..");
        //is it this players turn
        if (_gameManager.CurrentPlayer == this)
        {
            //valid play?
            if (_gameManager.IsValidPlay(clickedCard))
            {


                if (clickedCard.IsWild)
                {
                    _clickedCard = clickedCard;
                    MessagePanel.Instance.ShowSuitSelection();
                    //will get response via event handler
                    _waitingForSuitSelection = true;
                }
                else
                {
                    //play card
                    Card play = _hand.Find(x => x == clickedCard);
                    _hand.Remove(play);
                    play.CardClicked.RemoveListener(CardClickEventHandler);
                    //play.transform.SetParent(null);
                    _gameManager.PlayCard(play);
                }


            }
            else
            {
                Debug.Log("Not a valid play...");
                MessagePanel.Instance.ShowMessageText("Not a valid play ...");
            }

        }
    }
    private void WildCardSuitSelectionEventHandler(Card.Card_Suit wildSuit, Sprite suitImage)
    {
        //this event is called for each player but only want the current one to act
        //can't use the _gameManager.Current value because PlayCard resets it inside before the 
        //second call.
        if (_clickedCard == null)
        {
            return;
        }

        _clickedCard.Front = suitImage;
        _gameManager.PlayCard(_clickedCard, wildSuit);
        _clickedCard = null;
        _waitingForSuitSelection = false;

    }
}
