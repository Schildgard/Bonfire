using System.Collections.Generic;
using UnityEngine;

public class TalkScript : MonoBehaviour
{
    //Talk Script handles the active dialogue between the player and the npc.

    [SerializeField] private List<Dialogue> dialogue;
    [SerializeField] private Collider talkRangeCollider;
    [SerializeField] private GameObject talkToolTip;

    private bool talkRange;

    private int currentDialogueIndex = 0;
    private int currentTextIndex = 0;


    private void Update()
    {
        //Show 'press M to talk' if player is in talk Range
        if (talkRange && DialogueManager.instance.Canvas.gameObject.activeSelf == false)
        {
            talkToolTip.SetActive(true);
            if (Input.GetKeyDown(KeyCode.M))
            {
                talkToolTip.SetActive(false);
                DialogueManager.instance.Canvas.gameObject.SetActive(true);
                DialogueManager.instance.ActiveCanvasText.text = dialogue[currentDialogueIndex].Lines[currentTextIndex].text;

            }
        }
        //If player is in conversation, 'M' scrolls to next dialogue line
        else if (talkRange && DialogueManager.instance.Canvas.gameObject.activeSelf == true)
        {

            if (Input.GetKeyDown(KeyCode.M))
            {
                if (currentTextIndex >= dialogue[currentDialogueIndex].Lines.Count - 1)
                {
                    DialogueManager.instance.Canvas.gameObject.SetActive(false);
                    if (dialogue[currentDialogueIndex].dialogueEvent != null)
                    {
                        dialogue[currentDialogueIndex].dialogueEvent.Raise();
                    }

                    currentTextIndex = 0;
                }
                else
                {
                    currentTextIndex += 1;
                    DialogueManager.instance.ActiveCanvasText.text = dialogue[currentDialogueIndex].Lines[currentTextIndex].text;
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
            talkToolTip.SetActive(false);
            DialogueManager.instance.Canvas.gameObject.SetActive(false);
        }
    }
    public void ChangeActiveDialogue()
    {
        currentDialogueIndex++;
    }

    public void ChangeActiveDialogueAfterBossDown()
    {
        currentDialogueIndex = 3;
    }


}
