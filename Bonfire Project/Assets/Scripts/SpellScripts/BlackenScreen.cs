using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackenScreen : MonoBehaviour
{
    private Image screenPanel;
    [SerializeField] float blendingTime;

    private void Awake()
    {
        screenPanel = GetComponentInChildren<Image>(true);

    }

    public void BlendInPanel()
    {
        StartCoroutine(BlendPanel());
    }


    IEnumerator BlendPanel()
    {
        if (blendingTime <= 0f)
        {
            Debug.Log("Blending Time is set on 0 or below. The While Loop in this method would be unable to resolve, so instead this method returns here without blend");
        }
        else
        {
            screenPanel.gameObject.SetActive(true);
            Color panelColor = screenPanel.color;
            while (panelColor.a <= 1f)
            {
                panelColor.a += Time.deltaTime * blendingTime;
                screenPanel.color = panelColor;
                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
