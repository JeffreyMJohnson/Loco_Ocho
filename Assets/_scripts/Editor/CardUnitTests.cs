using System;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class CardUnitTests
{
    private GameObject _cardPrefab = null;
    private Sprite[] _sprites = null;

    [TestFixtureSetUp]
    public void Init()
    {
        _cardPrefab = Resources.Load<GameObject>(@"prefabs/Card");
        _sprites = Resources.LoadAll<Sprite>("cards_classic");
    }

    [Test]
    public void CardInitTest()
    {
        GameObject cardInstance = GameObject.Instantiate(_cardPrefab) as GameObject;
        Card card = cardInstance.GetComponent<Card>();
        Card.Card_Rank ace = Card.Card_Rank.ACE;
        Card.Card_Suit spades = Card.Card_Suit.SPADES;
        Sprite sprite = GetSprite(ace, spades);
        
        Assert.IsFalse(card.IsInitialized);
        card.Init(ace, spades, sprite);
        Assert.IsTrue(card.IsInitialized);

        Assert.AreEqual(ace, card.Rank);
        Assert.AreEqual(spades, card.Suit);
        Assert.NotNull(card.Front);
        Assert.AreSame(sprite, card.Front);
    }

    [Test]
    public void CardInitNullSpriteTest()
    {
        GameObject cardInstance = GameObject.Instantiate(_cardPrefab) as GameObject;
        Card threeDiamonds = cardInstance.GetComponent<Card>();
        threeDiamonds.Init(Card.Card_Rank.THREE, Card.Card_Suit.DIAMONDS, null);
        Assert.That(threeDiamonds.Rank, Is.EqualTo(Card.Card_Rank.THREE));
        Assert.That(threeDiamonds.Suit, Is.EqualTo(Card.Card_Suit.DIAMONDS));
        Assert.That(threeDiamonds.Front, Is.Null);
    }

    [Test]
    public void CardIsWildTest()
    {
        GameObject cardInstance_wild = GameObject.Instantiate(_cardPrefab) as GameObject;
        GameObject cardInstance_Non = GameObject.Instantiate(_cardPrefab) as GameObject;

        Card cardScript_Wild = cardInstance_wild.GetComponent<Card>();
        Card cardScript_Non = cardInstance_Non.GetComponent<Card>();

        Card.Card_Rank ace = Card.Card_Rank.ACE;
        Card.Card_Suit spades = Card.Card_Suit.SPADES;
        Card.Card_Rank eight = Card.Card_Rank.EIGHT;
        Card.Card_Suit diamonds = Card.Card_Suit.DIAMONDS;
        Sprite sprite = GetSprite(ace, spades);
        Sprite sprite2 = GetSprite(eight, diamonds);

        cardScript_Wild.Init(eight, diamonds, sprite2);
        cardScript_Non.Init(ace, spades, sprite);

        Assert.IsTrue(cardScript_Wild.IsWild);
        Assert.IsFalse(cardScript_Non.IsWild);

    }

    private Sprite GetSprite(Card.Card_Rank rank, Card.Card_Suit suit)
    {
        
        string rankString = Enum.GetName(typeof(Card.Card_Rank), rank).ToLower();
        string suitString = Enum.GetName(typeof(Card.Card_Suit), suit).ToLower();
        string file = string.Format("{0}_{1}",
            rankString, suitString);
        foreach (Sprite front in _sprites)
        {
            if (front.name == file)
            {
                return front;
            }
        }
        Debug.LogError("Did not find sprite: " + Enum.GetName(typeof(Card.Card_Rank), rank) + " of " + Enum.GetName(typeof(Card.Card_Suit), suit));
        return null;
    }
}
