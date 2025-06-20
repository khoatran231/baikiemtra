using UnityEngine;
using TMPro; // Hoặc UnityEngine.UI nếu bạn dùng Text thường

public class ScoreUI : MonoBehaviour
{
    // Kéo thả đối tượng Text từ Hierarchy vào đây trong Inspector
    public TextMeshProUGUI scoreText; // Hoặc public Text scoreText;

    private void Start()
    {
        // Đăng ký lắng nghe sự kiện và cập nhật điểm lần đầu
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged += UpdateScoreText;
            UpdateScoreText(ScoreManager.Instance.CurrentScore);
        }
    }

    private void OnDestroy()
    {
        // Hủy đăng ký để tránh lỗi khi đối tượng bị hủy
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateScoreText;
        }
    }

    private void UpdateScoreText(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + newScore;
        }
    }
}