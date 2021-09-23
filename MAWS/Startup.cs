using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MAWS.Areas.Identity;
using MAWS.Models;
using MAWS.Services;
using MAWS.Services.EmailNotification;
using MAWS.Services.UploadData;
using MAWS.Services.DataAccess;
using MAWS.IntermediateData;

namespace MAWS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            ConfigureIdentityDBContext(services);

            services.AddRazorPages();
            services.AddServerSideBlazor();

            ConfigureScoped(services);

            ConfigureTransients(services);

            services.AddHttpContextAccessor();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<UserRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            UserDataInitializer.SeedData(userManager, roleManager);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        public void ConfigureIdentityDBContext(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

            services.AddIdentity<ApplicationUser, UserRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI();

        }

        public void ConfigureScoped(IServiceCollection services)
        {

            //User State Provider
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<ApplicationUser>>();

            //Querying
            services.AddScoped<QueryAcademicStaff>();
            services.AddScoped<QueryResearch>();
            services.AddScoped<QueryService>();
            services.AddScoped<QuerySupervision>();
            services.AddScoped<QueryDiscipline>();
            services.AddScoped<QueryUnitOfferings>();
            services.AddScoped<QueryTeachingActivities>();
            services.AddScoped<QueryTeachingPatterns>();
            services.AddScoped<QueryMiscTeachingActivities>();
            services.AddScoped<QueryMiscTeachingActivities>();
            services.AddScoped<QueryTeachingActivityAssignments>();
            services.AddScoped<QueryUsers>();
            services.AddScoped<QueryWorkload>();

        }

        public void ConfigureTransients(IServiceCollection services)
        {

            //Email service
            services.AddTransient<IEmailNotification, EmailNotificationService>();

            //Upload service
            services.AddTransient<IUploadData, UploadAcademicStaff>();
            services.AddTransient<IUploadData, UploadDiscipline>();
            services.AddTransient<IUploadData, UploadUnit>();
            services.AddTransient<IUploadData, UploadUnitOffering>();
            services.AddTransient<IUploadData, UploadResearch>();
            services.AddTransient<IUploadData, UploadService>();
            services.AddTransient<IUploadData, UploadSupervision>();
            services.AddTransient<IUploadData, UploadMiscTeachingActivity>();

            //DataAccess
            services.AddTransient<IDataAccessServices<IntermediateResearch>, AcademicResearchService>();
            services.AddTransient<IDataAccessServices<IntermediateService>, AcademicServiceService>();
            services.AddTransient<IDataAccessServices<IntermediateSupervision>, AcademicSupervisionService>();
            services.AddTransient<IDataAccessServices<IntermediateMiscTeachingActivity>, MiscTeachingActivityService>();
            services.AddTransient<IDataAccessServices<IntermediateAcademicStaff>, AcademicStaffService>();
            services.AddTransient<IDataAccessServices<IntermediateUnit>, UnitService>();
            services.AddTransient<IDataAccessServices<IntermediateUnitOffering>, UnitOfferingService>();
            services.AddTransient<IDataAccessServices<IntermediateTeachingPattern>, TeachingPatternService>();

        }

        //ConfigureSingletons(services);
        public void ConfigureSingletons(IServiceCollection services)
        {
        }
    }
}