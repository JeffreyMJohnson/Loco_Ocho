using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
#if UNITY_EDITOR
public class DebugUtility : MonoBehaviour
{
    private Canvas _debugCanvas;
    private RectTransform _deckPanel;


    void Awake()
    {
        CreateCanvas();
        CreateDeckPanel();
    }

    private void CreateDeckPanel()
    {
        GameObject go = new GameObject("Deck Panel");
        _deckPanel = go.AddComponent<RectTransform>();
        _deckPanel.SetParent(_debugCanvas.transform);
        _deckPanel.anchorMin = Vector2.zero;
        _deckPanel.anchorMax = Vector2.one;
        _deckPanel.offsetMax = new Vector2(.5f, -95);
        _deckPanel.offsetMin = new Vector2(1235, 100);
    }

    void Update()
    {
        
    }

    void CreateCanvas()
    {
        GameObject go = new GameObject("Debug Canvas");
        go.layer = LayerMask.NameToLayer("UI");
        go.AddComponent<RectTransform>();
        _debugCanvas = go.AddComponent<Canvas>();
        _debugCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        go.AddComponent<CanvasScaler>();
        go.AddComponent<GraphicRaycaster>();
    }

}
#endif