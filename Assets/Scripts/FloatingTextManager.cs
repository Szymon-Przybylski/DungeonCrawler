using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> _floatingTexts = new List<FloatingText>();

    private FloatingText GetFloatingText()
    {
        FloatingText txt = _floatingTexts.Find(t => !t.active);

        if (txt == null)
        {
            txt = new FloatingText();
            txt.obj = Instantiate(textPrefab, textContainer.transform, true);
            txt.text = txt.obj.GetComponent<TextMeshProUGUI>();
            
            _floatingTexts.Add(txt);
        }

        return txt;
    }

    public void Show(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText txt = GetFloatingText();
        txt.text.text = message;
        txt.text.fontSize = fontSize;
        txt.text.color = color;
        txt.obj.transform.position = Camera.main.WorldToScreenPoint(position);
        txt.motion = motion;
        txt.duration = duration;
        
        txt.Show();
    }

    private void Update()
    {
        foreach (FloatingText txt in _floatingTexts)
        {
            txt.UpdateFloatingText();
        }
    }
}
