using Microsoft.AspNetCore.SignalR.Client;
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
using System.Windows;

namespace RestarauntClient.ViewModels
{
    public class OperatorVisitorsPageVM : INotifyPropertyChanged
    {
        private DateTime dateNow;
        private string dateNowStringFormat;

        public RelayCommand DeleteBooking { get; set; }
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

        private VisitorsTable selectedBooking;
        public VisitorsTable SelectedBooking
        {
            get 
            { 
                return selectedBooking;
            }
            set
            {
                selectedBooking = value;
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
                SSFFunction();
            }
        }

        private ObservableCollection<Table> tableOfBookingFiltration;
        public ObservableCollection<Table> TableOfBookingFiltration
        {
            get
            {
                return tableOfBookingFiltration;
            }
            set
            {
                tableOfBookingFiltration = value;
                OnPropertyChanged();
               
            }
        }

        private Table selectedTableOfBooking;
        public Table SelectedTableOfBooking
        {
            get
            {
                return selectedTableOfBooking;
            }
            set
            {
                selectedTableOfBooking = value;
                OnPropertyChanged();
                SSFFunction();
            }
        }



        private string searchText;
        public string SearchText
        {
            get
            { 
                return searchText; 
            }
            set 
            { 
                searchText = value;
                OnPropertyChanged();
                SSFFunction();
            }
        }



        public OperatorVisitorsPageVM()
        {
            dateNow = DateTime.Now;
            dateNowStringFormat = dateNow.ToString("dd MMMM yyyy", new CultureInfo("ru-RU"));
            DeleteBooking = new RelayCommand(deleteBooking);

            DateOfBookingFiltration = new ObservableCollection<string>
            {
                "Все записи",
                "На "+ dateNowStringFormat,
                "От "+ dateNowStringFormat
            };

            TableOfBookingFiltration = new ObservableCollection<Table>();
            
            var requestTables = new RestRequest("api/Tables", Method.GET);
            var responceTables = Client.Instance().httpClient.Execute(requestTables);
            if (responceTables.StatusCode == HttpStatusCode.OK)
            {
                TableOfBookingFiltration = JsonConvert.DeserializeObject<ObservableCollection<Table>>(responceTables.Content);
                
            }

            VisitorsViewList = new ObservableCollection<VisitorsTable>();
            VisitorsList = new ObservableCollection<VisitorsTable>();
            var requestBooking = new RestRequest("api/VisitorsTables/GetBookingList", Method.GET);
            var responceBooking = Client.Instance().httpClient.Execute(requestBooking);
            if (responceBooking.StatusCode == HttpStatusCode.OK)
            {
                VisitorsList = JsonConvert.DeserializeObject<ObservableCollection<VisitorsTable>>(responceBooking.Content);
                VisitorsViewList = VisitorsList;
            }

            Client.Instance().hubConnection.On<string>("DeletedBooking", (deletedBooking) =>
            {
                VisitorsList.Remove(VisitorsList.FirstOrDefault(p => p.BookingId.Equals(JsonConvert.DeserializeObject<VisitorsTable>(deletedBooking).BookingId)));
                VisitorsViewList = new ObservableCollection<VisitorsTable>(VisitorsList);
               
            });
           
        }

        private void deleteBooking(object obj)
        {
            var requestDeleteBooking = new RestRequest($"api/VisitorsTables/{SelectedBooking.BookingId}", Method.DELETE);
            var responceDeleteBooking = Client.Instance().httpClient.Execute(requestDeleteBooking);
            
            if (responceDeleteBooking.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show("Удаление произошло успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SSFFunction()
        {
            if (SearchText == null) 
            {
                SearchText = "";
            }
            VisitorsViewList = new ObservableCollection<VisitorsTable>(VisitorsList.Where(p => p.Visitor.VisitorFullName.Contains(SearchText)).ToList());

            if (selectedDateOfBooking == ("На " + dateNowStringFormat))
            {
                VisitorsViewList = new ObservableCollection<VisitorsTable>(VisitorsViewList.Where(p => Convert.ToDateTime(p.DateBooking).Date == dateNow.Date).ToList());
            }
            else if (selectedDateOfBooking == ("От " + dateNowStringFormat))
            {
                VisitorsViewList = new ObservableCollection<VisitorsTable>(VisitorsViewList.Where(p => Convert.ToDateTime(p.DateBooking).Date >= dateNow.Date).ToList());
            }

            if (SelectedTableOfBooking != null)
            {
                VisitorsViewList = new ObservableCollection<VisitorsTable>(VisitorsViewList.Where(p => p.Table.TableDescription == SelectedTableOfBooking.TableDescription));
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
