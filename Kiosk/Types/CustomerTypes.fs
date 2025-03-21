namespace Kiosk.Types

type Mood =
    | Good
    | Bad

type Order =
    { Name: ProductName
      Additives: Additives list }

type Customer =
    { Name: string
      Mood: Mood
      Orders: Order list }
