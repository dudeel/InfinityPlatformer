using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.SetText(player.distance.ToString("0.0") + " m");
    }
}
