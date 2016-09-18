using System;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class DrawPileUnitTests
{
    [Test]
    public void DrawCardNullPile()
    {
        var gameObject = new GameObject();
        DrawPile pile = gameObject.AddComponent<DrawPile>();

        Assert.Throws<NullReferenceException>(() => pile.DrawCard());
    }

    [Test]
    public void CreateNewPileTest()
    {
        int count = 10;
        Card.Card_Suit suit = Card.Card_Suit.CLUBS;
        Card[] cards = new Card[count];
        for (int i = 0; i < count; ++i)
        {
            GameObject cardInstance = new GameObject();
            Card card = cardInstance.AddComponent<Card>();
            card.Init((Card.Card_Rank)i, suit, null);
            cards[i] = card;
        }
        
        GameObject drawPileInstance = new GameObject();
        DrawPile drawPile = drawPileInstance.AddComponent<DrawPile>();
        drawPile.CreateNewPile(cards);

        for (int i = 0; i < count; ++i)
        {
            Card drawnCard = drawPile.DrawCard();
            Assert.That(drawnCard, Is.SameAs(cards[i]));
        }
    }

    [Test]
    public void IsEmptyTest()
    {
        GameObject cardInstance = new GameObject();
        Card aceSpades = cardInstance.AddComponent<Card>();
        aceSpades.Init(Card.Card_Rank.ACE, Card.Card_Suit.SPADES, null);

        GameObject drawPileInstance = new GameObject();
        DrawPile drawPile = drawPileInstance.AddComponent<DrawPile>();
        drawPile.CreateNewPile(new Card[] { aceSpades });
        Assert.That(drawPile.IsEmpty, Is.False);
        drawPile.DrawCard();
        Assert.That(drawPile.IsEmpty, Is.True);
    }

    [Test]
    public void DrawCardTest()
    {
        GameObject cardInstance = new GameObject();
        Card aceSpades = cardInstance.AddComponent<Card>();
        aceSpades.Init(Card.Card_Rank.ACE, Card.Card_Suit.SPADES, null);

        GameObject drawPileInstance = new GameObject();
        DrawPile drawPile = drawPileInstance.AddComponent<DrawPile>();
        drawPile.CreateNewPile(new Card[] {aceSpades});

        Card drawnCard = drawPile.DrawCard();
        Assert.That(drawnCard, Is.SameAs(aceSpades));

    }
    
    
}
