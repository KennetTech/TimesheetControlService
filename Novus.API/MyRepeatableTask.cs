using System.Text.Json.Serialization;
using Coravel.Invocable;
//using NovusConnector;
using NovusService.Producer;
using Newtonsoft.Json;
using Confluent.Kafka;
using Novus.API.Models;

namespace NovusService.Services;

public class MyRepeatableTask(IkafkaProducer producer) : IInvocable
{
    public async Task Invoke()
    {
        //List<int> RunIdCompleteKBH = new List<int>();

        //var Novuskbh = DataConnection.Novus.Storkøbenhavn;
        //var schedulelistkbh = Novuskbh.ScheduleList();
        //var scheduleidkbh = schedulelistkbh.First(x => x.ScheduleName == /*DateTime.Now.ToString("yyyyMMdd")*/ "20250530").ScheduleID;
        //var vehiclelist = Novuskbh.VehicleList(scheduleidkbh);

        //foreach (var vehicle in vehiclelist)
        //{
        //    List<int> RunIdKBHlist = new();
        //    RunIdKBHlist.Add(vehicle.RunID);
        //    foreach (var runid in RunIdKBHlist)
        //    {
        //        List<VehicleEvents> vehicleeventskbh = Novuskbh.VehicleEventsList(runid);

        //        var vevent = vehicleeventskbh.Where(x => x.Activity == 3 && (x.ActualArrive != null && x.ActualDepart != null));

        //        foreach (var item in vevent)
        //        {

        //            var testValue = JsonConvert.SerializeObject(Novuskbh.VehicleEventsList(item.RunID));

        //            Console.WriteLine(testValue);
        //            if (!RunIdCompleteKBH.Contains(item.RunID))
        //            {
                            string jsonString =
                            """
                            {
                                "employeeId": "EMP001",
                                "employeeName": "Kennet Christiansen",
                                "date": "2025-06-23T00:00:00",
                                "garageOut": "2025-06-23T07:30:00",
                                "firstPickup": "2025-06-23T08:00:00",
                                "lastDropoff": "2025-06-23T16:45:00",
                                "garageIn": "2025-06-23T17:15:00",
                                "pauseSeconds": 1800,
                                "totalSeconds": 34200,
                                "approvedByEmployee": true,
                                "vehicleId": "XYZ789"
                            }
                            """;
                            
                            await producer.ProduceAsync("rute-completed", new Message<string, string>
                            {
                                Key = "Test01",
                                Value = jsonString
                            });

                            Console.WriteLine($"testdata sent: " + jsonString);
                        //RunIdCompleteKBH.Add(item.RunID);
                        //Console.WriteLine($"{item.RunID} added to list of completed rutes");
            //        }
            //        else
            //        {
            //            Console.WriteLine("RunID already completed");
            //        }
            //    }

            //}
        }

    //    return;
    //}
}
