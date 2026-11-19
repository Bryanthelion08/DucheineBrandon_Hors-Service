using UnityEngine;

public class PorteAutomatique : MonoBehaviour
{
    [SerializeField] Ascenseur ascenseur;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ascenseur.LancerSequenceProximite();
        }
    }
}
