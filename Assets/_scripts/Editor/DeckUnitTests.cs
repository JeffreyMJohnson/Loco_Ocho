using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;


public class DeckUnitTests
{

    [Test]
    public void DealCardTest()
    {
        Deck deck = new Deck();
        List<Card> dealtCards = new List<Card>(52);

        for (int i = 0; i < 52; ++i)
        {
            Card dealtCard = deck.DealCard();
            Assert.That(dealtCards.Contains(dealtCard), Is.False);
            dealtCards.Add(dealtCard);
        }
        Assert.That(deck.ToArray().Length, Is.EqualTo(0));
        Assert.That(deck.DealCard(), Is.Null);
    }

    [Test]
    public void AddCardToMiddleTest()
    {
        Deck deck = new Deck();
        Card card = deck.DealCard();

        deck.AddCardToMiddle(card);
        Card[] cards = deck.ToArray();
        bool isInMiddleish = false;
        for (int i = (cards.Length / 2) - 1; i <= (cards.Length / 2) + 1; ++i)
        {
            if (cards[i].Equals(card))
            {
                isInMiddleish = true;
            }
        }
        Assert.That(isInMiddleish, Is.True);

    }

    [Test]
    public void ShuffleTest()
    {
        Deck deck = new Deck();
        //will pass if different spot percentage is greater than 90%

        Card[] before = deck.ToArray();
        deck.Shuffle(50);
        Card[] after = deck.ToArray();

        int diffcount = 0;
        for (int i = 0; i < before.Length; ++i)
        {
            if (before[i] != after[i])
            {
                ++diffcount;
            }
        }

        float diffPercentage = diffcount/52.0f;
        Assert.That(diffPercentage, Is.GreaterThan(.9f));
    }
}
