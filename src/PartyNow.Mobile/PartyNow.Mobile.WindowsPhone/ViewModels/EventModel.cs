﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;
using PartyNow.DataContract.Models;

namespace PartyNow.Mobile.ViewModels
{
    internal class EventModel
    {
        public ObservableCollection<object> Datas { get; set; }
        public string Name { get; set; }
        public string Where { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Organizer { get; set; }
        public string Tickets { get; set; }
        private string Description { get; set; }
        public string Active { get; set; }
        public string WhereToBuyTickets { get; set; }
        public string Facebook { get; set; }
        public string WWW { get; set; }
        public EventModel(Events @event)
        {
            if (@event?.name != null) Name = @event.name;
            if (@event?.place?.name != null) Where = @event.place?.name;
            if (@event?.startDate != null) StartDate = @event.startDate;
            if (@event?.endDate != null) EndDate = @event.endDate;
            if (@event?.organizer?.designation != null) Organizer = @event.organizer?.designation;
            if (@event?.tickets?.type != null) Tickets = @event.tickets?.type;
            if (@event?.descLong != null) Description = @event.descLong;
            if (@event?.active != null) Active = @event.active.ToString();
            if (@event?.urls?.tickets != null) WhereToBuyTickets = @event.urls?.tickets;
            if (@event?.urls?.www != null) WWW = @event?.urls?.www;
            if (@event?.urls?.fb != null) Facebook = @event?.urls?.fb;
            DownloadImages(@event.attachments);
        }
        private async void DownloadImages(IEnumerable<Attachment> urls)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                foreach (
                    var bmi in
                        urls.Select(attachment => new Uri(attachment.filename, UriKind.Absolute))
                            .Select(uri => new BitmapImage(uri)))
                {
                    Datas.Add(new Data
                    {
                        BitmapImage = bmi,
                        Title = ""
                    });
                }
            });
        }
    }

    public class Data
    {
        public BitmapImage BitmapImage { get; set; }
        public string Title { get; set; }
    }
}
