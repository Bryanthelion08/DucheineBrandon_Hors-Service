// using UnityEngine;

// public class DeplacementJoueur : MonoBehaviour
// {
//     [Header("Paramètres de mouvement")]
//     [SerializeField] float vitesseDeplacement = 5f;
//     public float vitesseRotation = 200f;
//     Vector3 deplacementAvant;

//     [Header("Paramètres deplacement special")]
//     [SerializeField] float forceSpeciale = 5f;
//     [SerializeField] float decelerationSpeciale = 15f;
//     Vector3 deplacementSpecial = Vector3.zero; // Déplacement spécial pour le knockback et autres effets

//     [Header("Composants")]
//     CharacterController characterController;

//     // ========================================

//     void Start()
//     {
//         characterController = GetComponent<CharacterController>();
//     }

//     // ========================================
//     public Vector3 CalculerDeplacement(Vector2 controleJoueur)
//     {
//         // deplacementAvant = transform.forward * controleJoueur.y * vitesseDeplacement;

//         // return deplacementAvant;
//         return (transform.right * controleJoueur.x + transform.forward * controleJoueur.y) * vitesseDeplacement;
//     }

//     public Vector3 CalculerGravite()
//     {

//         if (characterController.isGrounded && deplacementVertical.y < 0)
//         {
//             deplacementVertical.y = -2f;
//         }
//         else
//         {
//             deplacementVertical.y += gravite * Time.deltaTime;
//         }
//         return deplacementVertical;
//     }

//     public Vector3 CalculerForceSpeciale()
//     {
//         deplacementSpecial = Vector3.MoveTowards(
//             deplacementSpecial,
//             Vector3.zero,
//             decelerationSpeciale * Time.deltaTime
//         );
//         return deplacementSpecial;

//     }

//     public void MettreAJourRotation(float directionX)
//     {
//         transform.Rotate(Vector3.up * directionX * vitesseRotation * Time.deltaTime);
//     }

//     public void MettreAJourDeplacement(Vector3 direction)
//     {
//         characterController.Move(direction * Time.deltaTime);
//     }

//     // ========================================

//     public void Sauter()
//     {
//         if (characterController.isGrounded)
//         {
//             deplacementVertical.y = Mathf.Sqrt(hauteurSaut * -2f * gravite);
//         }
//     }

//     // public void AjouterForce(Vector3 direction, float force)
//     // {
//     //     deplacementSpecial += direction * force;
//     // }




// }

using UnityEngine;

public class DeplacementJoueur : MonoBehaviour
{
    [SerializeField] float vitesseDeplacement = 5f;
    [SerializeField] float gravite = -9.81f;

    Vector3 deplacementVertical;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public Vector3 CalculerDeplacement(Vector2 controleJoueur)
    {
        return (transform.right * controleJoueur.x + transform.forward * controleJoueur.y) * vitesseDeplacement;
    }

    public Vector3 CalculerGravite()
    {
        if (characterController.isGrounded && deplacementVertical.y < 0)
        {
            deplacementVertical.y = -2f;
        }
        else
        {
            deplacementVertical.y += gravite * Time.deltaTime;
        }
        return deplacementVertical;
    }

    public void MettreAJourDeplacement(Vector3 direction)
    {
        characterController.Move(direction * Time.deltaTime);
    }
}
