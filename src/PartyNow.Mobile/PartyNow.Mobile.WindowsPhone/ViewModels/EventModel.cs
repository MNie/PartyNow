using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Imaging;
using PartyNow.DataContract.Models;

namespace PartyNow.Mobile.ViewModels
{
    internal class EventModel
    {
        public ObservableCollection<object> Images { get; set; }
        public string Name { get; set; }
        public string Where { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Organizer { get; set; }
        public string Tickets { get; set; }
        public string Description { get; set; }
        public string Active { get; set; }
        public string WhereToBuyTickets { get; set; }
        public string Facebook { get; set; }
        public string WWW { get; set; }
        public EventModel(Events @event)
        {
            Images = new ObservableCollection<object>();
            if (@event?.nazwa != null) Name = @event.nazwa;
            if (@event?.miejsce?.name != null) Where = @event.miejsce?.name;
            if (@event?.start != null) StartDate = @event.start.ToString();
            if (@event?.koniec != null) EndDate = @event.koniec.ToString();
            if (@event?.organizator?.designation != null) Organizer = @event.organizator?.designation;
            if (@event?.bilet?.type != null) Tickets = @event.bilet?.type;
            if (@event?.descLong != null) Description = WebUtility.HtmlEncode(Regex.Replace(@event.descLong, @"<[^>]+>|&nbsp;", "").Trim());
            if (@event?.aktualny != null) Active = @event.aktualny == 1 ? "aktualne" : "nie aktualne";
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
                    Images.Add(new Data
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
