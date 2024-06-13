using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;
    // Start is called before the first frame update
    public List<GameObject> gameObjects;
    public List<Collider2D> list;
    public GameObject parents;
    public List<string> strings;
    public GameObject gameover;
    public GameObject wingame;

    private void Awake()
    {
        strings = new List<string>();
        InvokeRepeating("GameOver", 3, 2);
        InvokeRepeating("Wingame", 3, 2);

    }
    public void leak(string string1)
    {
        strings.Add(string1);
        Debug.Log(string1);
    }
    int random;
    private void GameOver()
    {
        if (parents.transform.childCount > 12)
        {
            Time.timeScale = 0;
            Debug.Log("gameover.....");
            gameover.SetActive(true);

        }
    }

    private void Wingame()
    {
        if (Gamemanager.Insta.Score > 24)
        {
            Time.timeScale = 0;
            Debug.Log("wingame.....");
            wingame.SetActive(true);
        }
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Vector2 touchPosition1 = Camera.main.ScreenToWorldPoint(touch.position);
                    GameObject a = Instantiate(gameObjects[random], parents.transform);
                    a.transform.position = touchPosition1;
                    break;
                case TouchPhase.Moved:
                    Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                    if (hit.collider != null)
                    {
                        //StartCoroutine(Ditheo(hit.collider.transform, touchPosition));
                        hit.collider.transform.Translate(touchPosition * Time.deltaTime);
                    }
                    break;
                case TouchPhase.Ended:
                    random = Random.Range(0, gameObjects.Count);
                    ListGame.Instance.ReplaceChild(random);
                    break;
                case TouchPhase.Stationary:
                    break;

                default:
                    break;
            }
        }
    }


    IEnumerator Ditheo(Transform a, Vector3 pos)
    {
        float elapsedTime = 0;
        float duration = 1.5f;

        while (elapsedTime < duration)
        {
            // Calculate the fraction of time that has passed
            float fraction = elapsedTime / duration;

            // Move the transform towards the target position
            a.position = Vector3.Lerp(a.position, pos, fraction);

            // Increase the elapsed time by the time passed since the last frame
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the final position is exactly the target position
        a.position = pos;
    }
    public void add(Collider2D a)
    {
        if (!list.Contains(a))
        {
            list.Add(a);
        }


        if (list.Count > 2)
        {
            foreach (var b in list)
            {
                if (b.name == "Under (1)" || b.name == "Under") continue;
                Destroy(b); // Destroy the game object attached to the collider
                b.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                b.GetComponent<BoxCollider2D>().enabled = true;
            }
            list.Clear();
            strings.Clear();
        }
        if (a.name == "Under (1)" || a.name == "Under") return;

    }
}
