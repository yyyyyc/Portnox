using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Models
{
    #region interfaces
    interface INetworkItem
    {
        void AddEvent(Event ev);
    }
    interface ISwitch
    {
        void AddPort(Port prt);
    }

    interface IPort
    {
        void AddDevice(Device dv);
    }

    #endregion

    #region Classes
    public class NetworkEvents : INetworkItem
    {
        //public SwitchCollection switches = new SwitchCollection();
        public List<Switch> switches = new List<Switch>();

        public void AddEvents(List<Event> events)
        {
            foreach (Event ev in events)
            {
                this.AddEvent(ev);
            }
        }

        public void AddEvent(Event ev)
        {
            //switches.AddEvent(ev);
            Switch sw = this.switches.Where(s => s.ip == ev.ip).FirstOrDefault();
            if (sw == null)
            {
                sw = new Switch(ev);
                this.switches.Add(sw);
            }
            else
            {
                sw.AddEvent(ev);
            }
        }
    }

    public class Switch : INetworkItem, ISwitch
    {
        public string ip { get; set; }
        public List<Port> ports { get; set; }
        public List<Event> events { get; set; }


        public Switch(string ip)
        {
            this.ip = ip;
            this.ports = new List<Port>();
            this.events = new List<Event>();
        }

        public Switch(Event ev)
        {
            this.ip = ev.ip;
            this.ports = new List<Port>();
            this.events = new List<Event>();

            this.AddEvent(ev);
        }

        public void AddEvent(Event ev)
        {
            this.events.Add(ev);
            this.AddPort(new Port(ev));
        }

        public void AddPort(Port prt)
        {
            Port prtt = this.ports.Where(p => p.id == prt.id).FirstOrDefault();

            if (prtt == null)
            {
                this.ports.Add(prt);
            }
            else
            {
                // Modify Port by the event
                prtt.AddEvent(prt.events.First());
            }
        }
    }

    public class Port : INetworkItem, IPort
    {
        public int id { get; set; }
        public string current_device { get; set; }
        public List<Device> connected_devices { get; set; }
        public List<Event> events { get; set; }

        public Port(int id)
        {
            this.id = id;
            this.connected_devices = new List<Device>();
            this.events = new List<Event>();
        }

        public Port(Event ev)
        {
            this.id = ev.port_id;
            this.connected_devices = new List<Device>();
            this.events = new List<Event>();

            this.AddEvent(ev);
        }

        public void AddEvent(Event ev)
        {
            // Add Event
            this.events.Add(ev);

            this.current_device = ev.mac;
            // Add Device
            this.AddDevice(new Device(ev.mac));

        }

        public void AddDevice(Device dv)
        {
            Device dvv = this.connected_devices.Where(dvvv => dvvv.mac == dv.mac).FirstOrDefault();

            if (dvv == null)
            {
                connected_devices.Add(dv);
            }
            else
            {
                // Modify existing device
            }
        }
    }

    public class Device : INetworkItem
    {
        public string mac { get; set; }
        List<Event> events { get; set; }
        public Device(string mac)
        {
            this.mac = mac;
            this.events = new List<Event>();
        }
        public Device(Event ev)
        {
            this.mac = ev.mac;
            this.events = new List<Event>();

            this.AddEvent(ev);
        }
        public void AddEvent(Event ev)
        {
            // Add Event
            this.events.Add(ev);
        }
    }

    public class Event
    {
        public int id { get; set; }
        public DateTime created_on { get; set; }
        public string ip { get; set; }
        public int port_id { get; set; }
        public string mac { get; set; }
        public string remarks { get; set; }

        public Event(int id, DateTime created_on, string ip, int port_id, string mac, string remarks)
        {
            this.id = id;
            this.created_on = created_on;
            this.ip = ip;
            this.port_id = port_id;
            this.mac = mac;
            this.remarks = remarks;

        }
    }

    #endregion

}
