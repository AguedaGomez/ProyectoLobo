﻿using UnityEngine;
using System.Collections;

public class DecisionTreeNode : MonoBehaviour {
    [HideInInspector]    public bool activated = false;
    public DecisionTreeNode nodeTrue;
    public DecisionTreeNode nodeFalse;
    // Use this for initialization
    public virtual void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public virtual DecisionTreeNode MakeDecision() { return null; }


}
