namespace DataContract.Categories

type Parent = 
    {
        id: int
        name: string
    }

type Categories =
    {
        id: int
        name: string
        root_category: Parent
        parent: Parent
    }