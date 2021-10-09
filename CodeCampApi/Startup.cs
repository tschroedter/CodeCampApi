using CodeCampApi.Controllers ;
using CodeCampApi.Data ;
using Microsoft.AspNetCore.Builder ;
using Microsoft.AspNetCore.Hosting ;
using Microsoft.Extensions.Configuration ;
using Microsoft.Extensions.DependencyInjection ;
using Microsoft.Extensions.Hosting ;
using Microsoft.OpenApi.Models ;

namespace CodeCampApi
{
    public class Startup
    {
        public Startup ( IConfiguration configuration )
        {
            Configuration = configuration ;
        }

        public IConfiguration Configuration { get ; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices ( IServiceCollection services )
        {
            services.AddDbContext < CampContext > ( ) ;
            services.AddScoped < ICampRepository , CampRepository > ( ) ;
            services.AddTransient < ICampsControllerProcessor , CampsControllerProcessor > ( ) ;
            services.AddControllers ( ) ;
            services.AddSwaggerGen ( c =>
                                     {
                                         c.SwaggerDoc ( "v1" ,
                                                        new OpenApiInfo { Title = "CodeCampApi" , Version = "v1" } ) ;
                                     } ) ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure ( IApplicationBuilder app ,
                                IWebHostEnvironment env )
        {
            if ( env.IsDevelopment ( ) )
            {
                app.UseDeveloperExceptionPage ( )
                   .UseSwagger ( )
                   .UseSwaggerUI ( c => c.SwaggerEndpoint ( "/swagger/v1/swagger.json" ,
                                                            "CodeCampApi v1" ) ) ;
            }

            app.UseHttpsRedirection ( )
               .UseRouting ( )
               .UseAuthentication ( )
               .UseAuthorization ( )
               .UseEndpoints ( endpoints => { endpoints.MapControllers ( ) ; } ) ;
        }
    }
}