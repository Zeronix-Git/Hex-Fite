using System;
using UnityEngine;

public class Unit {
    private UnitData unitData;

    private float currentHP;

    private Guid unitId;

    private Vector3Int pos;

    public Unit(UnitData unitData) {
        this.unitData = unitData;
        this.currentHP = unitData.maxHP;
        this.unitId = Guid.NewGuid();
    }

    public Unit(UnitData unitData, Vector3Int pos) {
        this.unitData = unitData;
        this.currentHP = unitData.maxHP;
        this.unitId = Guid.NewGuid();
        this.pos = pos;
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
}
