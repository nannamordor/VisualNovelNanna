using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using UnityEngine;

public class DL_DIALOGUE_DATA
{
    public List<DIALOGUE_SEGMENT> segments;
    private const string segmentIdentifierPattern = @"\{[ca]}|\{w[ca]\s\d*\.?\d*\}";

    public bool hasDialogue => segments.Count > 0;

    public DL_DIALOGUE_DATA(string rawDialogue)
    {
        segments = RipSegments(rawDialogue);
    }

    public List<DIALOGUE_SEGMENT> RipSegments(string rawDialogue)
    {
        List<DIALOGUE_SEGMENT> segments = new List<DIALOGUE_SEGMENT>();
        MatchCollection matches = Regex.Matches(rawDialogue, segmentIdentifierPattern);

        int lastIndex = 0;
        //first and only segment in the file
        DIALOGUE_SEGMENT segment = new DIALOGUE_SEGMENT();
        segment.dialogue = (matches.Count == 0 ? rawDialogue : rawDialogue.Substring(0, matches[0].Index));
        segment.startSignal = DIALOGUE_SEGMENT.StartSignal.NONE;
        segment.signalDelay = 0;
        segments.Add(segment);

        if (matches.Count == 0)
            return segments;
        else
            lastIndex = matches[0].Index;
        
        for(int i = 0; i < matches.Count; i++)
        {
                Match match = matches[i];
                segment = new DIALOGUE_SEGMENT();

                //get the start signal for the segment
                string signalMatch = match.Value;//{A}
                signalMatch = signalMatch.Substring(1, match.Length - 2);
                string[] signalSplit = signalMatch.Split(' ');

                segment.startSignal = (DIALOGUE_SEGMENT.StartSignal)Enum.Parse(typeof(DIALOGUE_SEGMENT.StartSignal), signalSplit[0].ToUpper());

                //get the signal delay because I have more than one value 
                if (signalSplit.Length > 1)
                    float.TryParse(signalSplit[1], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out segment.signalDelay);

                //get the dialogue for the segment
                int nextIndex = i + 1 < matches.Count ? matches[i + 1].Index : rawDialogue.Length;
                segment.dialogue = rawDialogue.Substring(lastIndex + match.Length, nextIndex - (lastIndex + match.Length));
                lastIndex = nextIndex;

                segments.Add(segment);
        }

        return segments;
    }
    //struct that gives us the segmentation of the dialogue
    public struct DIALOGUE_SEGMENT
    {
        public string dialogue;
        public StartSignal startSignal;
        public float signalDelay;

        public enum StartSignal { NONE, C, A, WA, WC}

        public bool appendText => (startSignal == StartSignal.A || startSignal == StartSignal.WA);
    }
}
