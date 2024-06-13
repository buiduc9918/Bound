using System.Collections.Generic;
using UnityEngine;

public class ListGame : MonoBehaviour
{
    public List<GameObject> prefabs;
    public static ListGame Instance;
    public GameObject father;
    public void Awake()
    {
        Instance = this;
    }
    public void ReplaceChild(int index)
    {
        // Check if the index is within the bounds of the prefabs list
        if (index < 0 || index >= prefabs.Count)
        {
            Debug.LogWarning("Index out of range: " + index);
            return;
        }

        // Destroy all existing child objects
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate the new prefab and set its parent to this transform
        GameObject x = Instantiate(prefabs[index], transform);

        // Ensure the new child is set up as required
        Rigidbody2D rb = x.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        CircleCollider2D bc = x.GetComponent<CircleCollider2D>();
        if (bc != null)
        {
            bc.enabled = false;
        }
        Destroy(gameObject.transform.GetChild(0));
    }

}
