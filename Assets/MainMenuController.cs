using UnityEngine;
using UnityEngine.SceneManagement; // Rất quan trọng: Phải có dòng này để dùng SceneManager

public class MainMenuController : MonoBehaviour
{
    // Hàm này sẽ được gọi khi nhấn nút Start
    public void StartGame()
    {
        // Tải scene có tên là "gamepixel"
        SceneManager.LoadScene("gamepixel");
        Debug.Log("Chuyển sang scene gamepixel...");
    }

    // Hàm này sẽ được gọi khi nhấn nút Quit
    public void QuitGame()
    {
        // In ra console để kiểm tra trong Editor
        Debug.Log("Thoát game!");

        // Dòng lệnh này chỉ hoạt động khi game đã được build thành file .exe, .apk...
        // Nó không có tác dụng trong Unity Editor.
        Application.Quit();
    }
}