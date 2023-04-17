using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Text dialogueText;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            ShowDialogue();
        }
    }

    private void ShowDialogue()
    {
        foreach(string instruction in dialogue.instructions)
        {
            dialogueText.text = instruction;
        }
    }

    //IEnumerator TypeInstruction(string instruction)
    //{
    //    dialogueText.text = "";
    //    foreach (char letter in instruction.ToCharArray())
    //    {
    //        dialogueText.text += letter;
    //        yield return null;
    //    }
    //}


}
