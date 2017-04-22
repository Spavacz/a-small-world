using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(Collider2D))]
public class Room : MonoBehaviour {

    public enum State {
        Cloner,
        Destroyer,
        Empty
    }

    public ContactFilter2D contactFilter;
    public int maxDestroyed = 5;
    public int maxCloned = 3;
    public float flashSpeed = 1f;

    [Tooltip("In seconds")]
    public float affectCooldown = 1f;
    [Tooltip("In seconds")]
    public float morphCooldown = 5f;

    public Color clonerColor;
    public Color clonerLidColor;
    public Color destroyerColor;
    public Color destroyerLidColor;

    public Renderer backgroundRenderer1;
    public Renderer backgroundRenderer2;

    private Collider2D collider2d;

    private float affectCooldownCounter;
    private float morphCooldownCounter;

    public State state;

    void Awake() {
        collider2d = GetComponent<Collider2D>();
    }

    void Update() {
        UpdateAffect();
        UpdateMorph();
        UpdateColor();
    }

    private void UpdateAffect() {
        affectCooldownCounter = Mathf.MoveTowards(affectCooldownCounter, 0, Time.deltaTime);

        if(affectCooldownCounter == 0) {
            AffectArea();
            affectCooldownCounter = affectCooldown;
        }
    }

    private void AffectArea() {
        SetColor(state == State.Cloner ? clonerLidColor : destroyerLidColor);

        int maxAffected = state == State.Cloner ? maxCloned : maxDestroyed;
        Collider2D[] inArea = new Collider2D[maxAffected];
        int affectedCount = collider2d.OverlapCollider(contactFilter, inArea);

        for(int i = 0; i < affectedCount; i++) {
            AffectCharacter(inArea[i].gameObject);
        }
    }

    private void AffectCharacter(GameObject character) {
        switch(state) {
            case State.Cloner:
                CloneCharacter(character);
                break;
            case State.Destroyer:
                DestroyCharacter(character);
                break;
        }
    }

    private void CloneCharacter(GameObject character) {
        Instantiate(character);
    }

    private void DestroyCharacter(GameObject character) {
        Destroy(character);
    }

    private void UpdateMorph() {
        morphCooldownCounter = Mathf.MoveTowards(morphCooldownCounter, 0, Time.deltaTime);
        if(morphCooldownCounter == 0) {
            Morph();
            morphCooldownCounter = morphCooldown;
            affectCooldownCounter = affectCooldown;
        }
    }

    private void Morph() {
        state = state == State.Cloner ? State.Destroyer : State.Cloner;
        SetColor(state == State.Cloner ? clonerColor : destroyerColor);
    }

    private void SetColor(Color color) {
        backgroundRenderer1.material.color = color;
        backgroundRenderer2.material.color = color;
    }

    private void UpdateColor() {
        Color color = state == State.Cloner ? clonerColor : destroyerColor;
        Color currentColor = backgroundRenderer1.material.color;
        color = Color.Lerp(currentColor, color, Time.deltaTime * flashSpeed);
        SetColor(color);
    }
}
