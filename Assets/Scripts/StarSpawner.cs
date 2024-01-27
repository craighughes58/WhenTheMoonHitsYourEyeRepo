using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [Header("Star Spawn Values")]
    [SerializeField] private float _starsToSpawn;
    [Tooltip("Ends the Star spawn after failing to place a start this many times")]
    [SerializeField] private float _maxAttempts;
    [Tooltip("Stars must be at least this far apart from each other")]
    [SerializeField] private float _minStarDist;
    [SerializeField] private GameObject _starNode;
    [SerializeField] private Vector2 _xMinMax;
    [SerializeField] private Vector2 _yMinMax;

    public void SpawnAllStars()
    {
        TrySpawnStar(null);
        //Try to spawn star root star
        //If can't, try again
        //If can't after X Attempts
        //end
        //If Can
        //Call this function again with a root


    }


    StarSpawnNode TrySpawnStar(StarSpawnNode parent)
    {
        Vector2 starPos = Vector2.zero;
        if (parent != null) starPos += parent.pos;
        else starPos = new Vector2((_xMinMax.x + _xMinMax.y) /2, (_yMinMax.x + _yMinMax.y)/2);

        starPos += (Random.insideUnitCircle.normalized * _minStarDist);

        if (starPos.x < _xMinMax.x || starPos.x > _xMinMax.y || starPos.y < _yMinMax.x || starPos.y > _yMinMax.y) return null;

        RaycastHit2D hit = Physics2D.CircleCast(starPos, _minStarDist - .15f, Vector2.zero);

        if (hit.transform != null) return null;

        Instantiate(_starNode, starPos, Quaternion.identity);

        StarSpawnNode thisNode = new StarSpawnNode(parent, starPos);
        int attempts = 0;
        while(attempts < _maxAttempts)
        {
            ++attempts;
            TrySpawnStar(thisNode);
        }

        return thisNode;
    }

}


public class StarSpawnNode
{
    public StarSpawnNode parent;
    public Vector2 pos;

    public StarSpawnNode(StarSpawnNode parent, Vector2 pos)
    {
        this.parent=parent;
        this.pos = pos;
    }
}
