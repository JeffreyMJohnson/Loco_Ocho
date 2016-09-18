using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;

public class DiscardPileUnitTests {

    [Test]
    public void AddCardTest()
    {
        GameObject instance = new GameObject();
        Card card = instance.AddComponent<Card>();
        card.Init(Card.Card_Rank.ACE, Card.Card_Suit.CLUBS, null);

        GameObject pileInstance = new GameObject();
        DiscardPile discardPile = pileInstance.AddComponent<DiscardPile>();
        discardPile.AddCard(card);

        Assert.That(discardPile.CurrentRank, Is.EqualTo(card.Rank));
        Assert.That(discardPile.CurrentSuit, Is.EqualTo(card.Suit));
        Assert.That(discardPile.CurrentIsWild, Is.False);
        Assert.That(card.transform.parent, Is.SameAs(discardPile.transform));
    }

    [Test]
    public void AddWildCardTest()
    {
        GameObject instance = new GameObject();
        Card card = instance.AddComponent<Card>();
        card.Init(Card.Card_Rank.EIGHT, Card.Card_Suit.CLUBS, null);

        GameObject pileInstance = new GameObject();
        DiscardPile discardPile = pileInstance.AddComponent<DiscardPile>();
        discardPile.AddCard(card,Card.Card_Suit.DIAMONDS);

        Assert.That(discardPile.CurrentRank, Is.EqualTo(card.Rank));
        Assert.That(discardPile.CurrentSuit, Is.EqualTo(Card.Card_Suit.DIAMONDS));
        Assert.That(discardPile.CurrentIsWild, Is.True);
        Assert.That(card.transform.parent, Is.SameAs(discardPile.transform));
    }

    [Test]
    public void IsMatchTest()
    {
        GameObject pileInstance = new GameObject();
        DiscardPile discardPile = pileInstance.AddComponent<DiscardPile>();

        GameObject cardInstance = new GameObject();
        Card aceSpades = cardInstance.AddComponent<Card>();
        aceSpades.Init(Card.Card_Rank.ACE, Card.Card_Suit.SPADES, null);

        discardPile.AddCard(aceSpades);

        GameObject testCardInstance = new GameObject();
        Card testCard = testCardInstance.AddComponent<Card>();
        //valid match by suit
        testCard.Init(Card.Card_Rank.FOUR, Card.Card_Suit.SPADES, null);
        Assert.That(discardPile.IsValidMatch(testCard), Is.True);

        //valid match by rank
        testCard.Init(Card.Card_Rank.ACE, Card.Card_Suit.DIAMONDS, null);
        Assert.That(discardPile.IsValidMatch(testCard), Is.True);

        //invalid match
        testCard.Init(Card.Card_Rank.FOUR, Card.Card_Suit.DIAMONDS, null);
        Assert.That(discardPile.IsValidMatch(testCard), Is.False);
    }

    [Test]
    public void IsMatchTestWild()
    {

        GameObject pileInstance = new GameObject();
        DiscardPile discardPile = pileInstance.AddComponent<DiscardPile>();

        GameObject wildCardInstance = new GameObject();
        Card wildCard = wildCardInstance.AddComponent<Card>();
        wildCard.Init(Card.Card_Rank.EIGHT, Card.Card_Suit.CLUBS, null);
        
        discardPile.AddCard(wildCard, Card.Card_Suit.DIAMONDS);

        GameObject testCardInstance = new GameObject();
        Card testCard = testCardInstance.AddComponent<Card>();
        //valid match by suit
        testCard.Init(Card.Card_Rank.FOUR, Card.Card_Suit.DIAMONDS, null);
        Assert.That(discardPile.IsValidMatch(testCard), Is.True);

        //valid match by rank (wild card)
        testCard.Init(Card.Card_Rank.EIGHT, Card.Card_Suit.CLUBS, null);
        Assert.That(discardPile.IsValidMatch(testCard), Is.True);

        //invalid match
        testCard.Init(Card.Card_Rank.FOUR, Card.Card_Suit.CLUBS, null);
        Assert.That(discardPile.IsValidMatch(testCard), Is.False);
    }
}
