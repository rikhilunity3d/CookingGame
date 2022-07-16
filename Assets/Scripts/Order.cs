using System.Collections.Generic;

public class Order
{
    int orderID;
    List<Recipe> recipeList;
    bool isOrderDelivered = false;
    public int OrderID { get { return orderID; } }

    public List<Recipe> RecipeList { get { return recipeList; } }
    public bool IsOrderDelivered
    {
        get{return isOrderDelivered;}
        set{isOrderDelivered=IsOrderDelivered;}
    }
   

    public Order()
    {

    }
    public Order(int orderID,List<Recipe> recipeList)
    {
        this.orderID = orderID;
        this.recipeList = recipeList;
    }
}
