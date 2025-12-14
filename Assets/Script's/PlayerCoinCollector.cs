using UnityEngine;

public class PlayerCoinCollector : MonoBehaviour
{
    public GameObject coinBurstFX;   // Assign particle prefab here

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            // Spawn particle effect at coin position
            if (coinBurstFX != null)
            {
                GameObject fx = Instantiate(coinBurstFX, other.transform.position, Quaternion.identity);
                Destroy(fx, 2f); // delete effect after 2 sec
            }

            // Add score
            UIManager.Instance.AddScore(1);

            // Destroy coin
            Destroy(other.gameObject);
        }
    }
}
