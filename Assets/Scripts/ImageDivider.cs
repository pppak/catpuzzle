using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ImageDivider : MonoBehaviour {
    
    public Transform piece;
    private int rows = 3;

    public GameObject loadingText;
    public GameObject sourceButton;

    public string url = "http://thecatapi.com/api/images/get?type=jpg";

    private string imageSourceUrl;

    IEnumerator Start() {
        rows = Random.Range(3, 7);

        // Start a download of the given URL
        WWW www = new WWW(url);

        // Wait for download to complete
        yield return www;

        // assign texture
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = www.texture;

        //sets in-game image size to match the image fetched
        transform.localScale = new Vector3(www.texture.width, www.texture.height, 1);

        setCameraSize(www);
        DivideIntoPieces();

        loadingText.SetActive(false);
        sourceButton.SetActive(true);

        imageSourceUrl = www.responseHeaders["LOCATION"];
    }

    public void SourceOpener() {
        Application.OpenURL(imageSourceUrl);
    }

    private void setCameraSize(WWW www) {
        if (Screen.width / (float)Screen.height < transform.localScale.x / transform.localScale.y) {
            Camera.main.orthographicSize = www.texture.width * (Screen.height / (float)Screen.width) * 0.5f;
        }
        else {
            Camera.main.orthographicSize = www.texture.height * 0.5f;
        }
    }

    private void DivideIntoPieces () {
        float pieceHeight = transform.localScale.y / rows;
        int columns = (int)(transform.localScale.x / pieceHeight);
        float surplusWidth = transform.localScale.x - (columns * pieceHeight);
        float pieceWidth = pieceHeight + (surplusWidth / columns);

        Vector3 imageCenter = transform.position;
        float startX = imageCenter.x - pieceWidth * (columns * 0.5f);
        float startY = imageCenter.y + pieceHeight * (rows * 0.5f);
        startX += pieceWidth * 0.5f;
        startY -= pieceHeight * 0.5f;

        Material originalMat = GetComponent<MeshRenderer>().material;

        float xTiling = pieceWidth / transform.localScale.x;
        float yTiling = pieceHeight / transform.localScale.y;

        for (int columnIndex = 0; columnIndex < columns; columnIndex++){
            for (int rowIndex = 0; rowIndex < rows; rowIndex++){
                Transform newPiece = Instantiate(piece) as Transform;
                newPiece.rotation = transform.rotation;
                newPiece.localScale = new Vector3(pieceWidth, pieceHeight, 1);
                newPiece.position = new Vector3(startX + (pieceWidth * columnIndex), startY - (pieceHeight * rowIndex));

                MeshRenderer newPieceRenderer = newPiece.GetComponent<MeshRenderer>();
                newPieceRenderer.material = originalMat;
                float yOffset = (rows - 1 - rowIndex) / (float)rows;
                float xOffset = columnIndex / (float)columns;
                newPieceRenderer.material.SetTextureOffset("_MainTex", new Vector2(xOffset, yOffset));
                newPieceRenderer.material.SetTextureScale("_MainTex", new Vector2(xTiling, yTiling));
            }
        }

        gameObject.SetActive(false);
    }	
}