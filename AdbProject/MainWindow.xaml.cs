using AdbProject.Constant;
using System;
using System.Windows;
using AdbProject.Helper;
using System.Windows.Input;
using System.Collections.Generic;

namespace AdbProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> CommandList = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                CommandList.Clear();
                var listDevices = Helper.Helper.FilterDeviceList(Helper.Helper.ExecuteAdbCommand(AdbCommands.ADB_DEVICES_LIST));
                comobox.ItemsSource = listDevices;
                comobox.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error while Fetching Adb Devices");
            }
            
        }
        
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommandList.Clear();
                listCommandUI.ItemsSource = null;
                var listDevices = Helper.Helper.FilterDeviceList(Helper.Helper.ExecuteAdbCommand(AdbCommands.ADB_DEVICES_LIST));
                comobox.ItemsSource = listDevices;
                comobox.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error while Fetching Adb Devices");
            }
        }
        
        

        private void All_logs_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                string selectitem = this.comobox.Text;
                var commandString = string.Format(AdbCommands.ADB_LOGCAT, Helper.Helper.SelectedDeviceSerialNum(selectitem));
                CommandList.Add("Executing... " + commandString);
                listCommandUI.ItemsSource = null;
                listCommandUI.ItemsSource = CommandList;
                Helper.Helper.OpenTerminalAndExecuteCommand(commandString);
            }
            catch
            {
                MessageBox.Show("Error Executing Adb Command");
            }
        }

        private void Tsalogs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectitem = this.comobox.Text;
                var commandString = string.Format(AdbCommands.ADB_TSAL_LOGCAT, Helper.Helper.SelectedDeviceSerialNum(selectitem));
                CommandList.Add("Executing... " + commandString);
                listCommandUI.ItemsSource = null;
                listCommandUI.ItemsSource = CommandList;
                Helper.Helper.OpenTerminalAndExecuteCommand(commandString);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Executing Adb Command");
            }
        }

        private void Naswilogs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectitem = this.comobox.Text;
                var commandString = string.Format(AdbCommands.ADB_TSAL_LOGCAT, Helper.Helper.SelectedDeviceSerialNum(selectitem));
                CommandList.Add("Executing... " + commandString);
                listCommandUI.ItemsSource = null;
                listCommandUI.ItemsSource = CommandList;
                Helper.Helper.OpenTerminalAndExecuteCommand(commandString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Executing Adb Command");
            }
        }

        private void Executebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectitem = this.comobox.Text;
                if (!string.IsNullOrWhiteSpace(executeTextbox.Text))
                {
                    var command = executeTextbox.Text.Trim();
                    if (command.Contains("adb"))
                    {
                        var commandString = command.Replace("adb", string.Format("adb -s {0}", Helper.Helper.SelectedDeviceSerialNum(selectitem)));
                        CommandList.Add("Executing... " + commandString);
                        listCommandUI.ItemsSource = null;
                        listCommandUI.ItemsSource = CommandList;
                        Helper.Helper.OpenTerminalAndExecuteCommand(commandString);
                    }
                    else
                    {
                        MessageBox.Show("Invalid Adb Command.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Executing Adb Command");
            }
        }

        private void ExecuteTextbox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                Executebtn_Click(this, new RoutedEventArgs());
            }
        }
    }
}
