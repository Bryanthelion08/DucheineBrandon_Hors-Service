using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Ascenseur : MonoBehaviour
{
    [Header("Joueur")]
    [SerializeField] ControlesJoueur controlesJoueur;
    [SerializeField] CharacterController characterController;
    [SerializeField] FPS_Camera fpsCamera;
    [SerializeField] Animator animatorJoueur;
    [SerializeField] GameObject lampePoche;

    [Header("Caméras 3e personne (Cinemachine)")]
    [SerializeField] GameObject camCabine;


    [Header("Repères")]
    [SerializeField] Transform pointInterieur;
    [SerializeField] Transform pointSortie;

    [Header("Portes")]
    [SerializeField] Porte porteDepart;
    [SerializeField] Porte porteArrivee;

    [Header("UI")]
    [SerializeField] GameObject textes;

    [Header("Écran de la cabine")]
    [SerializeField] TMP_Text texteConsole;
    [SerializeField] string etageDepart = "3";
    [SerializeField] string etageArrivee = "2";

    [Header("Fondu et durées")]
    [SerializeField] Image imageNoire;
    
    [SerializeField] float dureeFondu = 1f;
    [SerializeField] float dureeTrajet = 4f;

    bool enCours = false;
    bool sequenceDebutFaite = false;

    // Séquence A : au début de l'histoire (une seule fois)
    public void LancerSequenceDebut()
    {
        if (enCours == false && sequenceDebutFaite == false)
        {
            sequenceDebutFaite = true;
            StartCoroutine(SequenceDebut());
        }
    }

    // Séquence B : quand le joueur trouve l'élément
    public void LancerSequenceElement()
    {
        if (enCours == false)
        {
            StartCoroutine(SequenceElement());
        }
    }

    // Séquence déclenchée quand le personnage est proche de l'ascenseur
    public void LancerSequenceProximite()
    {
        if (enCours == false && sequenceDebutFaite == false)
        {
            sequenceDebutFaite = true;
            StartCoroutine(SequenceProximite());
        }
    }

    // Séquence finale : le joueur trouve le dernier panneau
    public void LancerSequenceFin()
    {
        if (enCours == false)
        {
            StartCoroutine(SequenceFin());
        }
    }

    IEnumerator SequenceProximite()
    {
        enCours = true;
        controlesJoueur.ChangerEtat(EtatPerso.ASCENSEUR);
        fpsCamera.enabled = false;

        // 1. Les portes s'ouvrent
        yield return StartCoroutine(porteDepart.Ouvrir());

        // 2. Noir, puis dans la cabine (3e personne, sans lampe)
        yield return StartCoroutine(Fondu(1f));
        lampePoche.SetActive(false);
        textes.SetActive(false);
        texteConsole.text = etageDepart;
        Teleporter(pointInterieur);
        camCabine.SetActive(true);  
        yield return StartCoroutine(Fondu(0f));

        // 3. Les portes se ferment, trajet
        yield return StartCoroutine(porteDepart.Fermer());
        yield return new WaitForSeconds(dureeTrajet);
        texteConsole.text = etageArrivee;

        // 4. Noir, puis dans la cabine de l'autre étage (1re personne, lampe)
        yield return StartCoroutine(Fondu(1f));
        camCabine.SetActive(false);
        lampePoche.SetActive(true);
        textes.SetActive(true);
        
        Teleporter(pointSortie);
        yield return StartCoroutine(Fondu(0f));

        // 5. Les portes d'arrivée s'ouvrent, le joueur sort
        yield return StartCoroutine(porteArrivee.Ouvrir());
        FinSequence();
    }

    IEnumerator SequenceDebut()
    {
        enCours = true;
        controlesJoueur.ChangerEtat(EtatPerso.ASCENSEUR);
        fpsCamera.enabled = false;

        // 2. Les portes de départ s'ouvrent
        yield return StartCoroutine(porteDepart.Ouvrir());

        // 3. Fondu noir, puis dans la cabine (3e personne, Idle)
        yield return StartCoroutine(Fondu(1f));
        Teleporter(pointInterieur);
        camCabine.SetActive(true);
        yield return StartCoroutine(Fondu(0f));

        // 4. Trajet, puis les portes d'arrivée s'ouvrent
        yield return new WaitForSeconds(dureeTrajet); 
        yield return StartCoroutine(porteArrivee.Ouvrir());
       
        // 5. Fondu noir, retour en 1re personne
        yield return StartCoroutine(Fondu(1f));
        
        camCabine.SetActive(false);
        Teleporter(pointSortie);
        yield return StartCoroutine(Fondu(0f));

        FinSequence();
    }

    IEnumerator SequenceElement()
    {
        enCours = true;
        controlesJoueur.ChangerEtat(EtatPerso.ASCENSEUR);
        fpsCamera.enabled = false;
        yield return new WaitForSeconds(1.5f);

        // 1. Fondu noir, puis directement dans la cabine (3e personne, Idle)
        yield return StartCoroutine(Fondu(1f));
        texteConsole.text = etageDepart;
        lampePoche.SetActive(false);
        Teleporter(pointInterieur);
        camCabine.SetActive(true);
        yield return StartCoroutine(Fondu(0f));

        // 2. Trajet, puis les portes d'arrivée s'ouvrent
        yield return new WaitForSeconds(dureeTrajet);
        texteConsole.text = etageArrivee;
        yield return StartCoroutine(porteArrivee.Ouvrir());

        // 3. Fondu noir, retour en 1re personne
        yield return StartCoroutine(Fondu(1f));
        camCabine.SetActive(false);
        lampePoche.SetActive(true);
        Teleporter(pointSortie);
        yield return StartCoroutine(Fondu(0f));

        FinSequence();
    }

    IEnumerator SequenceFin()
    {
        enCours = true;
        controlesJoueur.ChangerEtat(EtatPerso.ASCENSEUR);
        fpsCamera.enabled = false;

        yield return new WaitForSeconds(1.5f);
        texteConsole.text = etageArrivee;
        yield return StartCoroutine(Fondu(1f));
        SceneManager.LoadScene("SceneFin");
    }

    void FinSequence()
    {
        fpsCamera.enabled = true;
        controlesJoueur.RetourEtatLibre();
        enCours = false;
    }

    // Téléporter mon perso d'un point à l'autre (Référence: Forum Unity)
    void Teleporter(Transform destination)
    {
        characterController.enabled = false;
        characterController.transform.position = destination.position;
        characterController.transform.rotation = destination.rotation;
        characterController.enabled = true;
    }

    // Fondu au noir , grande aide de la part de Game Code Library sur Youtube 
    IEnumerator Fondu(float alphaFinal)
    {
        float alphaDepart = imageNoire.color.a;
        float temps = 0f;

        while (temps < dureeFondu)
        {
            temps += Time.deltaTime;
            float alpha = Mathf.Lerp(alphaDepart, alphaFinal, temps / dureeFondu);
            imageNoire.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        imageNoire.color = new Color(0f, 0f, 0f, alphaFinal);
    }
}