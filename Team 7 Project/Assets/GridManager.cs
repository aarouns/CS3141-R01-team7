using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Tilemap grid;

    Graph graph;

    private void Awake()
    {
        InitializeGraph();
    }

    private void InitializeGraph()
    {
        graph = new Graph();

        // Adds a node in each cell of the grid
        for(int x = grid.cellBounds.xMin; x < grid.cellBounds.xMax; x++)
        {
            for(int y = grid.cellsBounds.yMin; y < grid.cellBounds.yMax; y++)
            {
                Vector3Int localPosition = new Vector3Int(x, y, (int)grid.transform.postion.y);
                graph.AddNode(worldPosition);
            }
        }

        var allNodes = graph.nodes;

        // Adds an edge for each adjacent node
        foreach(Node from in allNodes)
        {
            foreach(Node to in allNodes)
            {
                if(Vector3.Distance(from.worldPosition, to.worldPosition) < 1f && from != to)
                {
                    graph.AddEdge(from, to);
                }
            }
        }
    }

    // Visualzes nodes and edges for debugging purposes
    private void OnDrawGizmos()
    {
        if(graph == null)
            return;
        
        var allEdges = graph.edges;

        foreach(Edge e in allEdges)
        {
            Debug.DrawLine(e.from.worldPosition, e.to.worldPosition, Color.black, 1);
        }

        var allNodes = graph.nodes;
        foreach(Node n in allNodes)
        {
            Gizmos.color = n.IsOccupied? Color.red : Color.green;
            Gizmos.DrawSphere(n.worldPosition, 0.1f);
        }
    }
}
