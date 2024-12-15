using System.Collections.Generic;
using Gley.EasyIAP;
using Gley.EasyIAP.Internal;
using UnityEngine;
using EasyUI.Toast;
class FBstatistics : UnitySingleton<FBstatistics>
{
    public class DM_StoreProducts
    {
        public ShopProductNames name;
        public bool bought;

        public DM_StoreProducts(ShopProductNames name, bool bought)
        {
            this.name = name;
            this.bought = bought;
        }
    }

  public  bool removeAds = false;

    private void Awake()
    {
       


        //   Debug.Log("INIT :" + PlayerPrefs.GetInt("isRemoveAds", 0));
        DontDestroyOnLoad(gameObject);
        API.Initialize(InitializationComplete);
    }


    private void InitializationComplete(IAPOperationStatus status, string message, List<StoreProduct> shopProducts)
    {
        
        if (status == IAPOperationStatus.Success)
        {
            
            //IAP was successfully initialized
            //loop through all products
            for (int i = 0; i < shopProducts.Count; i++)
            {
                if (shopProducts[i].productName == "RemoveAds")
                {
                    //if the active property is true, the product is bought
                    if (shopProducts[i].active)
                    {
                        removeAds = true;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Error occurred: " + message);
        }

    }

    public void MakeBuyProduct(int indexProduct)
    {

        switch (indexProduct)
        {
            case 1:
                {
                    API.BuyProduct(ShopProductNames.Coins1, ProductBought);
                    break;
                }
            case 2:
                {
                    API.BuyProduct(ShopProductNames.Coins2, ProductBought);
                    break;
                }
            case 3:
                {
                    API.BuyProduct(ShopProductNames.Coins3, ProductBought);
                    break;
                }
            case 4:
                {
                    API.BuyProduct(ShopProductNames.Coins4, ProductBought);
                    break;
                }
            case 5:
                {
                    API.BuyProduct(ShopProductNames.Coins5, ProductBought);
                    break;
                }
         

        }

    }


    public void OnClickRemoveAds()
    {
       
        API.BuyProduct(ShopProductNames.RemoveAds, ProductBought);
    }


    public void Restore()
    {
        API.RestorePurchases(ProductRestoredCallback, RestoreDone);
    }

    private void ProductRestoredCallback(IAPOperationStatus status, string message, StoreProduct product)
    {
        if (status == IAPOperationStatus.Success)
        {
            Debug.Log("Restore product success!: " + message);
        }
        else
        {
            //an error occurred in the buy process, log the message for more details
            Debug.Log("Restore product failed: " + message);
        }
    }

    private void RestoreDone()
    {
        Debug.Log("Restore done");
    }



    private void ProductBought(IAPOperationStatus status, string message, StoreProduct product)
    {
       
        if (status == IAPOperationStatus.Success)
        {
            if (IAPManager.Instance.debug)
            {
                Debug.Log("Buy product completed: " + product.localizedTitle + " receive value: " + product.value);
                ScreenWriter.Write("Buy product completed: " + product.localizedTitle + " receive value: " + product.value);
            }


            if (product.productType == ProductType.NonConsumable)
            {
                Gley.MobileAds.API.RemoveAds(true);
                Toast.Show("Successful remove ads !", 3f, ToastColor.Green);
                //Removeads.gameObject.SetActive(false);
                PlayerPrefs.SetInt("isRemoveAds", 1);
                removeAds = true;
            }
            else if (product.productType == ProductType.Consumable)
            {

                SaveModel.AddGold(product.value);
                MessageCenter.SendMessage(MyMessageType.GAME_UI, MyMessage.REFRESH_RES, product.value);
                
                
                Toast.Show("Successful purchase " + product.value + " coins!", 3f, ToastColor.Green);

            }
            else
            {
                //en error occurred in the buy process, log the message for more details
                if (IAPManager.Instance.debug)
                {
                    Debug.Log("Buy product failed: " + message);

                }
            }
        }
    }
}

