using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogReader : MonoBehaviour {

    DialogScriptable dialogScriptable;
   
    void Start(){
        
        if (dialogScriptable!=null) {
            StartCoroutine (ShowSentences(dialogScriptable));
        }
    }

    IEnumerator ShowSentences (DialogScriptable dialog)
    {
        foreach (var sentence in dialog.sentences) {
            print (sentence.title + "\n" + sentence.msg);
            yield return new WaitForSeconds (sentence.time);
        }

    }
}
