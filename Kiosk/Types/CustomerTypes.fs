namespace Kiosk.Types

type Mood =
    | Good
    | Bad

type Order =
    { Name: ProductName
      Additives: Additives list }

type Customer<'Id when 'Id : equality> =
    { Id: 'Id
      Name: string
      Mood: Mood
      Order: Order }
