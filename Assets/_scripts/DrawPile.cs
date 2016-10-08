using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;

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

public class DrawPile : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent DrawPileClicked = new UnityEvent();
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

    public void OnPointerClick(PointerEventData eventData)
    {
        DrawPileClicked.Invoke();
    }

    private Queue<Card> _cards;


}
