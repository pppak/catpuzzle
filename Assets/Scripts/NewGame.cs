using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {

    public void StartNewGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
