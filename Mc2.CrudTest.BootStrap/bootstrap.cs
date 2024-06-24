using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mc2.CrudTest.DomainModel.Model;
using Mc2.CrudTest.Presentation.Business;
using Mc2.CrudTest.Presentation.BusinessServiceContract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.BootStrap
{
    public static class bootstrap
    {
        public static void WireUp(IServiceCollection service, string ConnectionString)
        {
            service.AddDbContext<ProjectTestContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(ConnectionString);
            },ServiceLifetime.Scoped);

            service.AddScoped<ICustomerRepository, CustomerRepository>();
            service.AddScoped<ICustomerBuss, CustomerBuss>();
        }
    }
}
