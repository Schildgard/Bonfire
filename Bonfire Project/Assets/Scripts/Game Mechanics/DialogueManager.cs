using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    private void Awake()
    {
        if (instance == null)
        {
        instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    [SerializeField] private Canvas canvas;
    public Canvas Canvas { get { return canvas; } }

    [SerializeField]private TMP_Text activeCanvasText;
    public TMP_Text ActiveCanvasText  { get { return activeCanvasText; } set { activeCanvasText = value; } }



}
