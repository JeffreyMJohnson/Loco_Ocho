using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public RectTransform player1HandTransform = null;
    public RectTransform player2HandTransform = null;
    public Text message;

    private DrawPile _drawPile;
    private DiscardPile _discardPile;
    private Player _player1 = new Player();
    private Player _player2 = new Player();
    private Player _currentPlayer;


    private void Deal()
    {
        Deck deck = new Deck();
        deck.Shuffle(50);
        //Deal 5 cards one at a time, face down, beginning with the player to the left.
        for (int i = 0; i < 5; ++i)
        {
            Card topCard = deck.DealCard();
            _player1.Hand.Add(topCard);
            topCard.transform.SetParent(player1HandTransform, false);


            topCard = deck.DealCard();
            _player2.Hand.Add(topCard);
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


    #region unity lifecycle methods
    void Awake()
    {
        //if (message == null)
        //{
        //    Debug.LogError("message is null. Did you remember to set in the editor?");
        //    return;
        //}

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
            //message.text = "Your turn " + GetPlayerAsString(_currentPlayer);
        }
    }
    #endregion

    private string GetPlayerAsString(Player player)
    {
        return player == _player1 ? "Player 1" : "Player 2";
    }
}
