using System.Collections;
using TMPro;
using UnityEngine;

public class TextFading : MonoBehaviour
{
    private TMP_Text text;
    [SerializeField] float blendingTime;
    private void Awake()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    public void BlendInText()
    {
        StartCoroutine(BlendText());
    }

    IEnumerator BlendText()
    {
        if (blendingTime <= 0f)
        {
            Debug.Log("Blending Time is set on 0 or below. The While Loop in this method would be unable to resolve, so instead this method returns here without blend");
        }
        else
        {
            //Blend in. Increase the alpha of the panel color until it reaches its maximum.
            Color panelColor = text.color;
            while (panelColor.a <= 1f)
            {
                panelColor.a += Time.deltaTime * blendingTime;
                text.color = panelColor;
                yield return null;
            }

            yield return new WaitForSeconds(1f);
            //Blend out. Decrease the alpha of the panel color until it reaches 0.
            while (panelColor.a > 0f)
            {
                panelColor.a -= Time.deltaTime * (blendingTime * 2);
                text.color = panelColor;
                yield return null;
            }
        }
    }
}
