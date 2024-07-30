using System;
using System.Diagnostics;
using Newtonsoft.Json;

int iterations = 1000;

var instance = F.Get();

// Measure custom CSV serialization
var stopwatch = Stopwatch.StartNew();
for (int i = 0; i < iterations; i++)
{
    var csv = CsvSerializer.Serialize(instance);
}
stopwatch.Stop();
Console.WriteLine($"Custom CSV Serialization Time: {stopwatch.ElapsedMilliseconds} ms");

var csvInstance = CsvSerializer.Serialize(instance);
stopwatch.Restart();
for (int i = 0; i < iterations; i++)
{
    var obj = CsvSerializer.Deserialize<F>(csvInstance);
}
stopwatch.Stop();
Console.WriteLine($"Custom CSV Deserialization Time: {stopwatch.ElapsedMilliseconds} ms");

stopwatch.Restart();
for (int i = 0; i < iterations; i++)
{
    var json = JsonConvert.SerializeObject(instance);
}
stopwatch.Stop();
Console.WriteLine($"Standard JSON Serialization Time: {stopwatch.ElapsedMilliseconds} ms");

var jsonInstance = JsonConvert.SerializeObject(instance);
stopwatch.Restart();
for (int i = 0; i < iterations; i++)
{
    var obj = JsonConvert.DeserializeObject<F>(jsonInstance);
}
stopwatch.Stop();
Console.WriteLine($"Standard JSON Deserialization Time: {stopwatch.ElapsedMilliseconds} ms");

stopwatch.Restart();
for (int i = 0; i < iterations; i++)
{
    Console.WriteLine(csvInstance);
}
stopwatch.Stop();
Console.WriteLine($"Time taken to write to console: {stopwatch.ElapsedMilliseconds} ms");