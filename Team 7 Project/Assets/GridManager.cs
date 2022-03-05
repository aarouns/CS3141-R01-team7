using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : Manager<GridManager>
{
    public Tilemap grid;

    protected Graph graph;

    protected Dictionary<Team, int> startPositionPerTeam;

    public Node GetFreeNode(Team forTeam)
    {

        int startIndex = startPositionPerTeam[forTeam];
        int currentIndex = startIndex;

        while(graph.nodes[currentIndex].isOccupied)
        {
            if (startIndex == 0)
            {
                currentIndex++;
                if(currentIndex == graph.nodes.Count)
                {
                    return null;
                }
            }
            else
            {
                currentIndex--;
                if(currentIndex == -1)
                return null;
            }


        }

        return graph.nodes[currentIndex];

    }

    private void Awake()
    {
        base.Awake();
        InitializeGraph();
        startPositionPerTeam = new Dictionary<Team, int>();
        startPositionPerTeam.Add(Team.Team1, 0);
        startPositionPerTeam.Add(Team.Team2, graph.nodes.Count - 1);

    }

    private void InitializeGraph()
    {
        graph = new Graph();

        // Adds a node in each cell of the grid
        for(int x = grid.cellBounds.xMin; x < grid.cellBounds.xMax; x++)
        {
            for(int y = grid.cellBounds.yMin; y < grid.cellBounds.yMax; y++)
            {
                Vector3Int localPosition = new Vector3Int(x, y, (int)grid.transform.position.y);

                if(grid.HasTile(localPosition)){

                    Vector3 worldPosition = grid.CellToWorld(localPosition);
                    graph.AddNode(worldPosition);

                }
                
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

    public int fromIndex = 0;
    public int toIndex = 0;


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
            Gizmos.color = n.isOccupied? Color.red : Color.green;
            Gizmos.DrawSphere(n.worldPosition, 0.1f);
        }

        if(fromIndex < allNodes.Count && toIndex < allNodes.Count){
            List<Node> path = graph.GetPath(allNodes[fromIndex], allNodes[toIndex]);
            if(path.Count > 1){
                for(int i = 1; i < path.Count; i++){
                    Debug.DrawLine(path[i-1].worldPosition, path[i].worldPosition, Color.red);
                }
            }
        }
    }
}
