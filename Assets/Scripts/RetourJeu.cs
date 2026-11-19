using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetourJeu : MonoBehaviour
{
    [SerializeField] float delai = 5f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delai);
        SceneManager.LoadScene("SceneJeux");
    }
}