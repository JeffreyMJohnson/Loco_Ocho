using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PlayingCards;

namespace Unit_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DeckConstructor()
        {
            Deck myDeck = new Deck();

            Assert.AreEqual<int>(52, myDeck.cards.Count, "card count wrong in deck.");
        }
        
        [TestMethod]
        public void StandardDeckValues()
        {
            Deck myDeck = new Deck();           
            foreach(Card card in myDeck.cards)
            {
                uint expected = 0;
                switch(card.Rank)
                {
                    case Rank.ACE:
                        expected = 1;
                        break;
                    case Rank.TWO:
                        expected = 2;
                        break;
                    case Rank.THREE:
                        expected = 3;
                        break;
                    case Rank.FOUR:
                        expected = 4;
                        break;
                    case Rank.FIVE:
                        expected = 5;
                        break;
                    case Rank.SIX:
                        expected = 6;
                        break;
                    case Rank.SEVEN:
                        expected = 7;
                        break;
                    case Rank.EIGHT:
                        expected = 8;
                        break;
                    case Rank.NINE:
                        expected = 9;
                        break;
                    case Rank.TEN:
                        expected = 10;
                        break;
                    case Rank.JACK:
                        expected = 10;
                        break;
                    case Rank.QUEEN:
                        expected = 10;
                        break;
                    case Rank.KING:
                        expected = 10;
                        break;
                }
                Assert.AreEqual<uint>(expected, card.Value);
            }
        }

        [TestMethod]
        public void CardEquality()
        {
            Card card1 = new Card(Rank.KING, Suit.DIAMONDS, 10);
            Card card2 = new Card(Rank.KING, Suit.DIAMONDS, 10);
            //same card
            Assert.IsTrue(card1 == card1);
            Assert.IsFalse(card1 != card1);
            Assert.IsTrue(card1.Equals(card1));
            //same values
            Assert.IsTrue(card1 == card2);
            Assert.IsFalse(card1 != card2);
            Assert.IsTrue(card1.Equals(card2));
            //null card
            Assert.IsFalse(card1 == null);
            Assert.IsTrue(card1 != null);
            Assert.IsFalse(card1.Equals(null));
            //transitive
            Assert.IsTrue(card2 == card1);
            Assert.IsFalse(card2 != card1);
            Assert.IsTrue(card2.Equals(card1));

            card2 = new Card(Rank.KING, Suit.SPADES, 10);
            //diff suit
            Assert.IsFalse(card1 == card2);
            Assert.IsTrue(card1 != card2);
            Assert.IsFalse(card1.Equals(card2));

            card2 = new Card(Rank.ACE, Suit.DIAMONDS, 10);
            //diff rank
            Assert.IsFalse(card1 == card2);
            Assert.IsTrue(card1 != card2);
            Assert.IsFalse(card1.Equals(card2));

            //diff value
            card2 = new Card(Rank.KING, Suit.DIAMONDS, 13);
            Assert.IsFalse(card1 == card2);
        }

        [TestMethod]
        public void DeckShuffle()
        {
            Deck myDeck = new Deck();

            //original list
            List<Card> originalList = new List<Card>(myDeck.cards);
            myDeck.Shuffle();

            Assert.AreEqual<int>(originalList.Count, myDeck.cards.Count);
            int count = 0;
            for(int i = 0; i < originalList.Count; i++)
            {
                if(originalList[i] == myDeck.cards[i])
                {
                    count++;
                }
            }
            Assert.IsTrue(count < 5, "count: " + count);

        }
    }
}
