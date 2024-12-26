using Albion.Network;
using PacketDotNet;
using SharpPcap;
using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows;
using SharpPcap.LibPcap;

namespace X975.Radar.Sniffer
{
    [Obfuscation(Feature = "mutation", Exclude = false)]
    public class PacketDeviceSelector
    {
        private readonly IPhotonReceiver photonReceiver;

        public PacketDeviceSelector(IPhotonReceiver photonReceiver)
        {
            this.photonReceiver = photonReceiver;
        }

        public void Start()
        {
            try
            {
                var devices = CaptureDeviceList.Instance;

                if (devices.Count <= 0)
                {
                    if (System.Globalization.CultureInfo.CurrentCulture.ToString() == "ru-RU")
                    {
                        MessageBox.Show("Ошибка! \nНету доступных адаптеров для прослушки!");
                        Environment.Exit(0);
                    }
                    else
                    {
                        MessageBox.Show("Error! \nThere are no listening adapters available!");
                        Environment.Exit(0);
                    }
                }

                foreach (ILiveDevice device in devices)
                {
                   if (device.MacAddress != null ||
                       // capture loopback too
                       ((device as LibPcapLiveDevice)?.Addresses?.Any(e => e.Addr.ipAddress?.Equals(IPAddress.Loopback) ?? false) ?? false))
                   {
                       PacketEvent(device);
                   }
                }
            }
            catch (Exception e)
            {
                if (System.Globalization.CultureInfo.CurrentCulture.ToString() == "ru-RU")
                {
                    MessageBox.Show("Установи NPCAP \nНе трогай галки при установке!");
                    Environment.Exit(0);
                }
                else
                {
                    MessageBox.Show("Install NPCAP \nDon't change the checkboxes!");
                    Environment.Exit(0);
                }
            }
        }

        private void PacketEvent(ICaptureDevice device)
        {
            if (!device.Started)
            {
                device.Open(new DeviceConfiguration()
                {
                    Mode = DeviceModes.DataTransferUdp | DeviceModes.Promiscuous | DeviceModes.MaxResponsiveness,
                    ReadTimeout = 5
                });

                device.Filter = "udp and port 5050";
                device.OnPacketArrival += Device_OnPacketArrival;
                device.StartCapture();
            }
        }

        private void Device_OnPacketArrival(object sender, PacketCapture e)
        {
            try
            {
                var packet = Packet.ParsePacket(e.GetPacket().LinkLayerType, e.GetPacket().Data).Extract<UdpPacket>();

                if (packet != null)
                {
                    photonReceiver.ReceivePacket(packet.PayloadData);
                }
            }
            catch { }
        }
    }
}
