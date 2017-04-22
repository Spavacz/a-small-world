using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSequence : MonoBehaviour {

    [System.Serializable]
    public struct SequenceEvent {
        public Room.State roomA;
        public Room.State roomB;
        public Room.State roomC;
        public Room.State roomD;
        public float duration;
    }

    public SequenceEvent[] sequence;

    public Room roomA;
    public Room roomB;
    public Room roomC;
    public Room roomD;

    private int currentIndex;
    private float cooldown;

    void Update() {
//        if(cooldown <= 0) {
//            int nextIndex = 0;
//            if(sequence.Length < currentIndex + 1) {
//                nextIndex = currentIndex + 1;
//            }
//            SequenceEvent nextEvent = sequence[nextIndex];
//            cooldown = nextEvent.duration;
//            roomA.SetState(nextEvent.roomA);
//            roomB.SetState(nextEvent.roomB);
//            roomC.SetState(nextEvent.roomC);
//            roomD.SetState(nextEvent.roomD);

//            currentIndex = nextIndex;
//        }
    }
}
