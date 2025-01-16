using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
public class StartGame : MonoBehaviour
{
    float progress = 0;
    public Text loadingText;
    IEnumerator Start()
    {
       while(progress < 100)
        {
            progress +=90 * Time.deltaTime;
            loadingText.text = "game comming . " + progress.ToString("0") + "%";
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

   
}
