using Lotto3000App.DataAccess;
using Lotto3000App.DataAccess.Implementations;
using Lotto3000App.DataAccess.Interfaces;
using Lotto3000App.Domain.Models;
using Lotto3000App.Services.Implementations;
using Lotto3000App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto3000App.Helpers
{
    public class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Lotto3000AppDbContext>(x => x.UseSqlServer(connectionString));
        }

        public static void InjectRepository(IServiceCollection services)
        {
            services.AddTransient<IRepository<Ticket>, TicketRepository>();
            services.AddTransient<IRepository<Session>, SessionRepository>();
            services.AddTransient<IUserRepository<Admin>, AdminRepository>();
            services.AddTransient<IUserRepository<Player>, PlayerRepository>();
        }
        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IUserService<Admin>, AdminService>();
            services.AddTransient<IPlayerService, PlayerService>();
        }
    }
}
