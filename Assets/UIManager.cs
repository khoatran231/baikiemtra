using UnityEngine;
using UnityEngine.SceneManagement; // <-- ĐẢM BẢO CÓ DÒNG NÀY

public class UIManager : MonoBehaviour
{
    // Chúng ta không cần biến restartPanel nữa, bạn có thể xóa dòng này đi
    // [SerializeField] private GameObject restartPanel; 

    private void OnEnable()
    {
        // Đăng ký lắng nghe sự kiện từ Player
        Player.OnPlayerDeath += LoadGameOverScene; // Đổi tên hàm cho rõ nghĩa
    }

    private void OnDisable()
    {
        // Hủy đăng ký để tránh lỗi
        Player.OnPlayerDeath -= LoadGameOverScene;
    }

    // Hàm này sẽ được gọi khi Player chết
    void LoadGameOverScene()
    {
        // Thay vì kích hoạt một panel, giờ chúng ta tải scene "GameOver"
        SceneManager.LoadScene("GameOver");
    }

    // Hàm RestartGame này không còn cần thiết ở đây nữa vì nút Restart đã nằm trong scene GameOver
    // Bạn có thể xóa nó đi để code gọn hơn.
    /*
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    */
}