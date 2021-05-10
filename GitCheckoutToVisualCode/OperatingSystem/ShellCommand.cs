using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Candal.OperatingSystem
{
    public enum ShellElementCode
    {
        BeginJob,
        EndJob,
        BeginCommand,
        EndCommand,
        OutputData,
        ErrorData,
        ExceptionData
    };

    public class ShellDataElement
    {
        public ShellElementCode Code { get; set; }
        public string Data { get; set; }
    }

    public class ShellData
    {
        public ShellData()
        {
            ElementList = new List<ShellDataElement>();
        }

        public List<ShellDataElement> ElementList { get; }
        public int ExitCode { get; set; }

        public void Add(ShellElementCode code, string data)
        {
            ShellDataElement element = new ShellDataElement()
            {
                Code = code,
                Data = data
            };

            ElementList.Add(element);
        }
    }

    class ShellCommand
    {
        private event ShellDataEventHandler ShellDataEvent;

        private event DataReceivedEventHandler _outputDataReceivedEvent;
        private event DataReceivedEventHandler _errorDataReceivedEvent;

        private ShellData _output = null;
        private bool _saveOutput;

        public ShellCommand()
        {
            #region Events

            _outputDataReceivedEvent += delegate (object sender, DataReceivedEventArgs e)
            {
                if (e.Data == null)
                    return;

                if (_saveOutput) //save to output
                {
                    ShellDataElement item = new ShellDataElement() { Code = ShellElementCode.OutputData, Data = e.Data };
                    _output.ElementList.Add(item);
                }

                if (ShellDataEvent != null) //fire event
                    ShellDataEvent(ShellElementCode.OutputData, e.Data); 

                //Thread.Sleep(1000); //TODO: remove
            };

            _errorDataReceivedEvent += delegate (object sender, DataReceivedEventArgs e)
            {
                if (e.Data == null)
                    return;

                if (_saveOutput) //save to output
                {
                    ShellDataElement item = new ShellDataElement() { Code = ShellElementCode.ErrorData, Data = e.Data };
                    _output.ElementList.Add(item);
                }
                if (ShellDataEvent != null) //fire event
                    ShellDataEvent(ShellElementCode.ErrorData, e.Data);

                //Thread.Sleep(1000); //TODO: remove
            };
        }

        #endregion

        #region Public Methods
        public void RegisterEvents(ShellDataEventHandler shellDataEventHandler)
        {
            ShellDataEvent += shellDataEventHandler;
        }

        public ShellData ExecuteCommand(string command)
        {
            ExecuteAnyCommand(command, null, false);
            return _output;
        }

        public ShellData ExecuteCommand(string command, bool saveOutput)
        {
            ExecuteAnyCommand(command, null, saveOutput);
            return _output;
        }

        public ShellData ExecuteCommand(string command, string workingDirectoryt)
        {
            ExecuteAnyCommand(command, workingDirectoryt, false);
            return _output;
        }

        public ShellData ExecuteCommand(string command, string workingDirectory, bool saveOutput)
        {
            ExecuteAnyCommand(command, workingDirectory, saveOutput);
            return _output;
        }

        #endregion

        #region Private Methods

        private void ExecuteAnyCommand(string command, string workingDirectory, bool saveOutput)
        {
            _output = new ShellData();
            _saveOutput = saveOutput;

            try
            {
                //output command
                if (ShellDataEvent != null)
                    ShellDataEvent(ShellElementCode.BeginCommand, command);

                //create ProcessStartInfo
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
                procStartInfo.RedirectStandardInput = true; //understand
                procStartInfo.RedirectStandardError = true;
                procStartInfo.RedirectStandardOutput = true;
                
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;

                //procStartInfo.WindowStyle
                //procStartInfo.StandardOutputEncoding
                //procStartInfo.StandardErrorEncoding

                if (workingDirectory != null)
                {
                    procStartInfo.WorkingDirectory = workingDirectory;
                }

                // Create Process
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.EnableRaisingEvents = true;

                // Events Process
                proc.OutputDataReceived += _outputDataReceivedEvent;
                proc.ErrorDataReceived += _errorDataReceivedEvent;
                
                //v2
                //process.OutputDataReceived += (sender, e) => actionWrite(sender, e);
                //v3
                //proc.OutputDataReceived += delegate (object sender, System.Diagnostics.DataReceivedEventArgs e)
                //{ };

                proc.Start();

                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();

                proc.WaitForExit();

                //output command
                if (ShellDataEvent != null)
                    ShellDataEvent(ShellElementCode.EndCommand, null);

                _output.ExitCode = proc.ExitCode;
            }

            catch (Exception ex)
            {
                if (ShellDataEvent != null)
                    ShellDataEvent(ShellElementCode.ExceptionData, ex.Message);

                _output.ExitCode = 1;
            }
        }

        /// <summary>
        /// DataReceivedEventArgs has an internal constructor
        /// So the class cannot be instantiated out of namespace
        /// Magic: Use reflection to access
        /// </summary>
        private DataReceivedEventArgs CreateMockDataReceivedEventArgs(string data)
        {
            DataReceivedEventArgs MockEventArgs =
                (DataReceivedEventArgs)System.Runtime.Serialization.FormatterServices
                 .GetUninitializedObject(typeof(DataReceivedEventArgs));

            FieldInfo[] EventFields = typeof(DataReceivedEventArgs)
                .GetFields(
                    BindingFlags.NonPublic |
                    BindingFlags.Instance |
                    BindingFlags.DeclaredOnly);

            if (EventFields.Count() > 0)
                EventFields[0].SetValue(MockEventArgs, data);
            else
                throw new ApplicationException("Failed to find _data field!");

            return MockEventArgs;
        }

        #endregion
    }
}