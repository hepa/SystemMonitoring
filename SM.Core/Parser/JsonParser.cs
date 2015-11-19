using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SM.Contracts.Models;

namespace SM.Core.Parser
{
    public static class JsonParser
    {
        public static void Parse()
        {
            var client = new WebClient();
            var downloadString = client.DownloadString(@"http://localhost:55555");

            var interestingRecords = new List<SensorRecord>();

            var jRecrods = JArray.Parse(downloadString);
            foreach (var jRecord in jRecrods)
            {
                var record = JsonConvert.DeserializeObject<SensorRecord>(jRecord.ToString());
                if (record.SensorName == "Total CPU Usage"
                    || record.SensorName == "Core Max"
                    || record.SensorName == "CPU1/2"                    
                    || record.SensorName == "GPU Thermal Diode"
                    || (record.SensorName == "GPU D3D Usage" && record.SensorClass.Contains("Radeon"))
                    || record.SensorName == "GPU Fan Speed")
                {
                    Console.WriteLine(record.SensorName);
                    interestingRecords.Add(record);
                }
            }

            const string path = @"c:\sensor\logs.txt";

            if (!Directory.Exists(@"c:\sensor"))
            {
                Directory.CreateDirectory(@"c:\sensor");
            }

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    foreach (var interestingRecord in interestingRecords)
                    {
                        sw.Write(interestingRecord.SensorValue);
                        sw.Write("; ");
                    }
                    sw.WriteLine();
                }
            }

            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(path))
            {
                foreach (var interestingRecord in interestingRecords)
                {
                    sw.Write(interestingRecord.SensorValue);
                    sw.Write(";");
                }
                sw.WriteLine();
            }
        }
    }
}