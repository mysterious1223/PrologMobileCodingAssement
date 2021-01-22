/*
Author: Kevin Singh
Date: 1/22/2021
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace PrologMobileCodingQuestion.Utility
{
    public static class WebRetriever <T>
    {


        public static async Task<List<T>> GetWebDataAsync (string Url)
        {

            List<T> holder = new List<T>();

            using (WebClient wc = new WebClient())
            {

                try
                {
                    var jsonString = await wc.DownloadDataTaskAsync(Url);
                    holder = JsonSerializer.Deserialize<List<T>>(jsonString);
                }
                catch (Exception ex)
                {
                    //error parsing json return a empty list
                    Console.WriteLine($"Error : {ex.ToString()}");
                    return new List<T>();

                }
            }


            return holder;
        }


    }
}
