using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace igawPublic
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();

            app.Run(async (context) =>
            {
                var file = new FileInfo(@"wwwroot\index.html");
                byte[] buffer;
                if (file.Exists)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.ContentType = "text/html";

                    buffer = File.ReadAllBytes(file.FullName);
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    context.Response.ContentType = "text/plain";
                    buffer = Encoding.UTF8
                        .GetBytes("Unable to find the requested file");
                }

                context.Response.ContentLength = buffer.Length;

                using (var stream = context.Response.Body)
                {
                    await stream.WriteAsync(buffer, 0, buffer.Length);
                    await stream.FlushAsync();
                }
            });
        }
    }
}
