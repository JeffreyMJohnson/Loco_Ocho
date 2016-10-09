using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour
{
    private static Text _messageText;
    private static Image _panelImage;

    public static MessagePanel Instance;

    void Awake()
    {
        _messageText = GetComponentInChildren<Text>();
        _panelImage = GetComponent<Image>();

        if(Instance != null && Instance != this)
        {
            Debug.LogError("Another Message Panel instance was found, I die now.");
            Destroy(gameObject);
        }
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void ShowMessageText(string text)
    {
        StartCoroutine(ShowText(text));
    }

    private IEnumerator ShowText(string text)
    {
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
    }
}
