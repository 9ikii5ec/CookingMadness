using System.Collections.Generic;
using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnbleProducts;
    [SerializeField] private ConveyorMoving conveyor;
    [SerializeField] private float productsDistance = 0.5f;
    [SerializeField] private Transform productParent;

    private bool isSpawning = false;

    private void Update()
    {
        if (conveyor.IsConveyorMoving && !isSpawning)
        {
            isSpawning = true;
            SpawnProducts();
        }
    }

    private void SpawnProducts()
    {
        if (conveyor.spawnedProducts.Count >= conveyor.MaxProductsCount)
        {
            isSpawning = false;
            return;
        }

        GameObject randomProduct = GetRandomProduct();
        GameObject newProduct = Instantiate(randomProduct, conveyor.startPos.transform.position, Quaternion.identity, productParent);
        conveyor.spawnedProducts.Add(newProduct);

        Invoke(nameof(SpawnProducts), productsDistance);
    }

    private GameObject GetRandomProduct()
    {
        int randomIndex = Random.Range(0, spawnbleProducts.Count);
        return spawnbleProducts[randomIndex];
    }
}
