using Newtonsoft.Json;
using RestarauntClient.Models.POCOModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntClient.ViewModels
{
    public class OperatorVisitorsPageVM : INotifyPropertyChanged
    {
        private DateTime dateNow;
        private string dateNowStringFormat;

        public ObservableCollection<VisitorsTable> VisitorsList { get; set; }

        private ObservableCollection<VisitorsTable> visitorsViewList;
        public ObservableCollection<VisitorsTable> VisitorsViewList
        {
            get 
            {
                return visitorsViewList;
            }
            set 
            {
                visitorsViewList = value; 
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> dateOfBookingFiltration;
        public ObservableCollection<string> DateOfBookingFiltration
        {
            get 
            { 
                return dateOfBookingFiltration;
            }
            set
            { 
                dateOfBookingFiltration = value;
                OnPropertyChanged();
            }
        }

        private string selectedDateOfBooking;
        public string SelectedDateOfBooking
        {
            get 
            { 
                return selectedDateOfBooking; 
            }
            set 
            { 
                selectedDateOfBooking = value;
                OnPropertyChanged();

                if (selectedDateOfBooking == "Все записи")
                {
                    VisitorsViewList = VisitorsList;
                }
                else if(selectedDateOfBooking == ("На " + dateNowStringFormat))
                {
                    VisitorsViewList = new ObservableCollection<VisitorsTable>(VisitorsList.Where(p => Convert.ToDateTime(p.DateBooking).Date == dateNow.Date).ToList());
                }
                else if(selectedDateOfBooking == ("От " + dateNowStringFormat))
                {
                    VisitorsViewList = new ObservableCollection<VisitorsTable>(VisitorsList.Where(p => Convert.ToDateTime(p.DateBooking).Date >= dateNow.Date).ToList());
                }
            }
        }


        public OperatorVisitorsPageVM()
        {
            dateNow = DateTime.Now;
            dateNowStringFormat = dateNow.ToString("dd MMMM yyyy", new CultureInfo("ru-RU"));

            DateOfBookingFiltration = new ObservableCollection<string>
            {
                "Все записи",
                "На "+ dateNowStringFormat,
                "От "+ dateNowStringFormat
            };
            VisitorsViewList = new ObservableCollection<VisitorsTable>();
            VisitorsList = new ObservableCollection<VisitorsTable>();
            var requestBooking = new RestRequest("api/VisitorsTables/GetBookingList", Method.GET);
            var responceBooking = Client.Instance().httpClient.Execute(requestBooking);
            if (responceBooking.StatusCode == HttpStatusCode.OK)
            {
                VisitorsList = JsonConvert.DeserializeObject<ObservableCollection<VisitorsTable>>(responceBooking.Content);
                VisitorsViewList = VisitorsList;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
