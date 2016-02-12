using UnityEngine;
using System.Collections;

public class Tester : MonoBehaviour {

    public string url = "http://thecatapi.com/api/images/get?type=jpg";

    IEnumerator Start() {
        // Start a download of the given URL
        WWW www = new WWW(url);

        // Wait for download to complete
        yield return www;

        // assign texture
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = www.texture;
    }
}
