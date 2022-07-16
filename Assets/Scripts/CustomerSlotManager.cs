using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class CustomerSlotManager : MonoBehaviour
{
    [SerializeField]
    List <Transform> slots;
    [SerializeField]
    Transform customerSpwanPoint;
    [SerializeField]
    GameObject customerPrefab;
    List <GameObject> customerList;
    private void Start()
    {
        EventHandler.Instance.OnReadyBreadClick += CheckItem;
        slots = GetRandom(slots);
        customerList = new List<GameObject>();
        SpwanCustomer();

    }

    private void CheckItem(GameObject gameObject)
    {
        int itemCheckId;
        if (gameObject.transform.childCount == 1)
        {
             itemCheckId = gameObject.transform.GetChild(0).gameObject.GetComponent<ToastBread>().itemId;
            Debug.Log(" clicked item id = " + itemCheckId);
            for (int i = 0; i < customerList.Count; i++)
            {
                for(int j = 0; j < customerList[i].GetComponent<CustomerManager>().order.RecipeList.Count; j++)
                {
                    
                    if (customerList[i].GetComponent<CustomerManager>().order.RecipeList[j].recipeId == itemCheckId)
                    {
                        Debug.Log("recipe item id = " + customerList[i].GetComponent<CustomerManager>().order.RecipeList[j].recipeId);
                        customerList[i].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        Debug.Log("item name  " + customerList[i].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.name);
                        Debug.Log("i =  " + i + " j = " + j);
                        gameObject.GetComponent<Plates>().plateStateBread = PlateStateBread.Free;
                        Debug.Log(" plate state bread- " + gameObject.GetComponent<Plates>().plateStateBread);
                        i = customerList.Count;
                        Destroy(gameObject.transform.GetChild(0).gameObject);
                        break;
                    }
                }
                
            }
            //Destroy()
        }

    }
    private async void SpwanCustomer()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            GameObject customer = CustomerPooler.Instance.GetPooledObject();
            customer.SetActive(true);
            Vector3 temp = new Vector3(0f, 0f, 0f);
            customer.transform.parent = customerSpwanPoint;
            customer.transform.localPosition = temp;
            EventHandler.Instance.InvokeGiveSlotTransformToCustomer(slots[i].position,i);
            customerList.Add(customer);
            await new WaitForSeconds(5);
        }
       
    }

    private List<T> GetRandom <T> (List<T> temp)
    {
        var rnd = new System.Random();
        var randomized = temp.OrderBy(item => rnd.Next()).ToList();

        temp.Clear();
        foreach (var value in randomized)
        {
            print(value.ToString());

            temp.Add(value);
        }

        return temp;
    }

    private List<GameObject> AccessSlots(int slotsCout, List<Color> slotColor)
    {
        List<GameObject> slotsList = new List<GameObject>(slotsCout);

        slotsList.Clear();
        for (int i = 0; i < slotsCout; i++)
        {
            slotsList.Add(gameObject.transform.GetChild(i).gameObject);
            // Assign Customer from Customer List Here;
            slotsList[i].GetComponent<SpriteRenderer>().color = slotColor[i];
            print("Acessing Original slot list : " + slotsList[i].name);
        }
        return slotsList;
    }

    private void OnDisable()
    {
        EventHandler.Instance.OnReadyBreadClick -= CheckItem;
        ;
    }

}
