using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(Collider2D))]
public class Room : MonoBehaviour {

    public enum State {
        Cloner,
        Killer,
        Empty,
        COUNT
    }

    public ContactFilter2D contactFilter;
    public int maxDestroyed = 5;
    public int maxCloned = 3;
    public float flashSpeed = 1f;
    [Tooltip("In seconds")]
    public float affectCooldown = 1f;

    [HideInInspector]
    public State state;

    public GameController gameController;
    public Light light;
    public GameObject background;
    public Color clonerColor;
    public Color clonerLitColor;
    public Color killerColor;
    public Color killerLitColor;

    private Collider2D collider2d;
    private float affectCooldownCounter;

    void Awake() {
        collider2d = GetComponent<Collider2D>();
    }

    void Update() {
        UpdateAffect();
        UpdateColor();
    }

    private void UpdateAffect() {
        affectCooldownCounter = Mathf.MoveTowards(affectCooldownCounter, 0, Time.deltaTime);

        if(affectCooldownCounter == 0) {
            ResetCooldown();
            AffectArea();
        }
    }

    private void AffectArea() {
        SetColor(GetLitStateColor(state));

        // dont kill us in ff mode u bstrd
        if(Time.timeScale != 1f) {
            return;
        }

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
            case State.Killer:
                KillCharacter(character);
                break;
        }
    }

    private void CloneCharacter(GameObject character) {
        gameController.CloneCharacter(character);
    }

    private void KillCharacter(GameObject character) {
        character.GetComponent<Character>().Kill();
    }

    public void SetState(State state) {
        ResetCooldown(true);
        this.state = state;
        SetColor(GetLitStateColor(state));

        background.SetActive(state != State.Empty);
        light.enabled = state != State.Empty;
    }

    private void UpdateColor() {
        Color color = GetStateColor(state);
        Color currentColor = light.color;
        color = Color.Lerp(currentColor, color, Time.deltaTime * flashSpeed);
        SetColor(color);

        //light.intensity = Mathf.Lerp(light.intensity, lightIntensivity, Time.deltaTime * flashSpeed);
    }

    private void SetColor(Color color) {
//        backgroundRenderer1.material.color = color;
//        backgroundRenderer2.material.color = color;
        light.color = color;
    }

    private Color GetStateColor(State state) {
        Color color = Color.clear;
        switch(state) {
            case State.Cloner:
                color = clonerColor;
                break;
            case State.Killer:
                color = killerColor;
                break;
        }
        return color;
    }

    private Color GetLitStateColor(State state) {
        Color color = Color.clear;
        switch(state) {
            case State.Cloner:
                color = clonerLitColor;
                break;
            case State.Killer:
                color = killerLitColor;
                break;
        }
        return color;
    }

    private void ResetCooldown(bool lastChance = false) {
        if(lastChance && affectCooldownCounter < affectCooldown / 2) {
            AffectArea();
        }
        affectCooldownCounter = affectCooldown;
    }
}
