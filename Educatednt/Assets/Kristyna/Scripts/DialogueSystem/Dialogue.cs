using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all info about a single dialogue.
/// Used as an obj to be passed as a new dialogue with text and who is talking.
/// </summary>
[System.Serializable]
public class Dialogue : MonoBehaviour
{
    public const int minNumSentences = 1;
    public const int maxNumSentences = 10;

    [TextArea(minNumSentences, maxNumSentences)]
    public string[] sentences; //loaded to queue
}
