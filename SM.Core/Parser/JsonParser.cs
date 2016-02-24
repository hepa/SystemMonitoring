using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SM.Contracts.Attributes;
using SM.Contracts.Models;
using SM.Contracts.Models.HWiNFO;

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


            var date = DateTime.UtcNow.ToString("d");
            string path = string.Format(@"c:\sensor\log-{0}.txt", date);

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

        public static HwInfo ParseIntoContract()
        {
            var client = new WebClient();
            //var downloadString = client.DownloadString(@""http://localhost:55555"");
            var downloadString = @"[
                  {
                                ""SensorApp"": ""HWiNFO"",
                    ""SensorClass"": ""System"",
                    ""SensorName"": ""Virtual Memory Commited"",
                    ""SensorValue"": ""6602"",
                    ""SensorUnit"": ""MB"",
                    ""SensorUpdateTime"": 1456258608
                  },
                  {
                                ""SensorApp"": ""HWiNFO"",
                    ""SensorClass"": ""System"",
                    ""SensorName"": ""Virtual Memory Available"",
                    ""SensorValue"": ""3428"",
                    ""SensorUnit"": ""MB"",
                    ""SensorUpdateTime"": 1456258608
                  },
                  {
                    ""SensorApp"": ""HWiNFO"",
                    ""SensorClass"": ""CPU [#0]: Intel Core i5-4590"",
                    ""SensorName"": ""Core #0 VID"",
                    ""SensorValue"": ""1,0738525390625"",
                    ""SensorUnit"": ""V"",
                    ""SensorUpdateTime"": 1456336430
                  }]";

            var interestingRecords = new List<SensorRecord>();

            var hwinfo = new HwInfo();
            var properties = hwinfo.GetType().GetProperties();


            var jRecrods = JArray.Parse(downloadString);
            foreach (var jRecord in jRecrods)
            {
                var record = JsonConvert.DeserializeObject<SensorRecord>(jRecord.ToString());
                foreach (var property in properties)
                {
                    SensorClassAttribute customAttributeData = null;
                    if (property.PropertyType.IsGenericType)
                    {
                        customAttributeData =
                            (SensorClassAttribute)
                                property.PropertyType.GetGenericArguments()[0].GetCustomAttribute(
                                    typeof (SensorClassAttribute), false);
                    }
                    else
                    {
                        customAttributeData = (SensorClassAttribute)property.PropertyType.GetCustomAttribute(typeof(SensorClassAttribute), false);
                    }                    
                    var regex = new Regex(customAttributeData.ClassNameRegex);

                    if (regex.IsMatch(record.SensorClass))
                    {
                        var type = property.PropertyType;
                        if (type.IsGenericType && !type.Name.Contains("Data"))
                        {
                            type = type.GetGenericArguments()[0];
                        }

                        var propertyInfo = type.GetProperties().First(prop =>
                        {
                            var customAttribute = (SensorNameAttribute)prop.GetCustomAttribute(typeof(SensorNameAttribute), false);
                            
                            if (customAttribute == null) return false;

                            var regexName = new Regex(customAttribute.SensorNameRegex);
                            if (regexName.IsMatch(record.SensorName))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        });

                        if (propertyInfo == null) continue;
                        var value = property.GetValue(hwinfo);
                        var proptype = propertyInfo.PropertyType;

                        if (proptype.GetGenericTypeDefinition() == typeof (List<>))
                        {
                            var data = (dynamic)Activator.CreateInstance(propertyInfo.PropertyType.GetGenericArguments()[0]);
                            var valueType = propertyInfo.PropertyType.GetGenericArguments()[0].GetProperty("Value").PropertyType;

                            data.Value = (dynamic)Convert.ChangeType(record.SensorValue, valueType);
                            data.Unit = new DataType { Unit = DataUnit.MB };

                            value = propertyInfo.GetValue(property);
                            propertyInfo.SetValue(value, data);
                        }
                        else
                        {
                            var data = (dynamic)Activator.CreateInstance(proptype);
                            var valueType = proptype.GetProperty("Value").PropertyType;

                            data.Value = (dynamic)Convert.ChangeType(record.SensorValue, valueType);
                            data.Unit = new DataType { Unit = DataUnit.MB };

                            propertyInfo.SetValue(value, data);
                        }                        
                    }
                }
            }
            return hwinfo;
        }
    }
}