using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VL_VendasLanches.Areas.Admin.Services;
using VL_VendasLanches.Context;
using VL_VendasLanches.Mappings;
using VL_VendasLanches.Models;
using VL_VendasLanches.Repositories;
using VL_VendasLanches.Repositories.Interfaces;
using VL_VendasLanches.SeedDatabase;
using VL_VendasLanches.Services;
using VL_VendasLanches.Services.Interfaces;

namespace VL_VendasLanches
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 28))));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<ConfigurationImagens>(Configuration.GetSection("ConfigurationPastaImagens"));

            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);

            //services.Configure<IdentityOptions>(options =>
            //{
            //    //Definições padrões do identity para password
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireNonAlphanumeric = true;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequiredLength = 8; // padrao eh 6
            //    options.Password.RequiredUniqueChars = 1; //nao pode repetir nenhum caracter
            //});

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<ITamanhoRepository, TamanhoRepository>();
            services.AddScoped<ITamanhoProdutosRepository, TamanhoProdutosRepository>();
            services.AddScoped<IComplementoRepository, ComplementoRepository>();
            services.AddScoped<IInformacoesUsuariosRepository, InformacoesUsuariosRepository>();
            services.AddScoped<IComplementoProdutoRepository, ComplementoProdutoRepository>();

            services.AddScoped<ICarrinhoCompraService, CarrinhoCompraService>();
            services.AddScoped<ITamanhoService, TamanhoService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IComplementoService, ComplementoService>();
            services.AddScoped<IInformacoesUsuariosService, InformacoesUsuariosService>();
            services.AddScoped<IPedidosServices, PedidosServices>();
     
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
            services.AddScoped<RelatorioVendasService>();
            services.AddScoped<GraficoVendasService>();

            //Autorização baseada na role
            services.AddAuthorization(options =>
           {
               options.AddPolicy("Admin",
                   politica =>
                   {
                       politica.RequireRole("Admin");
                   });
           });

            //A instancia vale para aplicação toda e é possivel recuperar valores do httpContextAcesso e obter informações do request ou response da requisição atual
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

            services.AddControllersWithViews();

            //Habilitando a utilização do session para armazenar dados temporarios
            services.AddMemoryCache();
            services.AddSession();
        }

        //Injeta o serviço ISeedUserRoleInitial para ser executado ao iniciar o projeto
        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment, ISeedUserRoleInitial seedUserRoleInitial)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. 
                app.UseHsts();
            }

            //app.UseExceptionHandler("/error/500"); //Caminho para os erros não tratados
            //app.UseStatusCodePagesWithRedirects("/erro/{0}"); // Caminho para erros tratados
            //app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // permite que arquivos estaticos sejam servidos e acessado no wwwroot como imagens

            // ativando a utilização do Session
            app.UseSession();

            app.UseRouting();

            //Cria as roles
            seedUserRoleInitial.SeedRoles();
            //Cria os usuarios e atribui as roles
            seedUserRoleInitial.SeedUsers();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
             {
                 endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

                 //A ordenação importa, pq uma rota pode ser atendida pela outra dependendo dos parametros
                 //endpoints.MapControllerRoute(
                 //   name: "admin", //constante admin/action
                 //   pattern: "admin/{action=Index}/{id?}",
                 //   defaults: new { controller = "admin" });

                 endpoints.MapControllerRoute(
                    name: "categoriaFiltro",
                    pattern: "Lanche/{action}/{categoria?}", //Variavel a action Lanche/{action}
                    defaults: new { controller = "Lanche", Action = "List" });

                 endpoints.MapControllerRoute(
                    name: "Login",
                    pattern: "Account/{action}", //Variavel a action/{action}
                    defaults: new { controller = "Account", Action = "Login" });

                 endpoints.MapControllerRoute(
                         name: "default",
                         pattern: "{controller=Home}/{action=Index}/{id?}");
             });
        }
    }
}
