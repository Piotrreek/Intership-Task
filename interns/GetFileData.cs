using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace interns
{
    public class GetFileData
    {
        /// <summary>
        /// Send Http request and depending on content-type of response download file with correct extension and parse data from it to InternsList object
        /// </summary>
        /// <param name="URL"></param>
        /// <returns>InternsList object with list of InternDataModel objects</returns>
        public static async Task<InternsList> GetInternsDataModels(string URL)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(URL);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Error: Cannot get file.");
                Environment.Exit(0);
            }
            var contentType = response.Content.Headers.ContentType?.MediaType;
            switch (contentType)
            {
                case "application/json":
                    return await GetJsonData(httpClient, URL);
                case "application/zip":
                    return await GetZipData(httpClient, URL);
                case "text/csv":
                    return await GetCsvData(httpClient, URL);
                default:
                    Console.WriteLine("Unknown file");
                    break;
            }
            return new InternsList();
        }
        private static async Task<InternsList> GetJsonData(HttpClient client, string URL)
        {
            await using var stream = await client.GetStreamAsync(URL);
            await using var fileStream = new FileStream("interns.json", FileMode.Create);
            await stream.CopyToAsync(fileStream);
            await fileStream.DisposeAsync();
            try
            {
                using var r = new StreamReader("interns.json");
                var json = await r.ReadToEndAsync();
                var internDataModels = JsonConvert.DeserializeObject<InternsList>(json);
                return internDataModels;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Cannot process the file.");
                Environment.Exit(0);
            }
            return null;
        }
        private static async Task<InternsList> GetCsvData(HttpClient client, string URL)
        {
            await using var stream = await client.GetStreamAsync(URL);
            await using var fileStream = new FileStream("interns.csv", FileMode.Create);
            await stream.CopyToAsync(fileStream);
            await fileStream.DisposeAsync();
            try
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Encoding = Encoding.UTF8,
                    Delimiter = ","
                };
                await using var fs = File.Open("interns.csv", FileMode.Open, FileAccess.Read, FileShare.Read);
                using var reader = new StreamReader(fs);
                using var csv = new CsvReader(reader, configuration);
                var internsList = new InternsList()
                {
                    Interns = new List<InternDataModel>()
                };
                await csv.ReadAsync();
                csv.ReadHeader();
                while (await csv.ReadAsync())
                {
                    var record = new InternDataModel()
                    {
                        Id = csv.GetField<int>("interns/id"),
                        Age = csv.GetField<int>("interns/age"),
                        Email = csv.GetField<string>("interns/email"),
                        Name = csv.GetField<string>("interns/name"),
                        InternShipStart = csv.GetField<DateTime>("interns/internshipStart"),
                        InternShipEnd = csv.GetField<DateTime>("interns/internshipEnd")
                    };
                    internsList.Interns.Add(record);
                }
                return internsList;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Cannot process the file.");
                Environment.Exit(0);
            }
            return null;
        }
        private static async Task<InternsList> GetZipData(HttpClient client, string URL)
        {
            await using var stream = await client.GetStreamAsync(URL);
            await using var fileStream = new FileStream("interns.zip", FileMode.Create);
            await stream.CopyToAsync(fileStream);
            await fileStream.DisposeAsync();
            try
            {
                const string zipPath = "interns.zip";
                const string extractPath = @"./";
                ZipFile.ExtractToDirectory(zipPath, extractPath, true);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Cannot process the file.");
                Environment.Exit(0);
            }
            try
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Encoding = Encoding.UTF8,
                    Delimiter = ","
                };
                await using var fs = File.Open("interns.csv", FileMode.Open, FileAccess.Read, FileShare.Read);
                using var reader = new StreamReader(fs);
                using var csv = new CsvReader(reader, configuration);
                var internsList = new InternsList()
                {
                    Interns = new List<InternDataModel>()
                };
                await csv.ReadAsync();
                csv.ReadHeader();
                while (await csv.ReadAsync())
                {
                    var record = new InternDataModel()
                    {
                        Id = csv.GetField<int>("interns/id"),
                        Age = csv.GetField<int>("interns/age"),
                        Email = csv.GetField<string>("interns/email"),
                        Name = csv.GetField<string>("interns/name"),
                        InternShipStart = csv.GetField<DateTime>("interns/internshipStart"),
                        InternShipEnd = csv.GetField<DateTime>("interns/internshipEnd")
                    };
                    internsList.Interns.Add(record);
                }
                return internsList;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: Cannot process the file.");
                Environment.Exit(0);
            }
            return new InternsList();
        }
    }
}
