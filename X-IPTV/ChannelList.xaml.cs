﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace X_IPTV
{
    /// <summary>
    /// Interaction logic for ChannelList.xaml
    /// </summary>
    public partial class ChannelList : Window
    {
        public ChannelList()
        {
            InitializeComponent();

            ChannelModel model = new ChannelModel();

            ChannelLst.DataContext = model;
        }

        private void ChannelLst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 1) return;

            ChannelEntry entry = e.AddedItems[0] as ChannelEntry;


            Console.WriteLine(Instance.playlistDataMap[entry.stream_id.ToString()].stream_url);

            /*Console.WriteLine("Channel Info:");
            Console.WriteLine(entry.name);
            Console.WriteLine(entry.stream_id);
            Console.WriteLine(UnixTimeStampToDateTime(Convert.ToDouble(entry.added)));*/

            //ProcessStartInfo processStartInfo = new ProcessStartInfo(@"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe", $"https://iptv-pure.com:8000/live/sabihi/ek5jkfngrf/{entry.stream_id}.m3u8");

            ProcessStartInfo processStartInfo = new ProcessStartInfo(@"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe", Instance.playlistDataMap[entry.stream_id.ToString()].stream_url);

            string urlTest = Instance.playlistDataMap[entry.stream_id.ToString()].stream_url;


            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = $"/C \"C:/Program Files (x86)/VideoLAN/VLC/vlc.exe\" {urlTest}";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            Process processTemp = new Process();
            processTemp.StartInfo = startInfo;
            processTemp.EnableRaisingEvents = true;
            processTemp.Start();

            //works
            //string command = $"/C \"C:/Program Files (x86)/VideoLAN/VLC/vlc.exe\" {urlTest}";
            //Process.Start("cmd.exe", command);

        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }

    public class ChannelModel
    {
        public ChannelModel()
        {
            MyListBoxItems = new ObservableCollection<ChannelEntry>();
            for (int i = 0; i < Instance.ChannelsArray.Length; i++)
            {
                MyListBoxItems.Add(Instance.ChannelsArray[i]);
            }
        }

        public ObservableCollection<ChannelEntry> MyListBoxItems { get; set; }
    }

    public class MyMockClass
    {
        public MyMockClass()
        {
            MyListBoxItems = new ObservableCollection<ChannelEntry>();
            MyListBoxItems.Add(new ChannelEntry() { name = "|FR| TF1 UHD", stream_icon = "http://f.iptv-pure.com/tf14k.png" });
            MyListBoxItems.Add(new ChannelEntry() { name = "|FR| CSTAR FHD", stream_icon = "http://f.iptv-pure.com/cstar.png" });
        }
        public ObservableCollection<ChannelEntry> MyListBoxItems { get; set; }
    }
}
