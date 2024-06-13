using System.Collections.Generic;
using UnityEngine;

public class Finds : MonoBehaviour
{
    public int number;
    public Collider2D[] hit;
    public List<Collider2D> colliders;
    private float time = 0;

    private void Start()
    {
        colliders = new List<Collider2D>();
    }

    private void Update()
    {
        time += Time.deltaTime;

        Vector2 touchPosition = transform.position;
        float radius = 3.5f; // Bán kính kiểm tra

        if (transform.position.y < -14)
        {
            Destroy(gameObject);
        }
        // Tìm tất cả các collider trong bán kính xung quanh touchPosition
        hit = Physics2D.OverlapCircleAll(touchPosition, radius);

        if (time >= 1f)
        {
            bool colliderAdded = false;

            foreach (var hitCollider in hit)
            {
                // Chỉ thêm collider nếu nó chưa có trong danh sách
                if (!colliders.Contains(hitCollider) && hitCollider.GetComponent<Finds>().number == number)
                {
                    colliders.Add(hitCollider);
                    colliderAdded = true;
                }
            }
            if (!colliderAdded && colliders.Count > 2)
            {
                foreach (var hit in colliders)
                {

                    hit.GetComponent<CircleCollider2D>().enabled = false;
                }
                Gamemanager.Insta.AddScore(3);
            }

            time = 0;
        }

    }
}
