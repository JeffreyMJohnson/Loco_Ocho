using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    }

    private GameManager _gameManager;
    private PlayerID _id;
    private RectTransform _handParent;
    private List<Card> _hand = new List<Card>();
    private void PlayerTurnChangedEventHandler(Player currentPlayer)
    {
        if (currentPlayer != this)
        {
            return;
        }

        //check if have a valid play
        List<Card> validPlays = GetValidPlayCards();

        //draw card
        AddCardToHand(_gameManager.DrawCard());
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
        //Debug.Log("My card " + clickedCard.ToString() + " was clicked..");
        //is it this players turn
        if (_gameManager.CurrentPlayer == this)
        {
            //this players card?
            if (_hand.Contains(clickedCard))
            {
                //valid play?
                if (_gameManager.IsValidPlay(clickedCard))
                {

                    //todo check for wild and then get suit


                    //play card
                    Card play = _hand.Find(x => x == clickedCard);
                    _hand.Remove(play);
                    play.transform.SetParent(null);
                    _gameManager.PlayCard(play);
                }
                else
                {
                    Debug.Log("Not a valid play...");
                }
            }

        }
    }
}
