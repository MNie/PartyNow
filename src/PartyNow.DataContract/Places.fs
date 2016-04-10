namespace DataContract.Places

type Address = 
    {
        street: string
        zipcode: string
        city: string
        lat: string
        lng: string
    }

type Places = 
    {
        id: int
        address: Address
        name: string
        subname: string
    }