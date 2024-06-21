using DermDiag.Repository;
using Microsoft.EntityFrameworkCore;
using DermDiag.Models;
using System.Net.Mail;
using Stripe;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<Authentication>();
builder.Services.AddScoped<PatientRepository>();
builder.Services.AddScoped<DoctorRepository>();
builder.Services.AddScoped<MeetingRepository>();
builder.Services.AddScoped<PaymentRepository>();
builder.Services.AddScoped<EmailRepository>();

builder.Services.AddControllers();

builder.Services.AddHttpClient<PaymentRepository>();


//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DermDiagContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("OnlineConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
