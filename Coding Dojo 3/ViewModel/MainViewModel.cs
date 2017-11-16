using Coding_Dojo_3.DataSimulation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Shared.BaseModels;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Coding_Dojo_3.ViewModel
{
   
    public class MainViewModel : ViewModelBase
    {
        private Simulator sim;
        private List<ItemVm> modelItems = new List<ItemVm>();
        public ObservableCollection<ItemVm> SensorList { get; set; }
        public ObservableCollection<ItemVm> ActorList { get; set; }
        public RelayCommand SensorAddBtnClickCmd { get; set; }
        public RelayCommand SensorDelBtnCmd { get; set; }
        public RelayCommand ActorAddBtnClickCmd { get; set; }
        public RelayCommand ActorDelBtnClickCmd { get; set; }
        private string currentTime = DateTime.Now.ToLocalTime().ToShortTimeString();
        private string currentDate = DateTime.Now.ToLocalTime().ToShortDateString();
        public ObservableCollection<string> SensorModeSelectionList { get; private set; }
        public ObservableCollection<string> ActorModeSelectionList { get; private set; }

        public string CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; RaisePropertyChanged(); }
        }
        
        public string CurrentTime
        {
            get { return currentTime; }
            set { currentTime = value; RaisePropertyChanged(); }
        }
        
        public MainViewModel()
        {
            SensorList = new ObservableCollection<ItemVm>();
            ActorList = new ObservableCollection<ItemVm>();
            SensorModeSelectionList = new ObservableCollection<string>();
            ActorModeSelectionList = new ObservableCollection<string>();
            //fill ModeSelectionList
            foreach (var item in Enum.GetNames(typeof(SensorModeType)))
            {
                SensorModeSelectionList.Add(item);
            }

            foreach(var item in Enum.GetNames(typeof(ActorModeType)))
            {
                ActorModeSelectionList.Add(item);
            }

            //for time/ date update
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 40);
            timer.Tick += UpdateTime;

            if (!IsInDesignMode)
            {
                //LoadData
                LoadData();

                //start timer for date/time update
                timer.Start();
            }

        }

        private void LoadData()
        {
            Simulator sim = new Simulator(modelItems);
            foreach(var item in sim.Items)
            {
                if (item.ItemType.Equals(typeof(ISensor)))
                    SensorList.Add(item);
                else if (item.ItemType.Equals(typeof(IActor)))
                    ActorList.Add(item);
            }
        }

        private void UpdateTime(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToLocalTime().ToShortTimeString();
            CurrentDate = DateTime.Now.ToLocalTime().ToShortDateString();
        }
    }
}