using System;
using System.Collections;
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

        }

        public static void WriteIntoFile(HwInfo hi)
        {
            var date = DateTime.UtcNow.ToString("d");
            string path = string.Format(@"d:\sensor\log-{0}.txt", date);

            if (!Directory.Exists(@"d:\sensor"))
            {
                Directory.CreateDirectory(@"d:\sensor");
            }

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {                    
                    sw.WriteLine(JsonConvert.SerializeObject(hi));
                }
            }

            // This text is always added, making the file longer over time
            // if it is not deleted.
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(JsonConvert.SerializeObject(hi));
            }
        }

        public static HwInfo ParseIntoContract()
        {
            var client = new WebClient();
            var downloadString = client.DownloadString(@"http://localhost:55555");

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
                                    typeof(SensorClassAttribute), false);
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

                        var propertyInfo = type.GetProperties().FirstOrDefault(prop =>
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
                        var mainValue = property.GetValue(hwinfo);
                        var proptype = propertyInfo.PropertyType;

                        var mainType = mainValue.GetType();
                        if (proptype.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            var VIDs = (dynamic)Activator.CreateInstance(propertyInfo.PropertyType.GetGenericArguments()[0]);
                            var valueType = propertyInfo.PropertyType.GetGenericArguments()[0].GetProperty("Value").PropertyType;
                            var VIDsUnitType = propertyInfo.PropertyType.GetGenericArguments()[0].GetProperty("Unit").PropertyType;

                            VIDs.Value = (dynamic)Convert.ChangeType(record.SensorValue, valueType);
                            VIDs.Unit = (dynamic)Activator.CreateInstance(VIDsUnitType);

                            // do not create always a new instance
                            dynamic dynVids = null;

                            var mainIsGeneric = mainType.IsGenericType;
                            if (mainIsGeneric)
                            {
                                mainType = mainType.GetGenericArguments()[0];

                                dynamic cpu = null;
                                dynamic cpus = (dynamic) mainValue;
                                if (cpus.Count != 0)
                                {
                                    cpu = cpus[0];
                                    dynVids = propertyInfo.GetValue(cpu);
                                }
                                else
                                {
                                    dynVids = Activator.CreateInstance(propertyInfo.PropertyType);
                                }
                            }
                            else
                            {
                                dynVids = propertyInfo.GetValue(mainValue);
                            }

                            

                            dynVids.Add(VIDs);

                            var CPUs = (dynamic)mainValue;                                                        

                            if (mainIsGeneric)
                            {
                                var dynCPU = Activator.CreateInstance(mainType);
                                var actProp = dynCPU.GetType().GetProperties().First(p => p.Name == propertyInfo.Name);
                                actProp.SetValue(dynCPU, dynVids);

                                if (CPUs.Count == 0)
                                {
                                    var d = (dynamic) dynCPU;
                                    CPUs.Add(d);
                                }
                            }
                            else
                            {
                                var subType = mainValue.GetType().GetProperties().First(p => p.Name == propertyInfo.Name);

                                var act = (dynamic)subType.GetValue(mainValue);
                                var subIsGeneric = subType.PropertyType.IsGenericType;                                

                                subType.SetValue(CPUs, dynVids);
                            }

                            property.SetValue(hwinfo, CPUs);
                        }
                        else
                        {
                            var data = (dynamic)Activator.CreateInstance(proptype);
                            var valueType = proptype.GetProperty("Value").PropertyType;
                            var unitType = proptype.GetProperty("Unit").PropertyType;

                            data.Value = (dynamic)Convert.ChangeType(record.SensorValue, valueType);
                            data.Unit = (dynamic)Activator.CreateInstance(unitType);

                            if (mainType.IsGenericType)
                            {
//                                if (((dynamic) mainValue).Count == 0)
//                                {
                                    var dynMain = Activator.CreateInstance(mainType);
                                    var gpuType = dynMain.GetType().GetGenericArguments()[0];
                                    var actProp = gpuType.GetProperties().First(p => p.Name == propertyInfo.Name);

                                //var gpu = (dynamic) Activator.CreateInstance(gpuType);
                                //var gpulist = ((dynamic) dynMain).First();

                                dynamic gpu = ((dynamic)mainValue)[0];
                                actProp.SetValue(gpu, data);                                                                        
                                    //((dynamic) mainValue).Add(gpu);

//                                }                                
                            }
                            else
                            {
                                propertyInfo.SetValue(mainValue, (dynamic)data);
                            }                            
                        }
                    }
                }
            }
            return hwinfo;
        }
    }
}