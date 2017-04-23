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

    public List<SequenceEvent> sequence = new List<SequenceEvent>();

    public Room roomA;
    public Room roomB;
    public Room roomC;
    public Room roomD;

    public int sequenceSize;
    public float defaultDuration;
    public int sequenceSeed;

    private int currentIndex;
    private float cooldown;

    void Start() {
        SetSequenceEvent(currentIndex);
    }

    void Update() {
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
        int nextIndex = 0;
        if(currentIndex + 1 < sequence.Count) {
            nextIndex = currentIndex + 1;
        }
        return nextIndex;
    }

    public void GenerateSequence() {
        sequence.Clear();
        Random.InitState(sequenceSeed);
        for(int i = 0; i < sequenceSize; i++) {
            SequenceEvent seqEvent = new SequenceEvent(GetRandomState(), GetRandomState(), GetRandomState(), GetRandomState(), defaultDuration);
            sequence.Add(seqEvent);
        }
    }

    private Room.State GetRandomState() {
        return (Room.State)Random.Range(0, (int)Room.State.COUNT);
    }
}
