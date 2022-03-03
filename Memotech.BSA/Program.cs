using Memotech.BSA.Data.Repositories;
using Memotech.Core.Abstractions.Repositories;
using Memotech.Core.Abstractions.Services;
using Memotech.Core.Application.Services;
using Memotech.Core.Domain;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Tewr.Blazor.FileReader;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<IRepository<Memo>, JsonMemoRepository>();
builder.Services.AddTransient<IRepository<MemoSet>, JsonMemoSetRepository>();
builder.Services.AddTransient<IMemoService, MemoService>();
builder.Services.AddTransient<IMemoSetService, MemoSetService>();
builder.Services.AddFileReaderService(options => options.InitializeOnFirstCall = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
