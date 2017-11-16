using GalaSoft.MvvmLight;
using Shared.BaseModels;
using Shared.Interfaces;
using Shared.Models;
using System;


namespace Coding_Dojo_3.ViewModel
{
    public class ItemVm: ViewModelBase
    {
        private ItemBase baseItem;
        

        public int Id
        {
            get { return baseItem.Id; }
        }



        public string Description
        {
            get { return baseItem.Description; }
            set { baseItem.Description = value; RaisePropertyChanged(); }
        }

        public string Name
        {
            get { return baseItem.Name; }
            set { baseItem.Name = value; RaisePropertyChanged(); }
        }

        public string Room
        {
            get { return baseItem.Room; }
            set { baseItem.Room = value; RaisePropertyChanged(); }
        }

        public int PosX
        {
            get { return baseItem.PosX; }
            set { baseItem.PosX = value; RaisePropertyChanged(); }
        }

        
        public int PosY
        {
            get { return baseItem.PosY; }
            set { baseItem.PosY = value; RaisePropertyChanged(); }
        }

        

        public string ValueType
        {
            get
            {
                if (baseItem is ISensor)
                    return (baseItem as BaseSensor).SensorValueType.Name;
                else if (baseItem is IActor)
                    return (baseItem as BaseActor).ActorValueType.Name;
                else
                    throw new NotImplementedException();
            }
            
        }

        public Type ItemType
        {
            get
            {
                if (baseItem is ISensor) return typeof(ISensor);
                else if (baseItem is IActor) return typeof(IActor);
                else throw new NotImplementedException();
            }
            
        }

        
        public string Mode
        {
            get
            {
                if (baseItem is ISensor) return (baseItem as BaseSensor).SensorMode.ToString();
                else if (baseItem is IActor) return (baseItem as BaseActor).ActorMode.ToString();
                else return null;
            }
            set
            {
                if (baseItem is ISensor)
                    (baseItem as BaseSensor).SensorMode = (SensorModeType)Enum.Parse(typeof(SensorModeType), value, false);
                if (baseItem is IActor)
                    (baseItem as BaseActor).ActorMode = (ActorModeType)Enum.Parse(typeof(ActorModeType), value, false);
                RaisePropertyChanged();
            }
        }
        
        public object Value
        {
            get
            {
                if (baseItem is ISensor)
                    return (baseItem as BaseSensor).SensorValue;
                else if (baseItem is IActor)
                    return (baseItem as BaseActor).ActorValue;
                else throw new NotImplementedException();
            }
            set
            {
                if (baseItem is ISensor) (baseItem as BaseSensor).SensorValue = value;
                else if (baseItem is IActor) (baseItem as BaseActor).ActorValue = value;
                else
                    throw new NotImplementedException();
                RaisePropertyChanged();
            }
        }

        public ItemVm(ISensor sensor)
        {
            baseItem = sensor as ItemBase;
        }
        
        public ItemVm(IActor actor)
        {
            baseItem = actor as ItemBase;
        }






    }
}
