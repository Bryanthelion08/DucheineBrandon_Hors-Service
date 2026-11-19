using UnityEngine;

public class Interrupteur : MonoBehaviour
{
    [SerializeField] Ascenseur ascenseur;
    [SerializeField] bool panneauFinal = false;
    bool dejaActive = false;

    public void Activer()
    {
        Debug.Log("Panneau activé");
        if (dejaActive == false)
        {
            dejaActive = true;

            if (panneauFinal == true)
            {
                ascenseur.LancerSequenceFin();
            }
            else
            {
                ascenseur.LancerSequenceElement();
            }
        }
    }
}