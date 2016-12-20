﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroupScript : MonoBehaviour {

    public bool inGroup = false;
    public bool IAmTheLeader = false;

    public GameObject groupLeader;

    public List<GameObject> groupMembers;

    private int numberMembersGroup = 0;

    private float maxDistGroup = 500f;

    private Color originalColor;

    void Start()
    {
        groupMembers = new List<GameObject>();
        groupLeader = this.gameObject;
        originalColor = this.gameObject.GetComponent<SpriteRenderer>().color;

    }
  /*  void OnDrawGizmos()
    {
        if (groupMembers.Count > 0 && IAmTheLeader)
        {
            Gizmos.color = Color.white;
            Gizmos.color *= Random.Range(0f, 1f);
            for (int i = 1; i < groupMembers.Count; i++)
            {
                Gizmos.DrawLine(groupMembers[i - 1].transform.position+Vector3.up*3, groupMembers[i].transform.position + Vector3.up * 3);
            }
        }
    }*/
	
    public void FollowTheLeaderAttack()
    {
        for (int i = 0; i < numberMembersGroup; i++) {
            groupMembers[i].AddComponent<ActionAttack>();
        }

    }

    public void updateGroups(GameObject elQueseUne, List<GameObject> ysugrupo)
    {    
        foreach (var members in ysugrupo)
        {
            addSingleMember(members);
            var currentGroup = members.GetComponent<GroupScript>();
            currentGroup.groupLeader = this.gameObject;
        }
            addSingleMember(elQueseUne);
            if (elQueseUne.tag != "Player")
            {
                elQueseUne.GetComponent<VisibilityConeCycleIA>().enabled = false;
            }

    }

    public void resetMembersOfGroups(SpriteRenderer renderer)
    {

        foreach (var member in groupMembers)
        {
            var currentGroup = member.GetComponent<GroupScript>();
            currentGroup.groupMembers.Clear();
            currentGroup.groupMembers.AddRange(copyGroup());
            currentGroup.groupMembers.Remove(member);
            currentGroup.addSingleMember(this.gameObject);
            member.GetComponent<SpriteRenderer>().color = renderer.color;
        }

    }
    public void leaveGroup(GameObject component)
    {
      /*  foreach (var members in groupMembers)
        {
            members.GetComponent<GroupScript>().removeSingleMember(component);
        }
        if (groupMembers.Count > 0)
        {
            groupMembers[0].GetComponent<GroupScript>().makeLeader();
        }
        removeSingleMember(component);
        groupLeader = null;
        inGroup =IAmTheLeader= false;
        groupMembers.Clear();*/

    }
    public void addSingleMember(GameObject component)
    {
        groupMembers.Add(component);
    }

    public void removeSingleMember(GameObject component)
    {
        groupMembers.Remove(component);
    }

    public bool checkIAInGroup(GameObject IA)
    {
        return groupMembers.Contains(IA);
    }

    public List<GameObject> copyGroup()
    {
        return groupMembers;
    }

    public void makeLeader()
    {
        if (!inGroup) inGroup = true;
        if (!IAmTheLeader)
        {
            IAmTheLeader = true;
            groupLeader = this.gameObject;
        }
        foreach (var members in groupMembers)
        {
            members.GetComponent<GroupScript>().groupLeader = this.gameObject;
            members.GetComponent<GroupScript>().IAmTheLeader = false;
        }
    }

    void Update()
    {
        if(Vector2.Distance(this.gameObject.transform.position, groupLeader.transform.position) > maxDistGroup)
        {
            ExitGroup();

        }

    }

    public void ExitGroup() {
        List<GameObject> members = groupLeader.GetComponent<GroupScript>().groupMembers;
        foreach(var m in members)
        {
			
            m.GetComponent<GroupScript>().groupMembers.Remove(this.gameObject);

        }
        GroupScript leaderGroup = groupLeader.GetComponent<GroupScript>();
        leaderGroup.groupMembers.Remove(this.gameObject);
        if (leaderGroup.groupMembers.Count==0)
        {
            leaderGroup.groupLeader = leaderGroup.gameObject;
            leaderGroup.inGroup = false;
            leaderGroup.IAmTheLeader = false;
        }
        groupLeader = this.gameObject;
        inGroup = false;
        groupMembers.Clear();
        this.gameObject.GetComponent<SpriteRenderer>().color = originalColor;

        if (this.gameObject.tag != "Player")
        {
            GetComponent<VisibilityConeCycleIA>().enabled = true;
        }


    }
}
