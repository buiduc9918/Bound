using TMPro;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text; // Reference to the UI Text component
    private int _score; // Backing field for the Score property

    // Method to add to the score
    public static Gamemanager Insta;

    private void Awake()
    {
        Insta = this;
    }
    public void AddScore(int x)
    {
        Score += x; // Use the property to ensure the UI text is updated
    }

    // Property to get and set the score
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            UpdateScoreText(); // Update the UI text whenever the score is set
        }
    }

    // Method to update the UI text
    private void UpdateScoreText()
    {
        if (text != null)
        {
            text.text = $"{_score}";
        }
    }

}