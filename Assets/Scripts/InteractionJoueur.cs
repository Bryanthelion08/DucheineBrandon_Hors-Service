using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InteractionJoueur : MonoBehaviour
{
    [Header("Raycast d'interaction")]
    [SerializeField] float distanceInteraction = 2f; // Distance maximale pour interagir avec un interrupteur
    [SerializeField] LayerMask couchesInteractives; // Couches sur lesquelles le joueur peut


    [Header("Composants")]
    ControlesJoueur controles;
    Animator animator;

    void Start()
    {
        controles = GetComponent<ControlesJoueur>();
        animator = GetComponent<Animator>();
    }


    void OnDrawGizmos()
    {
        // Dessiner un rayon pour visualiser la distance d'interaction dans l'éditeur
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position + Vector3.up, transform.forward * distanceInteraction);
    }

    public void DetecterInterrupteur()
    {
        Vector3 depart = transform.position + Vector3.up; 
        Vector3 direction = transform.forward;

        bool estEnCollision = Physics.Raycast(depart, direction, out RaycastHit collisionInfos, distanceInteraction, couchesInteractives, QueryTriggerInteraction.Collide); 
        Debug.Log("Rayon touche quelque chose : " + estEnCollision);

        if (estEnCollision == true)
        {
            Interrupteur interrupteur = collisionInfos.collider.GetComponent<Interrupteur>();

            if (interrupteur != null)
            {
                interrupteur.Activer();
            }
        }

    }


}

