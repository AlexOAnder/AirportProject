using System;
using System.Data.Entity.Migrations;
using System.Net;
using System.Net.Http;
using AirPortWebApi.Data.DbContext;

namespace AirPortWebApi.Data.Application
{
    public sealed class ApplicationLogsConfiguration : DbMigrationsConfiguration<ApplicationLogsDbContext>
    {
        public ApplicationLogsConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            MigrationsDirectory = @"ApplicationLogsDbContextMigrations";
        }

        protected override void Seed(ApplicationLogsDbContext context)
        {
#if DEBUG
            var test = new ApplicationLogModel
            {
                ServerDateTime = DateTime.Now,
                HttpMessageType = "Request",
                AttendeeId = 1,
                CorrelationId = Guid.NewGuid(),
                IpAddress = "192.168.0.1",
                Method = HttpMethod.Post.Method,
                RawData = @"Lorem ipsum dolor sit amet, consectetur adipisicing elit. Cumque, porro eos debitis illo excepturi tempora reiciendis! Ullam, voluptates, recusandae, illum, quis consequuntur delectus mollitia cupiditate temporibus explicabo adipisci tenetur libero unde odit eius quia laborum est asperiores cum pariatur ut minus maxime possimus voluptatem atque dignissimos optio sapiente repellat facere repudiandae quasi aut fugit hic quam. Sint, alias ab fugiat deserunt sit mollitia quas tenetur ad quam quo aut eligendi harum omnis cum blanditiis? Vitae, sunt, eveniet, sequi, accusantium illum error asperiores minima hic veritatis nemo earum excepturi natus delectus fuga at optio assumenda perspiciatis provident praesentium sed! Molestiae, minima deserunt voluptas! Impedit, autem repellat eaque corrupti totam alias labore blanditiis deleniti nobis eius eos quaerat ducimus quidem nulla earum consequuntur facilis odio vel esse cum. Libero, totam, adipisci suscipit nam tempora quaerat sint delectus porro eveniet cum natus earum animi corporis harum aliquam. Cum, quod aut labore minima maxime.",
                RequestUri = "/",
                StatusCode = (int)HttpStatusCode.Created,
                UserName = "testUser"
            };
            var ctx = new ApplicationLogsDbContext();
            ctx.ApplicationLogs.Add(test);
            ctx.SaveChanges();
#endif
            base.Seed(context);
        }
    }
}
