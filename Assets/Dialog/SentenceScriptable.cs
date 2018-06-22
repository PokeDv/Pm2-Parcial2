using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sentence", menuName = "Sentence")]
public class SentenceScriptable : ScriptableObject {
    public string title;
    public string msg;
    public float time;

}
