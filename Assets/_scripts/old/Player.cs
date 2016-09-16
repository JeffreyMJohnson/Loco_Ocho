using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Old_Stuff
{
    class Player
    {
        public Player()
        {
               
        }

        public void AddCardToHand(Card card)
        {
            _hand.Add(card);
        }

        private List<Card> _hand = new List<Card>();
    }
}
