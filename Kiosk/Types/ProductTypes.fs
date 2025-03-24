namespace Kiosk.Types

type Additives =
    | CoffeeAdditive of CoffeeAdditive
    | HotdogAdditive of HotdogAdditive
    | BurgerAdditive of BurgerAdditive

and CoffeeAdditive =
    | Milk
    | Sugar
    | Syrup

and HotdogAdditive =
    | Ketchup
    | Mustard
    | Mayonnaise

and BurgerAdditive =
    | Cheese
    | Lettuce
    | Mushrooms
    | Onions


type Ingredient =
    | CoffeeIngredient of CoffeeIngredient
    | HotdogIngredient of HotdogIngredient
    | BurgerIngredient of BurgerIngredient

and CoffeeIngredient =
    | Water
    | CoffeeBeans

and HotdogIngredient =
    | Bread
    | Sausage

and BurgerIngredient =
    | Buns
    | Patty

type ProductName =
    | Coffee
    | Hotdog
    | Burger

type Product =
    { Name: ProductName
      Ingredients: Ingredient list
      Additives: Additives list }
