using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StarterPile : MonoBehaviour
{
    public Card TopCard
    {
        get
        {
            if (_cardsList == null || _cardsList.Count == 0)
            {
                return null;
            }
            return _cardsList.Peek();
        }

        private set
        {
            if (_cardsList == null)
            {
                _cardsList = new Stack<Card>();
            }
            _cardsList.Push(value);
            _image.sprite = value.Front;
        }
    }

    public void AddCard(Card card)
    {
        TopCard = card;
    }

    private Image _image;
    private Stack<Card> _cardsList;

    void Awake()
    {
        _image = GetComponent<Image>();
    }
}
