﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIPersonality: MonoBehaviour {

    public int health = 0;
    public int attack;
    public int confidence;

    public float charisma=3;
    public float selfAssertion=2; // supongo que esto es agresividad para los arboles de decisiones ¿?
    public float fear=4;

    public bool isMonster = false; // MOCK
    public bool inGroup = false; //MOCK
    public GameObject groupLeader; // al inicio esto señala a si mismo para evitar problemas en una decision :)
    public ObjectHandler.ObjectType myObject;

    public ActionsEnum.Actions interactionFromOtherCharacter;
    public int[] TrustInOthers;

    public int MyOwnIndex;

    private Memory myMemory;
    private GameObject rememberedMedicalaid;

    void Start()
    {
        TrustInOthers= new int[5]; // 5 characters
        myMemory = GetComponent<Memory>();
        
       // interactionFromOtherCharacter = ActionsEnum.Actions.ATTACK;
        initializeTrustInOthers();

    }
    public void SetInteraction(ActionsEnum.Actions a)
    {
        interactionFromOtherCharacter = a;
    }
    public void SetMyOwnIndex(int i) {
        MyOwnIndex = i;
    }
    public int GetMyOwnIndex() { return MyOwnIndex; }


    private void initializeTrustInOthers() {
        for (int i = 0; i < 5; i++) TrustInOthers[i] =4;
    }
    public ActionsEnum.Actions GetInteraction() { return interactionFromOtherCharacter; }

    void update()
    {
        if(health < 20)
        {
            rememberedMedicalaid = myMemory.SearchInMemory("MEDICALAID");
            if (rememberedMedicalaid != null)
            {
                //moverse hacia alli
            }
        }
    }
}
