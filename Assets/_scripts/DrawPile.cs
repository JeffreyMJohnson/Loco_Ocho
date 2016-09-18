using System;
using UnityEngine;
using System.Collections.Generic;

public class DrawOnEmptyPileException : Exception
{
    public DrawOnEmptyPileException()
    {
    }

    public DrawOnEmptyPileException(string message)
        : base(message)
    {
    }

    public DrawOnEmptyPileException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

public class DrawPile : MonoBehaviour
{

    public bool IsEmpty
    {
        get
        {
            if (_cards == null)
            {
                return true;
            }
            else
            {
                return _cards.Count == 0;
            }
        }
    }

    public Card DrawCard()
    {
        if (_cards == null)
        {
            throw new NullReferenceException("Pile was never created, must call CreateNewPile() before calling DrawCard().");
        }

        if (IsEmpty)
        {
            throw new DrawOnEmptyPileException("Draw pile is empty, can not draw!");
        }

        return _cards.Dequeue();
    }

    public void CreateNewPile(Card[] cards)
    {
        _cards = new Queue<Card>(cards);
    }

    private Queue<Card> _cards;


}
