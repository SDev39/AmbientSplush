using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkPaintTargetGenerator : MonoBehaviour
{
	[SerializeField]
	private GameObject inkPaintTargetPrefab;
    // Start is called before the first frame update
   public void CreateTarget()
   {
	   StartCoroutine(TargetCorountine());
   }

   private IEnumerator TargetCorountine()
    {
        GameObject newTouch = Instantiate(inkPaintTargetPrefab, Vector3.zero, Quaternion.identity);
        newTouch.transform.parent = transform;
        yield return null;
        newTouch.SetActive(true);
    }
	public void ClearAllMeshs()
	{
		foreach (Transform child in transform) {
			GameObject.Destroy(child.gameObject);
		}
	}
}
