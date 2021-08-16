using InternManagement.Api.Helpers;
using InternManagement.Api.Models;
using InternManagement.Api.Profiles;
using InternManagement.Api.Repository;
using InternManagement.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;

namespace InternManagement.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(builder =>
      {
        builder.AddDefaultPolicy(opt =>
                  opt.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod());
      });
      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "InternManagement.Api", Version = "v1" });
      });

      #region DbContext
      var config = Configuration.GetSection("MysqlConnection").Get<ConnectionConfig>();
      var connectionString = new MySqlConnectionStringBuilder
      {
        Server = config.Server,
        Database = config.Database,
        Password = config.Password,
        UserID = "InternAdmin"
      }.ConnectionString;
      services.AddDbContext<InternContext>(options =>
      {
        options.UseMySQL(connectionString);
      });
      #endregion

      #region Repository Registration
      services.AddScoped<IInternRepository, InternRepository>();
      services.AddScoped<IDashboardRepository, DashboardRepository>();
      services.AddScoped<IPunchInRepository, PunchInRepository>();
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IPreferencesRepository, PreferencesRepository>();
      services.AddScoped<IUiRepository, UiRepository>();
      #endregion

      services.AddAutoMapper(cfg =>
      {
        cfg.AddProfile<InternProfile>();
        cfg.AddProfile<DocumentsProfile>();
        cfg.AddProfile<DashboardProfile>();
        cfg.AddProfile<PunchInProfile>();
        cfg.AddProfile<UserProfile>();
        cfg.AddProfile<PreferenceProfile>();
        cfg.AddProfile<UiProfile>();
      });

      #region Service Registration
      services.AddScoped<IInternService, InternService>();
      services.AddScoped<IDashboardService, DashboardService>();
      services.AddScoped<IPunchInService, PunchInService>();
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IPrintHelper, PrintHelper>();
      services.AddScoped<IPreferencesService, PreferencesService>();
      services.AddScoped<IUiService, UiService>();
      #endregion
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InternManagement.Api v1"));
      }


      app.UseRouting();

      app.UseCors();

      // TODO Setup Authorization Logic

      // ! app.UseMiddleware<JwtMiddleware>();

      // ! app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
