using System.Collections;
using UnityEngine;


public class Porte : MonoBehaviour
{
    [SerializeField] Animation animationPorte;
    [SerializeField] Animation animationPorte02;
    [SerializeField] string nomOuverture = "Portes@Ouverture";
    [SerializeField] string nomFermeture = "Portes@Fermeture";
    [SerializeField] string nomOuverture02 = "Portes@Ouverture02";
    [SerializeField] string nomFermeture02 = "Portes@Fermeture02";
    [SerializeField] float dureeAnimation = 1f;

    bool estOuverte = false;

    public IEnumerator Ouvrir()
    {
        if (estOuverte == false)
        {
            estOuverte = true;
            animationPorte.Play(nomOuverture);
            animationPorte02.Play(nomOuverture02);
            yield return new WaitForSeconds(dureeAnimation);
        }
    }

    public IEnumerator Fermer()
    {
        if (estOuverte == true)
        {
            estOuverte = false;
            animationPorte.Play(nomFermeture);
            animationPorte02.Play(nomFermeture02);
            yield return new WaitForSeconds(dureeAnimation);
        }
    }
}