using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public GameObject cube;

    //Make some cubes to pick up
	void Start () {
        for (int i = -100; i < 100; i+=6)
        {
            for (int j = -100; j < 100; j+=6)
            {
                GameObject go = Instantiate(cube, new Vector3(i, 3, j), Quaternion.identity);
                go.transform.SetParent(transform);
            }
        }
	}
}
