using UnityEngine;

public class CacherObjet : MonoBehaviour
{
    [SerializeField] GameObject objetACacher;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objetACacher.SetActive(false);
        }
      
    }
}
