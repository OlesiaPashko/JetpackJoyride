using UnityEngine;

public class SectionEndingCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Disabled"))
        {
            SectionManager sectionManager = SectionManager.Instance;
            sectionManager.SpawnSection();
        }
    }
}
