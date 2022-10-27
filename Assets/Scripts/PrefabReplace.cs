using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabReplace : MonoBehaviour
{
	[SerializeField] GameObject prefabToReplaceWith;
	void Awake()
	{
		Instantiate(prefabToReplaceWith, transform.position, transform.rotation);
		Destroy(gameObject);
	}

}
