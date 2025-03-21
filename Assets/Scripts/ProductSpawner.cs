using System.Collections.Generic;
using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    [SerializeField] private ConveyorMoving conveyor;
    [SerializeField] private List<GameObject> spawnbleProducts;
    [SerializeField] private float productsDistance = 0.1f;
    [SerializeField] private Transform productParent;

    private void Awake()
    {
        SpawnProducts();
    }

    public void SpawnProducts()
    {
        GameObject randomProduct = GetRandomProduct();

        GameObject newProduct = Instantiate(randomProduct, conveyor.startPos.transform.position, Quaternion.identity);

        conveyor.spawnedProducts.Add(newProduct);

        if (conveyor.spawnedProducts.Count < conveyor.MaxProductsCount)
        {
            Invoke(nameof(SpawnProducts), productsDistance);
        }


    }

    private GameObject GetRandomProduct()
    {
        int randomIndex = Random.Range(0, spawnbleProducts.Count);
        return spawnbleProducts[randomIndex];
    }
}
