using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarNode : MonoBehaviour
{
    [SerializeField] GameObject[] _starPrefabs;
    GameObject _spawnedStar;

    private void Start()
    {
        GameController.Instance.onRoundEnd += RandomizeStar;
        RandomizeStar();
    }

    public void RandomizeStar()
    {
        if(_spawnedStar != null)
        {
            Destroy(_spawnedStar);
        }

       _spawnedStar = Instantiate(_starPrefabs[Random.Range(0, _starPrefabs.Length)], transform.position, Quaternion.identity);
    }
}
