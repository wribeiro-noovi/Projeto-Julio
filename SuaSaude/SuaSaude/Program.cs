using Microsoft.EntityFrameworkCore;
using SuaSaude.Contracts;
using SuaSaude.Repositories;
using SuaSaude.Repositories.DataAcess;
using SuaSaude.UseCase.Atividades.AlterCheck;
using SuaSaude.UseCase.Atividades.Create;
using SuaSaude.UseCase.Atividades.Delete;
using SuaSaude.UseCase.Atividades.GetByDia;
using SuaSaude.UseCase.Atividades.GetByMes;
using SuaSaude.UseCase.Atividades.Update;
using SuaSaude.UseCase.Autenticacao.EnviarToken;
using SuaSaude.UseCase.Autenticacao.ReceberToken;
using SuaSaude.UseCase.Autenticacao.RecuperarSenha;
using SuaSaude.UseCase.Autenticacao.Validar;
using SuaSaude.UseCase.Autenticacao.VerificaEmail;
using SuaSaude.UseCase.Usuarios.AlterSenha;
using SuaSaude.UseCase.Usuarios.Create;
using SuaSaude.UseCase.Usuarios.Delete;
using SuaSaude.UseCase.Usuarios.Get;
using SuaSaude.UseCase.Usuarios.ValidarDados;
using SuaSaude.Util;
using SuaSaude.Util.Cadastro;
using SuaSaude.Util.Email;
using SuaSaude.Util.RecuperaSenha;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SuaSaudeDbContext>(options =>
{
    options.UseMySQL("Server=127.0.0.1;Database=suasaude;User=root;Password=Config@123;Port=3306;");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5500")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<AutenticacaoRepository>();
builder.Services.AddScoped<EnviarEmail>();
builder.Services.AddScoped<VerificaEmailExistente>();
builder.Services.AddScoped<RecuperaSenha>();
builder.Services.AddScoped<ValidarDadosCadastrais>();
builder.Services.AddScoped<AtividadeRepository>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAutenticacaoRepository, AutenticacaoRepository>();
builder.Services.AddScoped<IEnviarEmail, EnviarEmail>();
builder.Services.AddScoped<IVerificaEmailRepository, VerificaEmailExistente>();
builder.Services.AddScoped<IRecuperaSenha, RecuperaSenha>();
builder.Services.AddScoped<IAtividadeRepository, AtividadeRepository>();

builder.Services.AddScoped<CreateUsuarioUseCase>();
builder.Services.AddScoped<ValidarLoginUseCase>();
builder.Services.AddScoped<EnviarTokenUseCase>();
builder.Services.AddScoped<ReceberTokenUseCase>();
builder.Services.AddScoped<RecuperarSenhaUseCase>();
builder.Services.AddScoped<ValidarDadosUseCase>();
builder.Services.AddScoped<VerificaEmailUseCase>();

builder.Services.AddScoped<GetAtividadeByDiaUseCase>();
builder.Services.AddScoped<GetAtividadeByMesUseCase>();
builder.Services.AddScoped<CreateAtividadeUseCase>();
builder.Services.AddScoped<UpdateAtividadeUseCase>();
builder.Services.AddScoped<DeleteAtividadeUseCase>();
builder.Services.AddScoped<AlterCheckUseCase>();

builder.Services.AddScoped<GetUsuarioUseCase>();
builder.Services.AddScoped<AlterarSenhaUsuarioUseCase>();
builder.Services.AddScoped<DeleteUsuarioUseCase>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
