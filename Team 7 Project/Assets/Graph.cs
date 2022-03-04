using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public List<Node> nodes;
    public List<Edge> edges;
    
    public Graph()
    {
        nodes = new List<Node>();
        edges = new List<Edge>();
    }

    public void AddNode(Vector3 worldPosition)
    {
        nodes.Add(new Node(nodes.Count, worldPosition));
    }

    public void AddEdge(Node from, Node to)
    {
        edges.Add(new Edge(from, to, Vector3.Distance(from.worldPosition, to.worldPosition)));
    }

    // Returns true if the two nodes are adjacent
    public bool Adjacent(Node from, Node to)
    {
        foreach(Edge e in edges)
        {
            if (e.from == from && e.to == to)
                return true;
        }

        return false;
    }

    // Returns a list of all neighbors of the given edge
    public List<Node> Neighbors(Node of)
    {
        List<Node> result = new List<Node>();

        foreach(Edge e in edges)
        {
            if (e.from == of)
                result.Add(e.to);
        }

        return result;
    }

    // Returns the distance (weight) between the two edges
    public float Distance(Node from, Node to)
    {
        foreach(Edge e in edges)
        {
            if (e.from == from && e.to == to)
                return e.GetWeight();
        }

        return Mathf.Infinity;
    }
}

// The spot in which an entity is positioned on
public class Node
{
    public int index;
    public Vector3 worldPosition; // 2D position on the map

    private bool occupied = false;
    public bool isOccupied => occupied; // Returns true if a node is occupied

    // Constructor
    public Node(int index, Vector3 worldPosition)
    {
        this.index = index;
        this.worldPosition = worldPosition;
        occupied = false;
    }

    // Sets the value of the occupied boolean
    public void SetOccupied(bool val)
    {
        occupied = val;
    }
}

// The connection between two nodes
public class Edge
{
    public Node from;
    public Node to;

    private float weight; // might not be needed, but much would need to be editied if this is removed

    // Constructor
    public Edge(Node from, Node to, float weight)
    {
        this.from = from;
        this.to = to;
        this.weight = weight;
    }

    // Returns and infinite weight if occupied, and the weight if not.
    public float GetWeight()
    {
        if(to.isOccupied)
        {
            return Mathf.Infinity;
        }

        return weight;
    }
}