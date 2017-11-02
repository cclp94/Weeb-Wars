using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformWrapper : MonoBehaviour {



	private void OnTriggerStay2D(Collider2D collision)
	{
		collision.transform.SetParent(transform);
	}

    private void OnTriggerExit2D(Collider2D collision)
	{
		collision.transform.SetParent(null);
	}
}
