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

        public SequenceEvent(Room.State stateRoomA, Room.State stateRoomB, Room.State stateRoomC, Room.State stateRoomD, float stateDuration) {
            roomA = stateRoomA;
            roomB = stateRoomB;
            roomC = stateRoomC;
            roomD = stateRoomD;
            duration = stateDuration;
        }
    }

    public SequenceEvent[] sequence;

    public GameController gameController;
    public Room roomA;
    public Room roomB;
    public Room roomC;
    public Room roomD;

    public int sequenceSize;
    public float defaultDuration;
    public int sequenceSeed;

    [HideInInspector]
    public float currentTime;
    [HideInInspector]
    public int currentIndex;
    private float cooldown;

    void Start() {
        SetSequenceEvent(currentIndex);
    }

    void Update() {
        currentTime += Time.deltaTime;
        cooldown -= Time.deltaTime;

        if(cooldown <= 0) {
            currentIndex = GetNextIndex();
            SetSequenceEvent(currentIndex);
        }
    }

    private void SetSequenceEvent(int index) {
        SequenceEvent seqEvent = sequence[index];
        cooldown = seqEvent.duration + cooldown;
        roomA.SetState(seqEvent.roomA);
        roomB.SetState(seqEvent.roomB);
        roomC.SetState(seqEvent.roomC);
        roomD.SetState(seqEvent.roomD);
    }

    private int GetNextIndex() {
        int nextIndex = currentIndex;
        if(currentIndex + 1 < sequence.Length) {
            nextIndex++;
        } else {
            gameController.OnSequenceEnd();
        }
        return nextIndex;
    }

    public void GenerateSequence() {
        sequence = new SequenceEvent[sequenceSize];
        Random.InitState(sequenceSeed);
        for(int i = 0; i < sequenceSize; i++) {
            SequenceEvent seqEvent = new SequenceEvent(GetRandomState(), GetRandomState(), GetRandomState(), GetRandomState(), defaultDuration);
            sequence[i] = seqEvent;
        }
    }

    private Room.State GetRandomState() {
        return (Room.State)Random.Range(0, (int)Room.State.COUNT);
    }
}
