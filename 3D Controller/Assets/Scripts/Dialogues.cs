using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogues : MonoBehaviour
{
    public List<TMP_Text> Lines;


    public Collider Collider;

    private bool talkRange;

    private int currentTextIndex = 0;


    private void Update()
    {
        if (talkRange && DialogueManager.instance.Canvas.gameObject.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {

                DialogueManager.instance.Canvas.gameObject.SetActive(true);
                DialogueManager.instance.ActiveCanvasText.text = Lines[currentTextIndex].text;
            }
        }
        else if (talkRange && DialogueManager.instance.Canvas.gameObject.activeSelf == true)
        {

            if (Input.GetKeyDown(KeyCode.M))
            {
                if (currentTextIndex >= Lines.Count - 1)
                {
                    DialogueManager.instance.Canvas.gameObject.SetActive(false);
                    currentTextIndex = 0;
                }
                else
                {
                    currentTextIndex += 1;
                    DialogueManager.instance.ActiveCanvasText.text = Lines[currentTextIndex].text;
                }

            }
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == 7)
        {
            talkRange = true;
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.layer == 7)
        {
            talkRange = false;
            DialogueManager.instance.Canvas.gameObject.SetActive(false);
        }
    }
}
