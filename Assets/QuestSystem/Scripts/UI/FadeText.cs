using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;
    [SerializeField]
    private float _delay;
    [SerializeField]
    private float _speed;

    private void Start()
    {
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 0f);

        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);

        float alpha = 0f;

        while(_text.color.a <= 0.9f)
        {
            alpha += Time.deltaTime * _speed;
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, alpha);

            yield return null;
        }

        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1f);
    }
}
