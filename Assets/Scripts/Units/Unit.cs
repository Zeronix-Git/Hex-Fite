using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit {
    private UnitData unitData;

    private float currentHP;

    private Guid unitId;

    private Vector3Int pos;

    private bool turnEnded;

    private List<Action> actions;

    public Unit(UnitData unitData) {
        this.unitData = unitData;
        this.currentHP = unitData.maxHP;
        this.unitId = Guid.NewGuid();
        actions = new List<Action>();
        foreach (Action action in unitData.actions) {
            actions.Add(action.CreateInstance(this));
        }
    }

    public Unit(UnitData unitData, Vector3Int pos) {
        this.unitData = unitData;
        this.currentHP = unitData.maxHP;
        this.unitId = Guid.NewGuid();
        this.pos = pos;
        actions = new List<Action>();
        foreach (Action action in unitData.actions) {
            actions.Add(action.CreateInstance(this));
        }
    }

    public String getName() {
        return unitData.name;
    }

    public Guid getUnitId() {
        return unitId;
    }

    public void setUnitId(Guid id) {
        this.unitId = id;
    }

    public float getCurrentHp() {
        return currentHP;
    }

    public void setCurrentHp(float hp) {
        this.currentHP = hp;
    }

    //TODO: implement different attack types, target types, etc. and update this. For now, we can attack always
    public bool canAttack(Unit targetUnit) {
        if (targetUnit != null) {
            return true;
        }
        return false;
    }

    public int getAttackRange() {
        return 1;
    }

    public int getMovementRange() {
        return unitData.movement.movementCost;
    }

    public bool isTurnEnded() {
        return turnEnded;
    }

    public void startTurn() {
        this.turnEnded = false;
    }

    public void endTurn() {
        this.turnEnded = true;
    }

    public void setTurnEnded(bool turnEnded) {
        this.turnEnded = turnEnded;
    }

    public List<Action> getActions() {
        return this.actions;
    }
}
