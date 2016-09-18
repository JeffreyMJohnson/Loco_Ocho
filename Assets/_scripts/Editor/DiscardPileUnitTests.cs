using UnityEngine;
using UnityEditor;
using NUnit.Framework;

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

        Assert.That(discardPile.PeekTopCard(), Is.SameAs(card));

    }
}
