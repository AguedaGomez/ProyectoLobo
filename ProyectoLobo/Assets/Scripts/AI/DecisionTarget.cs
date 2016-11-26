﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DecisionTarget : MonoBehaviour {

    //private AIPersonality personality;
    private Dictionary<GameObject, int> analyzedTargets;
    private PriorityTree priorityTree;
    private Memory memory;

    public GameObject AI;

    public bool IDecided = false;

    void Awake() {

        analyzedTargets = new Dictionary<GameObject, int>();
        priorityTree = new PriorityTree();
        memory = AI.GetComponent<Memory>();


	}


    /// <summary>
    /// Devuelve el GAmeObject, que la IA NO TIENE, más prioritario
    /// </summary>
    /// <param name="viewedTargets"></param>
    /// <param name="Ai"></param>
    /// <returns></returns>

    public GameObject ChooseTarget(List<GameObject> viewedTargets, GameObject Ai)
    {
        int priority;
        int currentTargetpriority;
        string nameCurrentTarget;
        GameObject chosenTarget = null;
        GameObject currentTarget = null;
        AIPersonality personality = Ai.GetComponent<AIPersonality>();


        foreach (GameObject target in viewedTargets)
        {
            priority = priorityTree.GetPriority(target, personality); // Llama al árbol de prioridad que devuelve la prioridad de ese GameObject
            //Debug.Log("La prioridad de " + target + " es " + priority);
            if (!analyzedTargets.ContainsKey(target))
                analyzedTargets.Add(target, priority);
        }

        chosenTarget = GivePriorityTarget(analyzedTargets); // Recoge el GameObject más prioritario
        nameCurrentTarget = objectTraduction(personality); // Mira qué objeto lleva en ese momento la IA

        if ((chosenTarget.tag == "IA" || chosenTarget.tag=="Player") && !IDecided)
        {
            IDecided = true;
            analyzedTargets.Clear();

            //active tree

            this.GetComponent<DecisionTreeISeeSomeoneWhatShouldIDo>().target = chosenTarget;
            this.GetComponent<DecisionTreeISeeSomeoneWhatShouldIDo>().StartTheDecision();

            return chosenTarget;
        }
        else if( nameCurrentTarget != "NONE")
        {
            string aux = "Prefabs/Objects/";
            aux += nameCurrentTarget;
            currentTarget = Resources.Load(aux) as GameObject;
            
            currentTargetpriority = priorityTree.GetPriority(currentTarget, personality);

            if (currentTargetpriority > analyzedTargets[chosenTarget])
            {
                analyzedTargets.Clear();
                chosenTarget = null;
            }
            else
            {
                if (chosenTarget.name == nameCurrentTarget) // Si lleva un objeto y es el que ha visto más prioritario: ese objeto se elimina del diccionario y se recoge el siguiente con más prioridad
                {
                    analyzedTargets.Remove(chosenTarget);
                    chosenTarget = GivePriorityTarget(analyzedTargets);
                    analyzedTargets.Clear();
                }

            }

            return chosenTarget;

        }
        else
        {
            analyzedTargets.Clear();
            return chosenTarget;

        }
        
    }
    /// <summary>
    /// Determina el objeto más prioritario que hay en el diccionario
    /// </summary>
    /// <param name="analyzedTargets"></param>
    /// <returns></returns>
    private GameObject GivePriorityTarget(Dictionary<GameObject, int> analyzedTargets)
    {
        int maxPriority = -1;
        GameObject chosenTarget = null;
        foreach (KeyValuePair<GameObject, int> par in analyzedTargets)
        {
            if (par.Value > maxPriority)
            {
                maxPriority = par.Value;
                chosenTarget = par.Key;
                
            }
            if (!memory.objectsSeenBefore.ContainsKey(par.Key.name))
            {
                memory.objectsSeenBefore.Add(par.Key.name, par.Key);
            }

        }
        return chosenTarget;

    }
    /// <summary>
    /// Traduce a una string el objeto que tiene la IA para poder compararlo con el nombre
    /// del GameObject más prioritario y saber así si ya lo tiene o no
    /// </summary>
    /// <param name="personality"></param>
    /// <returns></returns>

    public string objectTraduction (AIPersonality personality)
    {
        if (personality.myObject == ObjectHandler.ObjectType.AXE)
            return "Axe";
        else if (personality.myObject == ObjectHandler.ObjectType.BOOTS)
            return "Boots";
        else if (personality.myObject == ObjectHandler.ObjectType.FLASHLIGHT)
            return "Flashlight";
        else if (personality.myObject == ObjectHandler.ObjectType.JUMPSUIT)
            return "Jumpsuit";
        else if (personality.myObject == ObjectHandler.ObjectType.MEDICALAID)
            return "MedicalAid";
        else if (personality.myObject == ObjectHandler.ObjectType.SHIELD)
            return "Shield";
        else
            return "NONE";


    }
}
