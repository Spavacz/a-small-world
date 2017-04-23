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

    public GameController gameController;

    public ContactFilter2D contactFilter;
    public int maxDestroyed = 5;
    public int maxCloned = 3;
    public float flashSpeed = 1f;

    [Tooltip("In seconds")]
    public float affectCooldown = 1f;
    [Tooltip("In seconds")]
    public float morphCooldown = 5f;

    public Color clonerColor;
    public Color clonerLitColor;
    public Color killerColor;
    public Color destroyerLitColor;
    public Color emptyColor;

    public Light light;
    public float lightIntensivity;
    public float glowLightIntensivity;

    private Collider2D collider2d;

    private float affectCooldownCounter;

    public State state;

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
        //SetColor(GetLitStateColor(state));
        // todo glow more
        Glow();

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
        SetColor(GetStateColor(state));
    }

    private void UpdateColor() {
        Color color = GetStateColor(state);
        Color currentColor = light.color;
        color = Color.Lerp(currentColor, color, Time.deltaTime * flashSpeed);
        SetColor(color);

        light.intensity = Mathf.Lerp(light.intensity, lightIntensivity, Time.deltaTime * flashSpeed);
    }

    private void SetColor(Color color) {
//        backgroundRenderer1.material.color = color;
//        backgroundRenderer2.material.color = color;
        light.color = color;
    }

    private Color GetStateColor(State state) {
        Color color = Color.clear;
        switch(state) {
            case State.Empty:
                color = emptyColor;
                break;
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
        if(state == State.Cloner) {
            color = clonerLitColor;
        } else if(state == State.Killer) {
            color = destroyerLitColor;
        }
        return color;
    }

    private void Glow() {
        light.intensity = glowLightIntensivity;
    }

    private void ResetCooldown(bool lastChance = false) {
        if(lastChance && affectCooldownCounter < affectCooldown / 2) {
            AffectArea();
        }
        affectCooldownCounter = affectCooldown;
    }
}
