using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public int baseDamage = 1;
    public int baseHealth;
    [Range(1, 5)]
    public int range = 1;

    public float attackSpeed = 1f;

    protected Team myTeam;
    protected Node currentNode;

    public void Setup(Team team, Node spawnNode)
    {

        myTeam = team;
        if(myTeam == Team.Team2)
        {
            spriteRenderer.flipX = true;
        }

        this.currentNode = spawnNode;
        transform.position = currentNode.worldPosition;
        currentNode.SetOccupied(true);


    }

}