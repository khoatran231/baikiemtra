using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // Hàm này sẽ được gọi bởi nút "Chơi Lại"
    public void RestartGame()
    {
        // Tải lại màn chơi. 
        // Giả sử màn chơi của bạn tên là "gamepixel". Hãy thay đổi nếu tên khác.
        // Hoặc bạn có thể dùng chỉ số, ví dụ SceneManager.LoadScene(1);
        SceneManager.LoadScene("gamepixel");
    }

    // Hàm này sẽ được gọi bởi nút "Về Menu Chính"
    public void GoToMainMenu()
    {
        // Tải lại scene Menu chính
        SceneManager.LoadScene("MainMenu");
    }
}