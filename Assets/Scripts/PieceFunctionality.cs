using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PieceFunctionality : MonoBehaviour {

    private static bool gameWon;
    public Color bgColor;

	void Start () {
        gameWon = false;

        rotatePieceAtRandom();
	}

    void OnMouseDown(){
        if (gameWon) {
            return;
        }
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        transform.Rotate(Vector3.back * 90);
        transform.localScale = new Vector3(transform.localScale.y, transform.localScale.x, 1);
        CheckWin();
    }

    private void rotatePieceAtRandom() {
        int turns = Random.Range(0, 4);
        transform.Rotate(Vector3.back * (turns * 90));

        if (turns % 2 != 0) {
            transform.localScale = new Vector3(transform.localScale.y, transform.localScale.x, 1);
        }
    }

    private void CheckWin(){
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("Piece");        
        for (int i = 0; i < pieces.Length; i++) {
            float pieceRotation = pieces[i].transform.eulerAngles.z;
            //Angle is sometimes not exactly zero even if piece is in correct position
            if (pieceRotation > 45 && pieceRotation < 315) {
                return;
            }
        }
        gameWon = true;
        GameObject.FindGameObjectWithTag("Victory Text").GetComponent<Text>().enabled = true;
        Camera.main.backgroundColor = bgColor;
    }
}
