using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    Deck deck;


    void Start()
    {
        deck = GetComponent<Deck>();
        deck.OnCardClicked += OnCardClickedHandler;
    }
    
    void OnCardClickedHandler(Card card)
    {
        //Debug.Log(string.Format("{0} was clicked.", card));
        card.Flip();
    }
    

}
