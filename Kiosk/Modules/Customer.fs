namespace Kiosk

type Mood =
    | Good
    | Bad

type Customer =
    { Name: string
      Mood: Mood
      Order: Order }

module Customer =
    let getCoffeeWithAdditives () =
        { Name = Coffee
          Additives = [ CoffeeAdditive Milk; CoffeeAdditive Sugar ] }

    let getHotdogWithAdditives () =
        { Name = Hotdog
          Additives = [ HotdogAdditive Ketchup; HotdogAdditive Mustard ] }