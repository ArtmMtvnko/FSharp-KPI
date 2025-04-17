namespace Kiosk.Storage

module BlackList =
    let blackList<'Id when 'Id : equality> =
        [ 123321,
          "asdf123",
          12332.92 ]
