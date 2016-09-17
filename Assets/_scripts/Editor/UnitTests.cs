using System;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class UnitTests
{
    

    [Test]
    public void CreateDeckTests()
    {
        var gameManager = new GameManager();
        
    }

    [Test]
    public void CardInitTest()
    {
        GameObject cardPrefab = Resources.Load<GameObject>(@"prefabs/Card");
        GameObject cardInstance = GameObject.Instantiate(cardPrefab) as GameObject;
        Assert.NotNull(cardInstance);

        Card cardScript = cardInstance.GetComponent<Card>();
        Assert.NotNull(cardScript);

        Card.Card_Rank ace = Card.Card_Rank.ACE;
        Card.Card_Suit spades = Card.Card_Suit.SPADES;
        Sprite sprite = GetSprite(ace, spades);
        
        Assert.IsFalse(cardScript.IsInitialized);
        cardScript.Init(ace, spades, sprite);
        Assert.IsTrue(cardScript.IsInitialized);

        Assert.AreEqual(ace, cardScript.Rank);
        Assert.AreEqual(spades, cardScript.Suit);
        Assert.NotNull(cardScript.Front);
    }

    [Test]
    public void CardIsWildTest()
    {
        GameObject cardPrefab = Resources.Load<GameObject>(@"prefabs/Card");

        GameObject cardInstance_wild = GameObject.Instantiate(cardPrefab) as GameObject;
        Assert.NotNull(cardInstance_wild);

        GameObject cardInstance_Non = GameObject.Instantiate(cardPrefab) as GameObject;
        Assert.NotNull(cardInstance_Non);

        Card cardScript_Wild = cardInstance_wild.GetComponent<Card>();
        Assert.NotNull(cardScript_Wild);

        Card cardScript_Non = cardInstance_Non.GetComponent<Card>();
        Assert.NotNull(cardScript_Non);

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
        Sprite[] sprites =
        Resources.LoadAll<Sprite>("cards_classic");
        
        string rankString = Enum.GetName(typeof(Card.Card_Rank), rank).ToLower();
        string suitString = Enum.GetName(typeof(Card.Card_Suit), suit).ToLower();
        string file = string.Format("{0}_{1}",
            rankString, suitString);
        foreach (Sprite front in sprites)
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
