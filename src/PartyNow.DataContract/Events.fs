namespace DataContract.Events
open DataContract.Places
open DataContract.Organizers

type Attachment = 
    {
        filename: string
    }

type Url =
    {
        www: string
        tickets: string
        fb: string
    }

type Ticket = 
    {
        
        ``type``: string
        startTicket: string
        endTicket: string
    }

type Events = 
    {
        id: int
        place: Places
        endDate: string
        name: string
        urls: Url
        attachments: Attachment array
        descLong: string
        categoryId: int
        startDate: string
        organizer: Organizers
        active: int
        ticets: Ticket
        descShort: string
    }

