﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterTime : MonoBehaviour {

    public float timeToDeath;

	// Use this for initialization
	void Start () {
        StartCoroutine(Death());
	}
	
	IEnumerator Death()
    {
        yield return new WaitForSeconds(timeToDeath);
        Destroy(gameObject);
    }
}
