using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentingManagementLibrary.DataAccess
{
    public class Admin
    {
        public Admin()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            Email = configuration.GetSection("AdminAccount:Email").Value;
            Password = configuration.GetSection("AdminAccount:Password").Value;
        }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
