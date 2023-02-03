using Cwiczenia3.Models;
using Cwiczenia3.Models.DataConverter;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia3
{
    public class Program
    {

        public const string DataSetLocation = "DATA_SET/dane.csv";

        public static void Main(string[] args)
        {

            #region testing

            var recreateStudData = new CSVDataConverter();

            Student.RecreateStudentExtension(recreateStudData
                .ConvertDataSetForStudentExtensionRecreation(DataSetLocation));


            


            #endregion testing


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
