
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

//todo this needs work
public class MessagePanel : MonoBehaviour, IPointerClickHandler
{
    private Text _messageText;
    private Image _panelImage;
    private Transform _suitSelectionParent;
    //suit selection icons
    private Image _spades;
    private Image _clubs;
    private Image _hearts;
    private Image _diamonds;

    public class SuitSelected : UnityEvent<Card.Card_Suit, Sprite> { }
    public SuitSelected SuitSelectedEvent = new SuitSelected();
    public static MessagePanel Instance;
    public bool IsActive { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Another Message Panel instance was found, I die now.");
            Destroy(gameObject);
        }
        if (Instance == null)
        {
            Instance = this;
        }

        _messageText = GetComponentInChildren<Text>();
        _panelImage = GetComponent<Image>();
        Image[] images = gameObject.GetComponentsInChildren<Image>(true);
        foreach (Image image in images)
        {
            switch (image.name)
            {
                case "Clubs":
                    _clubs = image;
                    break;
                case "Spades":
                    _spades = image;
                    break;
                case "Hearts":
                    _hearts = image;
                    break;
                case "Diamonds":
                    _diamonds = image;
                    break;
            }
        }
        _suitSelectionParent = _clubs.transform.parent;
    }

    public void ShowSuitSelection()
    {
        StartCoroutine(ShowSuits());
    }


    public void ShowMessageText(string text)
    {
        StartCoroutine(ShowText(text));
    }

    private IEnumerator ShowSuits()
    {
        IsActive = true;
        _suitSelectionParent.gameObject.SetActive(true);
        yield return new WaitUntil(() => !IsActive);


    }

    private IEnumerator ShowText(string text)
    {
        IsActive = true;
        //turn on panel and text box
        _messageText.text = text;
        _messageText.enabled = true;
        _panelImage.enabled = true;
        //wait for a time
        yield return new WaitForSeconds(1);

        //reset back to 'off' state
        _messageText.enabled = false;
        _panelImage.enabled = false;
        _messageText.text = "";
        IsActive = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clicked = eventData.pointerCurrentRaycast.gameObject;
        SuitSelectedEvent.Invoke(GetSuit(clicked.name), clicked.GetComponent<Image>().sprite);
        //this flag is what ShowSuits coroutine waits on, so do not fuck with!
        IsActive = false;
    }

    private Card.Card_Suit GetSuit(string clicked)
    {        
        Card.Card_Suit selected = Card.Card_Suit.CLUBS;
        switch (clicked)
        {
            case "Clubs":
                selected = Card.Card_Suit.CLUBS;
                break;
            case "Diamonds":
                selected = Card.Card_Suit.DIAMONDS;
                break;
            case "Hearts":
                selected = Card.Card_Suit.HEARTS;
                break;
            case "Spades":
                selected = Card.Card_Suit.SPADES;
                break;
            //todo remove for release
            default:
                Debug.Assert(false, "Should never get here!");
                break;
        }
        
        _suitSelectionParent.gameObject.SetActive(false);
        return selected;
    }
}
