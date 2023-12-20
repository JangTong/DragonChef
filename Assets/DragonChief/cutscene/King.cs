using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Dialogue
{
    [TextArea]
    public string ddialogue;
    public Sprite sprite;
}

public class King : MonoBehaviour
{
    [SerializeField] private Image sprite_person;
    [SerializeField] private Image sprite_DialogueBox;
    [SerializeField] private TMP_Text txt_Dialogue;
    [SerializeField] private Button button;

    private bool isDialogue = false;  //대화진행중

    public int count =0;  //대화어느정도 진행되었는지

    [SerializeField] private Dialogue[] dialogue;



    public void ShowDialogue()
    {
        OnOff(true);
        button.gameObject.SetActive(false);
        //count = 0;
        NextDialogue();
    }

    private void OnOff(bool _flag)
    {
        sprite_DialogueBox.gameObject.SetActive(_flag);
        sprite_person.gameObject.SetActive(_flag);
        txt_Dialogue.gameObject.SetActive(_flag);
        isDialogue = _flag;
    }



    private void NextDialogue()
    {
        txt_Dialogue.text = dialogue[count].ddialogue;
        sprite_person.sprite = dialogue[count].sprite;
        Debug.Log(count);
        //count++;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (count < dialogue.Length)
                {
                    NextDialogue();
                    count++;
                }
                else
                    OnOff(false);
            }
        }
        else
            if (Input.GetKeyDown(KeyCode.Space))
                ShowDialogue();
    }
}
